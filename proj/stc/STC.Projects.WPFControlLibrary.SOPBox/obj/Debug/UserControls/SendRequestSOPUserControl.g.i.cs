﻿#pragma checksum "..\..\..\UserControls\SendRequestSOPUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F4D64C1C757A66FAB7A01C2B6203EF7"
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
    /// SendRequestSOPUserControl
    /// </summary>
    public partial class SendRequestSOPUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.SendRequestSOPUserControl SendRequestSOPuc;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btnback;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageText;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/sendrequestsopuserc" +
                    "ontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
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
            this.SendRequestSOPuc = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.SendRequestSOPUserControl)(target));
            return;
            case 2:
            this.Btnback = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
            this.Btnback.Click += new System.Windows.RoutedEventHandler(this.ClosePopup_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MessageText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\..\UserControls\SendRequestSOPUserControl.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

