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
using STC.Projects.WPFControlLibrary.TrafficMapControl.ServiceLayerReference;
using STC.Projects.ClassLibrary.Common.Helper;


namespace STC.Projects.WPFControlLibrary.TrafficMapControl.ViewModel
{
    public class TrafficMapControlViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerClient _client = new ServiceLayerClient();
        const int SHAPE_Z_INDEX = 1;
        const int SHAPE2_Z_INDEX = 2;
        const int MARKER_Z_INDEX = 3;
        IHubProxy _vehicleHub;
        #region properties

        public event ZoomOnMapEventHandler ZoomOnMapEvent;

        public string CurrentUsername { get; set; }

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

        private ViolationDetailsDTO _violationsOrAssetsToolTipDetailsOnGraphic;
        private IncidentDetailsDTO _incidentsToolTipDetailsOnGraphic;
        private PatrolFullDetailsDTO _patrolToolTipDetailsOnGraphic;
        private AssetDetailsForViolation _assetDetailsForViolation;

        private VehicleLiveTrackingDTO _vehicleDetail;

        public VehicleLiveTrackingDTO VehicleDetail
        {
            get { return _vehicleDetail; }
            set { _vehicleDetail = value; this.RaiseNotifyPropertyChanged(); }
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
        public PatrolFullDetailsDTO PatrolToolTipDetailsOnGraphic
        {
            get { return _patrolToolTipDetailsOnGraphic; }
            set { _patrolToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        }
        public Command FilterViolationsCommand { get; set; }
        public Command FilterPatrolsCommand { get; set; }
        public Command FilterAssetsCommand { get; set; }


        //selected dropdowns
        public Object SelectedAssetType { set; get; }
        public Object SelectedAssetStatus { set; get; }
        public Object SelectedViolationType { set; get; }
        public Object SelectedPatrolStatus { set; get; }

        public Object SelectedViolationDateFilter { set; get; }

        #endregion

        public TrafficMapControlViewModel()
        {
            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();

            Officers = new List<OfficersLastLocationViewDTO>();
            Officer = new OfficersLastLocationViewDTO();

            FilterViolationsCommand = new Command(FilterViolationsOnMap);
            FilterPatrolsCommand = new Command(FilterPatrolsOnMap);
            FilterAssetsCommand = new Command(FilterAssetsOnMap);

            GetLayerObservable(LayerTypeEnum.Incidents);
            GetLayerObservable(LayerTypeEnum.Violations);
            GetLayerObservable(LayerTypeEnum.Patrols);
            GetLayerObservable(LayerTypeEnum.Assets);
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
            AssetDetailsForViolation = new AssetDetailsForViolation();
            LoadData();
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
                    GetViolationsData(null, null);
                    break;
                case LayerTypeEnum.Incidents:
                    GetIncidentsDTOData();
                    break;
                case LayerTypeEnum.Patrols:
                    GetPatrolLastLocationDTOData(null);
                    break;
                case LayerTypeEnum.Assets:
                    GetAssetLastStatusData(null, null);
                    break;
                case LayerTypeEnum.Officers:
                    GetOfficersData();
                    break;
                case LayerTypeEnum.Traffic:
                    GetTrafficData();
                    break;
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

            HubConnection conn = new HubConnection(url);
            var violationsHub = conn.CreateHubProxy("ViolationsHub");
            var assetsHub = conn.CreateHubProxy("AssetsHub");
            var incidentHub = conn.CreateHubProxy("IncidentHub");
            var patrolHub = conn.CreateHubProxy("PatrolHub");
            _vehicleHub = conn.CreateHubProxy("VehicleLiveTrackingHub");
            violationsHub.On<ObservableCollection<ViolationNotificationDTO>>("NewViolations", UpdateViolationsBySignalR);
            assetsHub.On<ObservableCollection<AssetLastStatusDTO>>("NewAssets", UpdateAssetsBySignalR);
            incidentHub.On<ObservableCollection<IncidentsDTO>>("NewIncidents", UpdateIncidentsBySignalR);
            patrolHub.On<ObservableCollection<PatrolLastLocationDTO>>("NewLocations", UpdatePatrolsBySignalR);
            _vehicleHub.On<ObservableCollection<VehicleLiveTrackingDTO>>("NewVehicleLocations", UpdateWantedCarLocationBySignalR);
            conn.Start().Wait();
        }

        public void RegisterWantedCarSignalR(string plateNumber)
        {
            _vehicleHub.Invoke("RegisterDependency", plateNumber);
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

        private void UpdateWantedCarLocationBySignalR(ObservableCollection<VehicleLiveTrackingDTO> vehicles)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var vehicle in vehicles)
            {
                var markerGraphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_WantedCarCenter");
                var circleGraphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);

                if (markerGraphic != null)
                    Application.Current.Dispatcher.Invoke(() => markerGraphic.Geometry = new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)));

