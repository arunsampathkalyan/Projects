﻿#pragma checksum "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "78A4013D0EBE5B6AE0AE3A8A8C9D3343"
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


namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls {
    
    
    /// <summary>
    /// CamerasListActionPanelUserControl
    /// </summary>
    public partial class CamerasListActionPanelUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 8 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.CamerasListActionPanelUserControl CamerasListActionPanelUIcontrol;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btnback;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl CamerasList;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas flt_canvas_MediaPlayer;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement FullScreenVideo;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/cameraslistactionpa" +
                    "nelusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
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
            this.CamerasListActionPanelUIcontrol = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.CamerasListActionPanelUserControl)(target));
            return;
            case 2:
            this.Btnback = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
            this.Btnback.Click += new System.Windows.RoutedEventHandler(this.ClosePopup_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CamerasList = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 5:
            this.flt_canvas_MediaPlayer = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.FullScreenVideo = ((System.Windows.Controls.MediaElement)(target));
            
            #line 149 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
            this.FullScreenVideo.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.mediaElement1_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 153 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmEvent_OnClick);
            
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
            
            #line 139 "..\..\..\UserControls\CamerasListActionPanelUserControl.xaml"
            ((System.Windows.Controls.MediaElement)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.mediaElement1_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
