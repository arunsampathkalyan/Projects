﻿#pragma checksum "..\..\..\UserControls\RadarDetailsUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2692C40F4041CDAA77A8D10B94FE64B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using STC.Projects.WPFControlLibrary.SOPBox.Properties;
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
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Controls.Wizard;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using Telerik.Windows.Input.Touch;
using Telerik.Windows.Media.Imaging.ImageEditorCommands.RoutedCommands;
using Telerik.Windows.Media.Imaging.Tools.UI;
using Telerik.Windows.Shapes;


namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls {
    
    
    /// <summary>
    /// RadarDetailsUserControl
    /// </summary>
    public partial class RadarDetailsUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.RadarDetailsUserControl camUIcontrol;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btnback;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl radTabControlRadarDetails;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblCurrentSpeed;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadNumericUpDown SpeedLimit;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblCurrentSpeed2;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/radardetailsusercon" +
                    "trol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
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
            this.camUIcontrol = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.RadarDetailsUserControl)(target));
            return;
            case 2:
            this.Btnback = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
            this.Btnback.Click += new System.Windows.RoutedEventHandler(this.ClosePopup_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.radTabControlRadarDetails = ((System.Windows.Controls.TabControl)(target));
            return;
            case 4:
            this.lblCurrentSpeed = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.SpeedLimit = ((Telerik.Windows.Controls.RadNumericUpDown)(target));
            return;
            case 6:
            
            #line 233 "..\..\..\UserControls\RadarDetailsUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveSpeedLimit_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblCurrentSpeed2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

