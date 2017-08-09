using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.CrispService;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class PatrolsListViewModel : INotifyPropertyChanged
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate PatrolsAdded;
        private CrispService.CrsipServicesClient _crsipServicesClient;

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long NotificationId { get; set; }
        public SOPSources SOPSource { get; set; }

        private bool _isPatrolDetailsEnabled;

        public bool IsPatrolDetailsEnabled
        {
            get { return _isPatrolDetailsEnabled; }
            set { _isPatrolDetailsEnabled = value; this.RaiseNotifyPropertyChanged(); }
        }

        private PatrolLastLocationDTO _selectedPatrol;

        public PatrolLastLocationDTO SelectedPatrol
        {
            get { return _selectedPatrol; }
            set
            {
                
                _selectedPatrol = value;
                if (value != null)
                    IsPatrolDetailsEnabled = true;
                else
                    IsPatrolDetailsEnabled = false;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<PatrolLastLocationDTO> _patrolsList;

        public ObservableCollection<PatrolLastLocationDTO> PatrolsList
        {
            get { return _patrolsList; }
            set { _patrolsList = value; RaiseNotifyPropertyChanged(); }
        }
        //public ObservableCollection<PatrolLastLocationDTO> PatrolsList { get; set; }








        public Dictionary<string, ObservableCollection<Graphic>> LayersGraphicsDictionary { get; set; }



        public ObservableCollection<Graphic> GetLayerObservable()
        {
            ObservableCollection<Graphic> graphicCol = new ObservableCollection<Graphic>();
            if (LayersGraphicsDictionary.ContainsKey("DispatchingPatrol"))
                graphicCol = LayersGraphicsDictionary["DispatchingPatrol"];
            else
            {
                LayersGraphicsDictionary.Add("DispatchingPatrol", graphicCol);
            }
            return graphicCol;
        }

        public void AddLayerContent(string Type, double Latitude, double Longitude)
        {
            if (LayersGraphicsDictionary.ContainsKey("DispatchingPatrol"))
            {
                var graphic = CreateGraphic(Latitude, Longitude, GetMarkerImageUrl(Type));
                LayersGraphicsDictionary["DispatchingPatrol"].Add(graphic);
            }
        }

        private Graphic CreateGraphic(double Latitude, double Longitude, string IconPath)
        {
            CompositeSymbol compositeSymbol = new CompositeSymbol();
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();

            pictureMarkerSymbol.SetSourceAsync(new Uri(IconPath));

            compositeSymbol.Symbols.Add(pictureMarkerSymbol);

            var graphics = new Graphic(new MapPoint(Longitude, Latitude, new SpatialReference(4326)), compositeSymbol);
            return graphics;
        }

        private string GetMarkerImageUrl(string Type)
        {
            string strPath = @"pack://application:,,,/STC.Projects.WPFControlLibrary.SOPBox;component/" + @"images/marker/" + Type + "_marker.png";

            try
            {//Workaround to check if the image exists
                var tempImg = new System.Windows.Media.Imaging.BitmapImage(new Uri(strPath));

            }
            catch (Exception ex)
            {

                strPath = @"pack://application:,,,/STC.Projects.WPFControlLibrary.SOPBox;component/" + @"images/marker/event_marker.png";
            }
            return strPath;
        }


        public PatrolsListViewModel()
        {

            PatrolsList = new ObservableCollection<PatrolLastLocationDTO>();
            _crsipServicesClient = new CrsipServicesClient();
            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();
            GetLayerObservable();
        }

        public void GetAllPatrolsAroundPoint()
        {
            var client = new ServiceLayerClient();

            var task = client.GetNearByPatrolsByLatLonAsync(Longitude, Latitude, 5);
            var obs = task.ToObservable();
            obs.Subscribe((x) => AddNearPatrols(x == null ? new List<PatrolLastLocationDTO>() : x.ToList()));
        }

        private List<STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO> PreparePatrolsDTO(List<PatrolLastLocationDTO> patrols)
        {
            var res = new List<STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO>();
            foreach (var item in patrols)
            {
                var m = new STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO
                {
                    Altitude = item.Altitude,
                    CreationDate = item.CreationDate,
                    ETATime = item.ETATime,
                    isDeleted = item.isDeleted,
                    IsNoticed = item.IsNoticed,
                    isPatrol = item.isPatrol,
                    IsRecommended = item.IsRecommended,
                    Latitude = item.Latitude,
                    LocationDate = item.LocationDate,
                    Longitude = item.Longitude,
                    NumberOfAssignedIncident = item.NumberOfAssignedIncident,
                    OfficerName = item.OfficerName,
                    PatrolCode = item.PatrolCode,
                    PatrolId = item.PatrolId,
                    PatrolImage = item.PatrolImage,
                    PatrolLatLocationId = item.PatrolLatLocationId,
                    PatrolOriginalId = item.PatrolOriginalId,
                    PatrolPlateNo = item.PatrolPlateNo,
                    Speed = item.Speed,
                    StatusId = item.StatusId,
                    StatusName = item.StatusName
                };
                res.Add(m);
            }
            return res;
        }
        private void GetAllETA(List<PatrolLastLocationDTO> patrols)
        {

            var client = new CrsipServicesClient();
            var pats = PreparePatrolsDTO(patrols);
            var task = client.GetPatrolsETAsAsync(Longitude, Latitude, pats.ToArray());
            var obs = task.ToObservable();
            obs.Subscribe((x) => UpdatePatrolsETAs(x == null ? new List<STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO>() : x.ToList()));
        }
        private void UpdatePatrolsETAs(List<STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO> patrols)
        {
            foreach (var item in patrols)
            {
                var pListItem = PatrolsList.FirstOrDefault(x => x.PatrolId == item.PatrolId);
                if (pListItem != null)
                    pListItem.ETATime = item.ETATime;
            }
        }

        private void AddNearPatrols(List<PatrolLastLocationDTO> Patrols)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PatrolsList.Clear();
                foreach (var patrol in Patrols)
                {

                    patrol.ImgCheckedSource = "../images/false.png";

                    PatrolsList.Add(patrol);
                }
                GetAllETA(Patrols);
                return;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PatrolsList.Clear();
                    var recommendedPatrol = _crsipServicesClient.RecommendCar(Latitude, Longitude);

                    if (recommendedPatrol != null)
                    {
                        recommendedPatrol.IsRecommended = true;

                        if (!Patrols.Any(p => p.PatrolId == recommendedPatrol.PatrolId))
                        {
                            var recommendedPatrolDto = new PatrolLastLocationDTO()
                            {
                                Latitude = recommendedPatrol.Latitude,
                                Altitude = recommendedPatrol.Altitude,
                                Longitude = recommendedPatrol.Longitude,
                                NumberOfAssignedIncident = recommendedPatrol.NumberOfAssignedIncident,
                                PatrolId = recommendedPatrol.PatrolId,
                                PatrolPlateNo = recommendedPatrol.PatrolPlateNo,
                                PatrolCode = recommendedPatrol.PatrolCode,
                                IsNoticed = recommendedPatrol.IsNoticed,
                                LocationDate = recommendedPatrol.LocationDate,
                                PatrolLatLocationId = recommendedPatrol.PatrolLatLocationId,
                                Speed = recommendedPatrol.Speed,
                                StatusName = recommendedPatrol.StatusName,
                                PatrolOriginalId = recommendedPatrol.PatrolOriginalId,
                                StatusId = recommendedPatrol.StatusId,
                                ImgCheckedSource = "../images/false.png",
                                IsRecommended = recommendedPatrol.IsRecommended,
                                ETATime = recommendedPatrol.ETATime,
                                CreationDate = recommendedPatrol.CreationDate

                            };
                            Patrols.Add(recommendedPatrolDto);
                            //Patrols.Insert(0, recommendedPatrolDto);
                        }
                        else
                        {
                            Patrols.FirstOrDefault(x => x.PatrolId == recommendedPatrol.PatrolId).IsRecommended = true;
                            //var recPat = Patrols.FirstOrDefault(x => x.PatrolId == recommendedPatrol.PatrolId).IsRecommended = true;

                            //Patrols.Remove(Patrols.FirstOrDefault(x => x.PatrolId == recommendedPatrol.PatrolId));
                            //Patrols.Insert(0, recPat);
                        }

                    }

                    Patrols.OrderBy(p => p.IsRecommended);

                    foreach (var patrol in Patrols)
                    {

                        patrol.ImgCheckedSource = "../images/false.png";

                        PatrolsList.Add(patrol);
                    }
                    //var handler = PatrolsAdded;
                    //if (handler != null)
                    //    handler(this, new EventArgs());

                });
            });
        }

        public void SetNotificationID(long notificationId)
        {
            NotificationId = notificationId;
        }
        public void HandleClick()
        {
            var handler = PatrolsAdded;
            if (handler != null)
                handler(this, new EventArgs());
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

        #region INotifyPropertyChanged interface
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
