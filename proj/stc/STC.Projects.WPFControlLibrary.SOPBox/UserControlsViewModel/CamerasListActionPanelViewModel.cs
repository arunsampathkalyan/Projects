using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class CamerasListActionPanelViewModel
    {
        public LocationModel GeneralLocation { get; set; }

        public ObservableCollection<AssetsViewDTO> CamerasList { get; set; }

        public event CanvasEventHandler CamerasLoaded;

        public int NewSpeedValue { get; set; }


        public CamerasListActionPanelViewModel()
        {
            CamerasList = new ObservableCollection<AssetsViewDTO>();

            NewSpeedValue = 80;

        }

        public void SetViewModelData(FogLocationModel Location)
        {
            Handle(Location.Latitude, Location.Longitude);
        }

        public void SetViewModelData(DetectedAccidentLocationModel Location)
        {
            Handle(Location.Latitude, Location.Longitude);
        }

        public void SetViewModelData(WantedCarModel Location)
        {
            Handle(Location.Latitude, Location.Longitude);
        }

        private void Handle(double Latitude, double Longitude)
        {
            GeneralLocation = new LocationModel() { Latitude = Latitude, Longitude = Longitude };

            GetAllCamerasAroundPoint();
        }

        private void GetAllCamerasAroundPoint()
        {
            var client = new ServiceLayerClient();
            var task = client.GetNearByCamerasByLatLonAsync(GeneralLocation.Longitude, GeneralLocation.Latitude);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearCameras(x == null ? new List<AssetsViewDTO>() : x.ToList()));
        }

        private void AddNearCameras(List<AssetsViewDTO> Cameras)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var camera in Cameras)
                {
                    if (camera.ItemCategoryId != null && camera.ItemStatusId != null)
                        camera.ItemImage = SOPHelper.GetAssetImageUrl((AssetTypesEnum)camera.ItemCategoryId, (AssetStatusEnum)camera.ItemStatusId);
                    camera.ImgCheckedSource = "../images/false.png";

                    CamerasList.Add(camera);
                }
                var handler = CamerasLoaded;
                if (handler != null)
                    handler(this, new CanvasEventArgs());
            });
        }
    }
}
