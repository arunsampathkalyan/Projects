using System;
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
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for DangerousVehicleUserControl.xaml
    /// </summary>
    public partial class DangerousVehicleUserControl : UserControl
    {
        public event CanvasEventHandler CloseCanvas;

        public event GoToNextStepEventHandler GoToNextStep;

        DangerousVehicleViewModel VM;

        public DangerousVehicleUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();
            this.Loaded += DangerousVehicleUserControl_Loaded;
            VM = new DangerousVehicleViewModel();

            //  VM.GetDangerousVehicleDetails("8781");


            //if (imgGalleryUc.DataContext != null && (imgGalleryUc.DataContext as ImagePopupViewModel != null))
            //{
            //    VM.ImagePoupVM = imgGalleryUc.DataContext as ImagePopupViewModel;
            //}

            this.DataContext = VM;
        }

        void DangerousVehicleUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (VM == null)
                return;

            //VM.GetDangerousVehicleDetails();
            if (imgGalleryUc.DataContext != null && (imgGalleryUc.DataContext as ImagePopupViewModel != null))
            {
                VM.ImagePoupVM = imgGalleryUc.DataContext as ImagePopupViewModel;
            }
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            VM = DataContext as DangerousVehicleViewModel;
                
            if (VM == null)
                return;

            VM.SetCurrentPlateNumber(Location.VehiclePlateNumber);
            VM.GetDangerousVehicleDetails();
        }

        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            var handler = CloseCanvas;
            if (handler != null)
                handler(this, E);
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
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyStoryboard");
            sb.Begin();
            await Task.Delay(1000);

            CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            OnCloseCanvas(canvasEventArgs);
        }

        protected virtual void OnGoToNextStep(GoToNextStepEventArgs E)
        {
            try
            {
                var handler = GoToNextStep;
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

        private void ClearControls()
        {

        }

        public event CancelSOPEventHandler CancelSOP;
        protected virtual void OnCancelSOP(CancelSOPEventArgs E)
        {
            try
            {
                var handler = CancelSOP;
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

        private void CancelButton_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                // ClosePopup();

                ClearControls();

                OnCancelSOP(new CancelSOPEventArgs());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnGoToNextStep(new GoToNextStepEventArgs
                    {
                        Confirmation = true
                    });
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
