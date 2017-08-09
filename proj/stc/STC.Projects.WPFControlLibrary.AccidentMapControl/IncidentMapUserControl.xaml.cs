using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.WPFControlLibrary.IncidentsMapControl.ViewModel;
using STC.Projects.WPFControlLibrary.IncidentsMapControl.Properties;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.IncidentsMapControl
{
    /// <summary>
    /// Interaction logic for AccidentsMapUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "IncidentMapUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class IncidentMapUserControl : UserControl, IUserControl
    {
        IncidentsMapControlViewModel _incidentsMapControlViewModel = null;

        Color fillColorLightCoral;
        Color fillColorBlueViolet;

        private Point _movePrePoint;
        private Point _moveCurrentPoint;


        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 5) };

        int interval = 5;
        string sliderPlayMode = "Play";

        string selectedViewCategory = "Daily";
        public IncidentMapUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            esriMapView.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            mapTipIncidents.FlowDirection = Utility.GetLang() == "ar" ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;

            fillColorLightCoral = Colors.LightYellow;
            fillColorLightCoral.A = 60;
            fillColorBlueViolet = Colors.LightCoral;
            fillColorBlueViolet.A = 80;

            (this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol).Color = fillColorBlueViolet;
            (this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol).Color = fillColorLightCoral;

            ZoomOnMap(24.43666670, 54.45666669, 130000);

            _incidentsMapControlViewModel = new IncidentsMapControlViewModel();

            this.DataContext = _incidentsMapControlViewModel;

            AddGrphicLayer(LayerTypeEnum.Incidents);

            CultureInfo cul = Utility.GetLang() == "ar" ? new CultureInfo("ar-Eg") : new CultureInfo(Utility.GetLang());


            if (_incidentsMapControlViewModel.YearValueColl != null && _incidentsMapControlViewModel.YearValueColl.Count > 0)
            {
                _incidentsMapControlViewModel.YearValue = _incidentsMapControlViewModel.YearValueColl[0];
                EnableDisablePreviousNextButtons(_incidentsMapControlViewModel.YearValue, _incidentsMapControlViewModel.YearValueColl[_incidentsMapControlViewModel.YearValueColl.Count - 1], _incidentsMapControlViewModel.YearValueColl[0]);
            }




            timer.Tick += timer_Tick;




            ShowDefaultValuesAccordingtoFilterCategory();

            this.Loaded += AccidentsMapUserControl_Loaded;
            this.Unloaded += IncidentMapUserControl_Unloaded;
        }

        void IncidentMapUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in esriMapView.Map.Layers)
            {
                try
                {
                    System.Windows.Data.BindingOperations.ClearAllBindings(item);
                }
                catch
                {

                }
            }
            esriMapView.Map.Layers.Clear();
            timer.Stop();
            timer.Tick -= timer_Tick;
        }

        #region Event Handelers


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

        void AccidentsMapUserControl_Loaded(object sender, EventArgs e)
        {
            try
            {
                var vm = DataContext as IncidentsMapControlViewModel;

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
            try
            {
                HandleClick(e.GetPosition(esriMapView));
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void esriMapView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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


                    if (layer.ID == LayerTypeEnum.Incidents.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Incidents.ToString()))
                    {
                        _incidentsMapControlViewModel.GetIncidentDetails(int.Parse(graphic.Attributes["Id"].ToString()));

                        OpenPopupsPanel();

                        mapTipIncidents.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch
            {
            }

        }

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
            ObservableCollection<Graphic> layerCol = _incidentsMapControlViewModel.GetLayerObservable(layerType);

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
            _incidentsMapControlViewModel.AddGraphicsToLayersGraphicsDictionary(layerTypeEnum);
            GraphicsLayer graphicsLayer = new GraphicsLayer();
            graphicsLayer.ID = layerTypeEnum.ToString();
            esriMapView.Map.Layers.Add(graphicsLayer);
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding(string.Format("LayersGraphicsDictionary[{0}]", layerTypeEnum.ToString()));
            binding.Source = _incidentsMapControlViewModel;
            binding.Mode = System.Windows.Data.BindingMode.OneWay;
            System.Windows.Data.BindingOperations.SetBinding(graphicsLayer, GraphicsLayer.GraphicsSourceProperty, binding);
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

        private void CloseIncidentMapTip()
        {
            mapTipIncidents.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion



        #region Slider

        private void HideShowLayer(LayerTypeEnum layerType, bool IsShow)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            layer.IsVisible = IsShow;

            if (IsShow)
            {
                ZoomToExtent(layerType);
            }

            //mapTipRadarAssetsViolations.Visibility = System.Windows.Visibility.Collapsed;
            //mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;

        }
        static internal ImageSource GetImageSourceFromResource(string path)
        {
            Uri oUri = new Uri("pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + path, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }
        private void GetSelectedCategoryData(string category)
        {



            //HideShowLayer(LayerTypeEnum.Incidents, false);


            //GetIncidenetForCurrentValue();

            //switch (category)
            //{
            //    case "Daily":
            //        {

            //            GetIncidenetForCurrentValue();
            //            break;
            //        }
            //    case "Monthly":
            //        {

            //            _incidentsMapControlViewModel.GetHistoricalIncidentsListByDate();
            //            break;
            //        }

            //    case "Weekly":
            //        {
            //            _incidentsMapControlViewModel.GetHistoricalIncidentsListByDate();
            //            break;
            //        }
            //}

            //if (_incidentsMapControlViewModel.MonthStartEndDateList != null && _incidentsMapControlViewModel.MonthStartEndDateList.Count > 0)
            //{
            grdViolationSlider.Visibility = Visibility.Visible;


            btnPlayPause.Tag = "Pause";
            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            //HideShowLayer(LayerTypeEnum.Violations, false);
            //HideShowLayer(LayerTypeEnum.HistoricalViolations, true);



            //Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Violations.ToString());
            //if (layer != null)
            //{
            //    (layer as GraphicsLayer).Graphics.Clear();
            //}

            //_incidentsMapControlViewModel.SliderMinimum = 0;

            ////ViolationSlider.Minimum = _incidentsMapControlViewModel.SliderMinimum;


            //interval = 1;
            ////ViolationSlider.Maximum = _incidentsMapControlViewModel.SliderMaximum;
            //_incidentsMapControlViewModel.SliderCurrValue = _incidentsMapControlViewModel.SliderMaximum / 2;
            ////ViolationSlider.Value = _incidentsMapControlViewModel.SliderCurrValue;


            _incidentsMapControlViewModel.GetAndPopulateCurrentIterationIncidents((int)_incidentsMapControlViewModel.SliderCurrValue);

            //PopulateSelectedCategoryHistoricalDataForCurrentIteration(selectedViewCategory, (int)ViolationSlider.Value);


            //}
            //else
            //{
            //    //Message to be given as no data available
            //}
        }

        private void GetIncidenetForCurrentValue()
        {
            switch (_incidentsMapControlViewModel.PeriodCategory)
            {
                case ServiceLayerReference.PeriodCategory.Daily:
                    {
                        break;
                    }
                case ServiceLayerReference.PeriodCategory.Weekly:
                    {
                        break;
                    }
                case ServiceLayerReference.PeriodCategory.Monthly:
                    {
                        if (_incidentsMapControlViewModel.StartDate == null || _incidentsMapControlViewModel.StartDate < _incidentsMapControlViewModel.FirstIncidentDate)
                            _incidentsMapControlViewModel.StartDate = _incidentsMapControlViewModel.FirstIncidentDate;

                        IncidentsMapControlViewModel.DateRangeStruct dateSturct = _incidentsMapControlViewModel.DateRange(IncidentsMapControlViewModel.DateRangeOptions.Month, _incidentsMapControlViewModel.StartDate);
                        _incidentsMapControlViewModel.StartDate = (dateSturct.startDate >= _incidentsMapControlViewModel.FirstIncidentDate) ? dateSturct.startDate : _incidentsMapControlViewModel.FirstIncidentDate;
                        _incidentsMapControlViewModel.EndDate = dateSturct.endDate <= DateTime.Now ? dateSturct.endDate : DateTime.Now;
                        _incidentsMapControlViewModel.GetHistoricalIncidentsListByDate();

                        _incidentsMapControlViewModel.StartDate.AddMonths(1);
                        break;
                    }
                case ServiceLayerReference.PeriodCategory.Yearly:
                    {
                        break;
                    }
            }
        }

        private void StartTimer()
        {
            sliderPlayMode = "Play";
            timer_Tick(null, EventArgs.Empty);
            timer.Start();
        }


        private void ResetSliderToDefault()
        {
            interval = 1;

            _incidentsMapControlViewModel.SliderMinimum = 0;
            _incidentsMapControlViewModel.SliderMaximum = 2;
            _incidentsMapControlViewModel.SliderCurrValue = _incidentsMapControlViewModel.SliderMinimum + 1;

            //ViolationSlider.Minimum = _incidentsMapControlViewModel.SliderMinimum;
            //ViolationSlider.Maximum = _incidentsMapControlViewModel.SliderMaximum;
            //ViolationSlider.Value = _incidentsMapControlViewModel.SliderCurrValue;


            //if (_incidentsMapControlViewModel.HistoricalCategoryViolationList != null && _incidentsMapControlViewModel.HistoricalCategoryViolationList.Count > 0)
            //{
            //    ViolationSlider.Maximum = 2 * (_incidentsMapControlViewModel.HistoricalCategoryViolationList.Count - 1);
            //    ViolationSlider.Value = ViolationSlider.Maximum / 2;
            //}

            //if (_incidentsMapControlViewModel.HistoricalIncidentList != null && _incidentsMapControlViewModel.HistoricalIncidentList.Count() > 0)
            //{
            //    //ViolationSlider.Maximum = 2 * (_incidentsMapControlViewModel.HistoricalCategoryViolationList.Count - 1);
            //    //ViolationSlider.Value = ViolationSlider.Maximum / 2;


            //    _incidentsMapControlViewModel.SliderMaximum = 2 * (_incidentsMapControlViewModel.HistoricalIncidentList.Count() - 1);
            //    _incidentsMapControlViewModel.SliderCurrValue = _incidentsMapControlViewModel.SliderMaximum / 2;

            //    //ViolationSlider.Maximum = _incidentsMapControlViewModel.SliderMaximum;
            //    //ViolationSlider.Value = _incidentsMapControlViewModel.SliderCurrValue;
            //}
        }







        private void btnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (btnPlayPause.Tag.ToString() == "Play")
            {
                btnPlayPause.Tag = "Pause";
                this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
                timer.Stop();
            }
            else if (btnPlayPause.Tag.ToString() == "Pause")
            {
                if (_incidentsMapControlViewModel.SliderCurrValue <= (_incidentsMapControlViewModel.SliderMaximum / 2))
                {
                    btnPlayPause.Tag = "Play";
                    this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_pause.png");

                    //sliderPlayMode = "Play";
                    timer.Start();


                }
                //else
                //    timer.Stop();

            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            switch (sliderPlayMode)
            {
                case "Play":
                    {

                        if (_incidentsMapControlViewModel.SliderCurrValue - 1 >= _incidentsMapControlViewModel.SliderMinimum)
                        {
                            _incidentsMapControlViewModel.SliderCurrValue -= 1;

                            _incidentsMapControlViewModel.GetAndPopulateCurrentIterationIncidents((int)_incidentsMapControlViewModel.SliderCurrValue);
                            //ViolationSlider.Value = _incidentsMapControlViewModel.SliderCurrValue;


                            //string currentItem = _incidentsMapControlViewModel.StartDate.ToString("MMM yy");
                            //lblViewCategSliderValue.Content = _incidentsMapControlViewModel.CurrentDateRange;
                            //ViolationSlider.ToolTip = _incidentsMapControlViewModel.CurrentDateRange;

                        }
                        else
                        {
                            timer.Stop();
                            btnPlayPause.Tag = "Pause";
                            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
                            sliderPlayMode = "PlayBack";
                        }



                        break;
                    }
                case "PlayBack":
                    {
                        if (_incidentsMapControlViewModel.SliderCurrValue + 1 <= (_incidentsMapControlViewModel.SliderMaximum / 2))
                        {
                            _incidentsMapControlViewModel.SliderCurrValue += 1;
                            _incidentsMapControlViewModel.GetAndPopulateCurrentIterationIncidents((int)_incidentsMapControlViewModel.SliderCurrValue);
                        }
                        else
                        {
                            timer.Stop();
                            btnPlayPause.Tag = "Pause";
                            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
                            sliderPlayMode = "Play";
                        }
                        break;
                    }
            }

        }
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            //radToggleBtnViewSeachBlock.IsChecked = false;
            //radPanelBar.Visibility = System.Windows.Visibility.Collapsed;

            if (MenuPanelFilter.Visibility == Visibility.Collapsed)
            {
                //_mapControlViewModel.CheckBtn = "Check";

                MenuPanelFilter.Visibility = Visibility.Visible;
            }
            else if (MenuPanelFilter.Visibility == Visibility.Visible)
            {
                //_mapControlViewModel.CheckBtn = "";

                MenuPanelFilter.Visibility = Visibility.Collapsed;
            }

        }
        private void radTglFilBtnCount_Click(object sender, RoutedEventArgs e)
        {
            _incidentsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;
        }

        private void radTglFilBtnDays_Click(object sender, RoutedEventArgs e)
        {
            //grdCurrentDatetime.Visibility = Visibility.Visible;
            //grdCurrentMonthInfo.Visibility = Visibility.Collapsed;
            //grdCurrentWeekInfo.Visibility = Visibility.Collapsed;

            //chartAreaDetails.Visibility = Visibility.Visible;
            //chartAreaMonthly.Visibility = Visibility.Collapsed;
            //chartAreaWeekly.Visibility = Visibility.Collapsed;

            //HistoricalDataPanel.Visibility = Visibility.Visible;
            //HistoricalDataPanelMonthly.Visibility = Visibility.Collapsed;
            //HistoricalDataPanelWeekly.Visibility = Visibility.Collapsed;

            selectedViewCategory = "Daily";

            lblViewCategSlider.Content = Properties.Resources.strDailyView;
            //lblViewCategSliderValue.Content = "";


            _incidentsMapControlViewModel.CheckBtn = Properties.Resources.strDaily;

            _incidentsMapControlViewModel.PeriodCategory = ServiceLayerReference.PeriodCategory.Daily;
            //ResetSliderToDefault();
            GetSelectedCategoryData(selectedViewCategory);

            btnPlayPause.Tag = "Pause";
            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            timer.Stop();
        }

        private void radTglFilBtnMonthly_Click(object sender, RoutedEventArgs e)
        {
            //grdCurrentDatetime.Visibility = Visibility.Collapsed;
            //grdCurrentMonthInfo.Visibility = Visibility.Visible;
            //grdCurrentWeekInfo.Visibility = Visibility.Collapsed;

            //chartAreaDetails.Visibility = Visibility.Collapsed;
            //chartAreaMonthly.Visibility = Visibility.Visible;
            //chartAreaWeekly.Visibility = Visibility.Collapsed;

            //HistoricalDataPanel.Visibility = Visibility.Collapsed;
            //HistoricalDataPanelMonthly.Visibility = Visibility.Visible;
            //HistoricalDataPanelWeekly.Visibility = Visibility.Collapsed;

            selectedViewCategory = "Monthly";

            lblViewCategSlider.Content = Properties.Resources.strMonthlyView;
            //lblViewCategSliderValue.Content = "";



            _incidentsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;
            _incidentsMapControlViewModel.PeriodCategory = ServiceLayerReference.PeriodCategory.Monthly;

            //ClearHistoricalViewData();
            //ResetSliderToDefault();
            GetSelectedCategoryData(selectedViewCategory);

            btnPlayPause.Tag = "Pause";
            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            timer.Stop();


        }

        private void radTglFilBtnWeekly_Click(object sender, RoutedEventArgs e)
        {
            //grdCurrentDatetime.Visibility = Visibility.Collapsed;
            //grdCurrentMonthInfo.Visibility = Visibility.Collapsed;
            //grdCurrentWeekInfo.Visibility = Visibility.Visible;

            //chartAreaDetails.Visibility = Visibility.Collapsed;
            //chartAreaMonthly.Visibility = Visibility.Collapsed;
            //chartAreaWeekly.Visibility = Visibility.Visible;

            //HistoricalDataPanel.Visibility = Visibility.Collapsed;
            //HistoricalDataPanelMonthly.Visibility = Visibility.Collapsed;
            //HistoricalDataPanelWeekly.Visibility = Visibility.Visible;

            selectedViewCategory = "Weekly";

            lblViewCategSlider.Content = Properties.Resources.strWeeklyView;
            //lblViewCategSliderValue.Content = "";


            _incidentsMapControlViewModel.CheckBtn = Properties.Resources.strWeekly;

            _incidentsMapControlViewModel.PeriodCategory = ServiceLayerReference.PeriodCategory.Weekly;
            //ResetSliderToDefault();
            GetSelectedCategoryData(selectedViewCategory);

            btnPlayPause.Tag = "Pause";
            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            timer.Stop();
        }


        private void ShowDefaultValuesAccordingtoFilterCategory()
        {
            selectedViewCategory = "Monthly";

            lblViewCategSlider.Content = Properties.Resources.strMonthlyView;
            //lblViewCategSliderValue.Content = "";


            _incidentsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;
            _incidentsMapControlViewModel.PeriodCategory = ServiceLayerReference.PeriodCategory.Monthly;
            //ClearHistoricalViewData();
            //ResetSliderToDefault();
            GetSelectedCategoryData(selectedViewCategory);
        }
        private void ButtonPreviousClick_Click(object sender, RoutedEventArgs e)
        {

            _incidentsMapControlViewModel.YearValue = _incidentsMapControlViewModel.YearValue - 1;

            //if (_incidentsMapControlViewModel.ViolationsCollection != null && _incidentsMapControlViewModel.ViolationsCollection.Count > 0)
            //  PopulateViolationsCollectionData();

            if (_incidentsMapControlViewModel.YearValueColl != null && _incidentsMapControlViewModel.YearValueColl.Count > 0)
                EnableDisablePreviousNextButtons(_incidentsMapControlViewModel.YearValue, _incidentsMapControlViewModel.YearValueColl[_incidentsMapControlViewModel.YearValueColl.Count - 1], _incidentsMapControlViewModel.YearValueColl[0]);

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {

            _incidentsMapControlViewModel.YearValue = _incidentsMapControlViewModel.YearValue + 1;

            //if (_incidentsMapControlViewModel.ViolationsCollection != null && _incidentsMapControlViewModel.ViolationsCollection.Count > 0)
            // PopulateViolationsCollectionData();

            if (_incidentsMapControlViewModel.YearValueColl != null && _incidentsMapControlViewModel.YearValueColl.Count > 0)
                EnableDisablePreviousNextButtons(_incidentsMapControlViewModel.YearValue, _incidentsMapControlViewModel.YearValueColl[_incidentsMapControlViewModel.YearValueColl.Count - 1], _incidentsMapControlViewModel.YearValueColl[0]);

        }

        private void PopulateViolationsCollectionData()
        {
            //ViolationsMonthlyStatisticalViewModel currentDataVM = new ViolationsMonthlyStatisticalViewModel(_incidentsMapControlViewModel.YearValue);
            //chartAreaMonthly.DataContext = currentDataVM;
            //chartMonthlyBarChart.DataContext = null;
            //chartMonthlyLineChart.DataContext = null;
            //Application.Current.Dispatcher.Invoke(() => { chartMonthlyBarChart.DataContext = _incidentsMapControlViewModel.ViolationsCollection; });
            //Application.Current.Dispatcher.Invoke(() => { chartMonthlyLineChart.DataContext = _incidentsMapControlViewModel.ViolationsCollection; });
            //chartCurrentData = paramToPass.ToString();


        }

        private void EnableDisablePreviousNextButtons(int currentYear, int fromYear, int toYear)
        {
            if (_incidentsMapControlViewModel.YearValue >= toYear)
                btnNextData.IsEnabled = false;
            else
                btnNextData.IsEnabled = true;


            if (_incidentsMapControlViewModel.YearValue <= fromYear)
                btnPreviousData.IsEnabled = false;
            else
                btnPreviousData.IsEnabled = true;
        }


        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            _incidentsMapControlViewModel.SliderCurrValue = _incidentsMapControlViewModel.SliderMaximum / 2;
            _incidentsMapControlViewModel.GetAndPopulateCurrentIterationIncidents((int)_incidentsMapControlViewModel.SliderCurrValue);

            //PopulateSelectedCategoryHistoricalDataForCurrentIteration(selectedViewCategory, (int)ViolationSlider.Value);

            timer.Stop();
        }

        private void btnPlayBack_Click(object sender, RoutedEventArgs e)
        {

            sliderPlayMode = "PlayBack";
            timer.Start();

        }

        //private void ButtonIsLand_Click(object sender, RoutedEventArgs e)
        //{
        //    _incidentsMapControlViewModel.CheckBtn = "IsLand";
        //}

        //private void ButtonAlAin_Click(object sender, RoutedEventArgs e)
        //{
        //    _incidentsMapControlViewModel.CheckBtn = "Al Ain";
        //}

        //private void ButtonGharbaya_Click(object sender, RoutedEventArgs e)
        //{
        //    _incidentsMapControlViewModel.CheckBtn = "Gharbaya";
        //}


        #endregion
        private void btnClosePopups_Click(object sender, RoutedEventArgs e)
        {
            ClosePopupsPanel();
        }

        private void OpenPopupsPanel()
        {
            CollapsedAllPopups();

            var storyBoard = (Storyboard)TryFindResource("OpenPopupsPanel");

            storyBoard.Begin();
        }

        private void ClosePopupsPanel()
        {
            var storyBoard = (Storyboard)TryFindResource("ClosePopupsPanel");

            storyBoard.Begin();
        }

        private void CollapsedAllPopups()
        {
            mapTipIncidents.Visibility = Visibility.Collapsed;
        }

        private void ViolationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_incidentsMapControlViewModel.SliderCurrValue > _incidentsMapControlViewModel.SliderMaximum / 2)
            {
                _incidentsMapControlViewModel.SliderCurrValue = _incidentsMapControlViewModel.SliderMaximum / 2;
                _incidentsMapControlViewModel.GetAndPopulateCurrentIterationIncidents((int)_incidentsMapControlViewModel.SliderCurrValue);
            }

        }
    }
}
