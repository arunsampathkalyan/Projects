using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.ControlMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.MapControl.ViewModel;
using Telerik.Windows.Controls;
using UsersDTO = STC.Projects.WPFControlLibrary.MapControl.ServiceLayerReference.UsersDTO;
using ViolationNotificationDTO = STC.Projects.WPFControlLibrary.MapControl.ServiceLayerReference.ViolationNotificationDTO;
using STC.Projects.WPFControlLibrary.MapControl.ServiceLayerReference;
using Esri.ArcGISRuntime.Symbology;
using System.Globalization;
using System.Windows.Media.Animation;
using STC.Projects.ClassLibrary.Common.Enums;
using System.Runtime.InteropServices;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System.IO;

namespace STC.Projects.WPFControlLibrary.MapControl
{
    /// <summary>
    /// Interaction logic for MapUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "MapUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MapUserControl : UserControl, IUserControl
    {
        //public dele
        MapControlViewModel _mapControlViewModel = null;

        MapPoint PatrolmapPoint;

        bool mapPatrolFlag = false;
        Point mapPatrolPerviousPoint = new Point();

        Color fillColorLightCoral;
        Color fillColorBlueViolet;

        private Point _movePrePoint;
        private Point _moveCurrentPoint;
        Task _traffic;
        string _lastTag;
        bool isLastClickedAssetSmart;
        int? assetStatusId;
        bool isAssignGridTaskOpen;
        public MapUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            txtTitle.Visibility = Visibility.Collapsed;

            esriMapView.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            //PatrolEsriMapView.FlowDirection = FlowDirection.LeftToRight;
            var flowDir = Utility.GetLang() == "ar" ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;
            mapTipOfficers.FlowDirection = flowDir;
            mapTipWantedCar.FlowDirection = flowDir;

            fillColorLightCoral = Colors.LightYellow;
            fillColorLightCoral.A = 60;
            fillColorBlueViolet = Colors.LightCoral;
            fillColorBlueViolet.A = 80;

            (this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol).Color = fillColorBlueViolet;
            (this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol).Color = fillColorLightCoral;

            // ZoomOnMap(24.46666670, 54.36666669, 200000);

            ZoomOnMap(24.43666670, 54.45666669, 130000);
            _mapControlViewModel = new MapControlViewModel();
            _mapControlViewModel.ZoomOnMapEvent += WantedCarZoomOnMap;
            _mapControlViewModel.UpdateLocationOnMapEvent += UpdateWantedLocationOnMap;
            _mapControlViewModel.OpenPatrolEvent += mapControlViewModel_OpenPatrolEvent;
            _mapControlViewModel.OpenPatrolOfficerEvent += mapControlViewModel_OpenPatrolOfficerEvent;
            _mapControlViewModel.OpenIncidentEvent += mapControlViewModel_OpenIncidentEvent;
            _mapControlViewModel.OpenAssetEvent += mapControlViewModel_OpenAssetEvent;

            this.DataContext = _mapControlViewModel;

            radTabControlOfficers.DataContext = _mapControlViewModel;
            radTabControlWantedCar.DataContext = _mapControlViewModel;

            AddGrphicLayer(LayerTypeEnum.Traffic);

            AddGrphicLayer(LayerTypeEnum.Assets);

            AddGrphicLayer(LayerTypeEnum.AssetsRedLights);
            AddGrphicLayer(LayerTypeEnum.AssetsSpeed);
            AddGrphicLayer(LayerTypeEnum.AssetsSmartTowers);

            AddGrphicLayer(LayerTypeEnum.Violations);
            AddGrphicLayer(LayerTypeEnum.Incidents);

            AddGrphicLayer(LayerTypeEnum.Patrols);
            AddGrphicLayer(LayerTypeEnum.Notifications);
            //AddGrphicLayer(LayerTypeEnum.Officers);

            HideShowLayer(LayerTypeEnum.Assets, false, radToggleBtnAssetsRedLight, null, ImgAssetsLayerRedLight, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png");
            HideShowLayer(LayerTypeEnum.AssetsRedLights, false, radToggleBtnAssetsRedLight, null, ImgAssetsLayerRedLight, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png");
            HideShowLayer(LayerTypeEnum.AssetsSpeed, false, radToggleBtnAssetsSpeed, null, ImgAssetsSpeedLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png");
            HideShowLayer(LayerTypeEnum.AssetsSmartTowers, false, radToggleBtnAssetsSmartTower, null, ImgAssetsSpeedLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png");

            HideShowLayer(LayerTypeEnum.Violations, false, radToggleBtnViolations, null, ImgViolationsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violatians.png");
            HideShowLayer(LayerTypeEnum.Incidents, false, radToggleBtnIncidents, null, ImgIncidentsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accidents.png");
            //ShowHideTraffic(false);
            //HideShowLayer(LayerTypeEnum.Traffic, false, radToggleBtnTraffic, null, null, "");
            //HideShowLayer(LayerTypeEnum.Officers, false, radToggleBtnOfficers, mapTipOfficers);

            CultureInfo cul = Utility.GetLang() == "ar" ? new CultureInfo("ar-Eg") : new CultureInfo(Utility.GetLang());

            //DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            //{

            //    this.TxtTime.Text = DateTime.Now.ToString("t", cul);

            //    this.TxtDate.Text = DateTime.Now.ToString("D", cul);

            //}, this.Dispatcher);
            isAssignGridTaskOpen = false;

            this.Loaded += MapUserControl_Loaded;
            this.Unloaded += MapUserControl_Unloaded;
        }

        public void mapControlViewModel_OpenIncidentEvent()
        {
            var incidentDetails = _mapControlViewModel.IncidentDetails;
            var incidentDetailsClient = new IncidentsClientDTO()
            {
                ArrivedTime = incidentDetails.ArrivedTime,
                BackgroundColor = incidentDetails.BackgroundColor,
                CallerAddress = incidentDetails.CallerAddress,
                CallerLanguage = incidentDetails.CallerLanguage,
                CallerName = incidentDetails.CallerName,
                CallerNumber = incidentDetails.CallerNumber,
                CallTakerId = incidentDetails.CallTakerId,
                CallTakerName = incidentDetails.CallTakerName,
                CreatedTime = incidentDetails.CreatedTime,
                DispatcherId = incidentDetails.DispatcherId,
                DispatcherName = incidentDetails.DispatcherName,
                DispatcheTime = incidentDetails.DispatcheTime,
                EndTime = incidentDetails.EndTime,
                IncidentAddress = incidentDetails.IncidentAddress,
                IncidentId = incidentDetails.IncidentId,
                IncidentNumber = incidentDetails.IncidentNumber,
                IncidentTypeId = incidentDetails.IncidentTypeId,
                IncidentTypeName = incidentDetails.IncidentTypeName,
                IsCritical = incidentDetails.IsCritical,
                IsNoticed = incidentDetails.IsNoticed,
                Latitude = incidentDetails.Latitude,
                Longitude = incidentDetails.Longitude,
                MessageId = incidentDetails.MessageId,
                MessageText = incidentDetails.MessageText,
                NotificationId = incidentDetails.NotificationId,
                //PriorityId = incidentDetails.PriorityId,
                //PriorityName = incidentDetails.PriorityName,
                StatusId = incidentDetails.StatusId,
                StatusName = incidentDetails.StatusName,



                EmirateId = incidentDetails.EmirateId,
                EmirateName = incidentDetails.EmirateName,
                CityId = incidentDetails.CityId,
                CityName = incidentDetails.CityName,

                AreaId = incidentDetails.AreaId,
                AreaName = incidentDetails.AreaName,

                LocationId = incidentDetails.LocationId,
                LocationName = incidentDetails.LocationName,
                LocationTypeId = incidentDetails.LocationTypeId,
                LocationTypeName = incidentDetails.LocationTypeName,
                IntersectionId = incidentDetails.IntersectionId,
                IntersectionName = incidentDetails.IntersectionName,
                PoliceStation = incidentDetails.PoliceStation,

                CrashTypeId = incidentDetails.CrashTypeId,
                CrashTypeName = incidentDetails.CrashTypeName,
                CauseId = incidentDetails.CauseId,
                CauseName = incidentDetails.CauseName,

                SevereInjuries = incidentDetails.SevereInjuries,
                MediumInjuries = incidentDetails.MediumInjuries,
                SlightInjuries = incidentDetails.SlightInjuries,


                Fatalities = incidentDetails.Fatalities,
                WeatherId = incidentDetails.WeatherId,
                WeatherName = incidentDetails.WeatherName,
                LightingId = incidentDetails.LightingId,
                LightingName = incidentDetails.LightingName,
                PConditionId = incidentDetails.PConditionId,
                PConditionName = incidentDetails.PConditionName,
                RoadTypeId = incidentDetails.RoadTypeId,
                RoadTypeName = incidentDetails.RoadTypeName,

                CrashSeverityId = incidentDetails.CrashSeverityId,
                CrashSeverityName = incidentDetails.CrashSeverityName,

                IncidentDescription = incidentDetails.IncidentDescription,

                Speed = incidentDetails.Speed



            };
            this.Publish(new OpenIncidentTip()
            {
                OriginalItem = incidentDetailsClient
            });
        }

        private AssetViolationDetailsClientDTO PrepareAssetDetail()
        {
            var assetDetails = _mapControlViewModel.AssetViolationsDetails;
            var res = new AssetViolationDetailsClientDTO()
            {
                AssetCode = assetDetails.AssetCode,
                AssetLocation = assetDetails.AssetLocation,
                AssetVendor = assetDetails.AssetVendor,
                AssetViolationCount = _mapControlViewModel.AssetViolationCountDetails == null ? assetDetails.AssetViolationCount : _mapControlViewModel.AssetViolationCountDetails.DayCount,
                AssetViolationCountMonth = _mapControlViewModel.AssetViolationCountDetails == null ? assetDetails.AssetViolationCountMonth : _mapControlViewModel.AssetViolationCountDetails.MonthCount,
                AssetViolationCountYearly = _mapControlViewModel.AssetViolationCountDetails == null ? assetDetails.AssetViolationCountYearly : _mapControlViewModel.AssetViolationCountDetails.YearCount,
                AssetStatus = ((assetStatusId != null && assetStatusId.HasValue) ? (assetStatusId.Value == 1 ? Properties.Resources.strWorking : Properties.Resources.strNotWorking) : "")

            };
            return res;
        }

        public void mapControlViewModel_OpenAssetEvent()
        {
            if (isLastClickedAssetSmart)
            {
                this.Publish(new OpenSmartAssetTip()
                {
                    OriginalItem = PrepareAssetDetail()
                });
            }
            else
            {
                this.Publish(new OpenAssetTip()
                {
                    OriginalItem = PrepareAssetDetail()
                });
            }
        }



        public void mapControlViewModel_OpenPatrolEvent()
        {
            var patrolDetails = _mapControlViewModel.PatrolDetails;
            var patrolDetailsClient = new PatrolOfficersDetailsClientDTO()
            {
                IsPatrol = true,
                IsAvailable = patrolDetails.IsAvailable,
                PatrolAllocation = patrolDetails.PatrolAllocation,
                PatrolCode = patrolDetails.PatrolCode,
                PatrolPlateNumber = patrolDetails.PatrolPlateNumber,
                StatusArabic = patrolDetails.StatusArabic,
                StatusEnglish = patrolDetails.StatusEnglish
            };
            foreach (var item in patrolDetails.Officers)
            {
                patrolDetailsClient.Officers.Add(new StaffPatrolModelClient() { FirstName = item.FirstName, LastName = item.LastName, FullName = item.FullName, ImageArray = ImagetoByteArray(item.ImagePath), MilitaryNumber = item.MilitaryNumber });
            }



            this.Publish(new OpenPatrolTip()
            {
                OriginalItem = patrolDetailsClient,
                Tag = _lastTag
            });
            isAssignGridTaskOpen = true;
        }


        private byte[] ImagetoByteArray(string imgPath)
        {
            MemoryStream ms = null;
            try
            {
                Uri uriResult = null;

                string tempPath = (Uri.TryCreate(imgPath, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)) ? imgPath : @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/officer_popup.png";




                System.Drawing.Image img = System.Drawing.Image.FromFile(tempPath);

                ms = new MemoryStream();

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
            catch (Exception)
            {
                //ms = null;





                System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"..\..\..\STC.Projects.ClassLibrary.Common\Images\officer_popup.png");

                MemoryStream ms1 = new MemoryStream();

                img1.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms1.ToArray();


                //Uri uriResult = null;
                //if (Uri.TryCreate(@"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.SmartOfficerMarker, UriKind.Absolute, out uriResult))
                //{
                //    BitmapImage img = new BitmapImage(uriResult);

                //    ImageConverter conv = new ImageConverter();


                //    byte[] data;
                //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                //    encoder.Frames.Add(BitmapFrame.Create(img));
                //    using (MemoryStream mss = new MemoryStream())
                //    {
                //        encoder.Save(mss);
                //        data = mss.ToArray();
                //    }
                //    return data;



                //    //return ms.ToArray();
                //}
            }
            return null;
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public void mapControlViewModel_OpenPatrolOfficerEvent()
        {
            var patrolDetails = _mapControlViewModel.SmartOfficer;
            var patrolDetailsClient = new PatrolOfficersDetailsClientDTO()
            {
                IsPatrol = false,
                PatrolCode = patrolDetails.OfficerMilitaryId,
                PatrolPlateNumber = patrolDetails.OfficerPatrolPlateNumber,
                StatusArabic = patrolDetails.StatusName,
                StatusEnglish = patrolDetails.StatusName
            };

            patrolDetailsClient.Officers.Add(new StaffPatrolModelClient() { FirstName = patrolDetails.OfficerName, ImageArray = patrolDetails.OfficerImage });

            this.Publish(new OpenPatrolTip()
            {
                OriginalItem = patrolDetailsClient,
                Tag = _lastTag
            });
            isAssignGridTaskOpen = true;
        }



        void MapUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _mapControlViewModel.ZoomOnMapEvent -= WantedCarZoomOnMap;
            _mapControlViewModel.UpdateLocationOnMapEvent -= UpdateWantedLocationOnMap;
            _mapControlViewModel.OpenPatrolEvent -= mapControlViewModel_OpenPatrolEvent;
            _mapControlViewModel.OpenPatrolOfficerEvent -= mapControlViewModel_OpenPatrolOfficerEvent;
            _mapControlViewModel.OpenIncidentEvent -= mapControlViewModel_OpenIncidentEvent;
            _mapControlViewModel.OpenAssetEvent -= mapControlViewModel_OpenAssetEvent;
            foreach (var item in esriMapView.Map.Layers)
            {
                try
                {
                    System.Windows.Data.BindingOperations.ClearAllBindings(item);
                }
                catch (Exception ex)
                {

                }
            }
            esriMapView.Map.Layers.Clear();
        }



        void MapUserControl_Loaded(object sender, EventArgs e)
        {
            var vm = DataContext as MapControlViewModel;

            if (vm != null)
                vm.CurrentUsername = this.GetCurrentUsername();

            // AddGrphicLayer(LayerTypeEnum.Traffic);
        }

        void WantedCarZoomOnMap(double orgLat, double lat, double orgLon, double lon, string plateNumber)
        {

            //Application.Current.Dispatcher.Invoke(() => ZoomOnMap(lat, lon, 3000));
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mapPoints = new List<MapPoint>();
                mapPoints.Add(new MapPoint(orgLon, orgLat, new SpatialReference(4326)));
                mapPoints.Add(new MapPoint(lon, lat, new SpatialReference(4326)));
                ZoomToPointsExtend(mapPoints);
            });
            //Application.Current.Dispatcher.Invoke(() => this.Publish(new WantedCarLocationChanged { Lat = lat, Lon = lon, PlateNumber = plateNumber }));
        }

        void UpdateWantedLocationOnMap(double? lat, double? lon, string plateNumber)
        {
            //Application.Current.Dispatcher.Invoke(() => ZoomOnMap(lat.Value, lon.Value, 5000));

            Application.Current.Dispatcher.Invoke(() => this.Publish(new WantedCarLocationChanged { Lat = lat, Lon = lon, PlateNumber = plateNumber, IsNeedAlert = plateNumber != _mapControlViewModel._CurrentWantedPlate }));
        }

        private void esriMapView_Loaded(object sender, RoutedEventArgs e)
        {
            //mapRadSlider.Minimum = 0;
            //mapRadSlider.Maximum = 5000000;
        }
        public void AddGrphicLayer(LayerTypeEnum layerTypeEnum, bool isCluster = false)
        {
            if (layerTypeEnum == LayerTypeEnum.Traffic)
            {
                _traffic = new Task(GetTrafficData);
                _traffic.Start();
                //  GetTrafficData();
            }
            else
            {
                _mapControlViewModel.AddGraphicsToLayersGraphicsDictionary(layerTypeEnum);
                System.Windows.Data.Binding binding = new System.Windows.Data.Binding(string.Format("LayersGraphicsDictionary[{0}]", layerTypeEnum.ToString()));
                binding.Source = _mapControlViewModel;
                binding.Mode = System.Windows.Data.BindingMode.OneWay;
                if (!isCluster)
                {
                    GraphicsLayer graphicsLayer = new GraphicsLayer();

                    graphicsLayer.ID = layerTypeEnum.ToString();
                    esriMapView.Map.Layers.Add(graphicsLayer);

                    System.Windows.Data.BindingOperations.SetBinding(graphicsLayer, GraphicsLayer.GraphicsSourceProperty, binding);
                }
                else
                {
                    Options option = new Options();
                    option.distance = 100;
                    option.labelColor = Colors.Black;
                    option.labelOffset = 10;
                    var clusterLayer = new ClusterLayer(option);
                    clusterLayer.SetMap(esriMapView.Map, esriMapView);
                    clusterLayer.ID = layerTypeEnum.ToString();
                    var blue = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol { Color = Colors.Blue, Size = 10, Style = Esri.ArcGISRuntime.Symbology.SimpleMarkerStyle.Circle };
                    var green = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol { Color = Colors.Green, Size = 20, Style = Esri.ArcGISRuntime.Symbology.SimpleMarkerStyle.Circle };
                    var red = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol { Color = Colors.Red, Size = 40, Style = Esri.ArcGISRuntime.Symbology.SimpleMarkerStyle.Circle };
                    // create a default symbol for values not defined in the renderer's class definitions
                    var defaultSym = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol { Color = Colors.Gray, Size = 10, Style = Esri.ArcGISRuntime.Symbology.SimpleMarkerStyle.Circle };
                    // create a ClassBreakInfo for each range; define the min/max attribute values and associated symbol
                    var classRange0 = new Esri.ArcGISRuntime.Symbology.ClassBreakInfo { Minimum = 0, Maximum = 1, Symbol = defaultSym };
                    var classRange1 = new Esri.ArcGISRuntime.Symbology.ClassBreakInfo { Minimum = 1, Maximum = 2, Symbol = blue };
                    var classRange2 = new Esri.ArcGISRuntime.Symbology.ClassBreakInfo { Minimum = 2, Maximum = 200, Symbol = green };
                    var classRange3 = new Esri.ArcGISRuntime.Symbology.ClassBreakInfo { Minimum = 200, Maximum = 100001, Symbol = red };
                    // create a class breaks info collection; add the info for each class
                    var classInfos = new Esri.ArcGISRuntime.Symbology.ClassBreakInfoCollection();
                    classInfos.Add(classRange1);
                    classInfos.Add(classRange2);
                    classInfos.Add(classRange3);
                    classInfos.Add(classRange0);


                    // create the class breaks renderer
                    var classBreaksRenderer = new Esri.ArcGISRuntime.Symbology.ClassBreaksRenderer();

                    // specify the field that contains the attribute (population)
                    classBreaksRenderer.Field = "clusterCount";

                    // provide the class breaks info collection
                    classBreaksRenderer.Infos = classInfos;

                    // provide a symbol for values outside the defined classes
                    classBreaksRenderer.DefaultSymbol = defaultSym;

                    // apply the renderer to the layer
                    clusterLayer.Renderer = classBreaksRenderer;
                }

            }
            //System.Windows.Data.BindingOperations.ClearBinding()
        }

        public void GetTrafficData()
        {
            var url = "http://192.168.20.244:8080/geoserver/wms/?";
            if (System.Configuration.ConfigurationSettings.AppSettings["OptimaURL"] != null)
                url = System.Configuration.ConfigurationSettings.AppSettings["OptimaURL"];
            var tiledUri = new Uri(url);

            Application.Current.Dispatcher.Invoke(() =>
                {
                    //create a new WMS layer with the above URL 
                    var agsTiledLayer = new WmsLayer(tiledUri);

                    // add layer properties 
                    agsTiledLayer.Opacity = 0.7; //Layer Opacity (optional parameter)

                    //select the layers that you want to load from optima service
                    // optima:rlin_tre_rltm_shap_v is the traffic layer
                    // optima:node is the nodes layer at optima
                    // they have many layers to read it all call GetCapabilities from the below url 
                    //http://192.168.20.244:8080/geoserver/wms/?SERVICE=WMS&REQUEST=GetCapabilities&configuration=optima

                    // agsTiledLayer.Layers = new[] { "optima:rlin_tre_rltm_shap_v", "optima:node" };
                    //agsTiledLayer.Layers = new[] { "optima:rlin_tre_rltm_shap_v", "optima:signals_utc_shape" };
                    agsTiledLayer.Layers = new[] { "optima:rlin_tre_rltm_shap_v" };
                    //define the return type
                    agsTiledLayer.ImageFormat = "image/png";
                    agsTiledLayer.ID = LayerTypeEnum.Traffic.ToString();
                    //       agsTiledLayer.IsVisible = false;

                    // add the new layer to the map

                    esriMapView.Map.Layers.Add(agsTiledLayer);
                    ShowHideTraffic(false);
                });
        }

        public void ZoomToPointsExtend(List<MapPoint> points)
        {
            esriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.Polygon(points).Extent, new TimeSpan(0, 0, 3));
            //esriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.Polygon(points).Extent.GetCenter(), 5000, new TimeSpan(0, 0, 3));
        }

        #region HideShowLayers
        private void radToggleBtnViolations_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violatians.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violation_selc.png";

            HideShowLayer(LayerTypeEnum.Violations, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnViolations, null, ImgViolationsLayer, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }

        private void radToggleBtnAssetsRedLight_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons_selc.png";

            //_mapControlViewModel.AssetRedLightIsChecked = ((RadToggleButton)e.Source).IsChecked.Value;

            //ShowHideGraphics(LayerTypeEnum.AssetsRedLights);


            HideShowLayer(LayerTypeEnum.AssetsRedLights, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnAssetsRedLight, null, ImgAssetsLayerRedLight, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }

        private void radToggleBtnAssetsSpeed_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons_selc.png";

            //_mapControlViewModel.AssetSpeedIsChecked = ((RadToggleButton)e.Source).IsChecked.Value;

            //ShowHideGraphics(LayerTypeEnum.AssetsSpeed);



            HideShowLayer(LayerTypeEnum.AssetsSpeed, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnAssetsSpeed, null, ImgAssetsSpeedLayer, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }

        private void radToggleBtnAssetsSmartTower_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Assets_icons_selc.png";

            //_mapControlViewModel.AssetSmartTowersIsChecked = ((RadToggleButton)e.Source).IsChecked.Value;

            //ShowHideGraphics(LayerTypeEnum.AssetsSmartTowers);



            HideShowLayer(LayerTypeEnum.AssetsSmartTowers, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnAssetsSmartTower, null, ImgAssetsLayerSmartTower, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }


        private void ShowHideGraphics(LayerTypeEnum layerType)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            if (layer == null) return;
            if (layerType == LayerTypeEnum.Assets)
            {
                var graphicsAssets = (layer as GraphicsLayer).Graphics;

                foreach (var graphic in graphicsAssets)
                {
                    if (graphic.Attributes.ContainsKey("AssetsType"))
                    {
                        //"Smart Towers", "Vitronic Mobile radars ", "DOT counters", "Vitronic Radars", "Ekin Red Light Camera"
                        if (graphic.Attributes["AssetsType"].ToString() == "Ekin Red Light Camera" && _mapControlViewModel.AssetRedLightIsChecked == false)
                            graphic.IsVisible = false;

                        if (graphic.Attributes["AssetsType"].ToString() == "Vitronic Radars" && _mapControlViewModel.AssetSpeedIsChecked == false)
                            graphic.IsVisible = false;
                    }

                }
            }
        }

        private void radToggleBtnIncidents_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is RadToggleButton)
            {

                string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accidents.png";

                string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accident_selc.png";

                HideShowLayer(LayerTypeEnum.Incidents, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnIncidents, null, ImgIncidentsLayer, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
            }
        }

        private void radToggleBtnPatrols_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrols.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrol_selc.png";

            HideShowLayer(LayerTypeEnum.Patrols, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnPatrols, null, ImgPatrolsLayer, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }

        private void radToggleBtnOfficers_Click(object sender, RoutedEventArgs e)
        {
            //HideShowLayer(LayerTypeEnum.Officers, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnOfficers, mapTipOfficers);
        }

        private void radToggleBtnNotifications_Click(object sender, RoutedEventArgs e)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/noti.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/noti_selc.png";

            HideShowLayer(LayerTypeEnum.Notifications, ((RadToggleButton)e.Source).IsChecked.Value, radToggleBtnNotifications, null, ImgNotiLayer, ((RadToggleButton)e.Source).IsChecked.Value == true ? imgSelc : img);
        }

