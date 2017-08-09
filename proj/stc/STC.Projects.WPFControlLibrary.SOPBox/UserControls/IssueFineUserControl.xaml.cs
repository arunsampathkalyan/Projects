﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.ExtensionClasses;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.TFMServiceReference;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for IssueFineUserControl.xaml
    /// </summary>
    public partial class IssueFineUserControl : UserControl
    {
        IssueFineViewModel vm = null;
        public IssueFineUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            //EsriMapView.FlowDirection = FlowDirection.LeftToRight;

            vm = new IssueFineViewModel();
            vm.NewTicketCreated += vm_NewTicketCreated;

            DataContext = vm;

        }

        void vm_NewTicketCreated(object sender, EventArgs e)
        {
            ClosePopup();
            //throw new NotImplementedException();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            if (vm == null)
                return;

            vm.PlateNumber = Location.VehiclePlateNumber;
            vm.GetDangerousVehicleDetails(vm.PlateNumber);
        }

        public event CanvasEventHandler CloseCanvas;
        public event GoToNextStepEventHandler GoToNextStep;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            try
            {
                var handler = CloseCanvas;
                if (handler != null)
                    handler(this, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private async void ClosePopup()
        {
            await Task.Delay(1500);

            CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            OnCloseCanvas(canvasEventArgs);
        }

        private void CancelButton_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }


        protected virtual void OnGoToNextStep(GoToNextStepEventArgs E)
        {
            try
            {
                //var handler = GoToNextStep;
                //if (handler != null)
                //    handler(this, E);

                ClosePopup();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void IssueFineSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //OnGoToNextStep(new GoToNextStepEventArgs
                //{
                //    Confirmation = true
                //});
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }


    }
}