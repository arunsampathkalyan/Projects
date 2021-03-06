﻿#pragma checksum "..\..\IncidentMapUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DEC14172B724988EA77E356E5303B4A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Symbology.SceneSymbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Converters;
using STC.Projects.WPFControlLibrary.IncidentsMapControl.ChartUserControls;
using STC.Projects.WPFControlLibrary.IncidentsMapControl.Properties;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Behaviors;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.RadialMenu;
using Telerik.Windows.Controls.TabControl;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Controls.Wizard;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using Telerik.Windows.Input.Touch;
using Telerik.Windows.Shapes;


namespace STC.Projects.WPFControlLibrary.IncidentsMapControl {
    
    
    /// <summary>
    /// IncidentMapUserControl
    /// </summary>
    public partial class IncidentMapUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1637 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridPopups;
        
        #line default
        #line hidden
        
        
        #line 1651 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClosePopups;
        
        #line default
        #line hidden
        
        
        #line 1655 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border mapTipIncidents;
        
        #line default
        #line hidden
        
        
        #line 1747 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridChartArea;
        
        #line default
        #line hidden
        
        
        #line 1764 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridViolnCategryCheckBox;
        
        #line default
        #line hidden
        
        
        #line 1766 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chboxShowDangOnly;
        
        #line default
        #line hidden
        
        
        #line 1767 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chboxShowHeatMap;
        
        #line default
        #line hidden
        
        
        #line 1768 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chboxActivatePred;
        
        #line default
        #line hidden
        
        
        #line 1773 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridRegionType;
        
        #line default
        #line hidden
        
        
        #line 1911 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreviousData;
        
        #line default
        #line hidden
        
        
        #line 1942 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextData;
        
        #line default
        #line hidden
        
        
        #line 1982 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border chartAreaMonthly;
        
        #line default
        #line hidden
        
        
        #line 1989 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.IncidentsMapControl.ChartUserControls.IncidentsCountChartMonthlyUserControl chartMonthlyBarChart;
        
        #line default
        #line hidden
        
        
        #line 1991 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border abc;
        
        #line default
        #line hidden
        
        
        #line 1992 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.IncidentsMapControl.ChartUserControls.IncidentsCountMonthlyLineChartUserControl chartMonthlyLineChart;
        
        #line default
        #line hidden
        
        
        #line 2028 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtDate;
        
        #line default
        #line hidden
        
        
        #line 2029 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtTime;
        
        #line default
        #line hidden
        
        
        #line 2038 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadWatermarkTextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 2040 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 2046 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdFilterCateg;
        
        #line default
        #line hidden
        
        
        #line 2052 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button radToggleFilterTypeBtn;
        
        #line default
        #line hidden
        
        
        #line 2085 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Esri.ArcGISRuntime.Controls.MapView esriMapView;
        
        #line default
        #line hidden
        
        
        #line 2125 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnESRITOPO;
        
        #line default
        #line hidden
        
        
        #line 2130 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnESRIStreet;
        
        #line default
        #line hidden
        
        
        #line 2135 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnESRIImagery;
        
        #line default
        #line hidden
        
        
        #line 2142 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnZoomIn;
        
        #line default
        #line hidden
        
        
        #line 2182 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnZoomOut;
        
        #line default
        #line hidden
        
        
        #line 2222 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReturnToDefaultView;
        
        #line default
        #line hidden
        
        
        #line 2400 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadToggleButton radToggleFilterType;
        
        #line default
        #line hidden
        
        
        #line 2466 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MenuPanelFilter;
        
        #line default
        #line hidden
        
        
        #line 2620 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadToggleButton radTglFilBtnCount;
        
        #line default
        #line hidden
        
        
        #line 2628 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadToggleButton radTglFilBtnDays;
        
        #line default
        #line hidden
        
        
        #line 2636 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadToggleButton radTglFilBtnWeekly;
        
        #line default
        #line hidden
        
        
        #line 2643 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadToggleButton radTglFilBtnMonthly;
        
        #line default
        #line hidden
        
        
        #line 2747 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border grdCurrentMonthInfo;
        
        #line default
        #line hidden
        
        
        #line 2759 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFromMonth;
        
        #line default
        #line hidden
        
        
        #line 2761 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblToMonth;
        