                if (circleGraphic != null)
                    Application.Current.Dispatcher.Invoke(() => DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), (double)_client.GetWantedCarRadius(), circleGraphic.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red));

                if (ZoomOnMapEvent != null)
                    ZoomOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);
            }
        }
        private void UpdatePatrolsBySignalR(ObservableCollection<PatrolLastLocationDTO> patrols)
        {
            patrols = new ObservableCollection<PatrolLastLocationDTO>(patrols.Where(x =>
                  SelectedPatrolStatus != null && ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId != 0 ? x.StatusId == ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId : true
                  ).ToList<PatrolLastLocationDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Patrols);
            foreach (var item in patrols)
            {
                var graphic = layerCol.FirstOrDefault(x => long.Parse(x.Attributes["Id"].ToString()) == item.PatrolId);
                if (graphic != null)
                    Application.Current.Dispatcher.Invoke(() => graphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
                else
                    Application.Current.Dispatcher.Invoke(() =>
                    {

                        layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item)));
                    });
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
                        Application.Current.Dispatcher.Invoke(() => layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item))));
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
                        Application.Current.Dispatcher.Invoke(() => layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Violations, 1), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Violation, item), 1)));
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
        private void Add_PatrolLastLocationDTOData(List<PatrolLastLocationDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Patrols);
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item))));
        }

        private void GetAssetLastStatusData(int? SelectedAssetStatusDimDTO, int? SelectedAssetType)
        {
            var callTask = _client.GetAssetsListAsync(SelectedAssetStatusDimDTO, SelectedAssetType);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetLastStatusData(x == null ? null : x.ToList()));
        }
        
        private void GetTrafficData()
        {
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
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Officers), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Officers, item))));
            Officers = list;
        }

        private void Add_AssetLastStatusData(List<AssetsViewDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Assets);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
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
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Violations, item.ViolationsCount), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Violation, item), item.ViolationsCount)));
        }
        private void GetIncidentsDTOData()
        {
            var callTask = _client.GetActiveIncidentsListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_IncidentsDTOData(x != null ? new ObservableCollection<IncidentsDTO>(x) : null));
        }
        private void Add_IncidentsDTOData(ObservableCollection<IncidentsDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Incidents);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item))));
            }
        }
        public void AddNotificationViolationOnMap(ViolationToDraw violationToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == violationToDraw.Id && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Violations.ToString());
            if (graphic == null)
            {
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(violationToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Violations), violationToDraw.Latitude, violationToDraw.Longitude, GetMarkerImageUrl(MarkerType.ViolationsNotifications, violationToDraw)));
            }
        }
        public void AddNotificationIncidentOnMap(IncidentToDraw incidentToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == incidentToDraw.Id.ToString() && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Incidents.ToString());
            if (graphic == null)
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(incidentToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Incidents), incidentToDraw.Latitude, incidentToDraw.Longitude, GetMarkerImageUrl(MarkerType.IncidentNotification, incidentToDraw)));
        }
        public void ManageFogEventMessage(FogEventMessage fogEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(fogEventMessage.Longitude, fogEventMessage.Latitude, new SpatialReference(4326));
            DrawCircle(fogEventMessage.TowerId.ToString(), point, fogEventMessage.VisibilityRadius, BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, _client.GetRadiusForNearByAssets(), BufferSymbolCircleBig, SHAPE_Z_INDEX, LayerTypeEnum.Notifications);
            SearchNearByAssetsAndPatrolsForFogEvent(fogEventMessage);
        }

        //Manage Wanted Car Message
        public void ManageWantedCarEventMessage(WantedCarMessage wantedCarEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude, new SpatialReference(4326));
            DrawCircle(wantedCarEventMessage.VehiclePlateNumber.ToString(), point, (double)_client.GetWantedCarRadius(), BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, _client.GetRadiusForNearByAssets(), BufferSymbolCircleBig, SHAPE_Z_INDEX, LayerTypeEnum.Notifications);
            SearchNearByAssetsAndPatrolsForWantedCarEvent(wantedCarEventMessage);
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
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (fogEventMessage.TowerId.ToString() + "_FogCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(fogEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), fogEventMessage.Latitude, fogEventMessage.Longitude, GetMarkerImageUrl(MarkerType.Fog, fogEventMessage));
                graphicCol.Add(graphic);
            }
            return isNew;
        }

        //Add Wanted Car Tower Marker
        private bool AddWantedCarTowerMarker(WantedCarMessage wantedCarEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (wantedCarEventMessage.VehiclePlateNumber.ToString() + "_WantedCarCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(wantedCarEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), wantedCarEventMessage.Latitude, wantedCarEventMessage.Longitude, GetMarkerImageUrl(MarkerType.WantedCar, wantedCarEventMessage));
                graphicCol.Add(graphic);
            }
            return isNew;
        }
        private bool AddDetectedAccidentMarker(DetectedAccidentMessage detectedAccidentEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (detectedAccidentEventMessage.TowerId.ToString() + "_AccidentCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(detectedAccidentEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), detectedAccidentEventMessage.Latitude, detectedAccidentEventMessage.Longitude, GetMarkerImageUrl(MarkerType.DetectedAccident, detectedAccidentEventMessage));
                graphicCol.Add(graphic);
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

                var callTask = _client.GetNearByPatrolsByLatLonAsync(detectedAccidentEventMessage.Longitude, detectedAccidentEventMessage.Latitude);
                var obs = callTask.ToObservable();
                obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
            }
        }

        public void DrawPatrols(DrawPatrolsMessage PatrolsMessage)
        {
            var callTask = _client.GetNearByPatrolsByLatLonAsync(PatrolsMessage.Longitude, PatrolsMessage.Latitude);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
        }

        private void Add_NearByPatrolsForFogEvent(List<PatrolLastLocationDTO> PatrolsList)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var item in PatrolsList)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.PatrolId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item))));
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
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
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
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
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
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
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
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(asset, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), SOPMapDrawObject.Lat, SOPMapDrawObject.Lon, GetMarkerImageUrl(MarkerType.Assets, asset)));

        }

        public void ClearNotificationsLayer()
        {

            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var count = graphicCol.Count;

            for (int i = 0; i < count; i++)
            {

                var graphic = graphicCol[0];

                Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));

            }

        }

        #endregion

        #region MarkersDetailsToolTip
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
        public void GetIncidentDetails(int selectedIncidentId)
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

        private void Add_PatrolDetails(PatrolFullDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => PatrolToolTipDetailsOnGraphic = data);
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
            GetAssetLastStatusData(assetStatus, assetType);
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
        private Graphic CreateGraphic(Dictionary<string, object> attributes, double latitude, double longitude, string iconPath, int? countSymbol = 0)
        {
            CompositeSymbol compositeSymbol = new CompositeSymbol();
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();
            TextSymbol textSymbol = new TextSymbol();

            pictureMarkerSymbol.SetSourceAsync(new Uri(iconPath));
            compositeSymbol.Symbols.Add(pictureMarkerSymbol);

            if (countSymbol.HasValue && countSymbol != 0)
            {
                textSymbol.Text = countSymbol.ToString();
                textSymbol.HorizontalTextAlignment = HorizontalTextAlignment.Center;
                textSymbol.VerticalTextAlignment = VerticalTextAlignment.Middle;
                textSymbol.Font.FontSize = 18;
                textSymbol.YOffset = 7;
                textSymbol.Color = (Color)ColorConverter.ConvertFromString("#d91e18");
                compositeSymbol.Symbols.Add(textSymbol);
            }

            var graphics = new Graphic(new MapPoint(longitude, latitude, new SpatialReference(4326)), compositeSymbol);
            foreach (KeyValuePair<string, object> item in attributes)
                graphics.Attributes.Add(item.Key, item.Value);
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
                    break;
                case LayerTypeEnum.Officers:
                    attributes.Add("Id", item.OfficerId);
                    attributes.Add("Speed", item.Speed);
                    attributes.Add("Date", item.LocationDate);
                    attributes.Add("Name", item.OfficerName);
                    break;
                case LayerTypeEnum.Assets:
                    attributes.Add("Id", item.OriginalIdent);
                    attributes.Add("Name", item.ItemName);
                    attributes.Add("Status", item.ItemStatusName);
                    attributes.Add("LocationCode", item.LocationCode);
                    break;
                case LayerTypeEnum.Notifications:
                    if (item is AssetsViewDTO)
                    {
                        attributes.Add("Id", item.ItemId);
                        attributes.Add("LocationCode", item.LocationCode);
                    }
                    else if (item is FogEventMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_FogCenter");
                    }
                    else if (item is WantedCarMessage)
                    {
                        attributes.Add("Id", item.VehiclePlateNumber + "_WantedCarCenter");
                    }
                    else if (item is DetectedAccidentMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_AccidentCenter");
                        attributes.Add("DetectedEventMessageObj", item);
                    }
                    else if (item is PatrolLastLocationDTO)
                    {
                        attributes.Add("Id", item.PatrolId + "_PatrolCenter");
                    }
                    else
                    {
                        attributes.Add("Id", item.Id);
                    }

                    attributes.Add("LayerType", targetNotificatoinLayerType.Value.ToString());

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
        private void DrawCircle(string id, MapPoint point, double radius, SimpleFillSymbol circleFillSymbol, int ZIndex, LayerTypeEnum layerType, Color color)
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
            layerCol.Add(bufferGraphic);
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
                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.SmartOfficerMarker;

                        }
                        #endregion
                    }
                    break;
                case MarkerType.Patrols:
                    {
                        #region Patrols

                        var patrolLocation = MarkerObject as PatrolLastLocationDTO;

                        if (patrolLocation != null)
                        {

                            var patrolStatus = (PatrolStatusEnum)patrolLocation.StatusId.Value;

                            switch (patrolStatus)
                            {
                                case PatrolStatusEnum.Acknowledged:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolAcknowledgedMarker;

                                case PatrolStatusEnum.ArrivedToLocation:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolArrivedMarker;

                                case PatrolStatusEnum.AssignedToEvent:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolAssignedMarker;

                                case PatrolStatusEnum.Available:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolAvailableMarker;

                                case PatrolStatusEnum.Dispatched:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolDispatchedMarker;

                                case PatrolStatusEnum.OnTheWay:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolOnTheWayMarker;

                                case PatrolStatusEnum.UnderMaintenance:
                                    return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.PatrolUnderMaintenanceMarker;

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
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                    //    case AssetStatusEnum.UnderMaintenance:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                    //    case AssetStatusEnum.Working:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcounters;
                                    //}
                                    //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                    //switch (assetStatus)
                                    //{
                                    //    case AssetStatusEnum.NotWorking:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                    //    case AssetStatusEnum.UnderMaintenance:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                    //    case AssetStatusEnum.Working:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                    //}
                                    //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadars;
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
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                    //    case AssetStatusEnum.UnderMaintenance:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                    //    case AssetStatusEnum.Working:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerDOTcounters;
                                    //}
                                    //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                    //switch (assetStatus)
                                    //{
                                    //    case AssetStatusEnum.NotWorking:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                    //    case AssetStatusEnum.UnderMaintenance:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                    //    case AssetStatusEnum.Working:
                                    //        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                    //}
                                    //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.AssetMarkerVitronicRadars;
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

                        var incidentType = (IncidentTypeEnum)newIncident.IncidentTypeId;

                        switch (incidentType)
                        {
                            case IncidentTypeEnum.HighPriorityIncident:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerMed;
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
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerMed;
                        }

                        #endregion
                    }
                    break;

                case MarkerType.Violation:
                    {
                        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarker;
                    }

                case MarkerType.ViolationsNotifications:
                    {
                        #region ViolationsNotifications

                        var newViolation = MarkerObject as ViolationToDraw;

                        var violationType = (ViolationTypesEnum)newViolation.ViolationTypeId;

                        switch (violationType)
                        {
                            case ViolationTypesEnum.DirectionReversing:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerDirectionReversing;

                            case ViolationTypesEnum.P2P:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerP2P;

                            case ViolationTypesEnum.RedLight:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerRedLight;

                            case ViolationTypesEnum.RouteDeviation:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerRouteDeviation;

                            case ViolationTypesEnum.ShoulderSpeeding:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerShoulderSpeeding;

                            case ViolationTypesEnum.Speed:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerSpeed;

                            case ViolationTypesEnum.Tailgating:
                                return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.ViolationMarkerTailgating;
                        }

                        #endregion
                    }
                    break;
                case MarkerType.Fog:
                    {
                        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.FogTowerMarker;
                    }
                case MarkerType.DetectedAccident:
                    {
                        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.IncidentMarkerHigh;
                    }
                case MarkerType.WantedCar:
                    {
                        return @"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" + ImagesResx.WantedCarIcon;
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
}
