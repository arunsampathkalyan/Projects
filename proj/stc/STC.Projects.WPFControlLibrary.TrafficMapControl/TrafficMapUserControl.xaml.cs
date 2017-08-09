using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.WPFControlLibrary.TrafficMapControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
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
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.TrafficMapControl
{
    /// <summary>
    /// Interaction logic for TrafficMapUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "TrafficMapUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TrafficMapUserControl : UserControl, IUserControl
    {
        TrafficMapControlViewModel _trafficMapControlViewModel = null;

        Color fillColorLightCoral;
        Color fillColorBlueViolet;

        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 5, 0) };
        int trafficTimeInterval = 0;

        #region Constractor
        public TrafficMapUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            esriMapView.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            //mapTipAssetsOrViolations.FlowDirection = Utility.GetLang() == "ar" ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;

            fillColorLightCoral = Colors.LightYellow;
            fillColorLightCoral.A = 60;
            fillColorBlueViolet = Colors.LightCoral;
            fillColorBlueViolet.A = 80;

            (this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol).Color = fillColorBlueViolet;
            (this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol).Color = fillColorLightCoral;

            ZoomOnMap(24.43666670, 54.45666669, 130000);

            _trafficMapControlViewModel = new TrafficMapControlViewModel();

            AddGrphicLayer(LayerTypeEnum.Traffic);

            //radTabControlViolationsOrAssets.DataContext = _trafficMapControlViewModel;

            CultureInfo cul = Utility.GetLang() == "ar" ? new CultureInfo("ar-Eg") : new CultureInfo(Utility.GetLang());

            //DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            //{

            //    this.TxtTime.Text = DateTime.Now.ToString("t", cul);

            //    this.TxtDate.Text = DateTime.Now.ToString("D", cul);

            //}, this.Dispatcher);

            SetDefaultTimeforSlider();

            timer.Tick += timer_Tick;
            timer.Start();

            this.Loaded += TrafficMapUserControl_Loaded;
            this.Unloaded += TrafficMapUserControl_Unloaded;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblViewCurrentTimeVal.Content = DateTime.Now.ToLongTimeString().Replace(" AM", "").Replace(" PM", "");
        }

        private void TrafficMapUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            esriMapView.Map.Layers.Clear();

            timer.Stop();
            timer.Tick -= timer_Tick;
        }
        #endregion

        #region Event Handelers

        void TrafficMapUserControl_Loaded(object sender, EventArgs e)
        {
            try
            {
                var vm = DataContext as TrafficMapControlViewModel;

                if (vm != null)
                    vm.CurrentUsername = this.GetCurrentUsername();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void esriMapView_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void esriMapView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnESRITOPO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeESRIBaseMap(btnESRITOPO.Tag.ToString());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnESRIStreet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeESRIBaseMap(btnESRIStreet.Tag.ToString());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnESRIImagery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeESRIBaseMap(btnESRIImagery.Tag.ToString());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        #endregion

        #region Methods
        private void HideShowLayer(LayerTypeEnum layerType, bool IsShow, RadToggleButton radToggleBtn, Border mapTipBorder)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            layer.IsVisible = IsShow;
            radToggleBtn.IsChecked = IsShow;

            if (IsShow)
            {
                ZoomToExtent(layerType);
            }
            if (mapTipBorder != null)
                mapTipBorder.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ZoomToExtent(LayerTypeEnum layerType)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            ObservableCollection<Graphic> layerCol = _trafficMapControlViewModel.GetLayerObservable(layerType);

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
            }
            else
            {
                if (!(layerCol[0].Geometry is Esri.ArcGISRuntime.Geometry.Polygon))
                {
                    ZoomOnMap(((MapPoint)layerCol[0].Geometry).Y, ((MapPoint)layerCol[0].Geometry).X, 50000);
                }
            }

        }

        private void ZoomOnMap(double latitude, double longitude, int scale)
        {
            esriMapView.SetViewAsync(new Esri.ArcGISRuntime.Geometry.MapPoint(longitude, latitude, new SpatialReference(4326)), scale, new TimeSpan(0, 0, 2));
        }

        public void AddGrphicLayer(LayerTypeEnum layerTypeEnum)
        {
            try
            {
                var url = "http://192.168.20.244:8080/geoserver/wms/?CQL_FILTER=FORE=0";
                if (System.Configuration.ConfigurationSettings.AppSettings["OptimaURL"] != null)
                    url = System.Configuration.ConfigurationSettings.AppSettings["OptimaURL"];

                //string[] splitArr = url.Split(new char[] { '=' });

                //if (splitArr != null && splitArr.Length > 0)
                //{
                //    splitArr[splitArr.Length - 1] = trafficTimeInterval.ToString();

                //}

                int index = url.LastIndexOfAny(new char[] { '=' });

                if (index != -1)
                    url = url.Substring(0, index + 1) + trafficTimeInterval.ToString();


                var tiledUri = new Uri(url);

                //create a new WMS layer with the above URL 
                var agsTiledLayer = new WmsLayer(tiledUri);
                agsTiledLayer.ID = LayerTypeEnum.Traffic.ToString();
                agsTiledLayer.ShowLegend = true;
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

                Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Traffic.ToString());
                //esriMapView.Map.Layers.Clear();
                if (layer != null)
                {
                    layer = agsTiledLayer;
                }
                else
                {
                    //add the new layer to the map
                    esriMapView.Map.Layers.Add(agsTiledLayer);
                }


            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                //MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }



            //_trafficMapControlViewModel.AddGraphicsToLayersGraphicsDictionary(layerTypeEnum);
            //GraphicsLayer graphicsLayer = new GraphicsLayer();
            //graphicsLayer.ID = layerTypeEnum.ToString();
            //esriMapView.Map.Layers.Add(graphicsLayer);
            //System.Windows.Data.Binding binding = new System.Windows.Data.Binding(string.Format("LayersGraphicsDictionary[{0}]", layerTypeEnum.ToString()));
            //binding.Source = _trafficMapControlViewModel;
            //binding.Mode = System.Windows.Data.BindingMode.TwoWay;
            //System.Windows.Data.BindingOperations.SetBinding(graphicsLayer, GraphicsLayer.GraphicsSourceProperty, binding);
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

        #endregion

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                esriMapView.ZoomAsync(1.1);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                esriMapView.ZoomAsync(0.9);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnReturnToDefaultView_Click(object sender, RoutedEventArgs e)
        {
            ZoomOnMap(24.43666670, 54.45666669, 130000);
        }


        #region Slider

        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            //if (btnPlayPause.Tag.ToString() == "Play")
            //{
            //    btnPlayPause.Tag = "Pause";
            //    this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            //    timer.Stop();
            //}
            //else if (btnPlayPause.Tag.ToString() == "Pause")
            //{
            //    if (ViolationSlider.Value <= (ViolationSlider.Maximum / 2) && _incidentsMapControlViewModel.HistoricalCategoryViolationList != null && _incidentsMapControlViewModel.HistoricalCategoryViolationList.Count > 0)
            //    {
            //        btnPlayPause.Tag = "Play";
            //        this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_pause.png");

            //        //sliderPlayMode = "Play";
            //        timer.Start();


            //    }
            //    //else
            //    //    timer.Stop();

            //}
        }

        private void SetDefaultTimeforSlider()
        {
            ViolationSlider.Minimum = 0;
            ViolationSlider.Maximum = 3;
            ViolationSlider.Value = 0;

            lblViewCurrentTime.Content = Properties.Resources.strCurrentTime; ;
            lblViewCurrentTimeVal.Content = DateTime.Now.ToLongTimeString().Replace(" AM", "").Replace(" PM", "");

            //lblViewEndTime.Content = Properties.Resources.strEndTime;
            //lblViewEndTimeVal.Content = DateTime.Now.AddMinutes(45).ToLongTimeString().Replace(" AM", "").Replace(" PM", "");
        }

        #endregion

        private void ViolationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Stop();
            ViolationSlider.Value = (int)ViolationSlider.Value;

            switch ((int)ViolationSlider.Value)
            {
                case 0:
                    {
                        trafficTimeInterval = 0;
                        AddGrphicLayer(LayerTypeEnum.Traffic);
                        lblViewCurrentTimeVal.Content = DateTime.Now.ToLongTimeString().Replace(" AM", "").Replace(" PM", "");
                        timer.Start();
                        break;
                    }
                case 1:
                    {
                        trafficTimeInterval = 900;
                        AddGrphicLayer(LayerTypeEnum.Traffic);
                        break;
                    }

                case 2:
                    {
                        trafficTimeInterval = 1800;
                        AddGrphicLayer(LayerTypeEnum.Traffic);
                        break;
                    }
                case 3:
                    {
                        trafficTimeInterval = 2700;
                        AddGrphicLayer(LayerTypeEnum.Traffic);
                        break;
                    }
            }
        }

        private void radToggleBtnEsriMap_Click(object sender, RoutedEventArgs e)
        {
            radToggleBtnGoogleMap.IsChecked = false;
            radToggleBtnEsriMap.IsChecked = true;

            googleMapsBrowser.Visibility = Visibility.Collapsed;
            brdrBackgrond.Visibility = Visibility.Collapsed;
            esriMapView.Visibility = Visibility.Visible;
            ViolationSlider.Visibility = Visibility.Visible;
        }

        private void radToggleBtnGoogleMap_Click(object sender, RoutedEventArgs e)
        {

            radToggleBtnEsriMap.IsChecked = false;
            radToggleBtnGoogleMap.IsChecked = true;

            esriMapView.Visibility = Visibility.Collapsed;
            ViolationSlider.Visibility = Visibility.Collapsed;
            googleMapsBrowser.Visibility = Visibility.Visible;
            brdrBackgrond.Visibility = Visibility.Visible;

        }

        private void googleMapsBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            String sURL = AppDomain.CurrentDomain.BaseDirectory + "html/map.html";
            googleMapsBrowser.Navigate(new Uri(sURL));

            ((WebBrowser)sender).ObjectForScripting = new HtmlInteropInternalTestClass();
        }

        private void googleMapsBrowser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void googleMapsBrowser_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

    }

    // Object used for communication from JS -> WPF
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class HtmlInteropInternalTestClass
    {
        public void endDragMarkerCS(Decimal Lat, Decimal Lng)
        {
            //((MainWindow)Application.Current.MainWindow).tbLocation.Text = Math.Round(Lat, 5) + "," + Math.Round(Lng, 5);
        }
    }
}