        #line default
        #line hidden
        
        
        #line 2764 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border grdCurrentWeekInfo;
        
        #line default
        #line hidden
        
        
        #line 2778 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFromMonthforWeek;
        
        #line default
        #line hidden
        
        
        #line 2780 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFromWeek;
        
        #line default
        #line hidden
        
        
        #line 2782 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblToWeek;
        
        #line default
        #line hidden
        
        
        #line 2789 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdViolationSlider;
        
        #line default
        #line hidden
        
        
        #line 2802 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblViewCategSlider;
        
        #line default
        #line hidden
        
        
        #line 2803 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblViewCategSliderValue;
        
        #line default
        #line hidden
        
        
        #line 2807 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider ViolationSlider;
        
        #line default
        #line hidden
        
        
        #line 2823 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayPause;
        
        #line default
        #line hidden
        
        
        #line 2825 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgPausePlay;
        
        #line default
        #line hidden
        
        
        #line 2837 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayBack;
        
        #line default
        #line hidden
        
        
        #line 2869 "..\..\IncidentMapUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnToday;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.IncidentsMapControl;component/incidentmapusercont" +
                    "rol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\IncidentMapUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.gridPopups = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.btnClosePopups = ((System.Windows.Controls.Button)(target));
            
            #line 1651 "..\..\IncidentMapUserControl.xaml"
            this.btnClosePopups.Click += new System.Windows.RoutedEventHandler(this.btnClosePopups_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.mapTipIncidents = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.gridChartArea = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.gridViolnCategryCheckBox = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.chboxShowDangOnly = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.chboxShowHeatMap = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.chboxActivatePred = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.gridRegionType = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.btnPreviousData = ((System.Windows.Controls.Button)(target));
            
            #line 1911 "..\..\IncidentMapUserControl.xaml"
            this.btnPreviousData.Click += new System.Windows.RoutedEventHandler(this.ButtonPreviousClick_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnNextData = ((System.Windows.Controls.Button)(target));
            
            #line 1942 "..\..\IncidentMapUserControl.xaml"
            this.btnNextData.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.chartAreaMonthly = ((System.Windows.Controls.Border)(target));
            return;
            case 13:
            this.chartMonthlyBarChart = ((STC.Projects.WPFControlLibrary.IncidentsMapControl.ChartUserControls.IncidentsCountChartMonthlyUserControl)(target));
            return;
            case 14:
            this.abc = ((System.Windows.Controls.Border)(target));
            return;
            case 15:
            this.chartMonthlyLineChart = ((STC.Projects.WPFControlLibrary.IncidentsMapControl.ChartUserControls.IncidentsCountMonthlyLineChartUserControl)(target));
            return;
            case 16:
            this.TxtDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.TxtTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.txtSearch = ((Telerik.Windows.Controls.RadWatermarkTextBox)(target));
            return;
            case 19:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            return;
            case 20:
            this.grdFilterCateg = ((System.Windows.Controls.Grid)(target));
            return;
            case 21:
            this.radToggleFilterTypeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 2052 "..\..\IncidentMapUserControl.xaml"
            this.radToggleFilterTypeBtn.Click += new System.Windows.RoutedEventHandler(this.ToggleMenu_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.esriMapView = ((Esri.ArcGISRuntime.Controls.MapView)(target));
            
            #line 2085 "..\..\IncidentMapUserControl.xaml"
            this.esriMapView.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.esriMapView_MouseDown);
            
            #line default
            #line hidden
            
            #line 2085 "..\..\IncidentMapUserControl.xaml"
            this.esriMapView.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.esriMapView_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 23:
            this.btnESRITOPO = ((System.Windows.Controls.Button)(target));
            
            #line 2126 "..\..\IncidentMapUserControl.xaml"
            this.btnESRITOPO.Click += new System.Windows.RoutedEventHandler(this.btnESRITOPO_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.btnESRIStreet = ((System.Windows.Controls.Button)(target));
            
            #line 2131 "..\..\IncidentMapUserControl.xaml"
            this.btnESRIStreet.Click += new System.Windows.RoutedEventHandler(this.btnESRIStreet_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.btnESRIImagery = ((System.Windows.Controls.Button)(target));
            
            #line 2136 "..\..\IncidentMapUserControl.xaml"
            this.btnESRIImagery.Click += new System.Windows.RoutedEventHandler(this.btnESRIImagery_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            this.btnZoomIn = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            
            #line 2142 "..\..\IncidentMapUserControl.xaml"
            this.btnZoomIn.Click += new System.Windows.RoutedEventHandler(this.btnZoomIn_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            this.btnZoomOut = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            
            #line 2182 "..\..\IncidentMapUserControl.xaml"
            this.btnZoomOut.Click += new System.Windows.RoutedEventHandler(this.btnZoomOut_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            this.btnReturnToDefaultView = ((System.Windows.Controls.Button)(target));
            
            #line 2222 "..\..\IncidentMapUserControl.xaml"
            this.btnReturnToDefaultView.Click += new System.Windows.RoutedEventHandler(this.btnReturnToDefaultView_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            this.radToggleFilterType = ((Telerik.Windows.Controls.RadToggleButton)(target));
            return;
            case 30:
            this.MenuPanelFilter = ((System.Windows.Controls.Border)(target));
            return;
            case 31:
            
            #line 2517 "..\..\IncidentMapUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnDays_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 2551 "..\..\IncidentMapUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnWeekly_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 2585 "..\..\IncidentMapUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnMonthly_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.radTglFilBtnCount = ((Telerik.Windows.Controls.RadToggleButton)(target));
            
            #line 2620 "..\..\IncidentMapUserControl.xaml"
            this.radTglFilBtnCount.Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnCount_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.radTglFilBtnDays = ((Telerik.Windows.Controls.RadToggleButton)(target));
            
            #line 2628 "..\..\IncidentMapUserControl.xaml"
            this.radTglFilBtnDays.Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnDays_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.radTglFilBtnWeekly = ((Telerik.Windows.Controls.RadToggleButton)(target));
            
            #line 2636 "..\..\IncidentMapUserControl.xaml"
            this.radTglFilBtnWeekly.Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnWeekly_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.radTglFilBtnMonthly = ((Telerik.Windows.Controls.RadToggleButton)(target));
            
            #line 2643 "..\..\IncidentMapUserControl.xaml"
            this.radTglFilBtnMonthly.Click += new System.Windows.RoutedEventHandler(this.radTglFilBtnMonthly_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.grdCurrentMonthInfo = ((System.Windows.Controls.Border)(target));
            return;
            case 39:
            this.lblFromMonth = ((System.Windows.Controls.Label)(target));
            return;
            case 40:
            this.lblToMonth = ((System.Windows.Controls.Label)(target));
            return;
            case 41:
            this.grdCurrentWeekInfo = ((System.Windows.Controls.Border)(target));
            return;
            case 42:
            this.lblFromMonthforWeek = ((System.Windows.Controls.Label)(target));
            return;
            case 43:
            this.lblFromWeek = ((System.Windows.Controls.Label)(target));
            return;
            case 44:
            this.lblToWeek = ((System.Windows.Controls.Label)(target));
            return;
            case 45:
            this.grdViolationSlider = ((System.Windows.Controls.Grid)(target));
            return;
            case 46:
            this.lblViewCategSlider = ((System.Windows.Controls.Label)(target));
            return;
            case 47:
            this.lblViewCategSliderValue = ((System.Windows.Controls.Label)(target));
            return;
            case 48:
            this.ViolationSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 2817 "..\..\IncidentMapUserControl.xaml"
            this.ViolationSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ViolationSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 49:
            this.btnPlayPause = ((System.Windows.Controls.Button)(target));
            
            #line 2823 "..\..\IncidentMapUserControl.xaml"
            this.btnPlayPause.Click += new System.Windows.RoutedEventHandler(this.btnPlayPause_Click);
            
            #line default
            #line hidden
            return;
            case 50:
            this.imgPausePlay = ((System.Windows.Controls.Image)(target));
            return;
            case 51:
            this.btnPlayBack = ((System.Windows.Controls.Button)(target));
            
            #line 2838 "..\..\IncidentMapUserControl.xaml"
            this.btnPlayBack.Click += new System.Windows.RoutedEventHandler(this.btnPlayBack_Click);
            
            #line default
            #line hidden
            return;
            case 52:
            this.btnToday = ((System.Windows.Controls.Button)(target));
            
            #line 2870 "..\..\IncidentMapUserControl.xaml"
            this.btnToday.Click += new System.Windows.RoutedEventHandler(this.btnToday_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

