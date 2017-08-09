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
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ServiceLayerReference;
using STC.Projects.ClassLibrary.Common.Helper;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel
{
    public class AlertNotificationMapViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate NotificationRecieved;
        public int CurrentUserId { get; set; }

        ServiceLayerClient _client;
        const int SHAPE_Z_INDEX = 1;
        const int SHAPE2_Z_INDEX = 2;
        const int MARKER_Z_INDEX = 3;
        IHubProxy _vehicleHub;
        #region properties

        public int SuperVisorId { get; set; }

        public Dictionary<string, ObservableCollection<Graphic>> LayersGraphicsDictionary { get; set; }
        private ObservableCollection<SupervisorNotificationDTO> _supervisorNotifications;

        public ObservableCollection<SupervisorNotificationDTO> SupervisorNotifications
        {
            get { return _supervisorNotifications; }
            set
            {
                _supervisorNotifications = value;
                if (value != null && value.Count > 0)
                    NotificationsListHeaderCount = value.Count.ToString();
                else
                    NotificationsListHeaderCount = "0";
                this.RaiseNotifyPropertyChanged();
            }
        }

        private SupervisorNotificationDTO _newNotificationToNotify;

        public SupervisorNotificationDTO NewNotificationToNotify
        {
            get { return _newNotificationToNotify; }
            set
            {
                _newNotificationToNotify = value;

                var handler = NotificationRecieved;
                if (handler != null)
                    handler(this, new EventArgs());
                //Show Alerts

                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<SupervisorNotificationDTO> _notificationsToNotify;

        public ObservableCollection<SupervisorNotificationDTO> NotificationsToNotify
        {
            get { return _notificationsToNotify; }
            set
            {
                _notificationsToNotify = value;
                if (!IsAlertProcessing)
                {
                    var handler = NotificationRecieved;
                    if (handler != null)
                        handler(this, new EventArgs());
                    //Show Alerts
                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private SupervisorNotificationDTO _selectedSupervisorNotificationDTO;

        public SupervisorNotificationDTO SelectedSupervisorNotificationDTO
        {
            get { return _selectedSupervisorNotificationDTO; }
            set
            {
                _selectedSupervisorNotificationDTO = value;
                IsAlertProcessing = true;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private string _notificationsListHeader;

        public string NotificationsListHeader
        {
            get { return _notificationsListHeader; }
            set { _notificationsListHeader = value; this.RaiseNotifyPropertyChanged(); }
        }

        private string _notificationsListHeaderCount;

        public string NotificationsListHeaderCount
        {
            get { return _notificationsListHeaderCount; }
            set { _notificationsListHeaderCount = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _isAlertProcessing;

        public bool IsAlertProcessing
        {
            get { return _isAlertProcessing; }
            set
            {
                _isAlertProcessing = value;
                //NotifyRemainingAlerts();//Notify remaining alerts which are yet to notice
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        public AlertNotificationMapViewModel()
        {
            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();

            _client = new ServiceLayerClient();

            NotificationsListHeaderCount = " (0)";

            GetSuperVisorID();

            LoadData();


            SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>();

            if (System.Configuration.ConfigurationSettings.AppSettings["ProcessMockData"] != null && System.Configuration.ConfigurationSettings.AppSettings["ProcessMockData"] == "Y")
                LoadSampleData();
        }


        public void GetSuperVisorID()
        {
            var task = _client.GetSupervisorIdAsync().ToObservable();
            task.Subscribe(x => SetSuperVisorID(x));
            //var supId = await _client.GetSupervisorIdAsync();
            //SuperVisorId = supId;

        }

        public void SetSuperVisorID(int supervisorId)
        {
            SuperVisorId = supervisorId;
        }
        private void NotifyRemainingAlerts()
        {
            while (!IsAlertProcessing && NotificationsToNotify != null && NotificationsToNotify.Count > 0)
            {
                SupervisorNotificationDTO alertToNotify = null; //get next alert from the list
                NotifyAlert(alertToNotify);
            }
        }

        private void NotifyAlert(SupervisorNotificationDTO alertToNotify)
        {

        }


        private void LoadSampleData()
        {
            SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>() 
            {new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("110001"), Status=SupervisorNotificationStatus.Approved,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48721", NotificationText="notification text"}
            },

            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("110002"), Status=SupervisorNotificationStatus.Pending,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48722", NotificationText="notification text"}
            },

            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("110003"), Status=SupervisorNotificationStatus.Approved,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48723", NotificationText="notification text"}
            },
            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("110004"), Status=SupervisorNotificationStatus.Approved,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48724", NotificationText="notification text"}
            },
            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("110005"), Status=SupervisorNotificationStatus.Rejected,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48725", NotificationText="notification text"}
            }

            };



            NotificationsToNotify = new ObservableCollection<SupervisorNotificationDTO>()
            {new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("100001"), Status=SupervisorNotificationStatus.Pending,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48721", NotificationText="notification text1"}
            },

            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("100002"), Status=SupervisorNotificationStatus.Pending,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48722", NotificationText="notification text1"}
            },

            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("100003"), Status=SupervisorNotificationStatus.Pending,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48723", NotificationText="notification text1"}
            },
            new SupervisorNotificationDTO()
            {SupervisorNoticationId=Convert.ToInt64("100004"), Status=SupervisorNotificationStatus.Pending,
                DangerousViolatorDetails=new SupervisorNotificationReportDangerousDTO(){PlateNumber="48724", NotificationText="notification text1"}
            },
            };

        }


        private void LoadData()
        {


            //connect signalR
            Task connectTask = new Task(ConnectToSignalR);
            connectTask.Start();
        }

        public void GetUnNoticedNotifications()
        {
            int userId = (CurrentUserId == 0) ? 1 : CurrentUserId;
            var callTask = _client.GetSupervisorNotificationsByUserIdAsync(userId);

            var obs = callTask.ToObservable();
            obs.Subscribe((x) => AddUnNoticedNotification(x));
        }

        public void ConnectToSignalR()
        {
            string url = Utility.GetSignalRUrl();

            HubConnection conn = new HubConnection(url);
            var supervisorNotificationHub = conn.CreateHubProxy("SupervisorNotificatoinHub");

            supervisorNotificationHub.On<SupervisorNotificationDTO>("SupervisorNotificationsChanged", UpdateSupervisorNotificationsBySignalR);

            conn.Start().Wait();
            supervisorNotificationHub.Invoke("AddToGroup", CurrentUserId);
        }




        private void AddUnNoticedNotification(SupervisorNotificationDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (data == null)
                    return;
                if (CurrentUserId == SuperVisorId)
                    SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(data.Where(n => n.IsNoticed == false && n.Status == SupervisorNotificationStatus.Pending).ToList());
                else
                    SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(data.Where(n => n.IsNoticed == false && n.Status != SupervisorNotificationStatus.Pending).ToList());
            });
        }

        #region SignalR update

        private void UpdateSupervisorNotificationsBySignalR(SupervisorNotificationDTO newNotification)
        {

            //SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(supervisorNotifications);


            Application.Current.Dispatcher.Invoke(() =>
            {
                if (newNotification == null)
                    return;

                NewNotificationToNotify = newNotification;

                //if (CurrentUserId == SuperVisorId)
                //    SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(dtoObj.Where(n => n.IsNoticed == false && n.Status == SupervisorNotificationStatus.Pending).ToList());
                //else
                //    SupervisorNotifications = new ObservableCollection<SupervisorNotificationDTO>(dtoObj.Where(n => n.IsNoticed == false && n.Status != SupervisorNotificationStatus.Pending).ToList());
            });
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
