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
    public class PatrolCamerasListViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public SOPSources SOPSource { get; set; }

        public ObservableCollection<PatrolLastLocationDTO> PatrolsList { get; set; }


        public PatrolCamerasListViewModel()
        {

            PatrolsList = new ObservableCollection<PatrolLastLocationDTO>();

        }

        private void GetAllPatrolsAroundPoint()
        {
            var client = new ServiceLayerClient();

            var task = client.GetNearByPatrolsByLatLonAsync(Longitude, Latitude, 5);
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

        public void ProcessMessage(FogLocationModel Location)
        {
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            SOPSource = SOPSources.Fog;

            GetAllPatrolsAroundPoint();
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            SOPSource = SOPSources.DetectedAccident;

            GetAllPatrolsAroundPoint();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            Latitude = Location.Latitude;
            Longitude = Location.Longitude;

            SOPSource = SOPSources.WantedCar;

            GetAllPatrolsAroundPoint();
        }
    }
}
