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

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class RadarsListActionPanelViewModel
    {
        public LocationModel GeneralLocation { get; set; }

        public ObservableCollection<AssetsViewDTO> RadarsList { get; set; }

        public int NewSpeedValue { get; set; }

        public string OldSpeedValue { get; set; }

        public RadarsListActionPanelViewModel()
        {
            RadarsList = new ObservableCollection<AssetsViewDTO>();

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

        private void Handle(double Latitude, double Longitude)
        {
            GeneralLocation = new LocationModel() { Latitude = Latitude, Longitude = Longitude };

            GetAllRadarsAroundPoint();
        }

        private void GetAllRadarsAroundPoint()
        {
            var client = new ServiceLayerClient();
            var task = client.GetNearByRadarsByLatLonAsync(GeneralLocation.Longitude, GeneralLocation.Latitude);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearRadars(x == null ? new List<AssetsViewDTO>() : x.ToList()));
        }

        private void AddNearRadars(List<AssetsViewDTO> Radars)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var radar in Radars)
                {
                    if (radar.ItemCategoryId != null && radar.ItemStatusId != null)
                        radar.ItemImage = SOPHelper.GetAssetImageUrl((AssetTypesEnum)radar.ItemCategoryId, (AssetStatusEnum)radar.ItemStatusId);
                    radar.ImgCheckedSource = "../images/false.png";
                    OldSpeedValue += radar.CurrentValue + ","; 
                    RadarsList.Add(radar);
                }
                OldSpeedValue = OldSpeedValue.TrimEnd(',');
            });
        }

    }
}
