﻿#pragma checksum "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EF89992DAE063A58DDF5C848D383F7BD"
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
using STC.Projects.WPFControlLibrary.VideoStreamingControl;
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


namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls {
    
    
    /// <summary>
    /// TowerCamerasListActionPanelUserControl
    /// </summary>
    public partial class TowerCamerasListActionPanelUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 8 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.TowerCamerasListActionPanelUserControl CamerasListActionPanelUIcontrol;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btnback;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl CamerasList;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMediaPlayer;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1 light;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/towercameraslistact" +
                    "ionpanelusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
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
            this.CamerasListActionPanelUIcontrol = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.TowerCamerasListActionPanelUserControl)(target));
            return;
            case 2:
            this.Btnback = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            this.Btnback.Click += new System.Windows.RoutedEventHandler(this.ClosePopup_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CamerasList = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 6:
            
            #line 154 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmEvent_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 155 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FalseEvent_OnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.GridMediaPlayer = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            
            #line 171 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_fullscreen_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.light = ((STC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1)(target));
            
            #line 175 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            this.light.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UserControl1_MouseDown);
            
            #line default
            #line hidden
            
            #line 175 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            this.light.TouchLeave += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.UserControl1_TouchLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 141 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((STC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UserControl1_MouseDown);
            
            #line default
            #line hidden
            
            #line 141 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((STC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1)(target)).TouchLeave += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.UserControl1_TouchLeave);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 142 "..\..\..\UserControls\TowerCamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_fullscreen_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

