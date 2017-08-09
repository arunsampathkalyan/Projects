﻿#pragma checksum "..\..\..\UserControls\PatrolsListUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C90726FC0E30BB22793005BD27C6DE48"
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
using STC.Projects.WPFControlLibrary.SOPBox.Converters;
using STC.Projects.WPFControlLibrary.SOPBox.Properties;
using STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors;
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
    /// PatrolsListUserControl
    /// </summary>
    public partial class PatrolsListUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal STC.Projects.WPFControlLibrary.SOPBox.UserControls.PatrolsListUserControl PatrolsListUC;
        
        #line default
        #line hidden
        
        
        #line 329 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClosePopups;
        
        #line default
        #line hidden
        
        
        #line 332 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid backgroundGrid;
        
        #line default
        #line hidden
        
        
        #line 351 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewPatrolsList;
        
        #line default
        #line hidden
        
        
        #line 383 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridSelectedPatrolInfo;
        
        #line default
        #line hidden
        
        
        #line 414 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBlkComments;
        
        #line default
        #line hidden
        
        
        #line 417 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Esri.ArcGISRuntime.Controls.MapView EsriMapView;
        
        #line default
        #line hidden
        
        
        #line 427 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnZoomIn;
        
        #line default
        #line hidden
        
        
        #line 431 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.RepeatButton btnZoomOut;
        
        #line default
        #line hidden
        
        
        #line 437 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDispatchPatrol;
        
        #line default
        #line hidden
        
        
        #line 440 "..\..\..\UserControls\PatrolsListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDispatchCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/usercontrols/patrolslistusercont" +
                    "rol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.PatrolsListUC = ((STC.Projects.WPFControlLibrary.SOPBox.UserControls.PatrolsListUserControl)(target));
            return;
            case 2:
            this.btnClosePopups = ((System.Windows.Controls.Button)(target));
            
            #line 330 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.btnClosePopups.Click += new System.Windows.RoutedEventHandler(this.btnClosePopups_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.backgroundGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.listViewPatrolsList = ((System.Windows.Controls.ListView)(target));
            
            #line 352 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.listViewPatrolsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PatrolsList_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.gridSelectedPatrolInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.txtBlkComments = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.EsriMapView = ((Esri.ArcGISRuntime.Controls.MapView)(target));
            return;
            case 8:
            this.btnZoomIn = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            
            #line 427 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.btnZoomIn.Click += new System.Windows.RoutedEventHandler(this.btnZoomIn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnZoomOut = ((System.Windows.Controls.Primitives.RepeatButton)(target));
            
            #line 431 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.btnZoomOut.Click += new System.Windows.RoutedEventHandler(this.btnZoomOut_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnDispatchPatrol = ((System.Windows.Controls.Button)(target));
            
            #line 438 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.btnDispatchPatrol.Click += new System.Windows.RoutedEventHandler(this.DispatchButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnDispatchCancel = ((System.Windows.Controls.Button)(target));
            
            #line 441 "..\..\..\UserControls\PatrolsListUserControl.xaml"
            this.btnDispatchCancel.Click += new System.Windows.RoutedEventHandler(this.CancelButton_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

