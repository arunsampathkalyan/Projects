using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class RadarsListViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ObservableCollection<AssetsViewDTO> RadarsList { get; set; }
        public ObservableCollection<AssetsViewDTO> CheckedRadars { get; set; }
        public RadarsListViewModel()
        {

            RadarsList = new ObservableCollection<AssetsViewDTO>();
        }

        private void GetAllRadarsAroundPoint()
        {
            var client = new ServiceLayerClient();
            var task = client.GetNearByRadarsByLatLonAsync(Longitude, Latitude);
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

                    RadarsList.Add(radar);
                }
            });
        }
        public bool SetCheckedList(AssetsViewDTO CheckedItem)
        {
            if (!CheckedRadars.Any(x => x.ItemId == CheckedItem.ItemId))
                CheckedRadars.Add(CheckedItem);
            return CheckedRadars.Count == RadarsList.Count;
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            CheckedRadars = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;
            GetAllRadarsAroundPoint();

        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            CheckedRadars = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;
            GetAllRadarsAroundPoint();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            CheckedRadars = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;
            GetAllRadarsAroundPoint();
        }
    }
}
