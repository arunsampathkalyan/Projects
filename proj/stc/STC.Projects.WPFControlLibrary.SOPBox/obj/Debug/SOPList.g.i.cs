﻿#pragma checksum "..\..\SOPList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "71290CCA7A6EEB411C2F2CD5F67AC2C8"
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
using STC.Projects.WPFControlLibrary.SOPBox.Properties;
using STC.Projects.WPFControlLibrary.SOPBox.UserControls;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
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


namespace STC.Projects.WPFControlLibrary.SOPBox {
    
    
    /// <summary>
    /// SOPList
    /// </summary>
    public partial class SOPList : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 1177 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvasSOPList;
        
        #line default
        #line hidden
        
        
        #line 1187 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridItemBasicInfo;
        
        #line default
        #line hidden
        
        
        #line 1194 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView itemsCtrBasicInfo;
        
        #line default
        #line hidden
        
        
        #line 1260 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid radExpanderSOP;
        
        #line default
        #line hidden
        
        
        #line 1284 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SopListPanel;
        
        #line default
        #line hidden
        
        
        #line 1336 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas PopupCanvas;
        
        #line default
        #line hidden
        
        
        #line 1340 "..\..\SOPList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas PopupMediaCanvas;
        
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
            System.Uri resourceLocater = new System.Uri("/STC.Projects.WPFControlLibrary.SOPBox;component/soplist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SOPList.xaml"
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
            this.canvasSOPList = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.gridItemBasicInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.itemsCtrBasicInfo = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.radExpanderSOP = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.SopListPanel = ((System.Windows.Controls.ListView)(target));
            
            #line 1284 "..\..\SOPList.xaml"
            this.SopListPanel.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SopListPanel_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 1307 "..\..\SOPList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 1308 "..\..\SOPList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancle_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PopupCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 1336 "..\..\SOPList.xaml"
            this.PopupCanvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.PopupCanvas_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.PopupMediaCanvas = ((System.Windows.Controls.Canvas)(target));
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
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 6:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseLeftButtonDownEvent;
            
            #line 1290 "..\..\SOPList.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.SopListPanelItem_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
