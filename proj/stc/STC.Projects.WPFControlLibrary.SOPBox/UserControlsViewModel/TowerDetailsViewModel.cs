using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Reactive.Threading.Tasks;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.SmartTowerServiceReference;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    internal class TowerDetailsViewModel
    {
        public AssetsViewDTO Tower { get; set; }
        public ObservableCollection<TowerPredefinedMessageDTO> ActionsList { get; set; }
        public TowerPredefinedMessageDTO SelectedAction { get; set; }

        public TowerDetailsViewModel()
        {
            Tower = new AssetsViewDTO();
            ActionsList = new ObservableCollection<TowerPredefinedMessageDTO>();
            GetAllTowerActionsList();
            SelectedAction = new TowerPredefinedMessageDTO();

            //GetTowerCurrentMsg();
        }

        public void GetTowerCurrentMsg()
        {
            //var client = new SmartTowerIntegrationServiceClient();
            //var task = client.GetTowerCurrentMessageAsync("G_001");//replace it with the tower id later

            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();

            var currentMsg = smartTowerDAL.GetTowerCurrentMessage("G_001");

            if (currentMsg != null)
            {
                switch (currentMsg.MessageType)
                {
                    case 0: //English
                        Tower.CurrentVMSMessage = currentMsg.EnglishMessage;
                        break;

                    case 1: //Arabic
                        Tower.CurrentVMSMessage = currentMsg.ArabicMessage;
                        break;

                    case 2: //Mix
                        Tower.CurrentVMSMessage = currentMsg.MixedMessage;
                        break;
                }
                
            }
            
        }

        private void GetAllTowerActionsList()
        {
            //var client = new SmartTowerIntegrationServiceClient();
            //var task = client.GetAllTowerStaticMessagesAsync("G_001");//replace it with the tower id later
            //var obs = task.ToObservable();
            //obs.Subscribe((x) => AddActionTowers(x == null ? new List<TowerPredefinedMessageDTO>() : x.ToList()));

            try {
                SmartTowerDAL smartTowerDAL = new SmartTowerDAL();

                ActionsList = new ObservableCollection<TowerPredefinedMessageDTO>(smartTowerDAL.GetAllTowerStaticMessages("G_001"));
            }
            catch (Exception e) {
                
            }
          


        }

        private void AddActionTowers(List<TowerPredefinedMessageDTO> towersAction)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                towersAction.ForEach(x => ActionsList.Add(x));
            });
        }

        public bool UpdateTowerMessage()
        {
            //var client = new SmartTowerIntegrationServiceClient();

            TowerMessageDTO towerMessage = new TowerMessageDTO {
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
            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.UpdateTowerCurrentMessage(towerMessage); //result.Result;
        }
    }
}
