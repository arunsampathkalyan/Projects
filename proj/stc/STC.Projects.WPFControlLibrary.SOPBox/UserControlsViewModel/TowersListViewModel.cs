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
    public class TowersListViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ObservableCollection<AssetsViewDTO> CheckedTowers { get; set; }

        public ObservableCollection<AssetsViewDTO> TowersList { get; set; }
        //public ObservableCollection<TowerActionsDTO> ActionsList { get; set; }
        public TowersListViewModel()
        {
            //   ActionsList = new ObservableCollection<TowerActionsDTO>();
            TowersList = new ObservableCollection<AssetsViewDTO>();

        }



        private void GetAllTowersAroundPoint()
        {
            var client = new ServiceLayerClient();
            var task = client.GetNearByTowersByLatLonAsync(Longitude, Latitude);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearTowers(x == null ? new List<AssetsViewDTO>() : x.ToList()));
        }

        private void AddNearTowers(List<AssetsViewDTO> Towers)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var tower in Towers)
                {
                    if (tower.ItemCategoryId != null && tower.ItemStatusId != null)
                        tower.ItemImage = SOPHelper.GetAssetImageUrl((AssetTypesEnum)tower.ItemCategoryId, (AssetStatusEnum)tower.ItemStatusId);
                    tower.ImgCheckedSource = "../images/false.png";

                    TowersList.Add(tower);
                }
            });
        }
        public bool SetCheckedList(AssetsViewDTO CheckedItem)
        {
            if (!CheckedTowers.Any(x => x.ItemId == CheckedItem.ItemId))
                CheckedTowers.Add(CheckedItem);
            return CheckedTowers.Count == TowersList.Count;
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            CheckedTowers = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllTowersAroundPoint();
            //GetAllTowerActionsList();
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            CheckedTowers = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllTowersAroundPoint();
            //GetAllTowerActionsList();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            CheckedTowers = new ObservableCollection<AssetsViewDTO>();
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            GetAllTowersAroundPoint();
        }
    }
}
