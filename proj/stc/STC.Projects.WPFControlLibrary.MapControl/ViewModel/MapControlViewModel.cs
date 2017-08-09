using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using Microsoft.AspNet.SignalR.Client;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.MapControl.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.MapControl.TFMServiceReference;
using STC.Projects.WPFControlLibrary.MapControl.ViolationServiceReference;
using STC.Projects.WPFControlLibrary.MapControl.AccidentsServiceReference;
using STC.Projects.WPFControlLibrary.MapControl.SmartOfficerServiceReference;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using STC.Projects.ClassLibrary.Common.Helper;
using STC.Projects.WPFControlLibrary.MapControl.Control;
using System.Windows.Data;


namespace STC.Projects.WPFControlLibrary.MapControl.ViewModel
{
    public class MapControlViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerClient _client = new ServiceLayerClient();
        TFMIntegrationServiceClient _TFMClient = new TFMIntegrationServiceClient();
        ViolationsLayerClient _violationClient = new ViolationsLayerClient();
        AccidentsLayerClient _accidentsClient = new AccidentsLayerClient();
        SmartOfficerLayerClient _smartOfficerClient = new SmartOfficerLayerClient();

        bool isBulkOperationRunning = false;
        bool isSingleTask = false;

        const int SHAPE_Z_INDEX = 1;
        const int SHAPE2_Z_INDEX = 2;
        const int MARKER_Z_INDEX = 3;
        IHubProxy _vehicleHub;

        #region properties

        private string checkBtn;

        public string CheckBtn
        {
            get { return checkBtn; }
            set
            {
                checkBtn = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool? _assetRedLightIsChecked;

        public bool? AssetRedLightIsChecked
        {
            get { return _assetRedLightIsChecked; }
            set { _assetRedLightIsChecked = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool? _assetSpeedIsChecked;

        public bool? AssetSpeedIsChecked
        {
            get { return _assetSpeedIsChecked; }
            set { _assetSpeedIsChecked = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool? _assetSmartTowersIsChecked;

        public bool? AssetSmartTowersIsChecked
        {
            get { return _assetSmartTowersIsChecked; }
            set { _assetSmartTowersIsChecked = value; this.RaiseNotifyPropertyChanged(); }
        }


        //private string _mapLayerServiceUrl;

        //public string MapLayerServiceUrl
        //{
        //    get { return _mapLayerServiceUrl; }
        //    set
        //    {
        //        _mapLayerServiceUrl = value;
        //        this.RaiseNotifyPropertyChanged();
        //    }
        //}

        public event ZoomOnMapLiveTrackEventHandler ZoomOnMapEvent;
        public event ZoomOnMapEventHandler UpdateLocationOnMapEvent;

        public event OpenPopups OpenPatrolEvent;
        public event OpenPopups OpenPatrolOfficerEvent;
        public event OpenPopups OpenIncidentEvent;
        public event OpenPopups OpenAssetEvent;

        public string CurrentUsername { get; set; }
        HubConnection _conn;
        List<Graphic> _tempNotificationsGraphic;
        public string _CurrentWantedPlate { get; set; }
        public string _CurrentMessageId { get; set; }
        public string PolygonForSearch { get; set; }
        public List<OfficersLastLocationViewDTO> Officers { get; set; }
        //layers dynamic graphic collections
        public Dictionary<string, ObservableCollection<Graphic>> LayersGraphicsDictionary { get; set; }


        //filters dropdowns list
        public ObservableCollection<ViolationTypeDimDTO> ViolationTypesList { get; set; }
        public ObservableCollection<AssetStatusDimDTO> AssetStatusList { get; set; }
        public ObservableCollection<AssetTypeDimDTO> AssetTypesList { get; set; }
        public ObservableCollection<PatrolStatusDimDTO> PatrolStatusList { get; set; }
        public ObservableCollection<UsersDTO> AllUsers { get; set; }
        public ObservableCollection<NameValueModel> ViolationsByStatusData { get; set; }
        public ObservableCollection<NameValueModel> ViolationsByTypeData { get; set; }
        public Dictionary<string, List<string>> _wantedCarUpdateDic { get; set; }
        private ViolationDetailsDTO _violationsOrAssetsToolTipDetailsOnGraphic;
        private IncidentDetailsDTO _incidentsToolTipDetailsOnGraphic;
        private ServiceLayerReference.PatrolOfficersDetailsDTO _patrolDetails;
        private AssetDetailsForViolation _assetDetailsForViolation;

        private VehicleLiveTrackingDTO _vehicleDetail;

        private IncidentsDTO _incidentDatails;

        private AssetViolationDetailsDTO _assetViolationsDetails;

        public AssetViolationDetailsDTO AssetViolationsDetails
        {
            get { return _assetViolationsDetails; }
            set { _assetViolationsDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        private STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO _assetViolationCountDetails;

        public STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO AssetViolationCountDetails
        {
            get { return _assetViolationCountDetails; }
            set { _assetViolationCountDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ViolationNotificationDTO[] _vehicleViolationsDetails;

        public ViolationNotificationDTO[] VehicleViolationsDetails
        {
            get { return _vehicleViolationsDetails; }
            set { _vehicleViolationsDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        #region FirstItemOfvehicleViolationsDetails to be deleted when web service is modified

        private ViolationNotificationDTO _firstItemOfvehicleViolationsDetails;

        public ViolationNotificationDTO FirstItemOfvehicleViolationsDetails
        {
            get { return _firstItemOfvehicleViolationsDetails; }
            set { _firstItemOfvehicleViolationsDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        #endregion

        public VehicleLiveTrackingDTO VehicleDetail
        {
            get { return _vehicleDetail; }
            set { _vehicleDetail = value; this.RaiseNotifyPropertyChanged(); }
        }

        public IncidentsDTO IncidentDetails
        {
            get { return _incidentDatails; }
            set { _incidentDatails = value; this.RaiseNotifyPropertyChanged(); }
        }
        public AssetDetailsForViolation AssetDetailsForViolation
        {
            get { return _assetDetailsForViolation; }
            set { _assetDetailsForViolation = value; this.RaiseNotifyPropertyChanged(); }
        }
        private OfficersLastLocationViewDTO _officer;

        public OfficersLastLocationViewDTO Officer
        {
            get { return _officer; }
            set { _officer = value; this.RaiseNotifyPropertyChanged(); }
        }
        //public ViolationDetailsDTO ViolationsOrAssetsToolTipDetailsOnGraphic
        //{
        //    get { return _violationsOrAssetsToolTipDetailsOnGraphic; }
        //    set { _violationsOrAssetsToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        //}
        public IncidentDetailsDTO IncidentsToolTipDetailsOnGraphic
        {
            get { return _incidentsToolTipDetailsOnGraphic; }
            set { _incidentsToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        }
        public ServiceLayerReference.PatrolOfficersDetailsDTO PatrolDetails
        {
            get { return _patrolDetails; }
            set
            {
                _patrolDetails = value;
                //if (_patrolDetails != null && _patrolDetails.Officers != null && _patrolDetails.Officers.Length <= 0)
                //{
                //    _patrolDetails.Officers = new StaffPatrolModel[3];
                //    _patrolDetails.Officers[0] = new StaffPatrolModel() { ImagePath = "", FullName = "FullName FullName" };
                //    _patrolDetails.Officers[1] = new StaffPatrolModel() { ImagePath = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/officer_popup.png", FullName = "FullName" };
                //    _patrolDetails.Officers[2] = new StaffPatrolModel() { ImagePath = "", FullName = "FullNameExtn سيتم الغاء أي تعديلات," };

                //}

                if (_patrolDetails != null && _patrolDetails.Officers != null && _patrolDetails.Officers.Length > 0)
                {
                    foreach (var item in _patrolDetails.Officers)
                    {
                        Uri uriResult = null;
                        item.ImagePath = (Uri.TryCreate(item.ImagePath, UriKind.Absolute, out uriResult)
                            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)) ? item.ImagePath : "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/officer_popup.png";
                    }
                }

                this.RaiseNotifyPropertyChanged();
            }
        }
        public Command FilterViolationsCommand { get; set; }
        public Command FilterPatrolsCommand { get; set; }
        public Command FilterAssetsCommand { get; set; }
        public Command ShowVideoCommand { get; set; }
        public Command ShowImageCommand { get; set; }
        //selected dropdowns
        public Object SelectedAssetType { set; get; }
        public Object SelectedAssetStatus { set; get; }
        public Object SelectedViolationType { set; get; }
        public Object SelectedPatrolStatus { set; get; }

        public Object SelectedViolationDateFilter { set; get; }
        public double _wantedCarRadius = 10;
        #endregion

        public MapControlViewModel()
        {
            _wantedCarUpdateDic = new Dictionary<string, List<string>>();
            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();
            _CurrentWantedPlate = "";
            _CurrentMessageId = "";
            _wantedCarRadius = (double)_client.GetWantedCarRadius();
            _tempNotificationsGraphic = new List<Graphic>();
            Officers = new List<OfficersLastLocationViewDTO>();
            Officer = new OfficersLastLocationViewDTO();
            //MapLayerServiceUrl = System.Configuration.ConfigurationSettings.AppSettings["MapLayerServiceUrl"];

            FilterViolationsCommand = new Command(FilterViolationsOnMap);
            FilterPatrolsCommand = new Command(FilterPatrolsOnMap);
            FilterAssetsCommand = new Command(FilterAssetsOnMap);
            ShowVideoCommand = new Command(ShowVideo);
            ShowImageCommand = new Command(ShowImage);

            SearchAccidentsCommand = new Command(SearchAccidents);
            ClearAccidentsCommand = new Command(ClearAccidentsCriteria);

            ShowOnMapAccidentsCommand = new Command(ShowOnMapAccidents);

            ShowOnMapCheckedCommand = new Command(new Action<object>(ShowOnMapChecked));
            ShowOnMapUnCheckedCommand = new Command(new Action<object>(ShowOnMapUnChecked));

            _accidentAddres = string.Empty;
            _accidentNo = string.Empty;
            AccidentSearchStatus = string.Empty;
            //_checkUnCheckAllContent = Properties.Resources.strSelectAll;

            LoadAccidentLookups();

            GetLayerObservable(LayerTypeEnum.Incidents);
            GetLayerObservable(LayerTypeEnum.Violations);
            GetLayerObservable(LayerTypeEnum.Patrols);

            GetLayerObservable(LayerTypeEnum.Assets);

            GetLayerObservable(LayerTypeEnum.AssetsRedLights);
            GetLayerObservable(LayerTypeEnum.AssetsSpeed);
            GetLayerObservable(LayerTypeEnum.AssetsSmartTowers);

            GetLayerObservable(LayerTypeEnum.Notifications);
            GetLayerObservable(LayerTypeEnum.Officers);

            ViolationTypesList = new ObservableCollection<ViolationTypeDimDTO>();
            AssetStatusList = new ObservableCollection<AssetStatusDimDTO>();
            AssetTypesList = new ObservableCollection<AssetTypeDimDTO>();
            PatrolStatusList = new ObservableCollection<PatrolStatusDimDTO>();
            AllUsers = new ObservableCollection<UsersDTO>();
            //ViolationsOrAssetsToolTipDetailsOnGraphic = new ViolationDetailsDTO();

            ViolationsByStatusData = new ObservableCollection<NameValueModel>();
            ViolationsByTypeData = new ObservableCollection<NameValueModel>();
            AssetDetailsForViolation = new ServiceLayerReference.AssetDetailsForViolation();

            LoadData();
        }

        public long SendTaskToPatrol(Guid patrolID, string msg, DateTime date, double lat, double lon)
        {
            long result = 0;

            result = _TFMClient.AddDutyAsync(patrolID, msg, date, lat, lon, 0, 0).Result;

            if (result > 0)
            {
                _TFMClient.UpdatePatrolCurrentTask(patrolID, result);
            }

            return result;
        }

        public bool SendTaskToOfficer(Guid patrolID, string msg, DateTime date, double lat, double lon, int userId)
        {
            return _smartOfficerClient.AddTaskAsync(new ClassLibrary.DTO.OfficerTaskDTO()
            {
                TaskMessage = msg,
                Latitude = lat,
                Longitude = lon,
                CreateDate = date,
                OfficerMilitaryId = SmartOfficer.OfficerMilitaryId,
                UserId = userId,
                TaskTime = date
            }).Result;
        }

        public void SendIncidentToUsers(List<UsersDTO> usersToSend)
        {
            string xmlToSend = IncidentsToolTipDetailsOnGraphic.XML;
            _client.SaveSendControlToUsersAsync(xmlToSend, usersToSend.Select(x => x.UserName).ToArray());
        }

        public void AddGraphicsToLayersGraphicsDictionary(LayerTypeEnum layerType)
        {
            switch (layerType)
            {
                case LayerTypeEnum.Violations:
                    GetViolationsData(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0), null);
                    break;
                case LayerTypeEnum.Incidents:
                    GetIncidentsDTOData();
                    break;
                case LayerTypeEnum.Patrols:
                    GetPatrolLastLocationDTOData(null);
                    break;
                case LayerTypeEnum.Assets:
                    GetAssetLastStatusData(null, null, LayerTypeEnum.Assets);
                    break;
                case LayerTypeEnum.AssetsRedLights:
                    GetAssetLastStatusData(null, 2, LayerTypeEnum.AssetsRedLights);
                    break;
                case LayerTypeEnum.AssetsSpeed:
                    GetAssetLastStatusData(null, 3, LayerTypeEnum.AssetsSpeed);
                    break;
                case LayerTypeEnum.AssetsSmartTowers:
                    GetAssetLastStatusData(null, 7, LayerTypeEnum.AssetsSmartTowers);
                    break;
                case LayerTypeEnum.Officers:
                    GetOfficersData();
                    break;
                //case LayerTypeEnum.Traffic:
                //    GetTrafficData();
                //    break;
            }
        }

        private void LoadData()
        {
            //load filters
            GetAssetStatusDimDTOList();
            GetAssetTypesList();
            GetViolationTypeDimDTOsList();
            GetPatrolStatusList();
            GetAllUsersList();
            //connect signalR
            Task connectTask = new Task(ConnectToSignalR);
            connectTask.Start();

        }

        public void ConnectToSignalR()
        {
            string url = Utility.GetSignalRUrl();
            _conn = new HubConnection(url);
            var violationsHub = _conn.CreateHubProxy("ViolationsHub");
            var assetsHub = _conn.CreateHubProxy("AssetsHub");
            var incidentHub = _conn.CreateHubProxy("IncidentHub");
            var patrolHub = _conn.CreateHubProxy("PatrolHub");
            _vehicleHub = _conn.CreateHubProxy("VehicleLiveTrackingHub");
            violationsHub.On<ObservableCollection<ViolationNotificationDTO>>("NewViolations", UpdateViolationsBySignalR);
            assetsHub.On<ObservableCollection<AssetLastStatusDTO>>("NewAssets", UpdateAssetsBySignalR);
            incidentHub.On<ObservableCollection<IncidentsDTO>>("NewIncidents", UpdateIncidentsBySignalR);
            patrolHub.On<ObservableCollection<ServiceLayerReference.PatrolLastLocationDTO>>("NewLocations", UpdatePatrolsBySignalR);
            _vehicleHub.On<ObservableCollection<VehicleLiveTrackingDTO>>("NewVehicleLocations", UpdateWantedCarLocationBySignalR);

            //var supervisorNotificationHub = _conn.CreateHubProxy("SupervisorNotificatoinHub");

            //supervisorNotificationHub.On<ObservableCollection<SupervisorNotificationDTO>>("SupervisorNotificationsChanged", UpdateSupervisorNotificationsBySignalR);

            _conn.Start().Wait();
        }

        public void RegisterWantedCarSignalR(string plateNumber)
        {
            try
            {
                _vehicleHub.Invoke("RegisterDependency", plateNumber);
            }
            catch (Exception ex)
            {

            }
        }

        public void UnRegisterWantedCarSignalR()
        {
            try
            {
                _vehicleHub.Invoke("UnRegisterDependency");
            }
            catch
            {

            }
        }

        #region SignalR update


        private void UpdateSupervisorNotificationsBySignalR(ObservableCollection<SupervisorNotificationDTO> newNotifications)
        {

            //SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(supervisorNotifications);


            if (newNotifications != null && newNotifications.Count > 0)
            {
                var NotificationsToNotify = new ObservableCollection<SupervisorNotificationDTO>(newNotifications.Where(x => x.IsNoticed == false).ToList());
                //SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(supervisorNotifications.Where(x => x.IsNoticed == true && x.Status != SupervisorNotificationStatus.Rejected).ToList());
            }
        }
        private void UpdateWantedCarLocationBySignalRTemp(ObservableCollection<VehicleLiveTrackingDTO> vehicles)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var vehicle in vehicles)
            {
                if (vehicle.Latitude.HasValue && vehicle.Longitude.HasValue)
                {
                    var isCurrentSOP = vehicle.PlateNumber.ToLower() == _CurrentWantedPlate.ToLower();
                    var markerGraphics = layerCol.Where(x => !x.Attributes.ContainsKey("OldReferenceId") && x.Attributes.ContainsKey("PlateNumber")
                        && x.Attributes["PlateNumber"].ToString() == vehicle.PlateNumber.ToString()); // Get the original icons
                    double orgLat = 0.00;
                    double orgLon = 0.00;
                    var circleGraphics = layerCol.Where(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);
                    Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (markerGraphics != null)
                            {
                                foreach (var markerGraphic in markerGraphics.ToList())
                                {
                                    var lat = 0.00;
                                    var lon = 0.00;
                                    double.TryParse(markerGraphic.Attributes["Latitude"].ToString(), out lat);
                                    double.TryParse(markerGraphic.Attributes["Longitude"].ToString(), out lon);

                                    if (!markerGraphic.Attributes.ContainsKey("OrginalLatitude"))
                                    {
                                        markerGraphic.Attributes.Add("OrginalLatitude", lat);
                                    }
                                    if (!markerGraphic.Attributes.ContainsKey("OriginalLongitude"))
                                    {
                                        markerGraphic.Attributes.Add("OriginalLongitude", lon);
                                    }
                                    double.TryParse(markerGraphic.Attributes["OrginalLatitude"].ToString(), out orgLat);
                                    double.TryParse(markerGraphic.Attributes["OriginalLongitude"].ToString(), out orgLon);

                                    // move the original icon
                                    markerGraphic.Geometry = new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326));
                                    markerGraphic.Attributes["Latitude"] = vehicle.Latitude.Value;
                                    markerGraphic.Attributes["Longitude"] = vehicle.Longitude.Value;
                                    var symbol = new PictureMarkerSymbol();
                                    symbol.SetSourceAsync(new Uri(GetMarkerImageUrl(MarkerType.LastWantedCarPlace, null)));
                                    markerGraphic.Symbol = symbol;

                                    //if (isCurrentSOP)
                                    //{
                                    //    var maxHours = 3;
                                    //    if(System.Configuration.ConfigurationSettings.AppSettings["WantedCarMaxPeriodInHours"] != null)
                                    //        int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["WantedCarMaxPeriodInHours"].ToString(),out maxHours);
                                    //    var oldPathIcons = _client.GetAllWantedCarLiveTrack(vehicle.PlateNumber, maxHours); // get all path icons
                                    //    if(oldPathIcons != null && oldPathIcons.Any())
                                    //    {
                                    //        var liveTrackPath = GetMarkerImageUrl(MarkerType.WantedCarTracking, null);
                                    //        foreach (var item in oldPathIcons)
                                    //        {
                                    //            var oldGraphic = CreateGraphicByGeometry(markerGraphic.Attributes, new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)), liveTrackPath, null);
                                    //            if (oldGraphic != null)
                                    //            {
                                    //                oldGraphic.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_old";
                                    //                oldGraphic.Attributes["OldReferenceId"] = vehicle.PlateNumber;
                                    //                oldGraphic.Attributes["IndexValue"] = 1;
                                    //                layerCol.Add(oldGraphic);
                                    //            }
                                    //        }
                                    //    }
                                    //    var orgPath = GetMarkerImageUrl(MarkerType.WantedCar, null);
                                    //    var orgIcon = CreateGraphicByGeometry(markerGraphic.Attributes, new MapPoint(orgLon, orgLat, new SpatialReference(4326)), orgPath, null);
                                    //    if (orgIcon != null)
                                    //    {
                                    //        orgIcon.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_old";
                                    //        orgIcon.Attributes["OldReferenceId"] = vehicle.PlateNumber;
                                    //        orgIcon.Attributes["IndexValue"] = 1;
                                    //        layerCol.Add(orgIcon);
                                    //    }
                                    //}
                                }

                            }
                        });
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (circleGraphics != null)
                        {
                            foreach (var circleGraphic in circleGraphics.ToList())
                            {
                                DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), _wantedCarRadius, circleGraphic.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red);
                            }

                        }
                    });

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (UpdateLocationOnMapEvent != null)
                            UpdateLocationOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);

                        if (ZoomOnMapEvent != null && isCurrentSOP)
                            ZoomOnMapEvent(orgLat, vehicle.Latitude.Value, orgLon, vehicle.Longitude.Value, vehicle.PlateNumber);
                    });
                }
            }

        }

