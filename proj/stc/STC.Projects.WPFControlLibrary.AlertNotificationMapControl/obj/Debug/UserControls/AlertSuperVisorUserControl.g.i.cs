﻿#pragma checksum "..\..\..\UserControls\AlertSuperVisorUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "482C5C5A00C558AA587F6F4CF77183B1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using STC.Projects.ClassLibrary.Common.Converters;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Behaviors;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.Data.PropertyGrid;
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
using Telerik.Windows.Shapes;


namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls {
    
    
    /// <summary>
    /// AlertSuperVisorUserControl
    /// </summary>
    public partial class AlertSuperVisorUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.AlertSuperVisorUserControl ucAlertSuperVisor;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridAlertSupervisor;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseAlertReportedAlert;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewDetailsReportedAlert;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.AlertNotificationMapControl;component/usercontrol" +
                    "s/alertsupervisorusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
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
            this.ucAlertSuperVisor = ((STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.AlertSuperVisorUserControl)(target));
            return;
            case 2:
            this.gridAlertSupervisor = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.btnCloseAlertReportedAlert = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
            this.btnCloseAlertReportedAlert.Click += new System.Windows.RoutedEventHandler(this.btnClosePopup_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnViewDetailsReportedAlert = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\..\UserControls\AlertSuperVisorUserControl.xaml"
            this.btnViewDetailsReportedAlert.Click += new System.Windows.RoutedEventHandler(this.btnViewDetailsReportedAlert_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
