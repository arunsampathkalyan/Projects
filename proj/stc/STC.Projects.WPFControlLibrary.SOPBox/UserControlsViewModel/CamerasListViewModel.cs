using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Reactive.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class CamerasListViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ObservableCollection<AssetsViewDTO> CamerasList { get; set; }

        public ObservableCollection<AssetsViewDTO> CheckedCamera { get; set; }
        public CamerasListViewModel()
        {
            CamerasList = new ObservableCollection<AssetsViewDTO>();

        }

        private void GetAllCamerasAroundPoint()
        {
            //get values here
            var client = new ServiceLayerClient();
            var task = client.GetNearByCamerasByLatLonAsync(Longitude, Latitude);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearCameras(x == null ? new List<AssetsViewDTO>() : x.ToList()));
            //return new ObservableCollection<AssetsViewDTO>();
        }

        public bool SetCheckedList(AssetsViewDTO CheckedItem)
        {
            if (!CheckedCamera.Any(x => x.ItemId == CheckedItem.ItemId))
                CheckedCamera.Add(CheckedItem);
            return CheckedCamera.Count == CamerasList.Count;
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
                });
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            CheckedCamera = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllCamerasAroundPoint();
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            CheckedCamera = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllCamerasAroundPoint();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            CheckedCamera = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllCamerasAroundPoint();
        }
    }

}