        private void UpdateWantedCarLocationBySignalR(ObservableCollection<VehicleLiveTrackingDTO> vehicles)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            //var grpLayer = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var vehicle in vehicles)
            {
                var valueLoc = string.Format("{0},{1}", vehicle.Latitude, vehicle.Longitude);
                if ((_wantedCarUpdateDic.ContainsKey(vehicle.PlateNumber) && _wantedCarUpdateDic[vehicle.PlateNumber].Contains(valueLoc)))
                    continue;
                bool isTruck = false;

                var isCurrentSOP = vehicle.PlateNumber.ToLower() == _CurrentWantedPlate.ToLower();
                var markerGraphics = layerCol.Where(x => !x.Attributes.ContainsKey("OldReferenceId") && x.Attributes.ContainsKey("PlateNumber") && x.Attributes["PlateNumber"].ToString() == vehicle.PlateNumber.ToString());
                //if (markerGraphics == null)
                //{
                //    isTruck = true;
                //    markerGraphics = layerCol.FirstOrDefault(x => x.Attributes["PlateNumber"].ToString() == vehicle.PlateNumber.ToString() + "_TruckViolationCenter");
                //}
                double orgLat = 0.00;
                double orgLon = 0.00;
                var circleGraphics = layerCol.Where(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);

                Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (markerGraphics != null)
                        {
                            var liveTrackPath = GetMarkerImageUrl(MarkerType.WantedCarTracking, null);
                            var lastTrackIcon = GetMarkerImageUrl(MarkerType.LastWantedCarPlace, null);
                            foreach (var markerGraphic in markerGraphics.ToList())
                            {

                                double.TryParse(markerGraphic.Attributes["Latitude"].ToString(), out orgLat);
                                double.TryParse(markerGraphic.Attributes["Longitude"].ToString(), out orgLon);
                                if (orgLat == vehicle.Latitude && orgLon == vehicle.Longitude)
                                    continue;
                                var oldIcon = layerCol.Where(x => x.Attributes["Id"].ToString() == markerGraphic.Attributes["Id"].ToString() + "_new");
                                var trackingSymbol = new PictureMarkerSymbol();
                                trackingSymbol.SetSourceAsync(new Uri(liveTrackPath));
                                var lastIndex = 1;
                                if (oldIcon != null)
                                {
                                    foreach (var item in oldIcon.ToList())
                                    {
                                        if (isCurrentSOP)
                                        {
                                            //oldIcon.Geometry = new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326));
                                            item.Symbol = trackingSymbol;
                                            item.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_old";
                                            if (item.Attributes.ContainsKey("IndexValue"))
                                                int.TryParse(item.Attributes["IndexValue"].ToString(), out lastIndex);
                                        }
                                        else
                                        {
                                            layerCol.Remove(item);
                                        }
                                    }

                                }

                                var oldGraphic = CreateGraphicByGeometry(markerGraphic.Attributes, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), lastTrackIcon, null);
                                if (oldGraphic != null)
                                {
                                    oldGraphic.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_new";
                                    oldGraphic.Attributes["OldReferenceId"] = vehicle.PlateNumber;
                                    oldGraphic.Attributes["IndexValue"] = lastIndex + 1;
                                    layerCol.Add(oldGraphic);
                                }
                                if (!isCurrentSOP)
                                {
                                    markerGraphic.IsVisible = false;
                                }
                                //markerGraphic.IsVisible = false;

                            }


                        }
                    });

                Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (circleGraphics != null)
                        {
                            foreach (var circleGraphic in circleGraphics.ToList())
                            {
                                DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), _wantedCarRadius, circleGraphic.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red);
                            }

                        }
                    });

                Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (UpdateLocationOnMapEvent != null)
                            UpdateLocationOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);

                        if (ZoomOnMapEvent != null && vehicle.PlateNumber.ToLower() == _CurrentWantedPlate.ToLower())
                            ZoomOnMapEvent(orgLat, vehicle.Latitude.Value, orgLon, vehicle.Longitude.Value, vehicle.PlateNumber);
                    });

                if (_wantedCarUpdateDic.ContainsKey(vehicle.PlateNumber))
                    _wantedCarUpdateDic[vehicle.PlateNumber].Add(valueLoc);
                else
                {
                    var temp = new List<string>();
                    temp.Add(valueLoc);
                    _wantedCarUpdateDic.Add(vehicle.PlateNumber, temp);
                }
            }

        }

        //private void UpdateWantedCarLocationBySignalR(ObservableCollection<VehicleLiveTrackingDTO> vehicles)
        //{
        //    var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

        //    foreach (var vehicle in vehicles)
        //    {
        //        bool isTruck = false;

        //        var markerGraphics = layerCol.Where(x => x.Attributes.ContainsKey("PlateNumber") && x.Attributes["PlateNumber"].ToString() == vehicle.PlateNumber.ToString());

        //        var circleGraphics = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            if (markerGraphics != null)
        //            {
        //                foreach(var marker in markerGraphics.ToList())
        //                { 
        //                var liveTrackPath = GetMarkerImageUrl(MarkerType.WantedCarTracking, null);
        //                var oldGraphic = CreateGraphicByGeometry(markerGraphics.Attributes, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), liveTrackPath, null);
        //                if (oldGraphic != null)
        //                {
        //                    // oldGraphic.CopyFrom(markerGraphic.Attributes);
        //                    oldGraphic.Attributes["Id"] = oldGraphic.Attributes["Id"].ToString() + "_old";
        //                    var count = layerCol.Count(x => x.Attributes["Id"].ToString() == oldGraphic.Attributes["Id"].ToString() + "_old");
        //                    oldGraphic.Attributes["IndexValue"] = count + 1;
        //                    layerCol.Add(oldGraphic);

        //                    try
        //                    {
        //                        if (_CurrentWantedPlate.ToLower() == vehicle.PlateNumber.ToLower())
        //                        {
        //                            /// var myView =(ListCollectionView) CollectionViewSource.GetDefaultView(layerCol);
        //                            var oldLines = layerCol.Where(x => x.Attributes.ContainsKey("LinesMessageId") && x.Attributes["LinesMessageId"].ToString() == markerGraphics.Attributes["MessageId"].ToString()).ToList();
        //                            //myView.Filter = p => FilterOldLine((Graphic)p, markerGraphics.Attributes["MessageId"].ToString());
        //                            //myView.Refresh();
        //                            //var lines = layerCol.Where(x=> myView.Filter(x)).ToList();
        //                            if (oldLines != null)
        //                            {
        //                                oldLines.Clear();
        //                            }
        //                            var allOldGraphics = layerCol.Where(x => x.Attributes["Id"].ToString() == oldGraphic.Attributes["Id"].ToString()).OrderBy(x => int.Parse(oldGraphic.Attributes["IndexValue"].ToString())).AsEnumerable();
        //                            // myView.Filter = p => FilterOldGraphic((Graphic)p, oldGraphic.Attributes["Id"].ToString());
        //                            //myView.Refresh();
        //                            //var allOldGraphics = myView.SourceCollection as ObservableCollection<Graphic>;

        //                            if (allOldGraphics != null && allOldGraphics.Any())
        //                            {
        //                                // allOldGraphics = allOldGraphics.OrderBy(x => int.Parse(oldGraphic.Attributes["IndexValue"].ToString()));
        //                                var oldLatitude = double.Parse(markerGraphics.Attributes["Latitude"].ToString());
        //                                var oldLongitue = double.Parse(markerGraphics.Attributes["Longitude"].ToString());

        //                                var newLatitude = double.Parse(allOldGraphics.ElementAt(0).Attributes["Latitude"].ToString());
        //                                var newLongitude = double.Parse(allOldGraphics.ElementAt(0).Attributes["Longitude"].ToString());
        //                                CreateLineLiveTrack(oldLatitude, oldLongitue, newLatitude, newLongitude, layerCol, allOldGraphics.ElementAt(0).Attributes, markerGraphics.Attributes["MessageId"].ToString());
        //                                for (int i = 1; i < allOldGraphics.Count() - 1; i++)
        //                                {
        //                                    var oldLat = double.Parse(allOldGraphics.ElementAt(i).Attributes["Latitude"].ToString());
        //                                    var oldLon = double.Parse(allOldGraphics.ElementAt(i).Attributes["Longitude"].ToString());
        //                                    var newLat = double.Parse(allOldGraphics.ElementAt(i + 1).Attributes["Latitude"].ToString());
        //                                    var newLon = double.Parse(allOldGraphics.ElementAt(i + 1).Attributes["Longitude"].ToString());
        //                                    CreateLineLiveTrack(oldLat, oldLon, newLat, newLon, layerCol, allOldGraphics.ElementAt(i).Attributes, markerGraphics.Attributes["MessageId"].ToString());

        //                                }

        //                            }
        //                        }
        //                    }
        //                        //this.RaiseNotifyPropertyChanged("LayersGraphicsDictionary");
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }

        //            }
        //            //  this.RaiseNotifyPropertyChanged("LayersGraphicsDictionary");
        //        });

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            if (circleGraphics != null)
        //            {
        //                DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), (double)_client.GetWantedCarRadius(), circleGraphics.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red);
        //            }
        //        });

        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            if (UpdateLocationOnMapEvent != null)
        //                UpdateLocationOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);

        //            if (ZoomOnMapEvent != null && vehicle.PlateNumber.ToLower() == _CurrentWantedPlate.ToLower())
        //                ZoomOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);
        //        });
        //        //Application.Current.Dispatcher.Invoke(() =>
        //        //        {
        //        //            if (markerGraphics != null)
        //        //            {
        //        //                int markersCount = markerGraphics.Count();
        //        //                for (int i = 0; i < markersCount;i++ )
        //        //                {
        //        //                    var liveTrackPath = GetMarkerImageUrl(MarkerType.WantedCarTracking, null);
        //        //                    var oldGraphic = CreateGraphicByGeometry(markerGraphics.ElementAt(i).Attributes, markerGraphics.ElementAt(i).Geometry, liveTrackPath, null);
        //        //                    if (oldGraphic != null)
        //        //                    {
        //        //                        // oldGraphic.CopyFrom(markerGraphic.Attributes);
        //        //                        oldGraphic.Attributes["Id"] = oldGraphic.Attributes["Id"].ToString() + "_old";
        //        //                        layerCol.Add(oldGraphic);
        //        //                    }
        //        //                    markerGraphics.ElementAt(i).Geometry = new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326));

        //        //                }
        //        //            }
        //        //            if (circleGraphics != null)
        //        //            {
        //        //                int circlesCount = circleGraphics.Count();
        //        //                for (int i = 0; i < circlesCount;i++ )
        //        //                {
        //        //                    DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), (double)_client.GetWantedCarRadius(), circleGraphics.ElementAt(i).Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red);
        //        //                }
        //        //            }

        //        //            if (ZoomOnMapEvent != null)
        //        //                ZoomOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);
        //        //        });
        //    }
        //}

        private bool FilterOldGraphic(Graphic graphic, string Id)
        {
            return graphic.Attributes["Id"].ToString() == Id;
        }

        private bool FilterOldLine(Graphic graphic, string MessageId)
        {
            return graphic.Attributes.ContainsKey("LinesMessageId") && graphic.Attributes["LinesMessageId"].ToString() == MessageId;
        }

        public void CreateLineLiveTrack(double oldLat, double oldLon, double newLat, double newLon, ObservableCollection<Graphic> layerCol, IDictionary<string, object> attributes, string MessageId)
        {
            var oldPoint = new MapPoint(oldLon, oldLat, new SpatialReference(4326));
            var newPoint = new MapPoint(newLon, newLat, new SpatialReference(4326));
            var points = new List<MapPoint>();
            points.Add(oldPoint);
            points.Add(newPoint);
            var line = CreateGraphicByGeometry(attributes, new Polyline(points, new SpatialReference(4326)), "", null, true);
            if (line != null)
            {
                line.Attributes["LinesMessageId"] = MessageId; //markerGraphics.Attributes["MessageId"];
                layerCol.Add(line);
            }
        }
        private void UpdatePatrolsBySignalR(ObservableCollection<ServiceLayerReference.PatrolLastLocationDTO> patrols)
        {
            patrols = new ObservableCollection<ServiceLayerReference.PatrolLastLocationDTO>(patrols.Where(x =>
                  SelectedPatrolStatus != null && ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId != 0 ? x.StatusId == ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId : true
                  ).ToList<ServiceLayerReference.PatrolLastLocationDTO>());

            UpdatePatrolsLocations(patrols);
        }
        private void UpdatePatrolsLocations(ObservableCollection<ServiceLayerReference.PatrolLastLocationDTO> patrols)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Patrols);
            var notificationLayer = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in patrols.ToList())
            {
                var graphic = layerCol.FirstOrDefault(x => long.Parse(x.Attributes["Id"].ToString()) == item.PatrolId);
                if (graphic != null)
                    Application.Current.Dispatcher.Invoke(() => graphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
                else
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var attr = CreateGraphicDictionary(item, LayerTypeEnum.Patrols);
                        AddGraphicToLayer(attr, item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item), LayerTypeEnum.Patrols, null);
                    });

                var notificationGraphic = notificationLayer.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"] == item.PatrolId.ToString() + "_PatrolCenter");
                if (notificationGraphic != null)
                    Application.Current.Dispatcher.Invoke(() => notificationGraphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
            }



        }

        private void UpdateIncidentsBySignalR(ObservableCollection<IncidentsDTO> incidents)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Incidents);
            foreach (var item in incidents)
            {
                var graphic = layerCol.FirstOrDefault(x => long.Parse(x.Attributes["Id"].ToString()) == item.IncidentId);

                if (!item.EndTime.HasValue)
                {
                    if (graphic != null)
                        Application.Current.Dispatcher.Invoke(() => graphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
                    else
                    {
                        var attr = CreateGraphicDictionary(item, LayerTypeEnum.Incidents);
                        AddGraphicToLayer(attr, item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item), LayerTypeEnum.Incidents, null);
                    }
                }
                else
                {
                    if (graphic != null)
                        Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
                }
            }
        }
        private void UpdateAssetsBySignalR(ObservableCollection<AssetLastStatusDTO> assets)
        {
            assets = new ObservableCollection<AssetLastStatusDTO>(assets.Where(x =>
                  SelectedAssetStatus != null && ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId != 0 ? x.AssetStatusId == ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId : true &&
                   SelectedAssetType != null && ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId != 0 ? x.AssetTypeId == ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId : true
                  ).ToList<AssetLastStatusDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Assets);
            foreach (var item in assets)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.AssetId);
                if (graphic != null)
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        graphic.Attributes["Status"] = item.AssetStatusName;

                        CompositeSymbol compositeSymbol = new CompositeSymbol();
                        PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();

                        pictureMarkerSymbol.SetSourceAsync(new Uri(GetMarkerImageUrl(MarkerType.Assets, item)));


                        compositeSymbol.Symbols.Add(pictureMarkerSymbol);

                        graphic.Symbol = compositeSymbol;
                    });
            }
        }
        private void UpdateViolationsBySignalR(ObservableCollection<ViolationNotificationDTO> violations)
        {
            violations = new ObservableCollection<ViolationNotificationDTO>(violations.Where(x =>
                  SelectedViolationDateFilter != null ? x.DateTaken > (((DateTime?)SelectedViolationDateFilter)).Value : true &&
                   SelectedViolationType != null && ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId != 0 ? x.ViolationTypeId == ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId : true
                  ).ToList<ViolationNotificationDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Violations);
            foreach (var item in violations)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.LocationCode);
                if (graphic != null)
                {
                    int count = graphic.Attributes["Count"] != null ? int.Parse(graphic.Attributes["Count"].ToString()) : 0;
                    count++;
                    Application.Current.Dispatcher.Invoke(() => graphic.Attributes["Count"] = count.ToString());
                    Application.Current.Dispatcher.Invoke(() => ((graphic.Symbol as CompositeSymbol).Symbols[1] as TextSymbol).Text = count.ToString());
                }
                else
                {
                    if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                    {
                        var attr = CreateGraphicDictionary(item, LayerTypeEnum.Violations, 1);
                        AddGraphicToLayer(attr, item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Violation, item), LayerTypeEnum.Violations, 1);
                    }
                }
            }
        }

        #endregion

        #region AddLayerDataOnMap
        private void GetPatrolLastLocationDTOData(int? SelectedPatrolStatusId)
        {
            var callTask = _client.GetPatrolsListAsync(SelectedPatrolStatusId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolLastLocationDTOData(x == null ? null : x.ToList()));
        }

        private void Add_PatrolLastLocationDTOData(List<ServiceLayerReference.PatrolLastLocationDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Patrols);
            foreach (var item in list)
                AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item), LayerTypeEnum.Patrols, null);
        }

        private void GetAssetLastStatusData(int? SelectedAssetStatusDimDTO, int? SelectedAssetType, LayerTypeEnum layerType)
        {
            var callTask = _client.GetAssetsListAsync(SelectedAssetStatusDimDTO, SelectedAssetType);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetLastStatusData(x == null ? null : x.ToList(), layerType));
        }

        private void GetOfficersData()
        {
            var callTask = _client.GetOfficersListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe(x => Add_OfficersLastLocationDTOData(x.ToList()));
        }

        private void Add_OfficersLastLocationDTOData(List<OfficersLastLocationViewDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Officers);
            foreach (var item in list)
                AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Officers), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Officers, item), LayerTypeEnum.Officers, null);
            Officers = list;
        }

        private void Add_AssetLastStatusData(List<AssetsViewDTO> list, LayerTypeEnum layerType)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(layerType);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, layerType), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item), layerType, null));
                }
            }
        }
        private void GetViolationsData(DateTime? dateTaken, int? ViolationTypeDimDTOId)
        {
            var callTask = _client.GetViolationsListGroupedByLocationAsync(dateTaken, ViolationTypeDimDTOId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationData(x != null ? new ObservableCollection<ViolationsGroupedByLocationsDTO>(x) : null));
        }
        private void Add_ViolationData(ObservableCollection<ViolationsGroupedByLocationsDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Violations);
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Violations, item.ViolationsCount), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Violation, item), LayerTypeEnum.Violations, item.ViolationsCount));
        }
        private void GetIncidentsDTOData()
        {
            var callTask = _client.GetActiveIncidentsListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_IncidentsDTOData(x != null ? new ObservableCollection<IncidentsDTO>(x) : null));
        }


        private void Add_IncidentsDTOData(ObservableCollection<IncidentsDTO> list)
        {
            IncidentsListAll = list;
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Incidents);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item), LayerTypeEnum.Incidents, null));
            }
        }
        public void AddNotificationViolationOnMap(ViolationToDraw violationToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == violationToDraw.Id && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Violations.ToString());
            if (graphic == null)
            {
                AddGraphicToLayer(CreateGraphicDictionary(violationToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Violations), violationToDraw.Latitude, violationToDraw.Longitude, GetMarkerImageUrl(MarkerType.ViolationsNotifications, violationToDraw), LayerTypeEnum.Notifications, null);
            }
        }
        public void AddNotificationIncidentOnMap(IncidentToDraw incidentToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == incidentToDraw.Id.ToString() && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Incidents.ToString());
            if (graphic == null)
                AddGraphicToLayer(CreateGraphicDictionary(incidentToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Incidents), incidentToDraw.Latitude, incidentToDraw.Longitude, GetMarkerImageUrl(MarkerType.IncidentNotification, incidentToDraw), LayerTypeEnum.Notifications, null);
        }
        public void ManageFogEventMessage(FogEventMessage fogEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            //MapPoint point = new MapPoint(fogEventMessage.Longitude, fogEventMessage.Latitude, new SpatialReference(4326));
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, fogEventMessage.VisibilityRadius, BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, _client.GetRadiusForNearByAssets(), BufferSymbolCircleBig, SHAPE_Z_INDEX, LayerTypeEnum.Notifications);
            SearchNearByAssetsAndPatrolsForFogEvent(fogEventMessage);
        }

        public void DrawNotificationCircle(IXMLMessageObject message, SimpleFillSymbol BufferSymbolCircleSmall, Color color, double radius, string Id)
        {
            MapPoint point = new MapPoint(message.GetLongitude(), message.GetLatitude(), new SpatialReference(4326));
            DrawCircle(Id, point, radius, BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
        }
        public void DrawOldPath(WantedCarMessage message)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            AddWantedCarTowerMarker(message);
            var oldIconsToRemove = layerCol.Where(x => x.Attributes.ContainsKey("OldReferenceId") && x.Attributes.ContainsKey("PlateNumber") && x.Attributes["PlateNumber"].ToString() == message.VehiclePlateNumber);
            if (oldIconsToRemove != null && oldIconsToRemove.Any())
            {
                foreach (var item in oldIconsToRemove.ToList())
                {
                    layerCol.Remove(item);
                }
            }
            var maxHours = 24;
            if (System.Configuration.ConfigurationSettings.AppSettings["WantedCarMaxPeriodInHours"] != null)
                int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["WantedCarMaxPeriodInHours"].ToString(), out maxHours);
            var oldPathIcons = _client.GetAllWantedCarLiveTrack(message.VehiclePlateNumber, maxHours); // get all path icons
            var markerGraphic = layerCol.FirstOrDefault(x => !x.Attributes.ContainsKey("OldReferenceId") && x.Attributes.ContainsKey("PlateNumber")
                        && x.Attributes["PlateNumber"].ToString() == message.VehiclePlateNumber.ToString() && x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (message.VehiclePlateNumber.ToString() + "_WantedCarCenter_" + message.MessageId));
            if (markerGraphic != null)
                markerGraphic.IsVisible = true;
            if (oldPathIcons != null && oldPathIcons.Any(x => x.CaptureTime > message.CreatedDate))
            {
                var lastTrackPath = GetMarkerImageUrl(MarkerType.LastWantedCarPlace, null);
                var liveTrackPath = GetMarkerImageUrl(MarkerType.WantedCarTracking, null);
                var path = liveTrackPath;
                var index = 1;
                var iconsList = oldPathIcons.Where(x => x.CaptureTime > message.CreatedDate).OrderBy(x => x.CaptureTime).ToList();
                if (iconsList.Any())
                {
                    foreach (var item in iconsList)
                    {
                        if (index == iconsList.Count)
                        {
                            path = lastTrackPath;
                        }

                        var oldGraphic = CreateGraphicByGeometry(markerGraphic.Attributes, new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)), path, null);
                        if (oldGraphic != null)
                        {
                            if (index == iconsList.Count)
                                oldGraphic.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_new";
                            else
                                oldGraphic.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_old";
                            oldGraphic.Attributes["OldReferenceId"] = message.VehiclePlateNumber;
                            oldGraphic.Attributes["IndexValue"] = index;
                            index++;
                            layerCol.Add(oldGraphic);
                        }
                    }
                }
            }
            //    var orgPath = GetMarkerImageUrl(MarkerType.WantedCar, null);
            //    var orgIcon = CreateGraphicByGeometry(markerGraphic.Attributes, new MapPoint(orgLon, orgLat, new SpatialReference(4326)), orgPath, null);
            //    if (orgIcon != null)
            //    {
            //        orgIcon.Attributes["Id"] = markerGraphic.Attributes["Id"].ToString() + "_old";
            //        orgIcon.Attributes["OldReferenceId"] = vehicle.PlateNumber;
            //        orgIcon.Attributes["IndexValue"] = 1;
            //        layerCol.Add(orgIcon);
            //    }
        }
        //Manage Wanted Car Message
        public void ManageWantedCarEventMessage(WantedCarMessage wantedCarEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            //  _CurrentWantedPlate = wantedCarEventMessage.VehiclePlateNumber;
            //MapPoint point = new MapPoint(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude, new SpatialReference(4326));
            //DrawCircle(wantedCarEventMessage.VehiclePlateNumber.ToString(), point, (double)_client.GetWantedCarRadius(), BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);

            SearchNearByAssetsAndPatrolsForWantedCarEvent(wantedCarEventMessage);
        }

        public void ManageTruckPermissionMessage(TruckViolationMessage truckViolationMsg, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            // _CurrentWantedPlate = truckViolationMsg.TruckPlateNumber;
            //MapPoint point = new MapPoint(truckViolationMsg.Longitude, truckViolationMsg.Latitude, new SpatialReference(4326));
            //DrawCircle(truckViolationMsg.TruckPlateNumber.ToString(), point, (double)_client.GetWantedCarRadius(), BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);

            SearchNearByAssetsAndPatrolsForTruckViolation(truckViolationMsg);
        }

        public void ManageDetectedAccidentEventMessage(DetectedAccidentMessage detectedMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(detectedMessage.Longitude, detectedMessage.Latitude, new SpatialReference(4326));
            SearchNearByAssetsAndPatrolsForDetectedAccidentEvent(detectedMessage);
        }
        private bool AddFogTowerMarker(FogEventMessage fogEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (fogEventMessage.TowerId.ToString() + "_FogCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());
            if (graphic == null)
            {
                isNew = true;
                AddGraphicToLayer(CreateGraphicDictionary(fogEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Notifications), fogEventMessage.Latitude, fogEventMessage.Longitude, GetMarkerImageUrl(MarkerType.Fog, fogEventMessage), LayerTypeEnum.Notifications, null);

            }
            return isNew;
        }

        //Add Wanted Car Tower Marker
        private bool AddWantedCarTowerMarker(WantedCarMessage wantedCarEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (wantedCarEventMessage.VehiclePlateNumber.ToString() + "_WantedCarCenter_" + wantedCarEventMessage.MessageId) && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());
            if (graphic == null)
            {
                isNew = true;
                AddGraphicToLayer(CreateGraphicDictionary(wantedCarEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Notifications), wantedCarEventMessage.Latitude, wantedCarEventMessage.Longitude, GetMarkerImageUrl(MarkerType.WantedCar, wantedCarEventMessage), LayerTypeEnum.Notifications, null);

                //graphic = CreateGraphic(CreateGraphicDictionary(wantedCarEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), wantedCarEventMessage.Latitude, wantedCarEventMessage.Longitude, GetMarkerImageUrl(MarkerType.CarPlateNumber, wantedCarEventMessage));


            }
            return isNew;
        }

        private bool AddTruckViolationTowerMarker(TruckViolationMessage truckViolationMsg)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (truckViolationMsg.TruckPlateNumber.ToString() + "_TruckViolationCenter_" + truckViolationMsg.MessageId) && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());
            if (graphic == null)
            {
                isNew = true;
                AddGraphicToLayer(CreateGraphicDictionary(truckViolationMsg, LayerTypeEnum.Notifications, null, LayerTypeEnum.Notifications), truckViolationMsg.Latitude, truckViolationMsg.Longitude, GetMarkerImageUrl(MarkerType.TruckViolation, truckViolationMsg), LayerTypeEnum.Notifications, null);

            }
            return isNew;
        }

        private bool AddDetectedAccidentMarker(DetectedAccidentMessage detectedAccidentEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (detectedAccidentEventMessage.TowerId.ToString() + "_AccidentCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());
            if (graphic == null)
            {
                isNew = true;
                AddGraphicToLayer(CreateGraphicDictionary(detectedAccidentEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Notifications), detectedAccidentEventMessage.Latitude, detectedAccidentEventMessage.Longitude, GetMarkerImageUrl(MarkerType.DetectedAccident, detectedAccidentEventMessage), LayerTypeEnum.Notifications, null);

            }
            return isNew;
        }
        private void SearchNearByAssetsAndPatrolsForFogEvent(FogEventMessage fogEventMessage)
        {
            bool isNewTower = AddFogTowerMarker(fogEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByTowersByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByTowersForFogEvent(x == null ? null : x.ToList(), fogEventMessage.TowerId));
                //callTask = _client.GetNearByRadarsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForFogEvent(x == null ? null : x.ToList(), fogEventMessage));
                //callTask = _client.GetNearByCamerasByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByCamerasForFogEvent(x == null ? null : x.ToList(), fogEventMessage));

                //var callTask = _client.GetNearByPatrolsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
            }
        }

        //Search Near By Assets and Patrols For Wanted Car Event
        private void SearchNearByAssetsAndPatrolsForWantedCarEvent(WantedCarMessage wantedCarEventMessage)
        {
            bool isNewTower = AddWantedCarTowerMarker(wantedCarEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByRadarsByLatLonAsync(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForWantedCar(x == null ? null : x.ToList(), wantedCarEventMessage));
            }
        }

        private void SearchNearByAssetsAndPatrolsForTruckViolation(TruckViolationMessage truckViolationMsg)
        {
            bool isNewTower = AddTruckViolationTowerMarker(truckViolationMsg);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByRadarsByLatLonAsync(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForWantedCar(x == null ? null : x.ToList(), wantedCarEventMessage));
            }
        }

        private void SearchNearByAssetsAndPatrolsForDetectedAccidentEvent(DetectedAccidentMessage detectedAccidentEventMessage)
        {
            bool isNewTower = AddDetectedAccidentMarker(detectedAccidentEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByTowersByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByTowersForFogEvent(x == null ? null : x.ToList(), fogEventMessage.TowerId));
                //callTask = _client.GetNearByRadarsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForFogEvent(x == null ? null : x.ToList(), fogEventMessage));
                //callTask = _client.GetNearByCamerasByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByCamerasForFogEvent(x == null ? null : x.ToList(), fogEventMessage));

                var callTask = _client.GetNearByPatrolsByLatLonAsync(detectedAccidentEventMessage.Longitude, detectedAccidentEventMessage.Latitude, 5);
                var obs = callTask.ToObservable();
                obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
            }
        }

        public void DrawPatrols(DrawPatrolsMessage PatrolsMessage)
        {
            AddPatrolsToMap(PatrolsMessage.PatrolsList);
            //var callTask = _client.GetNearByPatrolsByLatLonAsync(PatrolsMessage.Longitude, PatrolsMessage.Latitude, 5);
            //var obs = callTask.ToObservable();
            //obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
        }

        public void DrawSugestedPatrols(RepositionMessage message)
        {
            if (message.IsAddToMap)
            {
                var patrolsMod = message.Patrols.Select(item => new ServiceLayerReference.PatrolLastLocationDTO
                {
                    Altitude = item.Altitude,
                    IsNoticed = item.IsNoticed,
                    Latitude = item.Latitude,
                    LocationDate = item.LocationDate,
                    Longitude = item.Longitude,
                    PatrolCode = item.PatrolCode,
                    PatrolId = item.PatrolId,
                    PatrolLatLocationId = item.PatrolLatLocationId,
                    PatrolOriginalId = item.PatrolOriginalId,
                    PatrolPlateNo = item.PatrolPlateNo,
                    Speed = item.Speed,
                    StatusId = item.StatusId,
                    StatusName = item.StatusName,
                    IsRecommended = item.IsRecommended,
                    IsBusy = item.IsBusy,
                    ETATime = item.ETATime,
                    NumberOfAssignedIncident = item.NumberOfAssignedIncident,
                    OfficerName = item.OfficerName,
                    isDeleted = item.isDeleted,
                    isPatrol = item.isPatrol,
                    PatrolImage = item.PatrolImage
                }).ToList();
                var layerCol = GetLayerObservable(LayerTypeEnum.Sugestions);
                ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Sugestions);
                foreach (var item in patrolsMod)
                {
                    var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.PatrolId.ToString() + "_PatrolCenter");
                    var markerType = MarkerType.SuggestedPatrol;

                    if (graphic == null)
                    {
                        Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Sugestions, null, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(markerType, item), LayerTypeEnum.Sugestions, null, true));
                    }
                }
            }
            else
            {
                ClearSuggestedPatrols();
            }
        }

        void AddPatrolsToMap(ObservableCollection<STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO> patrols)
        {
            var patrolsMod = new List<ServiceLayerReference.PatrolLastLocationDTO>();
            foreach (var item in patrols)
            {
                patrolsMod.Add(new ServiceLayerReference.PatrolLastLocationDTO
                {
                    Altitude = item.Altitude,
                    IsNoticed = item.IsNoticed,
                    Latitude = item.Latitude,
                    LocationDate = item.LocationDate,
                    Longitude = item.Longitude,
                    PatrolCode = item.PatrolCode,
                    PatrolId = item.PatrolId,
                    PatrolLatLocationId = item.PatrolLatLocationId,
                    PatrolOriginalId = item.PatrolOriginalId,
                    PatrolPlateNo = item.PatrolPlateNo,
                    Speed = item.Speed,
                    StatusId = item.StatusId,
                    StatusName = item.StatusName,
                    IsRecommended = item.IsRecommended,
                    IsBusy = item.IsBusy,
                    ETATime = item.ETATime,
                    NumberOfAssignedIncident = item.NumberOfAssignedIncident,
                    OfficerName = item.OfficerName,
                    isDeleted = item.isDeleted,
                    isPatrol = item.isPatrol,
                    PatrolImage = item.PatrolImage
                });
            }
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in patrolsMod)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.PatrolId.ToString() + "_PatrolCenter");
                var markerType = MarkerType.Patrols;
                if (item.IsRecommended)
                {
                    markerType = MarkerType.RecommendedPatrol;
                }
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(markerType, item), LayerTypeEnum.Notifications, null, true));
                }
            }
        }

        private void Add_NearByPatrolsForFogEvent(List<ServiceLayerReference.PatrolLastLocationDTO> PatrolsList)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var item in PatrolsList)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.PatrolId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item), LayerTypeEnum.Notifications, null));
                }
            }
        }

        private void Add_NearByCamerasForFogEvent(List<AssetsViewDTO> list, FogEventMessage fogEventMessage)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item), LayerTypeEnum.Notifications, null));
                }
            }
        }
        private void Add_NearByRadarsForFogEvent(List<AssetsViewDTO> list, FogEventMessage fogEventMessage)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item), LayerTypeEnum.Notifications, null));
                }
            }
        }
        private void Add_NearByTowersForFogEvent(List<AssetsViewDTO> list, long SourceFogTowerId)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var sourceFog = list.FirstOrDefault(x => x.ItemId == SourceFogTowerId);
            if (sourceFog != null)
                list.Remove(sourceFog);

            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => AddGraphicToLayer(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item), LayerTypeEnum.Notifications, null));
                }
            }
        }

        public void ClearSOPObjects()
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var tmpColl = graphicCol.Where(graphic => graphic.Attributes.ContainsKey("Id") && !graphic.Attributes["Id"].ToString().Contains("_FogCenter") && !graphic.Attributes["Id"].ToString().Contains("_Circle_")).ToList();

            var count = tmpColl.Count;

            //TODO: Edit here
            for (int i = 0; i < count; i++)
            {
                var graphic = tmpColl[0];

                if (graphic != null && graphic.Attributes.ContainsKey("Id") && (graphic.Attributes["Id"].ToString().Contains("_FogCenter") || graphic.Attributes["Id"].ToString().Contains("_Circle_")))
                {

                    continue;
                }

                Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            }



            //foreach (var graphic in graphicCol)
            //{

            //    if (graphic.Attributes.ContainsKey("Id") && graphic.Attributes["Id"].ToString().Contains("_FogCenter") && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString())
            //    {
            //        continue;
            //    }

            //    Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            //}

        }
        public void ClearSuggestedPatrols()
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Sugestions);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Sugestions);

            var tmpColl = graphicCol.Where(graphic => graphic.Attributes.ContainsKey("Id")).ToList();

            var count = tmpColl.Count;

            //TODO: Edit here
            for (int i = 0; i < count; i++)
            {
                var graphic = tmpColl[0];



                Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            }



            //foreach (var graphic in graphicCol)
            //{

            //    if (graphic.Attributes.ContainsKey("Id") && graphic.Attributes["Id"].ToString().Contains("_FogCenter") && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString())
            //    {
            //        continue;
            //    }

            //    Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            //}

        }
        public void DrawSOPMapObject(SOPMapDraw SOPMapDrawObject)
        {

            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var assetTemp = SOPMapDrawObject.ObjectToDraw as dynamic;

            var asset = new AssetsViewDTO
            {
                ItemCategoryName = assetTemp.ItemCategoryName,
                ItemCategoryId = assetTemp.ItemCategoryId,
                ItemId = assetTemp.ItemId,
                ItemImage = assetTemp.ItemImage,
                ItemName = assetTemp.ItemName,
                ItemStatusId = assetTemp.ItemStatusId,
                Latitude = assetTemp.Latitude,
                ItemStatusName = assetTemp.ItemStatusName,
                LocationCode = assetTemp.LocationCode,
                Longitude = assetTemp.Longitude,
                OriginalIdent = assetTemp.OriginalIdent,
                SerialNo = assetTemp.SerialNo
            };

            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == asset.ItemId.ToString() && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());

            if (graphic == null)
            {
                var attr = CreateGraphicDictionary(asset, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets);
                AddGraphicToLayer(attr, SOPMapDrawObject.Lat, SOPMapDrawObject.Lon, GetMarkerImageUrl(MarkerType.Assets, asset), LayerTypeEnum.Notifications, null);
            }
        }

        public void DrawAllTempGraphic()
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in _tempNotificationsGraphic)
                {
                    layerCol.Add(item);
                }
            });

            _tempNotificationsGraphic.Clear();
        }
        public void ClearOldIcons(string messageId)
        {
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            DrawAllTempGraphic();

            var otherIcons = graphicCol.Where(x => x.Attributes["MessageId"] == null || (x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() != messageId));
            if (otherIcons != null)
            {
                foreach (var grh in otherIcons.ToList())
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Remove(grh));
                    _tempNotificationsGraphic.Add(grh);
                    //Application.Current.Dispatcher.Invoke(() => grh.IsVisible = false);
                }
            }
            //var circleGraphics = graphicCol.Where(x => x.Attributes["Id"].ToString() == plateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);
            //if (circleGraphics != null)
            //{
            //    foreach (var circleGraphic in circleGraphics.ToList())
            //    {
            //        var grphsOrg = graphicCol.FirstOrDefault(x => x.Attributes["Id"] != null && x.Attributes["Id"].ToString() == plateNumber.ToString() + "_WantedCarCenter_" + messageId);
            //        if (grphsOrg != null && grphsOrg.Attributes.ContainsKey("Longitude") && grphsOrg.Attributes.ContainsKey("Latitude"))
            //            try
            //            {
            //                var lat = (double)grphsOrg.Attributes["Latitude"];
            //                var lon = (double)grphsOrg.Attributes["Longitude"];
            //                Application.Current.Dispatcher.Invoke(() => DrawCircle(plateNumber, new MapPoint(lon, lat, new SpatialReference(4326)), (double)_client.GetWantedCarRadius(), circleGraphic.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red));
            //            }
            //            catch (Exception ex)
            //            {

            //            }
            //    }

            //    //Application.Current.Dispatcher.Invoke(() => layerCol.Remove(circleGraphics));
            //}
        }

        public void SetCurrentTrackingPlateNumber(string plateNumber, string messageId)
        {
            ClearOldIcons(messageId);

            _CurrentWantedPlate = plateNumber;
            _CurrentMessageId = messageId;
        }

        public void ReturnToNotificationDefault(string MessageId)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

            if (MessageId != "")
            {
                var grhs = layerCol.Where(x => x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() == MessageId && (x.Attributes.ContainsKey("CircleType")));
                if (grhs != null)
                {
                    foreach (var grh in grhs.ToList())
                    {
                        Application.Current.Dispatcher.Invoke(() => layerCol.Remove(grh));
                    }
                }
                var trackingGrh = layerCol.Where(x => x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() == MessageId && x.Attributes.ContainsKey("OldReferenceId") && x.Attributes["Id"].ToString().Contains("_old"));
                if (trackingGrh != null && trackingGrh.Any())
                {
                    foreach (var item in trackingGrh.ToList())
                    {
                        Application.Current.Dispatcher.Invoke(() => layerCol.Remove(item));
                    }
                    var orgGrhps = layerCol.Where(x => x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() == MessageId && (!x.Attributes.ContainsKey("OldReferenceId")));
                    if (orgGrhps != null)
                    {
                        foreach (var grh in orgGrhps.ToList())
                        {
                            var plateNumber = grh.Attributes["PlateNumber"].ToString();
                            if (_wantedCarUpdateDic.ContainsKey(plateNumber))
                            {
                                _wantedCarUpdateDic[plateNumber].Clear();
                            }
                            var relatedGrp = layerCol.Where(x => x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() != MessageId && (!x.Attributes.ContainsKey("OldReferenceId")) && x.Attributes.ContainsKey("PlateNumber") && x.Attributes["PlateNumber"].ToString() == plateNumber);
                            if (relatedGrp != null && relatedGrp.Any())
                            {
                                foreach (var item in relatedGrp.ToList())
                                {
                                    Application.Current.Dispatcher.Invoke(() => item.IsVisible = false);
                                }
                            }
                            Application.Current.Dispatcher.Invoke(() => grh.IsVisible = false);
                        }
                    }
                }
            }
        }

        public void ClearNotificationsLayer(string MessageId = "", bool IsCancelOnly = false)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    _CurrentMessageId = "";
                    _CurrentWantedPlate = "";
                    DrawAllTempGraphic();
                    var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
                    ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

                    var tempPatrols = layerCol.Where(x => x.Attributes.ContainsKey("TempNotificationPatrol"));
                    foreach (var item in tempPatrols.ToList())
                    {
                        Application.Current.Dispatcher.Invoke(() => graphicCol.Remove(item));
                    }

                    if (IsCancelOnly)
                    {
                        ReturnToNotificationDefault(MessageId);
                        return;
                    }



                    if (MessageId != "")
                    {
                        var grhs = graphicCol.Where(x => x.Attributes["MessageId"] != null && x.Attributes["MessageId"].ToString() == MessageId);
                        if (grhs != null)
                        {
                            foreach (var grh in grhs.ToList())
                            {
                                Application.Current.Dispatcher.Invoke(() => graphicCol.Remove(grh));
                            }
                        }
                    }
                    else
                    {
                        var count = graphicCol.Count;
                        for (int i = 0; i < count; i++)
                        {

                            var graphic = graphicCol[i];

                            Application.Current.Dispatcher.Invoke(() => graphicCol.Remove(graphic));

                        }
                    }
                });
        }

        #endregion


        #region AccidentsSearch

        public Command SearchAccidentsCommand { get; set; }

        public Command ClearAccidentsCommand { get; set; }

        public Command ShowOnMapAccidentsCommand { get; set; }

        public Command ShowOnMapCheckedCommand { get; set; }

        public Command ShowOnMapUnCheckedCommand { get; set; }


        private string _accidentSearchStatus;

        public string AccidentSearchStatus
        {
            get { return _accidentSearchStatus; }
            set { _accidentSearchStatus = value; this.RaiseNotifyPropertyChanged(); }
        }


        private string _accidentNo;

        public string AccidentNo
        {
            get { return _accidentNo; }
            set { _accidentNo = value; this.RaiseNotifyPropertyChanged(); }
        }

        private string _accidentAddres;

        public string AccidentAddres
        {
            get { return _accidentAddres; }
            set { _accidentAddres = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentTypeId;

        public Int32? AccidentTypeId
        {
            get { return _accidentTypeId; }
            set { _accidentTypeId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentAreaDTO _accidentAreaId;

        public ClassLibrary.DTO.IncidentAreaDTO AccidentAreaId
        {
            get { return _accidentAreaId; }
            set { _accidentAreaId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentCityDTO _accidentCityId;

        public ClassLibrary.DTO.IncidentCityDTO AccidentCityId
        {
            get { return _accidentCityId; }
            set { _accidentCityId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentCrashTypeDTO _accidentCrashTypeId;

        public ClassLibrary.DTO.IncidentCrashTypeDTO AccidentCrashTypeId
        {
            get { return _accidentCrashTypeId; }
            set { _accidentCrashTypeId = value; this.RaiseNotifyPropertyChanged(); }
        }


        private Int32? _accidentDistrictId;

        public Int32? AccidentDistrictId
        {
            get { return _accidentDistrictId; }
            set { _accidentDistrictId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentEmirateDTO _accidentEmirateId;

        public ClassLibrary.DTO.IncidentEmirateDTO AccidentEmirateId
        {
            get { return _accidentEmirateId; }
            set { _accidentEmirateId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentLightingDTO _accidentLightStatusId;

        public ClassLibrary.DTO.IncidentLightingDTO AccidentLightStatusId
        {
            get { return _accidentLightStatusId; }
            set { _accidentLightStatusId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentPConditionDTO _accidentPConditionId;

        public ClassLibrary.DTO.IncidentPConditionDTO AccidentPConditionId
        {
            get { return _accidentPConditionId; }
            set { _accidentPConditionId = value; this.RaiseNotifyPropertyChanged(); }
        }



        private ClassLibrary.DTO.IncidentLocationDTO _accidentLocationId;

        public ClassLibrary.DTO.IncidentLocationDTO AccidentLocationId
        {
            get { return _accidentLocationId; }
            set { _accidentLocationId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private ClassLibrary.DTO.IncidentLocationTypeDTO _accidentLocationTypeId;

        public ClassLibrary.DTO.IncidentLocationTypeDTO AccidentLocationTypeId
        {
            get { return _accidentLocationTypeId; }
            set { _accidentLocationTypeId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private ClassLibrary.DTO.IncidentPoliceStationDTO _accidentPoliceStationId;

        public ClassLibrary.DTO.IncidentPoliceStationDTO AccidentPoliceStationId
        {
            get { return _accidentPoliceStationId; }
            set { _accidentPoliceStationId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private ClassLibrary.DTO.IncidentRoadTypesDTO _accidentRoadStatusId;

        public ClassLibrary.DTO.IncidentRoadTypesDTO AccidentRoadStatusId
        {
            get { return _accidentRoadStatusId; }
            set { _accidentRoadStatusId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private ClassLibrary.DTO.IncidentReportTypesDTO _accidentReasonTypeId;

        public ClassLibrary.DTO.IncidentReportTypesDTO AccidentReasonTypeId
        {
            get { return _accidentReasonTypeId; }
            set { _accidentReasonTypeId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private Int32? _accidentRoadWaterStatusId;

        public Int32? AccidentRoadWaterStatusId
        {
            get { return _accidentRoadWaterStatusId; }
            set { _accidentRoadWaterStatusId = value; this.RaiseNotifyPropertyChanged(); }
        }
        private ClassLibrary.DTO.IncidentSevertiesDTO _accidentSeverityId;

        public ClassLibrary.DTO.IncidentSevertiesDTO AccidentSeverityId
        {
            get { return _accidentSeverityId; }
            set { _accidentSeverityId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentSpeed;

        public Int32? AccidentSpeed
        {
            get { return _accidentSpeed; }
            set { _accidentSpeed = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ClassLibrary.DTO.IncidentWeatherDTO _accidentWeatherStatusId;

        public ClassLibrary.DTO.IncidentWeatherDTO AccidentWeatherStatusId
        {
            get { return _accidentWeatherStatusId; }
            set { _accidentWeatherStatusId = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentNoOfDeaths;

        public Int32? AccidentNoOfDeaths
        {
            get { return _accidentNoOfDeaths; }
            set { _accidentNoOfDeaths = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentNoOfEasyInjuries;

        public Int32? AccidentNoOfEasyInjuries
        {
            get { return _accidentNoOfEasyInjuries; }
            set { _accidentNoOfEasyInjuries = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentNoOfMedInjuries;

        public Int32? AccidentNoOfMedInjuries
        {
            get { return _accidentNoOfMedInjuries; }
            set { _accidentNoOfMedInjuries = value; this.RaiseNotifyPropertyChanged(); }
        }

        private Int32? _accidentNoOfFatalInjuries;

        public Int32? AccidentNoOfFatalInjuries
        {
            get { return _accidentNoOfFatalInjuries; }
            set { _accidentNoOfFatalInjuries = value; this.RaiseNotifyPropertyChanged(); }
        }






        private DateTime? _accidentStartTime;

        public DateTime? AccidentStartTime
        {
            get { return _accidentStartTime; }
            set { _accidentStartTime = value; this.RaiseNotifyPropertyChanged(); }
        }

        private DateTime? _accidentEndTime;

        public DateTime? AccidentEndTime
        {
            get { return _accidentEndTime; }
            set { _accidentEndTime = value; this.RaiseNotifyPropertyChanged(); }
        }


        private bool _checkUncheckAllisEnabled;

        public bool CheckUncheckAllisEnabled
        {
            get { return _checkUncheckAllisEnabled; }
            set
            {
                _checkUncheckAllisEnabled = value;

                SelectUnSelectAllAccidentsInSearch(value);
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _enableShowOnMap;

        public bool EnableShowOnMap
        {
            get { return _enableShowOnMap; }
            set
            {
                _enableShowOnMap = value;

                //SelectUnSelectAllAccidentsInSearch(value);
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _checkUnCheckAllContent;

        public string CheckUnCheckAllContent
        {
            get { return _checkUnCheckAllContent; }
            set { _checkUnCheckAllContent = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _checkUncheckAll;

        public bool CheckUncheckAll
        {
            get { return _checkUncheckAll; }
            set
            {
                _checkUncheckAll = value;
                if (!isSingleTask)
                {
                    SelectUnSelectAllAccidentsInSearch(value);
                    if (AccidentSearchResultsUI != null)
                        EnableShowOnMap = AccidentSearchResultsUI.Any(a => a.ShowOnMap == true);
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private void SelectUnSelectAllAccidentsInSearch(bool canShowAll)
        {
            isBulkOperationRunning = true;
            CheckUnCheckAllContent = (canShowAll == true) ? Properties.Resources.strUnSelectAll : Properties.Resources.strSelectAll;
            //EnableShowOnMap = canShowAll;


            if (AccidentSearchResultsUI != null && AccidentSearchResultsUI.Count > 0)
            {
                foreach (var searchItem in AccidentSearchResultsUI)
                {
                    searchItem.ShowOnMap = canShowAll;
                }
            }

            isBulkOperationRunning = false;
        }


        private ObservableCollection<IncidentDetailsDtoUI> _accidentSearchResultsUI;

        public ObservableCollection<IncidentDetailsDtoUI> AccidentSearchResultsUI
        {
            get { return _accidentSearchResultsUI; }
            set { _accidentSearchResultsUI = value; this.RaiseNotifyPropertyChanged(); }
        }



        private ClassLibrary.DTO.IncidentsDTO[] _accidentsSearchResults;

        public ClassLibrary.DTO.IncidentsDTO[] AccidentsSearchResults
        {
            get { return _accidentsSearchResults; }
            set { _accidentsSearchResults = value; this.RaiseNotifyPropertyChanged(); }
        }


        private ObservableCollection<IncidentsDTO> _incidentsListAll;

        public ObservableCollection<IncidentsDTO> IncidentsListAll
        {
            get { return _incidentsListAll; }
            set { _incidentsListAll = value; this.RaiseNotifyPropertyChanged(); }
        }

        private void LoadAccidentLookups()
        {
            GetAllAreas();
            GetAllCities();
            GetAllCrashTypes();
            GetAllEmirates();
            GetAllLightings();
            GetAllLocations();
            GetAllLocationTypes();
            GetAllPCondtions();
            GetAllPoliceStations();
            GetAllReportTypes();
            GetAllRoadTypes();
            GetAllSeverties();
            GetAllWeathers();
        }


        private ObservableCollection<ClassLibrary.DTO.IncidentAreaDTO> _accidentAreas;

        public ObservableCollection<ClassLibrary.DTO.IncidentAreaDTO> AccidentAreas
        {
            get { return _accidentAreas; }
            set { _accidentAreas = value; this.RaiseNotifyPropertyChanged(); }
        }

        private void GetAllAreas()
        {
            var callTask = _accidentsClient.GetAllAreasAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentAreas(x));
        }

        private void Add_AccidentAreas(ClassLibrary.DTO.IncidentAreaDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentAreas = new ObservableCollection<ClassLibrary.DTO.IncidentAreaDTO>(data);
                AccidentAreas.Insert(0, new ClassLibrary.DTO.IncidentAreaDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentAreaId = AccidentAreas.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentCityDTO> _accidentCities;

        public ObservableCollection<ClassLibrary.DTO.IncidentCityDTO> AccidentCities
        {
            get { return _accidentCities; }
            set { _accidentCities = value; this.RaiseNotifyPropertyChanged(); }
        }

        private void GetAllCities()
        {
            var callTask = _accidentsClient.GetAllCitiesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentCities(x));
        }

        private void Add_AccidentCities(ClassLibrary.DTO.IncidentCityDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentCities = new ObservableCollection<ClassLibrary.DTO.IncidentCityDTO>(data);
                AccidentCities.Insert(0, new ClassLibrary.DTO.IncidentCityDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentCityId = AccidentCities.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentCrashTypeDTO> _accidentCrashTypes;

        public ObservableCollection<ClassLibrary.DTO.IncidentCrashTypeDTO> AccidentCrashTypes
        {
            get { return _accidentCrashTypes; }
            set { _accidentCrashTypes = value; this.RaiseNotifyPropertyChanged(); }
        }

        private void GetAllCrashTypes()
        {
            var callTask = _accidentsClient.GetAllCrashTypesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentCrashTypes(x));
        }

        private void Add_AccidentCrashTypes(ClassLibrary.DTO.IncidentCrashTypeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentCrashTypes = new ObservableCollection<ClassLibrary.DTO.IncidentCrashTypeDTO>(data);
                AccidentCrashTypes.Insert(0, new ClassLibrary.DTO.IncidentCrashTypeDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentCrashTypeId = AccidentCrashTypes.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentEmirateDTO> _accidentEmirates;

        public ObservableCollection<ClassLibrary.DTO.IncidentEmirateDTO> AccidentEmirates
        {
            get { return _accidentEmirates; }
            set { _accidentEmirates = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllEmirates()
        {
            var callTask = _accidentsClient.GetAllEmiratesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentEmirates(x));
        }

        private void Add_AccidentEmirates(ClassLibrary.DTO.IncidentEmirateDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentEmirates = new ObservableCollection<ClassLibrary.DTO.IncidentEmirateDTO>(data);
                AccidentEmirates.Insert(0, new ClassLibrary.DTO.IncidentEmirateDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentEmirateId = AccidentEmirates.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentLightingDTO> _accidentLightings;

        public ObservableCollection<ClassLibrary.DTO.IncidentLightingDTO> AccidentLightings
        {
            get { return _accidentLightings; }
            set { _accidentLightings = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllLightings()
        {
            var callTask = _accidentsClient.GetAllLightingAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentLightings(x));
        }

        private void Add_AccidentLightings(ClassLibrary.DTO.IncidentLightingDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentLightings = new ObservableCollection<ClassLibrary.DTO.IncidentLightingDTO>(data);
                AccidentLightings.Insert(0, new ClassLibrary.DTO.IncidentLightingDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentLightStatusId = AccidentLightings.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentLocationDTO> _accidentLocations;

        public ObservableCollection<ClassLibrary.DTO.IncidentLocationDTO> AccidentLocations
        {
            get { return _accidentLocations; }
            set { _accidentLocations = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllLocations()
        {
            var callTask = _accidentsClient.GetAllLocationsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentLocations(x));
        }

        private void Add_AccidentLocations(ClassLibrary.DTO.IncidentLocationDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentLocations = new ObservableCollection<ClassLibrary.DTO.IncidentLocationDTO>(data);
                AccidentLocations.Insert(0, new ClassLibrary.DTO.IncidentLocationDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentLocationId = AccidentLocations.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentLocationTypeDTO> _accidentLocationTypes;

        public ObservableCollection<ClassLibrary.DTO.IncidentLocationTypeDTO> AccidentLocationTypes
        {
            get { return _accidentLocationTypes; }
            set { _accidentLocationTypes = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllLocationTypes()
        {
            var callTask = _accidentsClient.GetAllLocationTypesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentLocationTypes(x));
        }

        private void Add_AccidentLocationTypes(ClassLibrary.DTO.IncidentLocationTypeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentLocationTypes = new ObservableCollection<ClassLibrary.DTO.IncidentLocationTypeDTO>(data);
                AccidentLocationTypes.Insert(0, new ClassLibrary.DTO.IncidentLocationTypeDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentLocationTypeId = AccidentLocationTypes.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentPConditionDTO> _accidentPConditions;

        public ObservableCollection<ClassLibrary.DTO.IncidentPConditionDTO> AccidentPConditions
        {
            get { return _accidentPConditions; }
            set { _accidentPConditions = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllPCondtions()
        {
            var callTask = _accidentsClient.GetAllPConditionAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentPCondtions(x));
        }


        private void Add_AccidentPCondtions(ClassLibrary.DTO.IncidentPConditionDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    AccidentPConditions = new ObservableCollection<ClassLibrary.DTO.IncidentPConditionDTO>(data);
                    AccidentPConditions.Insert(0, new ClassLibrary.DTO.IncidentPConditionDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                    AccidentPConditionId = AccidentPConditions.FirstOrDefault();
                });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentPoliceStationDTO> _accidentPoliceStations;

        public ObservableCollection<ClassLibrary.DTO.IncidentPoliceStationDTO> AccidentPoliceStations
        {
            get { return _accidentPoliceStations; }
            set { _accidentPoliceStations = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllPoliceStations()
        {
            var callTask = _accidentsClient.GetAllPoliceStationsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentPoliceStations(x));
        }

        private void Add_AccidentPoliceStations(ClassLibrary.DTO.IncidentPoliceStationDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentPoliceStations = new ObservableCollection<ClassLibrary.DTO.IncidentPoliceStationDTO>(data);
                AccidentPoliceStations.Insert(0, new ClassLibrary.DTO.IncidentPoliceStationDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentPoliceStationId = AccidentPoliceStations.FirstOrDefault();
            });
        }

        private ObservableCollection<ClassLibrary.DTO.IncidentReportTypesDTO> _accidentReportTypes;

        public ObservableCollection<ClassLibrary.DTO.IncidentReportTypesDTO> AccidentReportTypes
        {
            get { return _accidentReportTypes; }
            set { _accidentReportTypes = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllReportTypes()
        {
            var callTask = _accidentsClient.GetAllReportTypesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentReportTypes(x));
        }

        private void Add_AccidentReportTypes(ClassLibrary.DTO.IncidentReportTypesDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentReportTypes = new ObservableCollection<ClassLibrary.DTO.IncidentReportTypesDTO>(data);
                AccidentReportTypes.Insert(0, new ClassLibrary.DTO.IncidentReportTypesDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentReasonTypeId = AccidentReportTypes.FirstOrDefault();
            });
        }


        private ObservableCollection<ClassLibrary.DTO.IncidentRoadTypesDTO> _accidentRoadTypes;

        public ObservableCollection<ClassLibrary.DTO.IncidentRoadTypesDTO> AccidentRoadTypes
        {
            get { return _accidentRoadTypes; }
            set { _accidentRoadTypes = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllRoadTypes()
        {
            var callTask = _accidentsClient.GetAllRoadTypesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentRoadTypes(x));
        }

        private void Add_AccidentRoadTypes(ClassLibrary.DTO.IncidentRoadTypesDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentRoadTypes = new ObservableCollection<ClassLibrary.DTO.IncidentRoadTypesDTO>(data);
                AccidentRoadTypes.Insert(0, new ClassLibrary.DTO.IncidentRoadTypesDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentRoadStatusId = AccidentRoadTypes.FirstOrDefault();
            });
        }


        private ObservableCollection<ClassLibrary.DTO.IncidentSevertiesDTO> _accidentSeverties;

        public ObservableCollection<ClassLibrary.DTO.IncidentSevertiesDTO> AccidentSeverties
        {
            get { return _accidentSeverties; }
            set { _accidentSeverties = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllSeverties()
        {
            var callTask = _accidentsClient.GetAllSevertiesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentSeverties(x));
        }

        private void Add_AccidentSeverties(ClassLibrary.DTO.IncidentSevertiesDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentSeverties = new ObservableCollection<ClassLibrary.DTO.IncidentSevertiesDTO>(data);
                AccidentSeverties.Insert(0, new ClassLibrary.DTO.IncidentSevertiesDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentSeverityId = AccidentSeverties.FirstOrDefault();
            });
        }


        private ObservableCollection<ClassLibrary.DTO.IncidentWeatherDTO> _accidentWeathers;

        public ObservableCollection<ClassLibrary.DTO.IncidentWeatherDTO> AccidentWeathers
        {
            get { return _accidentWeathers; }
            set { _accidentWeathers = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void GetAllWeathers()
        {
            var callTask = _accidentsClient.GetAllWeatherAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentWeathers(x));
        }

        private void Add_AccidentWeathers(ClassLibrary.DTO.IncidentWeatherDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AccidentWeathers = new ObservableCollection<ClassLibrary.DTO.IncidentWeatherDTO>(data);
                AccidentWeathers.Insert(0, new ClassLibrary.DTO.IncidentWeatherDTO() { Id = 0, ArabicName = Properties.Resources.strSelect, EnglishName = Properties.Resources.strSelect });
                AccidentWeatherStatusId = AccidentWeathers.FirstOrDefault();
            });
        }

        public void ShowOnMapAccidents()
        {
            if (AccidentsSearchResults != null && AccidentsSearchResults.Count() > 0 && AccidentSearchResultsUI != null && AccidentSearchResultsUI.Count > 0 && AccidentSearchResultsUI.Any(a => a.ShowOnMap == true))
            {
                ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Incidents);
                Application.Current.Dispatcher.Invoke(() => graphicCol.Clear());

                foreach (var item in AccidentSearchResultsUI)
                {
                    if (item.AccidentDetailDto.Latitude.HasValue != false && item.AccidentDetailDto.Longitude.HasValue != false && item.ShowOnMap == true)
                        Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item.AccidentDetailDto, LayerTypeEnum.Incidents), item.AccidentDetailDto.Latitude.Value, item.AccidentDetailDto.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item.AccidentDetailDto))));
                }

                //DefaulIncidentFilterValues();
            }
        }

        public void ClearFilteredAccidents()
        {
            if (IncidentsListAll != null && IncidentsListAll.Count() > 0)
            {
                ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Incidents);
                Application.Current.Dispatcher.Invoke(() => graphicCol.Clear());

                foreach (var item in IncidentsListAll)
                {
                    if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                        Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item))));
                }

                //DefaulIncidentFilterValues();
            }
        }

        private void DefaulIncidentFilterValues()
        {
            AccidentNo = string.Empty;
            AccidentAddres = string.Empty;
            AccidentAreaId = AccidentAreas.FirstOrDefault();
            AccidentCityId = AccidentCities.FirstOrDefault();
            AccidentCrashTypeId = AccidentCrashTypes.FirstOrDefault();

            AccidentEmirateId = AccidentEmirates.FirstOrDefault();
            AccidentLightStatusId = AccidentLightings.FirstOrDefault();
            AccidentLocationId = AccidentLocations.FirstOrDefault();
            AccidentLocationTypeId = AccidentLocationTypes.FirstOrDefault();
            AccidentPoliceStationId = AccidentPoliceStations.FirstOrDefault();
            AccidentRoadStatusId = null;
            AccidentReasonTypeId = AccidentReportTypes.FirstOrDefault();
            AccidentSeverityId = AccidentSeverties.FirstOrDefault();
            AccidentWeatherStatusId = AccidentWeathers.FirstOrDefault();
            AccidentSpeed = null;
            AccidentNoOfDeaths = null;
            AccidentNoOfEasyInjuries = null;
            AccidentNoOfFatalInjuries = null;
            AccidentNoOfMedInjuries = null;

            AccidentStartTime = null;
            AccidentEndTime = null;

            AccidentSearchStatus = string.Empty;
            //AccidentSearchResultsUI.Clear();

        }

        private void ClearAccidentsCriteria()
        {
            DefaulIncidentFilterValues();
        }

        public void SearchAccidents()
        {
            int? nullInt = null;
            ClassLibrary.DTO.AccidentSearchRequestDTO accidentSearchReq = new ClassLibrary.DTO.AccidentSearchRequestDTO();

            accidentSearchReq.AccidentNumber = AccidentNo;
            accidentSearchReq.Address = AccidentAddres;

            accidentSearchReq.AccidentTypeId = AccidentTypeId;
            accidentSearchReq.DistrictId = AccidentDistrictId;
            accidentSearchReq.RoadWaterStatusId = AccidentRoadWaterStatusId;


            accidentSearchReq.AreaId = (AccidentAreaId == null || (AccidentAreaId != null && AccidentAreaId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentAreaId.Id;
            accidentSearchReq.CityId = (AccidentCityId == null || (AccidentCityId != null && AccidentCityId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentCityId.Id;
            accidentSearchReq.CrashTypeId = (AccidentCrashTypeId == null || (AccidentCrashTypeId != null && AccidentCrashTypeId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentCrashTypeId.Id;

            accidentSearchReq.EmirateId = (AccidentEmirateId == null || (AccidentEmirateId != null && AccidentEmirateId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentEmirateId.Id;
            accidentSearchReq.EndTime = AccidentEndTime;
            accidentSearchReq.LightStatusId = (AccidentLightStatusId == null || (AccidentLightStatusId != null && AccidentLightStatusId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentLightStatusId.Id;
            accidentSearchReq.LocationId = (AccidentLocationId == null || (AccidentLocationId != null && AccidentLocationId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentLocationId.Id;
            accidentSearchReq.LocationTypeId = (AccidentLocationTypeId == null || (AccidentLocationTypeId != null && AccidentLocationTypeId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentLocationTypeId.Id;
            accidentSearchReq.PoliceStationId = (AccidentPoliceStationId == null || (AccidentPoliceStationId != null && AccidentPoliceStationId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentPoliceStationId.Id;
            accidentSearchReq.RoadStatusId = (AccidentRoadStatusId == null || (AccidentRoadStatusId != null && AccidentRoadStatusId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentRoadStatusId.Id;
            accidentSearchReq.ReasonTypeId = (AccidentReasonTypeId == null || (AccidentReasonTypeId != null && AccidentReasonTypeId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentReasonTypeId.Id;

            accidentSearchReq.SeverityId = (AccidentSeverityId == null || (AccidentSeverityId != null && AccidentSeverityId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentSeverityId.Id;
            accidentSearchReq.WeatherStatusId = (AccidentWeatherStatusId == null || (AccidentWeatherStatusId != null && AccidentWeatherStatusId.EnglishName == Properties.Resources.strSelect)) ? nullInt : AccidentWeatherStatusId.Id;
            accidentSearchReq.Speed = AccidentSpeed;
            accidentSearchReq.StartTime = AccidentStartTime;

            accidentSearchReq.NumOfDeaths = AccidentNoOfDeaths;

            accidentSearchReq.NumOfEasyInjuries = AccidentNoOfEasyInjuries;
            accidentSearchReq.NumOfFatalInjuries = AccidentNoOfFatalInjuries;
            accidentSearchReq.NumOfMedInjuries = AccidentNoOfMedInjuries;


            var callTask = _accidentsClient.SearchAccidentsAsync(accidentSearchReq);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentSearchDetails(x));
        }


        private void Add_AccidentSearchDetails(ClassLibrary.DTO.IncidentsDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckUncheckAllisEnabled = false;
                CheckUnCheckAllContent = string.Empty;
                AccidentSearchStatus = string.Empty;
                EnableShowOnMap = false;

                AccidentsSearchResults = data;

                AccidentSearchResultsUI = new ObservableCollection<IncidentDetailsDtoUI>();

                if (data == null || data.Count() == 0) AccidentSearchStatus = "No Results Found for this Search Criteria.";

                if (data == null)
                {
                    return;
                }
                CheckUncheckAll = true;
                foreach (var item in data)
                {
                    AccidentSearchResultsUI.Add(new IncidentDetailsDtoUI() { AccidentDetailDto = item, ShowOnMap = CheckUncheckAll });
                }
                EnableShowOnMap = true;
                CheckUncheckAllisEnabled = true;
                CheckUnCheckAllContent = (CheckUncheckAll == true) ? Properties.Resources.strSelectAll : Properties.Resources.strUnSelectAll;
            });


        }


        private void ShowOnMapChecked(object id)
        {
            if (!isBulkOperationRunning)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    isSingleTask = true;
                    EnableShowOnMap = true;
                    CheckUncheckAll = !AccidentSearchResultsUI.Any(a => a.ShowOnMap == false);
                    isSingleTask = false;
                });
            }
        }

        private void ShowOnMapUnChecked(object id)
        {
            if (!isBulkOperationRunning)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    isSingleTask = true;
                    EnableShowOnMap = AccidentSearchResultsUI.Any(a => a.ShowOnMap == true);
                    CheckUncheckAll = !AccidentSearchResultsUI.Any(a => a.ShowOnMap == false);
                    isSingleTask = false;
                });
            }
        }

        #endregion

        #region MarkersDetailsToolTip

        public void GetIncidentDetails(int incidentID)
        {
            var callTask = _client.GetIncidentDetailsAsync(incidentID);

            var obs = callTask.ToObservable();
            obs.Subscribe((x) => AddIncidentDetails(x));
        }

        private void AddIncidentDetails(IncidentsDTO data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                IncidentDetails = data;
                if (OpenIncidentEvent != null)
                    OpenIncidentEvent();
            });
        }




        public void GetVehicleDetails(string plateNumber)
        {
            var callTask = _client.GetVehicleDetailsByPlateNumberAsync(plateNumber);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehicleDetails(x));
        }
        private void Add_VehicleDetails(VehicleLiveTrackingDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => VehicleDetail = data);
        }

        public void GetAssetViolationDetails(string ID)
        {
            GetAssetViolationCountDetails(ID);

            var callTask = _client.GetAssetViolationsAsync(ID, Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetViolationsDetails(x));

        }



        private void Add_AssetViolationsDetails(AssetViolationDetailsDTO data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                AssetViolationsDetails = data;
                if (OpenAssetEvent != null)
                    OpenAssetEvent();
            });
        }

        public void GetAssetViolationCountDetails(string ID)
        {
            var callTask = _violationClient.GetViolationCountsByAssetCodeAsync(ID).ToObservable();

            callTask.Subscribe(x => Add_AssetViolationsCountDetails(x));
        }

        private void Add_AssetViolationsCountDetails(STC.Projects.ClassLibrary.DTO.AssetViolationCountDTO data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                AssetViolationCountDetails = data;
            });
        }

        public void GetVehicleViolationDetails(string plateNumber)
        {
            var callTask = _client.GetViolationsHistorySearchByDateAsync(DateTime.Now.AddYears(-1), DateTime.Now, plateNumber);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehicleViolationsDetails(x));
        }

        private void Add_VehicleViolationsDetails(ViolationNotificationDTO[] data)
        {
            if (data == null)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehicleViolationsDetails = data;

                FirstItemOfvehicleViolationsDetails = VehicleViolationsDetails.FirstOrDefault();
            });
        }

        public void GetIncidentFullDetails(int selectedIncidentId)
        {
            var callTask = _client.GetIncidentFullDetailsAsync(selectedIncidentId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_IncidentDetails(x));
        }
        private void Add_IncidentDetails(IncidentDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => IncidentsToolTipDetailsOnGraphic = data);
        }
        public void GetViolationDetails(int selectedLocationCode)
        {
            int? violationTypeId = null;
            if (SelectedViolationType != null)
                if (((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId == 0)
                    violationTypeId = null;

            var callTask = _client.GetViolationDetailsByAssetAsync(selectedLocationCode.ToString(), (DateTime?)SelectedViolationDateFilter, violationTypeId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationDetails(x));
        }
        private void Add_ViolationDetails(ViolationDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => AddViolationTotalsbyStatus(data.TotalsByStatus));
            Application.Current.Dispatcher.Invoke(() => AddViolationTotalsbyType(data.TotalsByTypes));
            Application.Current.Dispatcher.Invoke(() => AssetDetailsForViolation = data.AssetsDetails);
        }

        private void AddViolationTotalsbyType(TotalViolationValuesByTypes[] totalViolationValuesByTypes)
        {
            ViolationsByTypeData.Clear();
            this.RaiseNotifyPropertyChanged("ViolationsByTypeData");
            if (totalViolationValuesByTypes == null)
                return;
            for (int i = 0; i < totalViolationValuesByTypes.Length; i++)
            {
                ViolationsByTypeData.Add(new NameValueModel() { Name = totalViolationValuesByTypes[i].VioltionTypeName, Value = totalViolationValuesByTypes[i].TotalCountOfViolations });
            }
        }

        private void AddViolationTotalsbyStatus(TotalViolationValuesByStatus[] totalViolationValuesByStatus)
        {
            ViolationsByStatusData.Clear();
            this.RaiseNotifyPropertyChanged("ViolationsByStatusData");
            if (totalViolationValuesByStatus == null)
                return;
            for (int i = 0; i < totalViolationValuesByStatus.Length; i++)
            {
                ViolationsByStatusData.Add(new NameValueModel() { Name = totalViolationValuesByStatus[i].VioltionStatusName, Value = totalViolationValuesByStatus[i].TotalCountOfViolations });
            }
        }

        public void GetPatrolDetails(int patrolId)
        {
            var callTask = _client.GetPatrolDetailsAsync(patrolId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolDetails(x));
        }

        private void Add_PatrolDetails(ServiceLayerReference.PatrolOfficersDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    PatrolDetails = data;
                    if (OpenPatrolEvent != null)
                        OpenPatrolEvent();
                });
        }

        private STC.Projects.ClassLibrary.DTO.SmartOfficerDTO _smartOfficer;

        public STC.Projects.ClassLibrary.DTO.SmartOfficerDTO SmartOfficer
        {
            get { return _smartOfficer; }
            set
            {
                _smartOfficer = value; this.RaiseNotifyPropertyChanged();
            }
        }

        public void GetPatrolOfficerDetails(string patrolOfficerMilitaryId)
        {
            var callTask = _smartOfficerClient.GetOfficerAsync(patrolOfficerMilitaryId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolOfficerDetails(x));
        }

        private void Add_PatrolOfficerDetails(STC.Projects.ClassLibrary.DTO.SmartOfficerDTO data)
        {
            if (data == null) return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                SmartOfficer = data;
                if (OpenPatrolOfficerEvent != null)
                    OpenPatrolOfficerEvent();
            });
        }

        public void GetOfficerTipDetails(int officerId)
        {
            if (Officers != null && Officers.Any(x => x.OfficerId == officerId))
                Officer = Officers.FirstOrDefault(x => x.OfficerId == officerId);
        }

        #endregion

        #region Fill Dropdowns
        private void GetPatrolStatusList()
        {
            var callTask = _client.GetPatrolStatusListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolStatusList(x == null ? null : x.ToList()));
        }
        private void Add_PatrolStatusList(List<PatrolStatusDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new PatrolStatusDimDTO() { PatrolStatus = "All", PatrolStatusId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => PatrolStatusList.Add(item));
        }
        private void GetAllUsersList()
        {
            var callTask = _client.GetAllUsersListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_UsersList(x == null ? null : x.ToList()));
        }
        private void Add_UsersList(List<UsersDTO> list)
        {
            if (list == null)
                return;
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AllUsers.Add(item));
        }
        private void GetAssetTypesList()
        {
            var callTask = _client.GetAssetTypesListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetTypesList(x == null ? null : x.ToList()));
        }
        private void Add_AssetTypesList(List<AssetTypeDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new AssetTypeDimDTO() { AssetType = "All", AssetTypeId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AssetTypesList.Add(item));
        }
        private void GetAssetStatusDimDTOList()
        {
            var callTask = _client.GetAssetStatusListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetStatusDimDTOList(x == null ? null : x.ToList()));
        }
        private void Add_AssetStatusDimDTOList(List<AssetStatusDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new AssetStatusDimDTO() { AssetStatus = "All", AssetStatusId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AssetStatusList.Add(item));
        }
        private void GetViolationTypeDimDTOsList()
        {
            var callTask = _client.GetViolationTypesListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationTypeDimDTOsList(x == null ? null : x.ToList()));
        }
        private void Add_ViolationTypeDimDTOsList(List<ViolationTypeDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new ViolationTypeDimDTO() { ViolationType = "All", ViolationTypeId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => ViolationTypesList.Add(item));
        }

        #endregion

        #region Filters
        private void FilterAssetsOnMap(object parameter)
        {
            int? assetStatus = null;
            int? assetType = null;

            if (SelectedAssetStatus != null && ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId != 0)
                assetStatus = ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId;
            if (SelectedAssetType != null && ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId != 0)
                assetType = ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Assets).Clear());
            GetAssetLastStatusData(assetStatus, assetType, LayerTypeEnum.Assets);
        }
        private void ShowVideo(object violationNotificationDTO)
        {
            var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;
            string videopath = _client.GetViolationVideoURLById(violationNotification.ViolationNotificationId);
            ImagePopup imagePopup = new ImagePopup();
            (imagePopup.DataContext as ImagePopupViewModel).SourceURL = "Video";
            //(imagePopup.DataContext as ImagePopupViewModel).VideoURL = "http://hubblesource.stsci.edu/sources/video/clips/details/images/hst_1.mpg";
            if (videopath != null)
            {
                (imagePopup.DataContext as ImagePopupViewModel).VideoURL = videopath;
                (imagePopup.DataContext as ImagePopupViewModel).ShowStream();
                imagePopup.Show();
            }
        }
        private void ShowImage(object violationNotificationDTO)
        {
            var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;
            ImagePopup imagePopup = new ImagePopup();
            (imagePopup.DataContext as ImagePopupViewModel).SourceURL = "Image";
            //(imagePopup.DataContext as ImagePopupViewModel).ImageURLList = new List<string>() { "https://upload.wikimedia.org/wikipedia/commons/c/c0/Salik's_Al_Garhoud_Bridge_Toll_Gate.jpg", "https://upload.wikimedia.org/wikipedia/commons/c/c0/Salik's_Al_Garhoud_Bridge_Toll_Gate.jpg" };
            //List<string> imagepathList = _client.GetViolationImageURLsById(violationNotification.ViolationNotificationId).ToList();
            List<Byte[]> imagesBytesList = _client.GetViolationImagesById(violationNotification.ViolationNotificationId).ToList();

            if (imagesBytesList != null && imagesBytesList.Count > 0)
            {
                (imagePopup.DataContext as ImagePopupViewModel).ImagesBytesList = imagesBytesList;
                (imagePopup.DataContext as ImagePopupViewModel).ShowStream();
                imagePopup.Show();
            }
        }
        private void FilterViolationsOnMap(object parameter)
        {
            int? violationType = null;
            if (SelectedViolationType != null && ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId != 0)
                violationType = ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Violations).Clear());
            GetViolationsData(SelectedViolationDateFilter == null ? (DateTime?)null : (DateTime?)SelectedViolationDateFilter, violationType);
        }
        private void FilterPatrolsOnMap(object parameter)
        {
            int? patrolStatus = null;
            if (SelectedPatrolStatus != null && ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId != 0)
                patrolStatus = ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Patrols).Clear());
            GetPatrolLastLocationDTOData(patrolStatus);
        }
        #endregion

        #region HelperFunctions
        public ObservableCollection<Graphic> GetLayerObservable(LayerTypeEnum layerType)
        {
            ObservableCollection<Graphic> graphicCol = null;
            if (LayersGraphicsDictionary.ContainsKey(layerType.ToString()))
                graphicCol = LayersGraphicsDictionary[layerType.ToString()];
            else
            {
                graphicCol = new ObservableCollection<Graphic>();
                LayersGraphicsDictionary.Add(layerType.ToString(), graphicCol);
            }
            return graphicCol;
        }
        private Graphic CreateGraphic(IDictionary<string, object> attributes, double latitude, double longitude, string iconPath, int? countSymbol = 0)
        {
            var geometry = new MapPoint(longitude, latitude, new SpatialReference(4326));
            return CreateGraphicByGeometry(attributes, geometry, iconPath, countSymbol);
        }

        private void AddGraphicToLayer(Dictionary<string, object> attributes, double latitude, double longitude, string iconPath, LayerTypeEnum layer, int? countSymbol = 0, bool isSameMessage = false)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var grp = CreateGraphic(attributes, latitude, longitude, iconPath, countSymbol);

                    if (grp != null)
                    {
                        var layerCol = GetLayerObservable(layer);
                        if (layer == LayerTypeEnum.Notifications)
                        {
                            if (!isSameMessage && !string.IsNullOrEmpty(_CurrentMessageId))
                            {
                                _tempNotificationsGraphic.Add(grp);
                                return;
                            }
                        }
                        //if (layer == LayerTypeEnum.Notifications)
                        //{
                        //    var nearGraph = SearchNearGraphic(new MapPoint(longitude, latitude, new SpatialReference(4326)), layer, grp.Attributes["Id"].ToString(), grp.Attributes);
                        //    if (nearGraph != null)
                        //    {
                        //        grp.IsVisible = !nearGraph.IsVisible;
                        //        grp.Attributes.Add("ParentId", nearGraph.Attributes["Id"]);
                        //        layerCol.Add(nearGraph);
                        //    }
                        //}
                        layerCol.Add(grp);
                    }
                });
        }

        private Graphic SearchNearGraphic(MapPoint point, LayerTypeEnum layer, string ItemId, IDictionary<string, object> attributes)
        {
            var layerCol = GetLayerObservable(layer);
            if (layerCol != null)
            {
                foreach (var item in layerCol.Where(x => x.Attributes.ContainsKey("ChildGraphicsId") && x.Attributes["ChildGraphicsId"] as List<string> != null).ToList())
                {
                    var distance = Utility.CalculateDistance(point.Y, point.X, (double)item.Attributes["Latitude"], (double)item.Attributes["Longitude"], 'K');
                    if (distance < 0.1)
                    {
                        var list = item.Attributes["ChildGraphicsId"] as List<string>;
                        list.Add(ItemId);
                        item.Attributes["ChildGraphicsId"] = list;
                        return item;
                    }
                }
                foreach (var item in layerCol.Where(x => !x.Attributes.ContainsKey("ChildGraphicsId") && x.Attributes["Latitude"] != null && x.Attributes["Longitude"] != null).ToList())
                {
                    var lat = 0.00;
                    var lon = 0.00;
                    double.TryParse(item.Attributes["Latitude"].ToString(), out lat);
                    double.TryParse(item.Attributes["Longitude"].ToString(), out lon);
                    var distance = Utility.CalculateDistance(point.Y, point.X, lat, lon, 'K');
                    if (distance < 0.1)
                    {
                        var grph = CreateGraphic(attributes, (double)item.Attributes["Latitude"], (double)item.Attributes["Longitude"], GetMarkerImageUrl(MarkerType.Cluster, item), null);
                        if (grph != null)
                        {
                            var childIds = new List<string>();
                            childIds.Add(ItemId);
                            childIds.Add(item.Attributes["Id"].ToString());
                            grph.Attributes.Add("ChildGraphicsId", childIds);
                            grph.Attributes["Id"] = "Parent_" + ItemId;
                            var messageIdsList = new List<string>();

                            if (item.Attributes.ContainsKey("MessageId"))
                                messageIdsList.Add(item.Attributes["MessageId"].ToString());
                            if (attributes.ContainsKey("MessageId"))
                                messageIdsList.Add(attributes["MessageId"].ToString());

                            grph.Attributes.Add("MessagesIds", messageIdsList);
                            item.IsVisible = false;
                            item.Attributes.Add("ParentId", grph.Attributes["Id"]);
                            //layerCol.Add(grph);
                            return grph;
                        }
                    }
                }
            }
            return null;
        }
        private Graphic CreateGraphicByGeometry(IDictionary<string, object> attributes, Esri.ArcGISRuntime.Geometry.Geometry geometry, string iconPath, int? countSymbol = 0, bool IsLine = false)
        {
            CompositeSymbol compositeSymbol = new CompositeSymbol();
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();
            TextSymbol textSymbol = new TextSymbol();



            if (countSymbol.HasValue && countSymbol != 0)
            {
                textSymbol.Text = countSymbol.ToString();
                textSymbol.HorizontalTextAlignment = HorizontalTextAlignment.Center;
                textSymbol.VerticalTextAlignment = VerticalTextAlignment.Middle;
                textSymbol.Font.FontSize = 14;
                textSymbol.YOffset = 45;
                textSymbol.Color = (Color)ColorConverter.ConvertFromString("#d91e18");

                pictureMarkerSymbol.YOffset = 40;
            }

            if (!string.IsNullOrEmpty(iconPath))
            {
                pictureMarkerSymbol.SetSourceAsync(new Uri(iconPath));
                compositeSymbol.Symbols.Add(pictureMarkerSymbol);
            }

            if (IsLine)
            {
                var lineSymbol = new SimpleLineSymbol();
                lineSymbol.Color = (Color)Color.FromRgb(255, 0, 0);
                lineSymbol.Style = SimpleLineStyle.Solid;
                lineSymbol.Width = 5;
                compositeSymbol.Symbols.Add(lineSymbol);
            }
            if (countSymbol.HasValue)
                compositeSymbol.Symbols.Add(textSymbol);

            var graphics = new Graphic(geometry, compositeSymbol);
            foreach (KeyValuePair<string, object> item in attributes)
                graphics.Attributes.Add(item.Key, item.Value);


            //bool isWantedCar = attributes.Any(x => x.Key == "Id" && x.Value.ToString().Contains("_WantedCarCenter"));
            //if (isWantedCar)
            //{
            //    pictureMarkerSymbol = new PictureMarkerSymbol();
            //    pictureMarkerSymbol.XOffset = 0;
            //    pictureMarkerSymbol.YOffset = 30;
            //    pictureMarkerSymbol.Angle = 0;
            //    pictureMarkerSymbol.Opacity = 0.5;

            //    iconPath = GetMarkerImageUrl(MarkerType.CarPlateNumber, null);
            //    pictureMarkerSymbol.SetSourceAsync(new Uri(iconPath));
            //    compositeSymbol.Symbols.Add(pictureMarkerSymbol);

            //    textSymbol = new TextSymbol();
            //    textSymbol.Text = attributes.FirstOrDefault(x => x.Key == "Id" && x.Value.ToString().Contains("_WantedCarCenter")).Value.ToString().Replace("_WantedCarCenter", "");
            //    textSymbol.HorizontalTextAlignment = HorizontalTextAlignment.Center;
            //    textSymbol.VerticalTextAlignment = VerticalTextAlignment.Middle;
            //    textSymbol.Font.FontSize = 12;
            //    textSymbol.XOffset = 0;
            //    textSymbol.YOffset = 60;
            //    textSymbol.Angle = 0;
            //    textSymbol.Color = (Color)ColorConverter.ConvertFromString("#59d2a8");
            //    compositeSymbol.Symbols.Add(textSymbol);
            //}

            graphics.ZIndex = MARKER_Z_INDEX;

            return graphics;
        }
        private Dictionary<string, object> CreateGraphicDictionary(dynamic item, LayerTypeEnum layerType, int? violationCount = 0, LayerTypeEnum? targetNotificatoinLayerType = null)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            switch (layerType)
            {
                case LayerTypeEnum.Violations:
                    attributes.Add("Id", item.LocationCode);
                    attributes.Add("Count", violationCount);

                    if (item is ViolationNotificationDTO)
                        attributes.Add("ViolationObj", item);
                    else if (item is ViolationToDraw)
                        attributes.Add("ViolationObj", item.ViolationObj);
                    break;
                case LayerTypeEnum.Incidents:
                    attributes.Add("Id", item.IncidentId);
                    attributes.Add("Name", item.IncidentTypeName);
                    attributes.Add("Address", item.IncidentAddress);

                    if (item is IncidentsDTO)
                        attributes.Add("IncidentObj", item);
                    else if (item is IncidentToDraw)
                        attributes.Add("IncidentObj", item.IncidentObj);
                    break;
                case LayerTypeEnum.Patrols:
                    attributes.Add("Id", item.PatrolId);
                    attributes.Add("Speed", item.Speed);
                    attributes.Add("Date", item.LocationDate);
                    attributes.Add("Name", item.PatrolCode);
                    attributes.Add("GUID", item.PatrolOriginalId);
                    attributes.Add("PatrolPlateNo", item.PatrolPlateNo);
                    attributes.Add("StatusName", item.StatusName);
                    attributes.Add("ETATime", item.ETATime);
                    attributes.Add("IsPatrol", item.isPatrol);
                    attributes.Add("PatrolCode", item.PatrolCode);
                    break;
                case LayerTypeEnum.Officers:
                    attributes.Add("Id", item.OfficerId);
                    attributes.Add("Speed", item.Speed);
                    attributes.Add("Date", item.LocationDate);
                    attributes.Add("Name", item.OfficerName);
                    break;
                case LayerTypeEnum.Assets:
                case LayerTypeEnum.AssetsRedLights:
                case LayerTypeEnum.AssetsSpeed:
                case LayerTypeEnum.AssetsSmartTowers:
                    attributes.Add("Id", item.OriginalIdent);
                    attributes.Add("Name", item.ItemName);
                    attributes.Add("Status", item.ItemStatusName);
                    attributes.Add("LocationCode", item.LocationCode);
                    attributes.Add("AssetsType", item.ItemCategoryName);

                    if (item is AssetsViewDTO)
                    {                        
                        attributes.Add("AssetStatusId", item.ItemStatusId);
                    }
                    break;
                case LayerTypeEnum.Notifications:
                    if (item is AssetsViewDTO)
                    {
                        attributes.Add("Id", item.ItemId);
                        attributes.Add("LocationCode", item.LocationCode);
                        attributes.Add("AssetStatusId", item.ItemStatusId);
                    }
                    else if (item is FogEventMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_FogCenter");
                    }
                    else if (item is WantedCarMessage)
                    {
                        attributes.Add("Id", item.VehiclePlateNumber + "_WantedCarCenter_" + item.MessageId);
                        attributes.Add("PlateNumber", item.VehiclePlateNumber.ToString());
                        attributes.Add("CreatedDate", item.CreatedDate.ToString());
                        //var businesRuleDto = _client.GetMessageBusinessRule(item.MessageId);
                        //if (businesRuleDto != null)
                        attributes.Add("ViolatedBusinessRule", (string.IsNullOrEmpty(item.BusinessRuleName) ? "NA" : item.BusinessRuleName));
                    }
                    else if (item is DetectedAccidentMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_AccidentCenter");
                        attributes.Add("DetectedEventMessageObj", item);
                    }
                    else if (item is ServiceLayerReference.PatrolLastLocationDTO || item is STC.Projects.ClassLibrary.DTO.PatrolLastLocationDTO)
                    {
                        attributes.Add("Id", item.PatrolId + "_PatrolCenter");
                        attributes.Add("Speed", item.Speed);
                        attributes.Add("Date", item.LocationDate);
                        attributes.Add("Name", item.PatrolCode);
                        attributes.Add("GUID", item.PatrolOriginalId);
                        attributes.Add("PatrolPlateNo", item.PatrolPlateNo);
                        attributes.Add("StatusName", item.StatusName);
                        attributes.Add("ETATime", item.ETATime);
                        attributes.Add("TempNotificationPatrol", "true");
                    }
                    else if (item is TruckViolationMessage)
                    {
                        attributes.Add("Id", item.TruckPlateNumber.ToString() + "_TruckViolationCenter_" + item.MessageId);
                        attributes.Add("PlateNumber", item.TruckPlateNumber.ToString());
                    }
                    else
                    {
                        attributes.Add("Id", item.Id);
                    }

                    attributes.Add("LayerType", targetNotificatoinLayerType.Value.ToString());
                    try
                    {
                        attributes.Add("MessageId", item.MessageId);
                        attributes.Add("Latitude", item.Latitude);
                        attributes.Add("Longitude", item.Longitude);

                    }
                    catch (Exception ex)
                    {

                    }

                    if (item is ViolationToDraw)
                        attributes.Add("ViolationObj", item.ViolationObj);
                    else if (item is IncidentToDraw)
                        attributes.Add("IncidentObj", item.IncidentObj);
                    else if (item is FogEventMessage)
                        attributes.Add("FogEventMessageObj", item);
                    else if (item is WantedCarMessage)
                        attributes.Add("WantedCarMessageObj", item);
                    break;
            }
            return attributes;
        }
        public void DrawCircleOld(string id, MapPoint point, double radius, SimpleFillSymbol circleFillSymbol, int ZIndex, LayerTypeEnum layerType, Color color)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var bufferGraphic = layerCol.Where(x => x.Attributes.Count > 0 && x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (id + "_Circle_" + ZIndex).ToString()).FirstOrDefault();
            if (bufferGraphic != null)
                layerCol.Remove(bufferGraphic);

            var buffer = GeometryEngine.Buffer(point, radius * 0.0175);
            bufferGraphic = new Graphic { Geometry = buffer, Symbol = circleFillSymbol, ZIndex = ZIndex };

            //bufferGraphic = DrawCircle(point, radius * 0.0175, color);
            bufferGraphic.Attributes.Add("Id", id + "_Circle_" + ZIndex);
            bufferGraphic.Attributes.Add("LayerType", layerType.ToString());
            bufferGraphic.Attributes.Add("MessageId", _CurrentMessageId);
            bufferGraphic.Attributes.Add("CircleType", true);
            layerCol.Add(bufferGraphic);
        }

        public void DrawCircle(string id, MapPoint point, double radius, SimpleFillSymbol circleFillSymbol, int ZIndex, LayerTypeEnum layerType, Color color)
        {
            try
            {
                var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
                var bufferGraphics = layerCol.Where(x => x.Attributes.Count > 0 && x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (id + "_Circle_" + ZIndex).ToString());
                if (bufferGraphics != null)
                {
                    foreach (var item in bufferGraphics.ToList())
                    {
                        layerCol.Remove(item);
                    }
                }

                //var buffer = GeometryEngine.Buffer(point, radius * 0.0175);
                try
                {
                    var client = new GISReference.GisServiceClient();
                    var driveTimePolygons = client.GetDriveTimePolygons(new STC.Projects.ClassLibrary.DTO.MapPointDTO() { Latitude = point.Y, Longitude = point.X });
                    if (driveTimePolygons != null && driveTimePolygons.Any())
                    {
                        foreach (var item in driveTimePolygons)
                        {
                            var polygon = Polygon.FromJson(item.polygonObject);
                            var symbol = FillSymbol.FromJson(item.symbolObject);
                            var bufferGraphic = new Graphic()
                            {
                                Geometry = polygon,
                                Symbol = symbol
                            };

                            //bufferGraphic = DrawCircle(point, radius * 0.0175, color);
                            bufferGraphic.Attributes.Add("Id", id + "_Circle_" + ZIndex);
                            bufferGraphic.Attributes.Add("LayerType", layerType.ToString());
                            bufferGraphic.Attributes.Add("MessageId", _CurrentMessageId);
                            bufferGraphic.Attributes.Add("CircleType", true);
                            layerCol.Add(bufferGraphic);
                        }
                    }
                    else
                    {
                        DrawCircleOld(id, point, radius, circleFillSymbol, ZIndex, layerType, color);
                    }
                }
                catch (Exception ex)
                {
                    DrawCircleOld(id, point, radius, circleFillSymbol, ZIndex, layerType, color);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Graphic DrawCircle(MapPoint mapPoint, double radius, Color color)//GraphicsLayer myGraphicLayer, 
        {
            //myGraphicLayer.Graphics.Clear();
            //symbole that make a outline polygon circle. 
            SimpleLineSymbol symbolLine = new SimpleLineSymbol();
            symbolLine.Width = 1;
            symbolLine.Color = color;
            //symbole that make a polygon properties.
            SimpleFillSymbol myFillSymbol = new SimpleFillSymbol();
            myFillSymbol.Color = color;
            myFillSymbol.Outline = symbolLine;
            myFillSymbol.Style = SimpleFillStyle.Vertical;

            List<MapPoint> pointCollection = new List<MapPoint>();
            // point that make up the circle
            //MapPoint pp = new MapPoint(55.00206, 24.98167);
            var PointCount = 100; // number of points on the circle
            var angle = 360 / PointCount; // used to compute points on the circle

            for (var i = 1; i <= PointCount; i++)
            {
                // convert angle to raidans
                // var radians = i * angle * Math.PI / 180;
                double radians = 2 * Math.PI / PointCount * i;
                // add point to the circle
                double x = (mapPoint.X + radius * Math.Cos(radians));
                double y = (mapPoint.Y + radius * Math.Sin(radians));
                pointCollection.Add(new MapPoint(x, y));
            }

            Polygon myPolygon = new Polygon(pointCollection, SpatialReferences.Wgs84);
            Graphic myGraphic = new Graphic();
            myGraphic.Geometry = myPolygon;
            myGraphic.Symbol = myFillSymbol;
            //myGraphicLayer.Graphics.Add(myGraphic);

            return myGraphic;
        }

        private string GetMarkerImageUrl(MarkerType MarkerType, object MarkerObject)
        {
            //@"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" 
            switch (MarkerType)
            {
                case MarkerType.Officers:
                    {
                        #region Officers
                        var officerLocation = MarkerObject as OfficersLastLocationViewDTO;

                        if (officerLocation != null)
                        {
                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.SmrtOfficerMarker;

                        }
                        #endregion
                    }
                    break;
                case MarkerType.SuggestedPatrol:
                    {
                        #region Officers
                        var officerLocation = MarkerObject as OfficersLastLocationViewDTO;

                        if (officerLocation != null)
                        {
                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.TrackingPin;

                        }
                        #endregion
                    }
                    break;
                case MarkerType.Patrols:
                    {
                        #region Patrols

                        var patrolLocation = MarkerObject as ServiceLayerReference.PatrolLastLocationDTO;

                        if (patrolLocation != null)
                        {
                            if (patrolLocation.isPatrol == false)
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.SmrtOfficerMarker;

                            if (patrolLocation.IsRecommended)
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolRecommendedMarker;
                            else if (patrolLocation.IsBusy)
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolBusyMarker;

                            var patrolStatus = patrolLocation.StatusId.HasValue ? (PatrolStatusEnum)patrolLocation.StatusId.Value : PatrolStatusEnum.Available;

                            switch (patrolStatus)
                            {
                                case PatrolStatusEnum.Acknowledged:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolAcknowledgedMarker;

                                case PatrolStatusEnum.ArrivedToLocation:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolArrivedMarker;

                                case PatrolStatusEnum.AssignedToEvent:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolAssignedMarker;

                                case PatrolStatusEnum.Available:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolMarker;

                                case PatrolStatusEnum.Dispatched:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolDispatchedMarker;

                                case PatrolStatusEnum.OnTheWay:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolOnTheWayMarker;

                                case PatrolStatusEnum.UnderMaintenance:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolUnderMaintenanceMarker;

                            }

                        }

                        #endregion
                    }
                    break;
                case MarkerType.RecommendedPatrol:
                    {
                        #region Patrols

                        var patrolLocation = MarkerObject as ServiceLayerReference.PatrolLastLocationDTO;

                        if (patrolLocation != null)
                        {

                            var patrolStatus = patrolLocation.StatusId.HasValue ? (PatrolStatusEnum)patrolLocation.StatusId.Value : PatrolStatusEnum.Available;

                            switch (patrolStatus)
                            {
                                case PatrolStatusEnum.Acknowledged:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolAcknowledgedMarker;

                                case PatrolStatusEnum.ArrivedToLocation:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolArrivedMarker;

                                case PatrolStatusEnum.AssignedToEvent:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolAssignedMarker;

                                case PatrolStatusEnum.Available:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolAvailableMarker;

                                case PatrolStatusEnum.Dispatched:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolDispatchedMarker;

                                case PatrolStatusEnum.OnTheWay:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolOnTheWayMarker;

                                case PatrolStatusEnum.UnderMaintenance:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.RecommendedPatrolUnderMaintenanceMarker;

                            }

                        }

                        #endregion
                    }
                    break;
                case MarkerType.Assets:
                    {
                        #region Assets

                        var assetLastStatus = MarkerObject as AssetLastStatusDTO;

                        if (assetLastStatus != null)
                        {

                            var assetStatus = (AssetStatusEnum)assetLastStatus.AssetStatusId;

                            var assetType = (AssetTypesEnum)assetLastStatus.AssetTypeId;

                            switch (assetType)
                            {
                                case AssetTypesEnum.DOTcounters:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcounters;
                                //}
                                //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                //}
                                //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadars;
                                    }
                                    break;
                            }

                        }

                        var assetsView = MarkerObject as AssetsViewDTO;

                        if (assetsView != null)
                        {

                            var assetStatus = (AssetStatusEnum)assetsView.ItemStatusId;

                            var assetType = (AssetTypesEnum)assetsView.ItemCategoryId;

                            switch (assetType)
                            {
                                case AssetTypesEnum.DOTcounters:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcounters;
                                //}
                                //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                //}
                                //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadars;
                                    }
                                    break;
                            }

                        }

                        #endregion
                    }
                    break;

                case MarkerType.Incident:
                    {
                        #region Incident

                        var newIncident = MarkerObject as IncidentsDTO;
                        var newIncidentDto = MarkerObject as ClassLibrary.DTO.IncidentsDTO;


                        var severityType = newIncident != null ? (IncidentTypeEnum)newIncident.CrashSeverityId : (IncidentTypeEnum)newIncidentDto.CrashSeverityId;

                        switch (severityType)
                        {
                            case IncidentTypeEnum.HighPriorityIncident:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerMed;

                            case IncidentTypeEnum.Fatality:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerFatality;
                        }

                        #endregion
                    }
                    break;

                case MarkerType.IncidentNotification:
                    {
                        #region IncidentNotification

                        var newIncident = MarkerObject as IncidentToDraw;

                        var incidentType = (IncidentTypeEnum)newIncident.IncidentTypeId;

                        switch (incidentType)
                        {
                            case IncidentTypeEnum.HighPriorityIncident:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerMed;

                            case IncidentTypeEnum.Fatality:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerFatality;
                        }

                        #endregion
                    }
                    break;

                case MarkerType.Violation:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarker;
                    }

                case MarkerType.ViolationsNotifications:
                    {
                        #region ViolationsNotifications

                        var newViolation = MarkerObject as ViolationToDraw;

                        var violationType = (ViolationTypesEnum)newViolation.ViolationTypeId;

                        switch (violationType)
                        {
                            case ViolationTypesEnum.DirectionReversing:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerDirectionReversing;

                            case ViolationTypesEnum.P2P:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerP2P;

                            case ViolationTypesEnum.RedLight:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerRedLight;

                            case ViolationTypesEnum.RouteDeviation:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerRouteDeviation;

                            case ViolationTypesEnum.ShoulderSpeeding:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerShoulderSpeeding;

                            case ViolationTypesEnum.Speed:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerSpeed;

                            case ViolationTypesEnum.Tailgating:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerTailgating;
                        }

                        #endregion
                    }
                    break;
                case MarkerType.Fog:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.FogTowerMarker;
                    }
                case MarkerType.DetectedAccident:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;
                    }
                case MarkerType.WantedCar:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.WantedCarIcon;
                    }
                case MarkerType.WantedCarTracking:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.WantedCarTrackIcon;
                    }
                case MarkerType.LastWantedCarPlace:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.LastWantedCarPlace;
                    }
                case MarkerType.TruckViolation:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.TruckViolation;
                    }
                case MarkerType.CarPlateNumber:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.CarPlateNumber;
                    }
                case MarkerType.Cluster:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ClusterMarkerIcon;
                    }
            }

            return "";
        }
        #endregion

        #region INotifyPropertyChanged interface
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class IncidentDetailsDtoUI : System.ComponentModel.INotifyPropertyChanged
    {
        private ClassLibrary.DTO.IncidentsDTO _accidentDetailDto;

        public ClassLibrary.DTO.IncidentsDTO AccidentDetailDto
        {
            get { return _accidentDetailDto; }
            set { _accidentDetailDto = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _showOnMap;

        public bool ShowOnMap
        {
            get { return _showOnMap; }
            set { _showOnMap = value; this.RaiseNotifyPropertyChanged(); }
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
