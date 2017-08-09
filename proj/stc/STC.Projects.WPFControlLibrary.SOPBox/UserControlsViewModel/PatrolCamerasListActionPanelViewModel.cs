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
    public class PatrolCamerasListActionPanelViewModel
    {
        public ObservableCollection<PatrolLastLocationDTO> PatrolsList { get; set; }

        public PatrolCamerasListActionPanelViewModel()
        {
            PatrolsList = new ObservableCollection<PatrolLastLocationDTO>();

        }

        public void SetViewModelData(FogLocationModel FogLocation)
        {
            GetAllPatrolsAroundPoint(FogLocation);
        }

        private void GetAllPatrolsAroundPoint(FogLocationModel FogLocation)
        {
            var client = new ServiceLayerClient();

            var task = client.GetNearByPatrolsByLatLonAsync(FogLocation.Longitude, FogLocation.Latitude, 5);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearPatrols(x == null ? new List<PatrolLastLocationDTO>() : x.ToList()));
        }

        private void GetAllPatrolsAroundPoint(WantedCarModel Location)
        {
            var client = new ServiceLayerClient();

            var task = client.GetNearByPatrolsByLatLonAsync(Location.Longitude, Location.Latitude, 5);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearPatrols(x == null ? new List<PatrolLastLocationDTO>() : x.ToList()));
        }

        private void AddNearPatrols(List<PatrolLastLocationDTO> Patrols)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var patrol in Patrols)
                {
                    patrol.ImgCheckedSource = "../images/false.png";

                    PatrolsList.Add(patrol);
                }


            });
        }

    }
}
