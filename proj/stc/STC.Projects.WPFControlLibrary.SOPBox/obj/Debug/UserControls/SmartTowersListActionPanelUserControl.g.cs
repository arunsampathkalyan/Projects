﻿#pragma checksum "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB23FBE0668A69F68CF1BA77EE9757FD"
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
    /// SmartTowersListActionPanelUserControl
    /// </summary>
    public partial class SmartTowersListActionPanelUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.SmartTowersListActionPanelUserControl SmartTowersListActionPanelUIcontrol;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btnback;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Actions;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblCurrentMsg;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/smarttowerslistacti" +
                    "onpanelusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
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
            this.SmartTowersListActionPanelUIcontrol = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.SmartTowersListActionPanelUserControl)(target));
            return;
            case 2:
            this.Btnback = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
            this.Btnback.Click += new System.Windows.RoutedEventHandler(this.ClosePopup_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmb_Actions = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            
            #line 124 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveVMS_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblCurrentMsg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            
            #line 129 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmEvent_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 130 "..\..\..\UserControls\SmartTowersListActionPanelUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SkipEvent_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
