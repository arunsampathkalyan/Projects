using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.ViolationsMapControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl
{
    /// <summary>
    /// Interaction logic for ViolationsMapUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "ViolationsMapUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ViolationsMapUserControl : UserControl, IUserControl
    {
        ViolationsMapControlViewModel _ViolationsMapControlViewModel = null;
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        Color fillColorLightCoral;
        Color fillColorBlueViolet;
        int interval = 5;
        string sliderPlayMode = "Play";

        private Point _movePrePoint;
        private Point _moveCurrentPoint;

        string selectedViewCategory = "Daily";

        public ViolationsMapUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            esriMapView.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            mapTipRadarAssetsViolations.FlowDirection = Utility.GetLang() == "ar" ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;
            mapTipSmartTowerAssetsViolations.FlowDirection = mapTipRadarAssetsViolations.FlowDirection;

            fillColorLightCoral = Colors.LightYellow;
            fillColorLightCoral.A = 60;
            fillColorBlueViolet = Colors.LightCoral;
            fillColorBlueViolet.A = 80;

            (this.Resources["BufferSymbolCircleSmall"] as SimpleFillSymbol).Color = fillColorBlueViolet;
            (this.Resources["BufferSymbolCircleBig"] as SimpleFillSymbol).Color = fillColorLightCoral;

            ZoomOnMap(24.43666670, 54.45666669, 130000);

            HistoricalDataPanel.Visibility = Visibility.Collapsed;
            frmDatePicker.SelectedDate = DateTime.Today;
            toDatePicker.SelectedDate = DateTime.Today;
            grdCurrentDatetime.Visibility = Visibility.Collapsed;
            _ViolationsMapControlViewModel = new ViolationsMapControlViewModel();

            this.DataContext = _ViolationsMapControlViewModel;

            AddGrphicLayer(LayerTypeEnum.Assets);
            AddGrphicLayer(LayerTypeEnum.Violations);
            AddGrphicLayer(LayerTypeEnum.HistoricalViolations);

            CultureInfo cul = Utility.GetLang() == "ar" ? new CultureInfo("ar-Eg") : new CultureInfo(Utility.GetLang());

            //if(Utility.GetLang() == "ar" )
            //{
            //    ViolationSlider.FlowDirection = System.Windows.FlowDirection.RightToLeft;
            //}

            //DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            //{

            //    this.TxtTime.Text = DateTime.Now.ToString("t", cul);

            //    this.TxtDate.Text = DateTime.Now.ToString("D", cul);

            //}, this.Dispatcher);




            if (_ViolationsMapControlViewModel.YearValueColl != null && _ViolationsMapControlViewModel.YearValueColl.Count > 0)
            {
                _ViolationsMapControlViewModel.YearValue = _ViolationsMapControlViewModel.YearValueColl[0];
                EnableDisablePreviousNextButtons(_ViolationsMapControlViewModel.YearValue, _ViolationsMapControlViewModel.YearValueColl[_ViolationsMapControlViewModel.YearValueColl.Count - 1], _ViolationsMapControlViewModel.YearValueColl[0]);
            }


            //if (_ViolationsMapControlViewModel.ViolationsCollection != null)
            //  PopulateViolationsCollectionData();

            //ViolationsMonthlyStatisticalViewModel currentDataVM = new ViolationsMonthlyStatisticalViewModel(_ViolationsMapControlViewModel.YearValue);
            //chartAreaMonthly.DataContext = currentDataVM;

            //Application.Current.Dispatcher.Invoke(() => { chartMonthlyBarChart.DataContext = _ViolationsMapControlViewModel.ViolationsCollection; });
            //Application.Current.Dispatcher.Invoke(() => { chartMonthlyLineChart.DataContext = _ViolationsMapControlViewModel.ViolationsCollection; });


            timer.Tick += timer_Tick;
            this.Loaded += ViolationsMapUserControl_Loaded;
            this.Unloaded += ViolationsMapUserControl_Unloaded;

            ShowDefaultValuesAccordingtoFilterCategory();
        }

        void ViolationsMapUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // timer.Stop();
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

        private void radBtnCloseRadarAssetsViolations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;

                //ChangeRadarSpeedPanel.Visibility = Visibility.Collapsed;

                //Clear Textbox
                //txtChangeRadar.Text = "";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void hyperLinkTextChangeRadarSpeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChangeRadarSpeedPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnChangeRadarSpeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChangeRadarSpeedPanel.Visibility = Visibility.Collapsed;

                //Clear Textbox
                //txtChangeRadar.Text = "";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void radBtnCloseSmartTowerAssetsViolations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;

                //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Collapsed;

                //Clear Textbox
                //txtChangeSmartTower.Text = "";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void hyperLinkTextChangeSmartTowerSpeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void hyperLinkTextChangeSmartTowerMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnChangeSmartTowerSpeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ChangeSmartTowerSpeedPanel.Visibility = Visibility.Collapsed;

                //Clear Textbox
                //txtChangeSmartTower.Text = "";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnHistoricalView_Click(object sender, RoutedEventArgs e)
        {
            if (btnHistoricalView.Tag.ToString() == "Historical")
            {
                btnHistoricalView.Content = Properties.Resources.strLive;

                btnHistoricalView.Tag = "Live";
                frmDatePicker.SelectedDate = null;
                toDatePicker.SelectedDate = null;
                HideShowLayer(LayerTypeEnum.Violations, false);
                HideShowLayer(LayerTypeEnum.HistoricalViolations, true);
                _ViolationsMapControlViewModel.ClearHistoricalViolationsObjects();
                Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.HistoricalViolations.ToString());
                if (_ViolationsMapControlViewModel.HioricalViolationList != null && layer != null)
                    (layer as GraphicsLayer).Graphics = new GraphicCollection(_ViolationsMapControlViewModel.HioricalViolationList);
                HistoricalDataPanel.Visibility = Visibility.Visible;
                radToggleFilterType.Visibility = Visibility.Visible;
            }
            else
            {
                btnHistoricalView.Content = Properties.Resources.strHistorical;
                grdCurrentDatetime.Visibility = Visibility.Collapsed;
                btnHistoricalView.Tag = "Historical";
                HistoricalDataPanel.Visibility = Visibility.Collapsed;
                _ViolationsMapControlViewModel.ClearHistoricalViolationsObjects();
                HideShowLayer(LayerTypeEnum.Violations, true);
                HideShowLayer(LayerTypeEnum.HistoricalViolations, false);
                timer.Stop();
                grdViolationSlider.Visibility = Visibility.Collapsed;
                radToggleFilterType.Visibility = Visibility.Collapsed;
            }
        }

        private void GetSelectedCategoryData(string category)
        {

            if (_ViolationsMapControlViewModel.LayersGraphicsDictionary.ContainsKey(LayerTypeEnum.HistoricalViolations.ToString()))
                _ViolationsMapControlViewModel.ClearHistoricalViolationsObjects();

            if (_ViolationsMapControlViewModel.LayersGraphicsDictionary.ContainsKey(LayerTypeEnum.Violations.ToString()))
                _ViolationsMapControlViewModel.ClearViolationsObjects();

            HideShowLayer(LayerTypeEnum.Violations, false);

            //switch (category)
            //{
            //    case "Daily":
            //        {

            //            _ViolationsMapControlViewModel.GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Daily);
            //            break;
            //        }
            //    case "Monthly":
            //        {

            //            _ViolationsMapControlViewModel.GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Monthly);


            //            break;
            //        }

            //    case "Weekly":
            //        {
            //            _ViolationsMapControlViewModel.GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Weekly);
            //            break;
            //        }
            //}


            btnPlayPause.Tag = "Pause";
            this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");

            PopulateDataAndUpdateHisViolationsMapLayer();
            //_ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);

            //if (_ViolationsMapControlViewModel.HistoricalCategoryViolationList != null && _ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count > 0)
            //{
            //    grdViolationSlider.Visibility = Visibility.Visible;


            //    btnPlayPause.Tag = "Pause";
            //    this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_play.png");
            //    //HideShowLayer(LayerTypeEnum.Violations, false);
            //    //HideShowLayer(LayerTypeEnum.HistoricalViolations, true);



            //    //Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.Violations.ToString());
            //    //if (layer != null)
            //    //{
            //    //    (layer as GraphicsLayer).Graphics.Clear();
            //    //}

            //    ViolationSlider.Minimum = 0;


            //    interval = 1;
            //    ViolationSlider.Maximum = 2 * (_ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count - 1);
            //    ViolationSlider.Value = ViolationSlider.Maximum / 2;
            //    PopulateSelectedCategoryHistoricalDataForCurrentIteration(selectedViewCategory, (int)ViolationSlider.Value);


            //}
            //else
            //{
            //    //Message to be given as no data available
            //}
        }

        private void StartTimer()
        {
            sliderPlayMode = "Play";
            timer_Tick(null, EventArgs.Empty);
            timer.Start();
        }

        private void ClearHistoricalViewData()
        {
            if (_ViolationsMapControlViewModel.LayersGraphicsDictionary.ContainsKey(LayerTypeEnum.HistoricalViolations.ToString()))
                _ViolationsMapControlViewModel.ClearHistoricalViolationsObjects();

        }

        private void ResetSliderToDefault()
        {
            interval = 1;
            ViolationSlider.Minimum = 0;
            ViolationSlider.Maximum = 2;
            ViolationSlider.Value = ViolationSlider.Minimum + 1;



            //if (_ViolationsMapControlViewModel.HistoricalCategoryViolationList != null && _ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count > 0)
            //{
            //    ViolationSlider.Maximum = 2 * (_ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count - 1);
            //    ViolationSlider.Value = ViolationSlider.Maximum / 2;
            //}
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (_ViolationsMapControlViewModel.LayersGraphicsDictionary.ContainsKey(LayerTypeEnum.HistoricalViolations.ToString()))
                _ViolationsMapControlViewModel.ClearHistoricalViolationsObjects();

            ViolationSlider.Minimum = 0;
            ViolationSlider.Value = 0;

            switch (selectedViewCategory)
            {
                case "Daily":
                    {
                        if (frmDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue
                && frmDatePicker.SelectedDate.Value < toDatePicker.SelectedDate.Value)
                        {
                            grdViolationSlider.Visibility = Visibility.Visible;
                            grdCurrentDatetime.Visibility = Visibility.Visible;

                            double totalmins = (toDatePicker.SelectedDate.Value - frmDatePicker.SelectedDate.Value).TotalMinutes;

                            interval = 1440;
                            ViolationSlider.Maximum = totalmins / interval;
                            timer_Tick(null, EventArgs.Empty);
                            timer.Start();
                        }

                        break;
                    }
                case "Monthly":
                    {
                        if (cmbBoxFromMonth.SelectedIndex != -1 && cmbBoxToMonth.SelectedIndex != -1 && ((int)cmbBoxToYear.SelectedValue > (int)cmbBoxFromYear.SelectedValue) || (((int)cmbBoxToYear.SelectedValue == (int)cmbBoxFromYear.SelectedValue) && (cmbBoxToMonth.SelectedIndex > cmbBoxFromMonth.SelectedIndex)))
                        {
                            grdViolationSlider.Visibility = Visibility.Visible;
                            grdCurrentMonthInfo.Visibility = Visibility.Visible;
                            int totalYears = 0;
                            int monthsToAdd = 0;

                            totalYears = (int)cmbBoxToYear.SelectedValue - (int)cmbBoxFromYear.SelectedValue;

                            if (cmbBoxToMonth.SelectedIndex >= cmbBoxFromMonth.SelectedIndex)
                                monthsToAdd = cmbBoxToMonth.SelectedIndex - cmbBoxFromMonth.SelectedIndex;
                            else
                                monthsToAdd = (12 - (cmbBoxFromMonth.SelectedIndex + 1)) + (cmbBoxToMonth.SelectedIndex + 1);



                            //monthsToAdd = (12 - (cmbBoxFromMonth.SelectedIndex + 1)) + (cmbBoxToMonth.SelectedIndex + 1);

                            //if (totalYears == 0)
                            //    monthsToAdd = cmbBoxToMonth.SelectedIndex - cmbBoxFromMonth.SelectedIndex;
                            if (totalYears == 1 && cmbBoxToMonth.SelectedIndex < cmbBoxFromMonth.SelectedIndex)
                            {
                                totalYears = totalYears - 1;
                            }


                            int totalMonths = (totalYears * 12) + monthsToAdd;


                            interval = 1;
                            ViolationSlider.Maximum = totalMonths / interval;
                            timer_Tick(null, EventArgs.Empty);
                            timer.Start();
                        }

                        break;
                    }

                case "Weekly":
                    {
                        if (frmDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue
                && frmDatePicker.SelectedDate.Value < toDatePicker.SelectedDate.Value)
                        {
                            grdViolationSlider.Visibility = Visibility.Visible;
                            grdCurrentWeekInfo.Visibility = Visibility.Visible;

                            double totalmins = (toDatePicker.SelectedDate.Value - frmDatePicker.SelectedDate.Value).TotalMinutes;

                            interval = 1;

                            ViolationSlider.Maximum = totalmins / interval;
                            timer_Tick(null, EventArgs.Empty);
                            timer.Start();
                        }

                        break;
                    }
            }

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
                if (_ViolationsMapControlViewModel.SliderMaximum == 2)
                    _ViolationsMapControlViewModel.UpdateSliderValues();

                if (_ViolationsMapControlViewModel.SliderCurrValue <= (_ViolationsMapControlViewModel.SliderMaximum / 2))
                {
                    btnPlayPause.Tag = "Play";
                    this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_pause.png");

                    //sliderPlayMode = "Play";
                    timer.Start();


                }


                //if (_ViolationsMapControlViewModel.SliderCurrValue <= (_ViolationsMapControlViewModel.SliderMaximum / 2) && _ViolationsMapControlViewModel.HistoricalCategoryViolationList != null && _ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count > 0)
                //{
                //    btnPlayPause.Tag = "Play";
                //    this.imgPausePlay.Source = GetImageSourceFromResource("Images/icons/icons_pause.png");

                //    //sliderPlayMode = "Play";
                //    timer.Start();


                //}
                //else
                //    timer.Stop();

            }
        }

        private void PopulateSelectedCategoryHistoricalDataForCurrentIteration(string category, int currentIndex)
        {
            switch (selectedViewCategory)
            {
                case "Count":
                    {
                        var currentdatetime = frmDatePicker.SelectedDate.Value.AddMinutes(ViolationSlider.Value * interval);
                        ViolationSlider.ToolTip = currentdatetime.ToString();
                        _ViolationsMapControlViewModel.GetViolationsDataByLocation(frmDatePicker.SelectedDate, currentdatetime, 0);
                        lblDate.Content = currentdatetime.ToShortDateString();

                        lblTime.Content = currentdatetime.ToShortTimeString();

                        break;
                    }
                case "Daily":
                case "Weekly":
                case "Monthly":
                    {
                        if (_ViolationsMapControlViewModel.HistoricalCategoryViolationList != null && _ViolationsMapControlViewModel.HistoricalCategoryViolationList.Count > currentIndex)
                        {
                            lblViewCategSliderValue.Content = "";
                            _ViolationsMapControlViewModel.PopulateCurrentCategoryHistoricalData(currentIndex);

                            string currentItem = _ViolationsMapControlViewModel.HistoricalCategoryViolationList[currentIndex].DateElement;


                            ViolationSlider.ToolTip = currentItem;

                            //lblFromMonth.Content = currentItem;
                            //lblToMonth.Content = currentItem;

                            lblViewCategSliderValue.Content = currentItem;
                        }

                        break;
                    }

            }




            ObservableCollection<Graphic> lst = _ViolationsMapControlViewModel.HioricalViolationList;
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.HistoricalViolations.ToString());
            if (lst != null && layer != null)
            {
                //(layer as GraphicsLayer).Graphics.Clear();
                //(layer as GraphicsLayer).Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(layer);


                //GraphicsLayer graphicsLayer = esriMapView.Map.Layers[LayerTypeEnum.HistoricalViolations.ToString()] as Esri.ArcGISRuntime.Layers.GraphicsLayer;
                // graphicsLayer.Graphics.Clear();
                (layer as GraphicsLayer).Graphics.Clear();
                (layer as GraphicsLayer).Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(layer);
                //foreach (var item in lst)
                //{
                //    graphicsLayer.Graphics.Add(item);
                //}

                //graphicsLayer.Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(graphicsLayer);
                //graphicsLayer.Graphics.Add(graphicSymbol);
                //esriMapView.Map.Layers.Add(graphicsLayer);
                /////////////////////////
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            switch (sliderPlayMode)
            {
                case "Play":
                    {

                        if (_ViolationsMapControlViewModel.SliderCurrValue - 1 >= _ViolationsMapControlViewModel.SliderMinimum)
                        {
                            _ViolationsMapControlViewModel.SliderCurrValue -= 1;

                            PopulateDataAndUpdateHisViolationsMapLayer();
                            //_ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);
                            //ViolationSlider.Value = _incidentsMapControlViewModel.SliderCurrValue;


                            //string currentItem = _incidentsMapControlViewModel.StartDate.ToString("MMM yy");
                            //lblViewCategSliderValue.Content = _incidentsMapControlViewModel.CurrentDateRange;
                            //ViolationSlider.ToolTip = _incidentsMapControlViewModel.CurrentDateRange;

                        }

                        //if (ViolationSlider.Value - 1 >= ViolationSlider.Minimum)
                        //{
                        //    ViolationSlider.Value -= 1;
                        //    PopulateSelectedCategoryHistoricalDataForCurrentIteration(selectedViewCategory, (int)ViolationSlider.Value);
                        //}
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
                        if (_ViolationsMapControlViewModel.SliderCurrValue + 1 <= (_ViolationsMapControlViewModel.SliderMaximum / 2))
                        {
                            _ViolationsMapControlViewModel.SliderCurrValue += 1;
                            PopulateDataAndUpdateHisViolationsMapLayer();
                            //_ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);
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

        private void PopulateDataAndUpdateHisViolationsMapLayer()
        {
            _ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);


            ObservableCollection<Graphic> lst = _ViolationsMapControlViewModel.HioricalViolationList;
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == LayerTypeEnum.HistoricalViolations.ToString());
            if (lst != null && layer != null)
            {
                //(layer as GraphicsLayer).Graphics.Clear();
                //(layer as GraphicsLayer).Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(layer);


                //GraphicsLayer graphicsLayer = esriMapView.Map.Layers[LayerTypeEnum.HistoricalViolations.ToString()] as Esri.ArcGISRuntime.Layers.GraphicsLayer;
                // graphicsLayer.Graphics.Clear();
                (layer as GraphicsLayer).Graphics.Clear();
                (layer as GraphicsLayer).Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(layer);
                //foreach (var item in lst)
                //{
                //    graphicsLayer.Graphics.Add(item);
                //}

                //graphicsLayer.Graphics = new GraphicCollection(lst);
                //esriMapView.Map.Layers.Add(graphicsLayer);
                //graphicsLayer.Graphics.Add(graphicSymbol);
                //esriMapView.Map.Layers.Add(graphicsLayer);
                /////////////////////////
            }

        }

        private void frmDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (frmDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue && frmDatePicker.SelectedDate >= toDatePicker.SelectedDate)
            {
                toDatePicker.SelectedDate = frmDatePicker.SelectedDate.Value.AddDays(1);
            }
        }
        private void toDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (frmDatePicker.SelectedDate.HasValue && toDatePicker.SelectedDate.HasValue && frmDatePicker.SelectedDate >= toDatePicker.SelectedDate)
            {
                frmDatePicker.SelectedDate = toDatePicker.SelectedDate.Value.AddDays(-1);
            }
        }

        private void btnReturnToDefaultView_Click(object sender, RoutedEventArgs e)
        {
            ZoomOnMap(24.43666670, 54.45666669, 130000);
        }

        private void mapTipAssetsStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _movePrePoint = e.GetPosition(null);

            e.Handled = true;
        }

        private void mapTipAssetsStackPanel_MouseMove(object sender, MouseEventArgs e)
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

        void ViolationsMapUserControl_Loaded(object sender, EventArgs e)
        {
            var vm = DataContext as ViolationsMapControlViewModel;

            if (vm != null)
                vm.CurrentUsername = this.GetCurrentUsername();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                //8781
                //GetVehicleViolationsDetails();
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtSearch.Text != "")
                {
                    //GetVehicleViolationsDetails();
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

        private void esriMapView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HandleClick(e.GetPosition(esriMapView));
        }

        private void esriMapView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void radBtnCloseAssetsOrViolations_Click(object sender, RoutedEventArgs e)
        {
            CloseAssetsMapTip();
        }

        private void btnESRITOPO_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRITOPO.Tag.ToString());
        }

        private void btnESRIStreet_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRIStreet.Tag.ToString());
        }

        private void btnESRIImagery_Click(object sender, RoutedEventArgs e)
        {
            ChangeESRIBaseMap(btnESRIImagery.Tag.ToString());
        }
        #endregion

        #region Methods

        static internal ImageSource GetImageSourceFromResource(string path)
        {
            Uri oUri = new Uri("pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + path, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
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

                    if (layer.ID == LayerTypeEnum.Violations.ToString() ||
                        (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Violations.ToString()))
                    {
                        //mapTipRadarAssetsViolations.Visibility = System.Windows.Visibility.Visible;

                    }
                    else if (layer.ID == LayerTypeEnum.Assets.ToString() ||
                             (layer.ID == LayerTypeEnum.Notifications.ToString() && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString()))
                    {
                        _ViolationsMapControlViewModel.GetAssetViolationDetails(graphic.Attributes["Id"].ToString());

                        if (graphic.Attributes.ContainsKey("AssetsType") && graphic.Attributes["AssetsType"].ToString() == "Smart Towers")
                        {
                            OpenPopupsPanel();

                            //Smart Tower Popups
                            mapTipSmartTowerAssetsViolations.Visibility = Visibility.Visible;

                            //mapTipSmartTowerAssetsViolations.DataContext = new ChartsViewModel();
                        }
                        else
                        {
                            OpenPopupsPanel();

                            //Radars Popups
                            mapTipRadarAssetsViolations.Visibility = Visibility.Visible;

                            //mapTipRadarAssetsViolations.DataContext = new ChartsViewModel();
                        }

                        this.Publish(new SOPGeneralMessage());
                    }
                }
            }
            catch
            {
            }

        }

        private void HideShowLayer(LayerTypeEnum layerType, bool IsShow)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            layer.IsVisible = IsShow;

            if (IsShow)
            {
                ZoomToExtent(layerType);
            }

            mapTipRadarAssetsViolations.Visibility = System.Windows.Visibility.Collapsed;
            mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;
        }

        private void ZoomToExtent(LayerTypeEnum layerType)
        {
            Layer layer = esriMapView.Map.Layers.FirstOrDefault(x => x.ID == layerType.ToString());
            ObservableCollection<Graphic> layerCol = _ViolationsMapControlViewModel.GetLayerObservable(layerType);

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
            _ViolationsMapControlViewModel.AddGraphicsToLayersGraphicsDictionary(layerTypeEnum);
            GraphicsLayer graphicsLayer = new GraphicsLayer();
            graphicsLayer.ID = layerTypeEnum.ToString();
            esriMapView.Map.Layers.Add(graphicsLayer);
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding(string.Format("LayersGraphicsDictionary[{0}]", layerTypeEnum.ToString()));
            binding.Source = _ViolationsMapControlViewModel;
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

        private void CloseAssetsMapTip()
        {
            mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;
            mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;
        }

        #endregion

        private void radTglFilBtnCount_Click(object sender, RoutedEventArgs e)
        {
            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;
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

            _ViolationsMapControlViewModel.PeriodTypeFilter = ServiceLayerReference.PeriodType.Daily;
            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strDaily;

            ClearHistoricalViewData();
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



            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;
            _ViolationsMapControlViewModel.PeriodTypeFilter = ServiceLayerReference.PeriodType.Monthly;
            ClearHistoricalViewData();
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


            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strWeekly;
            _ViolationsMapControlViewModel.PeriodTypeFilter = ServiceLayerReference.PeriodType.Weekly;
            ClearHistoricalViewData();
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


            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strMonthly;

            _ViolationsMapControlViewModel.PeriodTypeFilter = ServiceLayerReference.PeriodType.Monthly;

            ClearHistoricalViewData();
            //ResetSliderToDefault();
            GetSelectedCategoryData(selectedViewCategory);
        }
        private void ButtonPreviousClick_Click(object sender, RoutedEventArgs e)
        {

            _ViolationsMapControlViewModel.YearValue = _ViolationsMapControlViewModel.YearValue - 1;

            //if (_ViolationsMapControlViewModel.ViolationsCollection != null && _ViolationsMapControlViewModel.ViolationsCollection.Count > 0)
            //  PopulateViolationsCollectionData();

            if (_ViolationsMapControlViewModel.YearValueColl != null && _ViolationsMapControlViewModel.YearValueColl.Count > 0)
                EnableDisablePreviousNextButtons(_ViolationsMapControlViewModel.YearValue, _ViolationsMapControlViewModel.YearValueColl[_ViolationsMapControlViewModel.YearValueColl.Count - 1], _ViolationsMapControlViewModel.YearValueColl[0]);

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {

            _ViolationsMapControlViewModel.YearValue = _ViolationsMapControlViewModel.YearValue + 1;

            //if (_ViolationsMapControlViewModel.ViolationsCollection != null && _ViolationsMapControlViewModel.ViolationsCollection.Count > 0)
            // PopulateViolationsCollectionData();

            if (_ViolationsMapControlViewModel.YearValueColl != null && _ViolationsMapControlViewModel.YearValueColl.Count > 0)
                EnableDisablePreviousNextButtons(_ViolationsMapControlViewModel.YearValue, _ViolationsMapControlViewModel.YearValueColl[_ViolationsMapControlViewModel.YearValueColl.Count - 1], _ViolationsMapControlViewModel.YearValueColl[0]);

        }

        private void PopulateViolationsCollectionData()
        {
            //ViolationsMonthlyStatisticalViewModel currentDataVM = new ViolationsMonthlyStatisticalViewModel(_ViolationsMapControlViewModel.YearValue);
            //chartAreaMonthly.DataContext = currentDataVM;
            chartMonthlyBarChart.DataContext = null;
            chartMonthlyLineChart.DataContext = null;
            Application.Current.Dispatcher.Invoke(() => { chartMonthlyBarChart.DataContext = _ViolationsMapControlViewModel.ViolationsCollection; });
            Application.Current.Dispatcher.Invoke(() => { chartMonthlyLineChart.DataContext = _ViolationsMapControlViewModel.ViolationsCollection; });
            //chartCurrentData = paramToPass.ToString();


        }

        private void EnableDisablePreviousNextButtons(int currentYear, int fromYear, int toYear)
        {
            if (_ViolationsMapControlViewModel.YearValue >= toYear)
                btnNextData.IsEnabled = false;
            else
                btnNextData.IsEnabled = true;


            if (_ViolationsMapControlViewModel.YearValue <= fromYear)
                btnPreviousData.IsEnabled = false;
            else
                btnPreviousData.IsEnabled = true;
        }


        private void btnToday_Click(object sender, RoutedEventArgs e)
        {

            _ViolationsMapControlViewModel.SliderCurrValue = _ViolationsMapControlViewModel.SliderMaximum / 2;

            PopulateDataAndUpdateHisViolationsMapLayer();
            //_ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);

            //ViolationSlider.Value = ViolationSlider.Maximum / 2;

            //PopulateSelectedCategoryHistoricalDataForCurrentIteration(selectedViewCategory, (int)ViolationSlider.Value);

            timer.Stop();
        }

        private void btnPlayBack_Click(object sender, RoutedEventArgs e)
        {

            sliderPlayMode = "PlayBack";
            timer.Start();

        }

        private void ButtonIsLand_Click(object sender, RoutedEventArgs e)
        {
            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strIsLand;
        }

        private void ButtonAlAin_Click(object sender, RoutedEventArgs e)
        {
            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strAlAin;
        }

        private void ButtonGharbaya_Click(object sender, RoutedEventArgs e)
        {
            _ViolationsMapControlViewModel.CheckBtn = Properties.Resources.strGharbaya;
        }

        private void btnClosePopups_Click(object sender, RoutedEventArgs e)
        {
            ClosePopupsPanel();
        }

        private void OpenPopupsPanel()
        {
            CollapsedAllPopups();

            _ViolationsMapControlViewModel.AssetViolationsDetails = null;

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
            mapTipRadarAssetsViolations.Visibility = Visibility.Collapsed;

            mapTipSmartTowerAssetsViolations.Visibility = Visibility.Collapsed;
        }

        private void ViolationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_ViolationsMapControlViewModel.SliderCurrValue > _ViolationsMapControlViewModel.SliderMaximum / 2)
            {
                _ViolationsMapControlViewModel.SliderCurrValue = _ViolationsMapControlViewModel.SliderMaximum / 2;
                _ViolationsMapControlViewModel.GetAndPopulateCurrentIterationViolations((int)_ViolationsMapControlViewModel.SliderCurrValue);
            }

        }

    }


}