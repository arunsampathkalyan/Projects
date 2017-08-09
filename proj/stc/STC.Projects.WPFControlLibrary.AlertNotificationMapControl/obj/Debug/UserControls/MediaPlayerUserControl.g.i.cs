﻿#pragma checksum "..\..\..\UserControls\MediaPlayerUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F8BE8E88F5A7A3094C5975CC64B8E18A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties;
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


namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls {
    
    
    /// <summary>
    /// MediaPlayerUserControl
    /// </summary>
    public partial class MediaPlayerUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.MediaPlayerUserControl MediaPlayerUC;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayCentre;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid btnPauseCentre;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mePlayer;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlay;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPause;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblProgressStatus;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliProgress;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pbVolume;
        
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
                    "s/mediaplayerusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
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
            this.MediaPlayerUC = ((STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.MediaPlayerUserControl)(target));
            return;
            case 2:
            
            #line 8 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Open_CanExecute);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Open_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 9 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Play_CanExecute);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Play_Executed);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 10 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Pause_CanExecute);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Pause_Executed);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 11 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Stop_CanExecute);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Stop_Executed);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 13 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Grid_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnPlayCentre = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.btnPauseCentre = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.mePlayer = ((System.Windows.Controls.MediaElement)(target));
            return;
            case 10:
            this.btnPlay = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.btnPause = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.lblProgressStatus = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.sliProgress = ((System.Windows.Controls.Slider)(target));
            
            #line 119 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            this.sliProgress.AddHandler(System.Windows.Controls.Primitives.Thumb.DragStartedEvent, new System.Windows.Controls.Primitives.DragStartedEventHandler(this.sliProgress_DragStarted));
            
            #line default
            #line hidden
            
            #line 119 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            this.sliProgress.AddHandler(System.Windows.Controls.Primitives.Thumb.DragCompletedEvent, new System.Windows.Controls.Primitives.DragCompletedEventHandler(this.sliProgress_DragCompleted));
            
            #line default
            #line hidden
            
            #line 120 "..\..\..\UserControls\MediaPlayerUserControl.xaml"
            this.sliProgress.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.sliProgress_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.pbVolume = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

