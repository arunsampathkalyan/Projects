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
    public class TowerCamerasListActionPanelViewModel
    {
        public ObservableCollection<AssetsDetailsViewDTO> CamerasList { get; set; }

        public int NewSpeedValue { get; set; }


        public TowerCamerasListActionPanelViewModel()
        {
            CamerasList = new ObservableCollection<AssetsDetailsViewDTO>();
            
            NewSpeedValue = 80;

        }

        public void SetViewModelData(TowerModel Tower)
        {
            GetAllTowerCameras(Tower.TowerId);
        }

        public void SetViewModelData(DetectedAccidentTowerModel Tower)
        {
            GetAllTowerCameras(Tower.TowerId);
        }

        private void GetAllTowerCameras(long TowerId)
        {
            var client = new ServiceLayerClient();
            var task = client.GetAllTowerCamerasAsync(TowerId);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearCameras(x == null ? new List<AssetsDetailsViewDTO>() : x.ToList()));
        }

        private void AddNearCameras(List<AssetsDetailsViewDTO> Cameras)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var camera in Cameras)
                {
                    CamerasList.Add(camera);
                }
            });
        }
    }
}