        private void radToggleBtnTraffic_Click(object sender, RoutedEventArgs e)
        {
            ShowHideTraffic(((RadToggleButton)e.Source).IsChecked.Value);
        }

        private void ShowHideTraffic(bool isChecked)
        {
            string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Traffic_icons.png";

            string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Traffic_icons_selc.png";

            HideShowLayer(LayerTypeEnum.Traffic, isChecked, radToggleBtnTraffic, null, ImgTrafficLayer, isChecked ? imgSelc : img);
        }
        #endregion

        #region TooltipOnMap

        private void esriMapView_MouseDown(object sender, MouseEventArgs e)
        {
            HandleClick(e.GetPosition(esriMapView));
        }
        #endregion

        #region GoMessages
        public void Go(ZoomToExtend sopZoomMessage)
        {
            if (sopZoomMessage == null || sopZoomMessage.points == null || !sopZoomMessage.points.Any())
                return;
            var list = new List<MapPoint>();
            foreach (var item in sopZoomMessage.points)
            {
                list.Add(new MapPoint(item.y, item.x, new SpatialReference(4326)));
            }
            ZoomToPointsExtend(list);
        }
        public void Go(ViewNotificationLayer viewNotificationLayer)
        {
            HideShowLayer(LayerTypeEnum.Notifications, true, radToggleBtnNotifications, null, ImgNotiLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/noti_selc.png");
            // HideShowLayer(LayerTypeEnum.Patrols, true, radToggleBtnPatrols, null, ImgPatrolsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrol_selc.png");

        }
        public void Go(ViewViolationLayer incidentMessage)
        {
            HideShowLayer(LayerTypeEnum.Violations, true, radToggleBtnViolations, null, ImgViolationsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violation_selc.png");
        }
        public void Go(ViewIncidentLayer violationMessage)
        {
            HideShowLayer(LayerTypeEnum.Incidents, true, radToggleBtnIncidents, null, ImgIncidentsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accident_selc.png");
        }
        public void Go(ViewPatrolLayer patrolMessage)
        {
            HideShowLayer(LayerTypeEnum.Patrols, true, radToggleBtnPatrols, null, ImgPatrolsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrol_selc.png");
        }

        public void Go(ViolationToDraw violationToDraw)
        {
            this._mapControlViewModel.AddNotificationViolationOnMap(violationToDraw);
            //long assetId = 0;
            //long.TryParse(violationToDraw.ViolationObj.AssetId, out assetId);
            //this.Publish(new WantedCarToSOPMessage
            //{
            //    TowerId = assetId,
            //    Longitude = violationToDraw.Longitude,
            //    Latitude = violationToDraw.Latitude,
            //    MessageId = violationToDraw.ViolationObj.MessageId,
            //    Discription = violationToDraw.ViolationObj.MessageText,
            //    VehiclePlateNumber = violationToDraw.ViolationObj.PlateNumber
            //});
        }
        public void Go(IncidentToDraw incidentToDraw)
        {
            this._mapControlViewModel.AddNotificationIncidentOnMap(incidentToDraw);
        }
        public void Go(HideAllLayers hideAllLayers)
        {
            HideShowLayer(LayerTypeEnum.Incidents, false, radToggleBtnIncidents, null, ImgIncidentsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accidents.png");
            HideShowLayer(LayerTypeEnum.Violations, false, radToggleBtnViolations, null, ImgViolationsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violatians.png");
            HideShowLayer(LayerTypeEnum.Patrols, false, radToggleBtnPatrols, null, ImgPatrolsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrols.png");
            //HideShowLayer(LayerTypeEnum.Officers, false, radToggleBtnOfficers, mapTipOfficers);
            HideShowLayer(LayerTypeEnum.Notifications, false, radToggleBtnNotifications, null, ImgNotiLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/noti.png");
            HideShowLayer(LayerTypeEnum.Traffic, false, radToggleBtnTraffic, null, ImgTrafficLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Traffic_icons.png");

            mapTipWantedCar.Visibility = Visibility.Collapsed;
        }
        //Zoom on map
        public void Go(ZoomOnMap zoomOnMap)
        {
            ZoomOnMap(zoomOnMap.Latitude, zoomOnMap.Longitude, 5000);
        }
        public void Go(SearchByMapPolygonResult polygon)
        {
            _mapControlViewModel.PolygonForSearch = polygon.PolyResult;
        }


        public void Go(FogEventMessage fogEventMessage)
        {
            _mapControlViewModel.ManageFogEventMessage(fogEventMessage, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol, fillColorBlueViolet);
            if (fogEventMessage.IsPublishSOP)
            {
                _mapControlViewModel.SetCurrentTrackingPlateNumber("", fogEventMessage.MessageId);
                _mapControlViewModel.DrawNotificationCircle(fogEventMessage, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, fillColorBlueViolet, fogEventMessage.VisibilityRadius, fogEventMessage.TowerId.ToString());


                txtTitle.Text = fogEventMessage.Discription;
                txtTitle.Visibility = Visibility.Visible;

                this.Publish(new FogEventToSOPMessage
                {
                    CamerasList = fogEventMessage.CamerasList,
                    Discription = fogEventMessage.Discription,
                    Latitude = fogEventMessage.Latitude,
                    Longitude = fogEventMessage.Longitude,
                    MessageId = fogEventMessage.MessageId,
                    RadarsList = fogEventMessage.RadarsList,
                    TowerId = fogEventMessage.TowerId,
                    TowersList = fogEventMessage.TowersList,
                    VisibilityRadius = fogEventMessage.VisibilityRadius,
                    CreatedDate = fogEventMessage.CreatedDate,
                    NotificationId = fogEventMessage.NotificationId
                });

                ScaleMap();
            }

        }

        //Wanted Car Message
        public void Go(WantedCarMessage wantedCarEventMessage)
        {
            _mapControlViewModel.ManageWantedCarEventMessage(wantedCarEventMessage, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol, fillColorBlueViolet);
            if (wantedCarEventMessage.IsPublishSOP)
            {
                _mapControlViewModel.SetCurrentTrackingPlateNumber(wantedCarEventMessage.VehiclePlateNumber, wantedCarEventMessage.MessageId);
                _mapControlViewModel.DrawNotificationCircle(wantedCarEventMessage, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, fillColorBlueViolet, _mapControlViewModel._wantedCarRadius, wantedCarEventMessage.VehiclePlateNumber.ToString());
                _mapControlViewModel.DrawOldPath(wantedCarEventMessage);
                mapTipWantedCar.Visibility = Visibility.Collapsed;
                radTabControlWantedCar.SelectedIndex = 0;

                txtTitle.Visibility = Visibility.Visible;
                txtTitle.Text = Utility.GetLang() == "ar" ? wantedCarEventMessage.Discription : wantedCarEventMessage.EnglishDiscription;

                _mapControlViewModel.GetVehicleDetails(wantedCarEventMessage.VehiclePlateNumber);

                GraphicsLayer layer = esriMapView.Map.Layers[LayerTypeEnum.Notifications.ToString()] as GraphicsLayer;
                var graphicSources = layer.GraphicsSource;

                foreach (var graphic in graphicSources)
                {
                    if (graphic.Attributes.ContainsKey("Id") && graphic.Attributes["Id"].ToString().Contains("_WantedCarCenter"))
                    {
                        Storyboard sb = (Storyboard)TryFindResource("BlankingAnimation");

                        Storyboard.SetTarget(sb, layer);
                        sb.Begin();
                    }
                }
                this.Publish(new WantedCarToSOPMessage
                {
                    TowerId = wantedCarEventMessage.TowerId,
                    Longitude = wantedCarEventMessage.Longitude,
                    Latitude = wantedCarEventMessage.Latitude,
                    MessageId = wantedCarEventMessage.MessageId,
                    Discription = Utility.GetLang() == "ar" ? wantedCarEventMessage.Discription : wantedCarEventMessage.EnglishDiscription,
                    VehiclePlateNumber = wantedCarEventMessage.VehiclePlateNumber,
                    VehiclePlateKind = wantedCarEventMessage.VehiclePlateKind,
                    VehiclePlateType = wantedCarEventMessage.VehiclePlateType,
                    VehiclePlateSource = wantedCarEventMessage.VehiclePlateSource,
                    VehiclePlateColor = wantedCarEventMessage.VehiclePlateColor,
                    CreatedDate = wantedCarEventMessage.CreatedDate,
                    NotificationId = wantedCarEventMessage.NotificationId
                });

                ScaleMap();
                // _mapControlViewModel.RegisterWantedCarSignalR(wantedCarEventMessage.VehiclePlateNumber);
            }
            _mapControlViewModel.RegisterWantedCarSignalR(wantedCarEventMessage.VehiclePlateNumber);
        }

        // Truck Permission Message
        public void Go(TruckViolationMessage truckViolationMsg)
        {
            _mapControlViewModel.ManageTruckPermissionMessage(truckViolationMsg, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol, fillColorBlueViolet);
            if (truckViolationMsg.IsPublishSOP)
            {
                _mapControlViewModel.SetCurrentTrackingPlateNumber(truckViolationMsg.TruckPlateNumber, truckViolationMsg.MessageId);
                _mapControlViewModel.DrawNotificationCircle(truckViolationMsg, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, fillColorBlueViolet, _mapControlViewModel._wantedCarRadius, truckViolationMsg.TruckPlateNumber.ToString());

                GraphicsLayer layer = esriMapView.Map.Layers[LayerTypeEnum.Notifications.ToString()] as GraphicsLayer;
                var graphicSources = layer.GraphicsSource;

                foreach (var graphic in graphicSources)
                {
                    if (graphic.Attributes.ContainsKey("Id") && graphic.Attributes["Id"].ToString().Contains("_TruckViolationCenter"))
                    {
                        Storyboard sb = (Storyboard)TryFindResource("BlankingAnimation");

                        Storyboard.SetTarget(sb, layer);
                        sb.Begin();
                    }
                }

                this.Publish(new TruckViolationToSOPMessage
                {
                    TowerId = truckViolationMsg.TowerId,
                    Longitude = truckViolationMsg.Longitude,
                    Latitude = truckViolationMsg.Latitude,
                    MessageId = truckViolationMsg.MessageId,
                    Discription = truckViolationMsg.Discription,
                    TruckPlateNumber = truckViolationMsg.TruckPlateNumber,
                    CreatedDate = truckViolationMsg.CreatedDate,
                    NotificationId = truckViolationMsg.NotificationId
                });

                ScaleMap();
                //_mapControlViewModel.RegisterWantedCarSignalR(truckViolationMsg.TruckPlateNumber);
            }
            _mapControlViewModel.RegisterWantedCarSignalR(truckViolationMsg.TruckPlateNumber);
        }

        public void Go(DetectedAccidentMessage detectedAccidentMessage)
        {
            _mapControlViewModel.ManageDetectedAccidentEventMessage(detectedAccidentMessage, this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol, this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol, fillColorBlueViolet);
            if (detectedAccidentMessage.IsPublishSOP)
            {
                txtTitle.Text = detectedAccidentMessage.Discription;
                txtTitle.Visibility = Visibility.Visible;

                this.Publish(new DetectedAccidentSOPMessage
                {
                    CamerasList = detectedAccidentMessage.CamerasList,
                    Discription = detectedAccidentMessage.Discription,
                    Latitude = detectedAccidentMessage.Latitude,
                    Longitude = detectedAccidentMessage.Longitude,
                    MessageId = detectedAccidentMessage.MessageId,
                    RadarsList = detectedAccidentMessage.RadarsList,
                    TowerId = detectedAccidentMessage.TowerId,
                    TowersList = detectedAccidentMessage.TowersList,
                    CreatedDate = detectedAccidentMessage.CreatedDate,
                    NotificationId = detectedAccidentMessage.NotificationId
                });
            }
        }

        public void Go(SOPMapClearObjects SOPMapClearObject)
        {
            _mapControlViewModel.ClearSOPObjects();
        }

        public void Go(SOPMapZoom SOPMapZoomObject)
        {
            ZoomOnMap(SOPMapZoomObject.Lat, SOPMapZoomObject.Lon, 5000);
        }

        public void Go(SOPMapDraw SOPMapDrawObject)
        {
            _mapControlViewModel.DrawSOPMapObject(SOPMapDrawObject);
        }

        public void Go(ShowAllLayers ShowAllLayers)
        {
            HideShowLayer(LayerTypeEnum.Patrols, true, radToggleBtnPatrols, null, ImgPatrolsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrol_selc.png");
            HideShowLayer(LayerTypeEnum.Notifications, true, radToggleBtnNotifications, null, ImgNotiLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/noti_selc.png");
            //HideShowLayer(LayerTypeEnum.Traffic, true, radToggleBtnTraffic, null, ImgTrafficLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Traffic_icons_selc.png");

        }

        public void Go(UnregisterLiveTrackingDependency UnregisterLiveTrackingDependency)
        {
            _mapControlViewModel.UnRegisterWantedCarSignalR();
        }

        public void Go(ClearAllNotificationLayer ClearNotifications)
        {
            _mapControlViewModel.ClearNotificationsLayer(ClearNotifications.MessageId, ClearNotifications.IsCancle);

            txtTitle.Text = "";
            txtTitle.Visibility = Visibility.Collapsed;
            mapTipWantedCar.Visibility = Visibility.Collapsed;

            ReturnToDefaultScaleMap();
        }

        public void Go(DrawPatrolsMessage DrawPatrols)
        {
            _mapControlViewModel.DrawPatrols(DrawPatrols);
        }
        public void Go(RepositionMessage repositionMessage)
        {
            _mapControlViewModel.DrawSugestedPatrols(repositionMessage);
        }

        #endregion

        private void ZoomOnMap(double latitude, double longitude, int scale)
        {
            // esriMapView.Map.SpatialReference = new SpatialReference(4326);
            esriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.MapPoint(longitude, latitude, new SpatialReference(4326)), scale, new TimeSpan(0, 0, 3));
        }

        //private void ZoomOnMapAsync(double latitude, double longitude, int scale)
        //{
        //    // esriMapView.Map.SpatialReference = new SpatialReference(4326);
        //    esriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.MapPoint(longitude, latitude, new SpatialReference(4326)), scale, new TimeSpan(0, 0, 2));

        //    PatrolEsriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.MapPoint(longitude, latitude, new SpatialReference(4326)), scale, new TimeSpan(0, 0, 2));
        //}

        private void ZoomToExtent(LayerTypeEnum layerType)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            ObservableCollection<Graphic> layerCol = _mapControlViewModel.GetLayerObservable(layerType);

            if (layerCol.Count == 0)
                return;

            List<double> Xs = new List<double>();
            List<double> Ys = new List<double>();
            for (int i = 0; i < layerCol.Count; i++)
            {
                if (!(layerCol[i].Geometry is Esri.ArcGISRuntime.Geometry.Polygon))
                {
                    Xs.Add(((MapPoint)layerCol[i].Geometry).X);
                    Ys.Add(((MapPoint)layerCol[i].Geometry).Y);
                }
            }

            if (layerCol.Count > 1 && Xs.Count > 0 && Ys.Count > 0)
            {
                ZoomOnMap(24.43666670, 54.45666669, 130000);
                //Esri.ArcGISRuntime.Geometry.Envelope myEnvelope = new Esri.ArcGISRuntime.Geometry.Envelope(Xs.Min(), Ys.Min(), Xs.Max(), Ys.Max(), new SpatialReference(4326));
                // esriMapView.Map.InitialViewpoint = new Esri.ArcGISRuntime.Controls.Viewpoint(myEnvelope);
                // esriMapView.SetViewAsync(myEnvelope.GetCenter(), 70000);
            }
            else
            {
                if (!(layerCol[0].Geometry is Esri.ArcGISRuntime.Geometry.Polygon))
                {
                    ZoomOnMap(((MapPoint)layerCol[0].Geometry).Y, ((MapPoint)layerCol[0].Geometry).X, 50000);
                }
            }

        }
        private void radBtn_SearchViolations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HideShowLayer(LayerTypeEnum.Violations, true, radToggleBtnViolations, null, ImgViolationsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/violation_selc.png");
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void radBtn_SearchPatrols_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HideShowLayer(LayerTypeEnum.Patrols, true, radToggleBtnPatrols, null, ImgPatrolsLayer, "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/patrol_selc.png");
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
        private void HideShowLayer(LayerTypeEnum layerType, bool IsShow, RadToggleButton radToggleBtn, Border mapTipBorder, Image img, string imgPath)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            layer.IsVisible = IsShow;
            radToggleBtn.IsChecked = IsShow;

            ImageSource imgSrc = new BitmapImage(new Uri(imgPath)); ;
            img.Source = imgSrc;

            if (IsShow)
            {
                ZoomToExtent(layerType);
            }
            if (mapTipBorder != null)
                mapTipBorder.Visibility = Visibility.Collapsed;
            else
            {
                if (layerType == LayerTypeEnum.Assets || layerType == LayerTypeEnum.AssetsRedLights || layerType == LayerTypeEnum.AssetsSpeed || layerType == LayerTypeEnum.AssetsSmartTowers)
                {
                    this.Publish(new CloseAssetPopups());
                }
                else if (layerType == LayerTypeEnum.Patrols)
                    this.Publish(new ClosePatrolPopups());
                else if (layerType == LayerTypeEnum.Incidents)
                    this.Publish(new CloseIncidentPopups());
            }
        }

        //Show Wanted Car Map Tip
        private void ShowWantedCarMapTipLayer(WantedCarMessage wantedCarMessage)
        {
            //try
            //{
            //    MapPoint myMapPoint = new MapPoint(wantedCarMessage.Longitude, wantedCarMessage.Latitude,SpatialReferences.Wgs84);
            //    //MapPoint projectedMapPoint = GeometryEngine.Project(myMapPoint, SpatialReferences.Wgs84) as MapPoint;

            //    Point screenPoint = esriMapView.LocationToScreen(myMapPoint);
            //    //Point screenPoint = new Point(800, 450);

            //    foreach (Layer layer in esriMapView.Map.Layers)
            //    {
            //        if (!(layer is GraphicsLayer) || !layer.IsVisible)
            //            continue;
            //        Graphic graphic = await ((GraphicsLayer)layer).HitTestAsync(esriMapView, screenPoint);

            //        if (graphic == null)
            //            continue;

            //        if (layer.ID == LayerTypeEnum.Notifications.ToString())
            //        {

            //        }
            //    }
            //}
            //catch { }
        }
        private void radToggleBtnViewSeachBlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuPanel.Visibility = Visibility.Collapsed;

                if (radToggleBtnViewSeachBlock.IsChecked.HasValue && radToggleBtnViewSeachBlock.IsChecked.Value == true)
                    radPanelBar.Visibility = Visibility.Visible;
                else
                    radPanelBar.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
        private void mapRadSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                esriMapView.ZoomToScaleAsync(e.NewValue);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        bool flage = true;

        private void esriMapView_ExtentChanged(object sender, EventArgs e)
        {
            // mapRadSlider.Value=esriMapView.Scale;

            //if( esriMapView.Scale <20000 && flage == true)
            //{
            //    Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Assets.ToString());
            //    layer.IsVisible = true;

            //    layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Violations.ToString());
            //    layer.IsVisible = true;

            //    flage = false;
            //}
            //else if (esriMapView.Scale >= 20000 && flage == false)
            //{
            //    Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Assets.ToString());
            //    layer.IsVisible = false;

            //    layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Violations.ToString());
            //    layer.IsVisible = false;

            //    flage = true;
            //}
        }

        private void radBtnCloseIncidents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CloseIncidentMapTip();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void CloseIncidentMapTip()
        {
            // mapTipIncidents.Visibility = Visibility.Collapsed;
        }
        private void radBtnCloseAssetsOrViolations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void radBtnClosePatrols_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // mapTipPatrols.Visibility = Visibility.Collapsed;

                // btnAssignTask.Visibility = Visibility.Visible;

                // AssignTaskGrid.Visibility = Visibility.Collapsed;
                // txtAddressToDispatch.Text = "";

                GraphicsLayer graphicsLayer = esriMapView.Map.Layers["MyGraphicsLayer"] as Esri.ArcGISRuntime.Layers.GraphicsLayer;
                if (graphicsLayer != null)
                    graphicsLayer.Graphics.Clear();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void radBtnCloseOfficers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mapTipOfficers.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            radToggleBtnViewSeachBlock.IsChecked = false;
            radPanelBar.Visibility = Visibility.Collapsed;

            if (MenuPanel.Visibility == Visibility.Collapsed)
            {
                _mapControlViewModel.CheckBtn = "Check";

                MenuPanel.Visibility = Visibility.Visible;
            }
            else if (MenuPanel.Visibility == Visibility.Visible)
            {
                _mapControlViewModel.CheckBtn = "";

                MenuPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ViewSearchPolygonOnMap()
        {
            this.Publish(new ShowSearchMapUserControl());
        }

        private void ScaleMap()
        {
            //DoubleAnimation da = (DoubleAnimation)TryFindResource("ScaleMapControl");

            //mainGridMapControl.BeginAnimation(Grid.WidthProperty, da);
        }

        private void ReturnToDefaultScaleMap()
        {
            //DoubleAnimation da = (DoubleAnimation)TryFindResource("ReturnToDefaultScaleMapControl");

            //mainGridMapControl.BeginAnimation(Grid.WidthProperty, da);
        }

        private async void HandleClick(Point screenPoint)
        {
            try
            {
                // TouchPoint screenPoint = e.GetTouchPoint(esriMapView);
                foreach (Layer layer in esriMapView.Map.Layers)
                {
                    if (!(layer is GraphicsLayer) || !layer.IsVisible)
                        continue;
                    Graphic graphic = await ((GraphicsLayer)layer).HitTestAsync(esriMapView, screenPoint);

                    if (graphic == null)
                        continue;

                    if (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes.ContainsKey("MessageId") && (!graphic.Attributes.ContainsKey("CircleType")))
                    {
                        CollapsedAllPopups();
                        this.Publish(new OpenNotification() { MessageId = graphic.Attributes["MessageId"].ToString() });
                        break;
                    }

                    else if (layer.ID == LayerTypeEnum.Violations.ToString() ||
                        (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Violations.ToString()))
                    {
                        //mapTipRadarAssetsViolations.Visibility = System.Windows.Visibility.Visible;

                        _mapControlViewModel.GetViolationDetails(int.Parse(graphic.Attributes["Id"].ToString()));

                        bool messagePublished = false;

                        if (graphic.Attributes["ViolationObj"] != null)
                        {

                            if (graphic.Attributes["ViolationObj"] is ViolationNotificationDTO)
                            {
                                var violationObj = graphic.Attributes["ViolationObj"] as ViolationNotificationDTO;

                                if (violationObj.Latitude != null && violationObj.Longitude != null)
                                {
                                    messagePublished = true;

                                    //this.Publish(new SOPGeneralMessage
                                    //{
                                    //    GeneralType = SOPSources.Violation,
                                    //    MessageId = violationObj.ViolationNotificationId.ToString(),
                                    //    OriginalObject = violationObj
                                    //});
                                }
                            }
                            else if (graphic.Attributes["ViolationObj"] is STC.Projects.ClassLibrary.DTO.ViolationNotificationDTO)
                            {
                                var violationObj = graphic.Attributes["ViolationObj"] as STC.Projects.ClassLibrary.DTO.ViolationNotificationDTO;

                                if (violationObj.Latitude != null && violationObj.Longitude != null)
                                {
                                    messagePublished = true;

                                    this.Publish(new SOPGeneralMessage
                                    {
                                        GeneralType = SOPSources.Violation,
                                        MessageId = violationObj.ViolationNotificationId.ToString(),
                                        OriginalObject = violationObj
                                    });
                                }
                            }
                            else if (graphic.Attributes["ViolationObj"] is ViolationToDraw)
                            {

                                var violationObj = graphic.Attributes["ViolationObj"] as ViolationToDraw;

                                messagePublished = true;

                                this.Publish(new SOPGeneralMessage
                                {
                                    GeneralType = SOPSources.Violation,
                                    MessageId = violationObj.ViolationObj.ViolationNotificationId.ToString(),
                                    OriginalObject = violationObj.ViolationObj
                                });

                            }


                        }
                    }
                    else if (layer.ID == LayerTypeEnum.Incidents.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Incidents.ToString()))
                    {
                        _mapControlViewModel.GetIncidentDetails(int.Parse(graphic.Attributes["Id"].ToString()));

                        //this.Publish(new OpenIncidentTip() { OriginalItem = _mapControlViewModel.IncidentDetails });
                        //OpenPopupsPanel();

                        //mapTipIncidents.Visibility = Visibility.Visible;

                        LayerTypeEnum type = layer.ID == LayerTypeEnum.Incidents.ToString() ? LayerTypeEnum.Incidents : LayerTypeEnum.Notifications;

                        bool messagePublished = false;

                        if (graphic.Attributes["IncidentObj"] != null)
                        {

                            if (graphic.Attributes["IncidentObj"] is IncidentsDTO)
                            {

                                var incidentObj = graphic.Attributes["IncidentObj"] as IncidentsDTO;

                                if (incidentObj.Latitude != null && incidentObj.Longitude != null)
                                {

                                    messagePublished = true;

                                    //this.Publish(new SOPGeneralMessage
                                    //{
                                    //    GeneralType = SOPSources.Incident,
                                    //    MessageId = incidentObj.IncidentId.ToString(),
                                    //    OriginalObject = incidentObj
                                    //});

                                }
                            }
                            else if (graphic.Attributes["IncidentObj"] is STC.Projects.ClassLibrary.DTO.IncidentsDTO)
                            {

                                var incidentObj = graphic.Attributes["IncidentObj"] as STC.Projects.ClassLibrary.DTO.IncidentsDTO;

                                if (incidentObj.Latitude != null && incidentObj.Longitude != null)
                                {
                                    messagePublished = true;

                                    this.Publish(new SOPGeneralMessage
                                    {
                                        GeneralType = SOPSources.Incident,
                                        MessageId = incidentObj.IncidentId.ToString(),
                                        OriginalObject = incidentObj
                                    });

                                }
                            }
                            else if (graphic.Attributes["IncidentObj"] is IncidentToDraw)
                            {

                                var incidentObj = graphic.Attributes["IncidentObj"] as IncidentToDraw;

                                messagePublished = true;

                                this.Publish(new SOPGeneralMessage
                                {
                                    GeneralType = SOPSources.Incident,
                                    MessageId = incidentObj.IncidentObj.IncidentId.ToString(),
                                    OriginalObject = incidentObj.IncidentObj
                                });

                            }


                            //this.Publish(new IncidentSOPMessages {
                            //    IncidentObj = graphic.Attributes["IncidentObj"]
                            //});
                        }

                        if (!messagePublished)
                        {
                            this.Publish(new SOPGeneralMessage());
                        }

                    }
                    else if (layer.ID == LayerTypeEnum.Officers.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Patrols.ToString()))
                    {
                        //mapTipOfficers.DataContext = graphic;
                        //mapTipOfficers.Visibility = System.Windows.Visibility.Visible;
                        //radTabControlOfficers.SelectedIndex = 0;
                        //mapTipIncidents.Visibility = System.Windows.Visibility.Collapsed;
                        //mapTipRadarAssetsViolations.Visibility = System.Windows.Visibility.Collapsed;
                        //mapTipPatrols.Visibility = System.Windows.Visibility.Collapsed;
                        ////this.Publish(new SOPGeneralMessage());
                        //int id = 0;
                        //if (graphic.Attributes["Id"] != null && int.TryParse(graphic.Attributes["Id"].ToString(), out id))
                        //    _mapControlViewModel.GetOfficerTipDetails(id);
                    }

                    else if (layer.ID == LayerTypeEnum.Patrols.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Patrols.ToString()))
                    {
                        if (graphic.Attributes["IsPatrol"].ToString() == "True")
                        {
                            _mapControlViewModel.SmartOfficer = null;
                            _mapControlViewModel.GetPatrolDetails(int.Parse(graphic.Attributes["Id"].ToString()));

                        }
                        else
                        {
                            _mapControlViewModel.PatrolDetails = null;
                            _mapControlViewModel.GetPatrolOfficerDetails(graphic.Attributes["PatrolCode"].ToString());
                        }

                        _lastTag = graphic.Attributes["GUID"].ToString();

                        //OpenPopupsPanel();

                        //mapTipPatrols.Visibility = Visibility.Visible;

                        //AssignTaskGrid.Tag = graphic.Attributes["GUID"].ToString();

                        //this.Publish(new SOPGeneralMessage());
                    }
                    else if (layer.ID == LayerTypeEnum.Assets.ToString() || layer.ID == LayerTypeEnum.AssetsSmartTowers.ToString() || layer.ID == LayerTypeEnum.AssetsRedLights.ToString() || layer.ID == LayerTypeEnum.AssetsSpeed.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString()))
                    {
                        _mapControlViewModel.GetAssetViolationDetails(graphic.Attributes["Id"].ToString());
                        isLastClickedAssetSmart = graphic.Attributes.ContainsKey("AssetsType") && graphic.Attributes["AssetsType"].ToString() == "Smart Towers";
                        assetStatusId = null;
                        if (graphic.Attributes.ContainsKey("AssetStatusId"))
                        {
                            int statusid;
                            if (Int32.TryParse(graphic.Attributes["AssetStatusId"].ToString(), out statusid))
                            {
                                assetStatusId = statusid;
                            }
                        }
                        //if (graphic.Attributes.ContainsKey("AssetsType") && graphic.Attributes["AssetsType"].ToString() == "Smart Towers")
                        //{
                        //OpenPopupsPanel();
                        //this.Publish(new OpenSmartAssetTip() { OriginalItem = _mapControlViewModel.AssetViolationsDetails });
                        //Smart Tower Popups
                        //mapTipSmartTowerAssetsViolations.Visibility = Visibility.Visible;

                        //mapTipSmartTowerAssetsViolations.DataContext = new ChartsViewModel();
                        //}
                        //else
                        //{
                        //this.Publish(new OpenAssetTip() { OriginalItem = _mapControlViewModel.AssetViolationsDetails });
                        //OpenPopupsPanel();

                        //Radars Popups
                        //mapTipRadarAssetsViolations.Visibility = Visibility.Visible;

                        //mapTipRadarAssetsViolations.DataContext = new ChartsViewModel();
                        //}

                        //mapTipPatrols.Visibility = Visibility.Collapsed;
                        //mapTipIncidents.Visibility = Visibility.Collapsed;

                        //this.Publish(new SOPGeneralMessage());
                    }
                }
            }
            catch
            {
            }

        }


        private void Logout_OnClick(object Sender, RoutedEventArgs E)
        {

            var lstWindows = Application.Current.Windows;

            foreach (Window window in lstWindows)
            {
                if (window is IParent)
                {
                    (window as IParent).Logout();
                    break;
                }
            }
        }

        private void esriMapView_TouchLeave(object sender, TouchEventArgs e)
        {
            HandleClick(e.GetTouchPoint(esriMapView).Position);

        }

        private void radBtnCloseAssetsOrViolations_TouchLeave(object sender, TouchEventArgs e)
        {
            //mapTipAssetsOrViolations.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void radBtnCloseOfficers_TouchLeave(object sender, TouchEventArgs e)
        {
            //mapTipOfficers.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void radBtnClosePatrols_TouchLeave(object sender, TouchEventArgs e)
        {
            //mapTipPatrols.Visibility = Visibility.Collapsed;
        }

        private void radBtnCloseIncidents_TouchLeave(object sender, TouchEventArgs e)
        {
            //mapTipIncidents.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void radBtnCloseWantedCar_Click(object sender, RoutedEventArgs e)
        {
            mapTipWantedCar.Visibility = Visibility.Collapsed;
        }

        private void radBtnCloseWantedCar_TouchLeave(object sender, TouchEventArgs e)
        {
            //mapTipWantedCar.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void btnESRIImagery_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRIImagery.Tag.ToString());
        }

        private void btnESRITOPO_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRITOPO.Tag.ToString());
        }

        private void btnESRIStreet_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRIStreet.Tag.ToString());
        }

        private void ChangeESRIBaseMap(string baseMap)
        {
            //if (esriMapView == null)
            //{
            //    return;
            //}

            //var oldBasemap = esriMapView.Map.Layers["BaseMap"];
            //esriMapView.Map.Layers.Remove(oldBasemap);

            //var newBasemap = new Esri.ArcGISRuntime.Layers.ArcGISTiledMapServiceLayer();

            //newBasemap.ServiceUri = baseMap;

            //newBasemap.ID = "BaseMap";

            //esriMapView.Map.Layers.Insert(0, newBasemap);
        }

        private void esriMapView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Publish(new AccessNotificationFromMapClick());

            SearchGrid.Visibility = Visibility.Collapsed;

            mapPatrolFlag = true;
        }

        private void esriMapView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(esriMapView) != mapPatrolPerviousPoint)
            {
                mapPatrolFlag = false;

                mapPatrolPerviousPoint = e.GetPosition(esriMapView);
            }
            HandleHover(e.GetPosition(esriMapView));
        }


        private async void HandleHover(Point screenPoint)
        {
            try
            {
                // TouchPoint screenPoint = e.GetTouchPoint(esriMapView);
                foreach (Layer layer in esriMapView.Map.Layers)
                {
                    if (!(layer is GraphicsLayer) || !layer.IsVisible)
                        continue;
                    Graphic graphic = await ((GraphicsLayer)layer).HitTestAsync(esriMapView, screenPoint);

                    if (graphic == null)
                        continue;
                    // adding popup to cars 
                    if (graphic.Attributes.ContainsKey("PatrolPlateNo"))
                    {
                        BorderPatrolsId.Visibility = Visibility.Visible;

                        if (graphic.Attributes["Id"] != null)
                        {
                            txtPatrolId.Text = graphic.Attributes["PatrolPlateNo"].ToString();
                        }
                        if (graphic.Attributes["StatusName"] != null)
                        {
                            txtStatus.Text = graphic.Attributes["StatusName"].ToString();
                        }
                        //if (graphic.Attributes["ETATime"] != null)
                        //{
                        //    txtETA.Text = Convert.ToInt32((double)graphic.Attributes["ETATime"]).ToString();
                        //}

                        if (Utility.GetLang() == "ar")
                        {
                            BorderPatrolsId.FlowDirection = FlowDirection.RightToLeft;
                            BorderPatrolsId.HorizontalAlignment = HorizontalAlignment.Right;
                            //BorderPatrolsId.SetValue(Grid.MarginProperty, new Thickness(0, (screenPoint.Y + 20), screenPoint.X - (BorderPatrolsId.ActualWidth - 10), 0));
                            //BorderPatrolsId.SetValue(Grid.MarginProperty, new Thickness(0, (screenPoint.Y - BorderPlate.ActualHeight - 5), screenPoint.X - (BorderPlate.ActualWidth / 2), 0));
                            //BorderPatrolsId.HorizontalAlignment = HorizontalAlignment.Right;

                            double right = BorderPatrolsId.ActualWidth / 2;
                            double top = BorderPatrolsId.ActualHeight + 10;

                            BorderPatrolsId.SetValue(Grid.MarginProperty, new Thickness(0, (screenPoint.Y - top), screenPoint.X - right, 0));
                        }
                        else
                        {
                            BorderPatrolsId.FlowDirection = FlowDirection.LeftToRight;
                            BorderPatrolsId.HorizontalAlignment = HorizontalAlignment.Left;
                            //BorderPatrolsId.SetValue(Grid.MarginProperty, new Thickness(screenPoint.X - 20, (screenPoint.Y + 10), 0, 0));

                            double left = BorderPatrolsId.ActualWidth / 2;
                            double top = BorderPatrolsId.ActualHeight + 10;

                            BorderPatrolsId.SetValue(Grid.MarginProperty, new Thickness(screenPoint.X - left, (screenPoint.Y - top), 0, 0));
                        }

                        return;
                    }

                    if (!graphic.Attributes.ContainsKey("PlateNumber")) continue;
                    BorderPlate.Visibility = Visibility.Visible;
                    if (graphic.Attributes["PlateNumber"] != null)
                    {
                        txtPlateNo.Text = graphic.Attributes["PlateNumber"].ToString();
                        txtViolationTime.Text = graphic.Attributes["CreatedDate"].ToString();

                        txtViolationType.Text = graphic.Attributes["ViolatedBusinessRule"].ToString();
                    }


                    if (Utility.GetLang() == "ar")
                    {
                        //BorderPlate.SetValue(Grid.MarginProperty, new Thickness(0, (screenPoint.Y + 20), screenPoint.X - (BorderPlate.ActualWidth - 10), 0));
                        BorderPlate.HorizontalAlignment = HorizontalAlignment.Right;

                        double right = BorderPlate.ActualWidth / 2;
                        double top = BorderPlate.ActualHeight + 10;

                        BorderPlate.SetValue(Grid.MarginProperty, new Thickness(0, (screenPoint.Y - top), screenPoint.X - right, 0));
                    }
                    else
                    {
                        BorderPlate.HorizontalAlignment = HorizontalAlignment.Left;
                        //BorderPlate.SetValue(Grid.MarginProperty, new Thickness(screenPoint.X - 20, (screenPoint.Y + 10), 0, 0));

                        double left = BorderPlate.ActualWidth / 2;
                        double top = BorderPlate.ActualHeight + 10;

                        BorderPlate.SetValue(Grid.MarginProperty, new Thickness(screenPoint.X - left, (screenPoint.Y - top), 0, 0));
                    }
                    return;
                }
            }
            catch (Exception exception)
            {
                Utility.WriteLog(exception);
            }

            BorderPlate.Visibility = Visibility.Collapsed;
            BorderPatrolsId.Visibility = Visibility.Collapsed;
        }



        private void esriMapView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //return;
            if (mapPatrolFlag == true && isAssignGridTaskOpen)
            {
                Point point = e.GetPosition(esriMapView);

                PatrolmapPoint = esriMapView.ScreenToLocation(point);

                var geometry = GeometryEngine.Project(PatrolmapPoint, SpatialReferences.Wgs84);
                PatrolmapPoint = geometry as MapPoint;

                //////////////////////////
                PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();

                // pictureMarkerSymbol.SetSourceAsync(new Uri("http://static.arcgis.com/images/Symbols/Shapes/RedPin1LargeB.png", UriKind.RelativeOrAbsolute));
                var iconPath = @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + "Images/Icons/RedPin1LargeB.png";
                pictureMarkerSymbol.SetSourceAsync(new Uri(iconPath));
                pictureMarkerSymbol.XOffset = 0;
                pictureMarkerSymbol.YOffset = 25;

                var graphicSymbol = new Graphic(new MapPoint(PatrolmapPoint.X, PatrolmapPoint.Y, new SpatialReference(4326)), pictureMarkerSymbol);

                graphicSymbol.ZIndex = 3;

                GraphicsLayer graphicsLayer = esriMapView.Map.Layers["MyGraphicsLayer"] as Esri.ArcGISRuntime.Layers.GraphicsLayer;

                if (graphicsLayer != null)
                {
                    graphicsLayer.Graphics.Clear();
                    graphicsLayer.Graphics.Add(graphicSymbol);
                    esriMapView.Map.Layers.Add(graphicsLayer);
                }
                /////////////////////////

                this.Publish(new PopUpAddress() { Address = "Long: " + PatrolmapPoint.X.ToString("00.00000") + "    Lat: " + PatrolmapPoint.Y.ToString("00.00000") });

            }
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            esriMapView.ZoomAsync(1.1);
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            esriMapView.ZoomAsync(0.9);
        }

        private void btnReturnToDefaultView_Click(object sender, RoutedEventArgs e)
        {
            ZoomOnMap(24.43666670, 54.45666669, 130000);
        }

        private void radBtnCloseRadarAssetsViolations_Click(object sender, RoutedEventArgs e)
        {
            //mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;

            //ChangeRadarSpeedPanel.Visibility = Visibility.Collapsed;

            //Clear Textbox
            //txtChangeRadar.Text = "";
        }

        private void hyperLinkTextChangeRadarSpeed_Click(object sender, RoutedEventArgs e)
        {
            //ChangeRadarSpeedPanel.Visibility = Visibility.Visible;
        }

        private void radBtnCloseSmartTowerAssetsViolations_Click(object sender, RoutedEventArgs e)
        {
            //mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;

            //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Collapsed;

            //Clear Textbox
            //txtChangeSmartTower.Text = "";
        }

        private void hyperLinkTextChangeSmartTowerSpeed_Click(object sender, RoutedEventArgs e)
        {
            //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Visible;
        }

        private void hyperLinkTextChangeSmartTowerMessage_Click(object sender, RoutedEventArgs e)
        {
            //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Visible;
        }

        private void btnChangeSmartTowerSpeed_Click(object sender, RoutedEventArgs e)
        {
            //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Collapsed;

            //Clear Textbox
            //txtChangeSmartTower.Text = "";
        }

        private void btnChangeRadarSpeed_Click(object sender, RoutedEventArgs e)
        {
            //ChangeRadarSpeedPanel.Visibility = Visibility.Collapsed;

            //Clear Textbox
            //txtChangeRadar.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                //8781
                GetVehicleViolationsDetails();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtSearch.Text != "")
                {
                    GetVehicleViolationsDetails();
                }
            }
        }

        private void GetVehicleViolationsDetails()
        {
            _mapControlViewModel.GetVehicleViolationDetails(txtSearch.Text);

            SearchResultGrid.Visibility = Visibility.Visible;
        }

        public void Go(AssignPatrol message)
        {
            if (message.Address != "" && PatrolmapPoint != null)
            {
                long result = 0;
                bool officerResult = false;
                if (_mapControlViewModel.SmartOfficer == null && _mapControlViewModel.PatrolDetails != null)
                    result = _mapControlViewModel.SendTaskToPatrol(Guid.Parse(message.Tag.ToString()), message.TaskMessage, DateTime.Now, PatrolmapPoint.Y, PatrolmapPoint.X);
                else if (_mapControlViewModel.PatrolDetails == null && _mapControlViewModel.SmartOfficer != null)
                    officerResult = _mapControlViewModel.SendTaskToOfficer(Guid.Parse(message.Tag.ToString()), message.TaskMessage, DateTime.Now, PatrolmapPoint.Y, PatrolmapPoint.X, this.GetCurrentUserId());

                if (result > 0 || officerResult)
                {
                    isAssignGridTaskOpen = false;
                    var msgBox = new MessageBoxUserControl("تم إسناد المهمة", false);

                    msgBox.Owner = Window.GetWindow(this);
                    msgBox.ShowDialog();
                    msgBox.GetResult();
                    CollapsedAllPopups();
                }
                else
                {
                    var msgBox = new MessageBoxUserControl("خطأ. لم يتم إسناد المهمة", false);

                    msgBox.Owner = Window.GetWindow(this);
                    msgBox.ShowDialog();
                    msgBox.GetResult();
                }
                DeleteLayer("MyGraphicsLayer");
            }
            else
            {
                var msgBox = new MessageBoxUserControl("من فضلك إختر الموقع المراد إرساله", false);

                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();
                msgBox.GetResult();
            }
        }

        public void DeleteLayer(string LayerName)
        {
            try
            {
                GraphicsLayer graphicsLayer = esriMapView.Map.Layers[LayerName] as Esri.ArcGISRuntime.Layers.GraphicsLayer;
                if (graphicsLayer == null)
                    return;
                graphicsLayer.Graphics.Clear();
                esriMapView.Map.Layers.Remove(graphicsLayer);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }
        }

        private void radBtnCloseSearchResultGrid_Click(object sender, RoutedEventArgs e)
        {
            SearchResultGrid.Visibility = Visibility.Collapsed;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var violationNotificationDTO = (LstVehicleViolationsDetails.SelectedItem as ViolationNotificationDTO);
            if (violationNotificationDTO.Latitude.HasValue && violationNotificationDTO.Longitude.HasValue)
                ZoomOnMap(violationNotificationDTO.Latitude.Value, violationNotificationDTO.Longitude.Value, 6125);

            SearchResultGrid.Visibility = Visibility.Collapsed;
        }

        private void mapTipIncidentsStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _movePrePoint = e.GetPosition(null);

            e.Handled = true;
        }

        private void mapTipIncidentsStackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(sender as UIElement);

                Thickness margin = (sender as UIElement).ParentOfType<Border>().Margin;

                _moveCurrentPoint = e.GetPosition(null);

                margin.Left += Utility.GetLang() == "ar" ? -(_moveCurrentPoint.X - _movePrePoint.X) : (_moveCurrentPoint.X - _movePrePoint.X);

                margin.Top += (_moveCurrentPoint.Y - _movePrePoint.Y);

                ((sender as UIElement).ParentOfType<Border>()).Margin = margin;

                _movePrePoint = _moveCurrentPoint;
            }
            else
            {
                Mouse.Capture(null);
            }

        }

        public void Go(ClosedPopUpMessage message)
        {
            _mapControlViewModel.AssetViolationsDetails = null;
            isAssignGridTaskOpen = false;
            DeleteLayer("MyGraphicsLayer");
            //ClosePopupsPanel();
        }

        private void OpenPopupsPanel()
        {
            CollapsedAllPopups();

            var storyBoard = (Storyboard)TryFindResource("OpenPopupsPanel");

            storyBoard.Begin();
        }

        //private void ClosePopupsPanel()
        //{
        //    var storyBoard = (Storyboard)TryFindResource("ClosePopupsPanel");

        //    storyBoard.Begin();
        //}

        private void CollapsedAllPopups()
        {
            //mapTipIncidents.Visibility = Visibility.Collapsed;

            //mapTipPatrols.Visibility = Visibility.Collapsed;

            //mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;

            //mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;

            //btnAssignTask.Visibility = Visibility.Visible;

            //AssignTaskGrid.Visibility = Visibility.Collapsed;
            this.Publish(new CloseAllPopups());
            isAssignGridTaskOpen = false;
            DeleteLayer("MyGraphicsLayer");
            //txtAddressToDispatch.Text = "";

            //txtAssignTaskMessage.Text = "";
        }

        private void tglBtnIncidentFilter_Checked(object sender, RoutedEventArgs e)
        {
            brdrIncidentFilterPopUP.Visibility = Visibility.Visible;

            //brdrAccidentsSearchResultGrid.Visibility = Visibility.Visible;
            //OpenAccidentsSearchResultGrid();
        }

        private void tglBtnIncidentFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            _mapControlViewModel.ClearFilteredAccidents();
            //brdrIncidentFilterPopUP.Visibility = Visibility.Collapsed;
            brdrAccidentsSearchResultGrid.Visibility = Visibility.Collapsed;
        }

        private void btnCloseIncidentFilterPopUP_Click(object sender, RoutedEventArgs e)
        {
            brdrIncidentFilterPopUP.Visibility = Visibility.Collapsed;
            brdrAccidentsSearchResultGrid.Visibility = Visibility.Collapsed;
            CloseAccidentsSearchResultGrid();
            tglBtnIncidentFilter.IsChecked = false;
        }

        private void btnSearchAccidents_Click(object sender, RoutedEventArgs e)
        {
            _mapControlViewModel.SearchAccidents();
            brdrAccidentsSearchResultGrid.Visibility = Visibility.Visible;
            OpenAccidentsSearchResultGrid();
            radToggleBtnShowSearchResult.IsChecked = true;

        }


        private void radToggleBtnShowOnMap_Click(object sender, RoutedEventArgs e)
        {
            if (radToggleBtnShowOnMap.IsChecked == true)
            {
                _mapControlViewModel.ShowOnMapAccidents();

                if (_mapControlViewModel.AccidentSearchResultsUI != null && _mapControlViewModel.AccidentSearchResultsUI.Count > 0)
                {
                    brdrIncidentFilterPopUP.Visibility = Visibility.Collapsed;
                    //brdrAccidentsSearchResultGrid.Visibility = Visibility.Collapsed;
                    CloseAccidentsSearchResultGrid();
                    if (radToggleBtnIncidents.IsChecked == false)
                    {
                        radToggleBtnIncidents.IsChecked = true;

                        string img = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accidents.png";

                        string imgSelc = "pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/accident_selc.png";

                        HideShowLayer(LayerTypeEnum.Incidents, true, radToggleBtnIncidents, null, ImgIncidentsLayer, imgSelc);

                        //_mapControlViewModel.AccidentSearchResultsUI.Clear();

                    }
                }
                radToggleBtnShowOnMap.IsChecked = false;
                radToggleBtnShowSearchFilter.IsChecked = false;
                radToggleBtnShowSearchResult.IsChecked = false;
            }
        }

        private async void CloseAccidentsSearchResultGrid()
        {
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("AccidentsSearchResultGridClose");
            sb.Begin();
            await Task.Delay(1000);
        }

        private async void OpenAccidentsSearchResultGrid()
        {
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("AccidentsSearchResultGridOpen");
            sb.Begin();
            await Task.Delay(1000);
        }

        private void radToggleBtnShowSearchFilter_Click(object sender, RoutedEventArgs e)
        {
            if (radToggleBtnShowSearchFilter.IsChecked.HasValue && radToggleBtnShowSearchFilter.IsChecked == true)
                brdrIncidentFilterPopUP.Visibility = Visibility.Visible;
            else if (radToggleBtnShowSearchFilter.IsChecked.HasValue && radToggleBtnShowSearchFilter.IsChecked == false)
                brdrIncidentFilterPopUP.Visibility = Visibility.Collapsed;
        }

        private void radToggleBtnShowSearchResult_Click(object sender, RoutedEventArgs e)
        {
            if (radToggleBtnShowSearchResult.IsChecked.HasValue && radToggleBtnShowSearchResult.IsChecked == true)
                OpenAccidentsSearchResultGrid();
            else if (radToggleBtnShowSearchResult.IsChecked.HasValue && radToggleBtnShowSearchResult.IsChecked == false)
                CloseAccidentsSearchResultGrid();
        }

        private void txtBlkIncidentNum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
            {
                if (sender as TextBlock != null)
                {

                    if (((sender as TextBlock).Tag as STC.Projects.WPFControlLibrary.MapControl.ViewModel.IncidentDetailsDtoUI != null))
                    {
                        var incidentDto = ((sender as TextBlock).Tag as STC.Projects.WPFControlLibrary.MapControl.ViewModel.IncidentDetailsDtoUI).AccidentDetailDto;

                        _mapControlViewModel.GetIncidentDetails(Convert.ToInt32(incidentDto.IncidentId));

                        this.Publish(new SOPGeneralMessage());
                    }


                }

            }

        }

        private void esriMapView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.Publish(new AccessNotificationFromMapClick());

            //SearchGrid.Visibility = Visibility.Collapsed;

            mapPatrolFlag = true;
        }


    }
}