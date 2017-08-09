using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.SmartTowerServiceReference;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    class SmartTowersListActionPanelViewModel
    {
        ServiceLayerReference.ServiceLayerClient _client = new ServiceLayerReference.ServiceLayerClient();
        public LocationModel GeneralLocation { get; set; }

        public ObservableCollection<AssetsViewDTO> TowersList { get; set; }

        public ObservableCollection<TowerPredefinedMessageDTO> ActionsList { get; set; }

        public TowerPredefinedMessageDTO SelectedAction { get; set; }

        public string OldVMS { get; set; }
        public SmartTowersListActionPanelViewModel()
        {
            TowersList = new ObservableCollection<AssetsViewDTO>();


            ActionsList = new ObservableCollection<TowerPredefinedMessageDTO>();
            GetAllTowerActionsList();

            SelectedAction = new TowerPredefinedMessageDTO();

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

            GetAllTowersAroundPoint();
        }


        private void GetAllTowersAroundPoint()
        {
            var client = new ServiceLayerClient();
            var task = client.GetNearByTowersByLatLonAsync(GeneralLocation.Longitude, GeneralLocation.Latitude);
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
                    OldVMS += tower.CurrentValue + ",";
                    TowersList.Add(tower);
                }
                OldVMS = OldVMS.TrimEnd(',');
            });
        }

        private void GetAllTowerActionsList()
        {
            //var client = new SmartTowerIntegrationServiceClient();
            //var task = client.GetAllTowerStaticMessagesAsync("G_001");//replace it with the tower id later
            //var obs = task.ToObservable();
            //obs.Subscribe((x) => AddActionTowers(x == null ? new List<TowerPredefinedMessageDTO>() : x.ToList()));

            try
            {
                SmartTowerDAL smartTowerDAL = new SmartTowerDAL();

                ActionsList = new ObservableCollection<TowerPredefinedMessageDTO>(smartTowerDAL.GetAllTowerStaticMessages("G_001"));
            }
            catch (Exception e)
            {

            }

        }
        private void AddActionTowers(List<TowerPredefinedMessageDTO> towersAction)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                towersAction.ForEach(x => ActionsList.Add(x));
            });
        }

        public bool UpdateTowerMessage(AssetsViewDTO Tower)
        {
            //var client = new SmartTowerIntegrationServiceClient();

            TowerMessageDTO towerMessage = new TowerMessageDTO
            {
                ArabicMessage = SelectedAction.MessageDescription,
                EnableNotification = true,
                MessageId = SelectedAction.MessageId,
                MessageType = 1,
                MyLastUpdate = DateTime.Now,
                TowerId = SelectedAction.Location,
                MixedMessage = "",
                EnglishMessage = "",
                IncidentImage = ""
            };

            //var result = client.UpdateTowerCurrentMessageAsync(towerMessage);
            if(Tower.SelectedAction != null)
                _client.UpdateAssetValueAsync(Tower.ItemId, Tower.SelectedAction.Description);
            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.UpdateTowerCurrentMessage(towerMessage); //result.Result;
            
        }
    }
}
