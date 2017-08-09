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
using STC.Projects.WPFControlLibrary.MessageBoxControl;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for PatrolDetailsUserControl.xaml
    /// </summary>
    public partial class PatrolDetailsUserControl : UserControl
    {
        public PatrolDetailsUserControl(ServiceLayerReference.PatrolLastLocationDTO Patrol, SOPSources SOPSource, double Latitude, double Longitude, long NotificationId)
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            EsriMapView.FlowDirection = FlowDirection.LeftToRight;

            PatrolDetailsViewModel vm = new PatrolDetailsViewModel
            {
                Patrol = Patrol,
                SOPSource = SOPSource,
                EventLatitude = Latitude,
                EventLongitude = Longitude,
                NotificationId = NotificationId
            };

            DataContext = vm;

            ZoomOnMap(Latitude, Longitude, 1);
            AddGrphicLayer();

            if (Patrol.Latitude != null && Patrol.Longitude != null)
                vm.AddLayerContent("patrol", Patrol.Latitude.Value, Patrol.Longitude.Value);

            vm.AddLayerContent("event", Latitude, Longitude);

            ZoomToExtent();
        }

        private void ZoomOnMap(double Latitude, double Longitude, int Scale)
        {
            EsriMapView.SetView(new MapPoint(Longitude, Latitude, new SpatialReference(4326)), Scale);
        }

        public void AddGrphicLayer()
        {
            GraphicsLayer graphicsLayer = new GraphicsLayer();
            graphicsLayer.ID = "DispatchingPatrol";
            EsriMapView.Map.Layers.Add(graphicsLayer);
            Binding binding = new Binding(string.Format("LayersGraphicsDictionary[{0}]", graphicsLayer.ID));
            binding.Source = DataContext as PatrolDetailsViewModel;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(graphicsLayer, GraphicsLayer.GraphicsSourceProperty, binding);
        }

        private void ZoomToExtent()
        {
            var vm = DataContext as PatrolDetailsViewModel;
            if (vm == null)
                return;

            ObservableCollection<Graphic> layerCol = vm.GetLayerObservable();

            if (layerCol.Count == 0)
                return;

            List<double> Xs = new List<double>();
            List<double> Ys = new List<double>();
            for (int i = 0; i < layerCol.Count; i++)
            {
                if (!(layerCol[i].Geometry is Esri.ArcGISRuntime.Geometry.Polygon))
                {
                    Xs.Add(((MapPoint)layerCol[i].Geometry).X);
                    Ys.Add(((MapPoint)layerCol[i].Geometry).Y);
                }
            }

            if (layerCol.Count > 1 && Xs.Count > 0 && Ys.Count > 0)
            {
                Envelope myEnvelope = new Envelope(Xs.Min(), Ys.Min(), Xs.Max(), Ys.Max(), new SpatialReference(4326));
                EsriMapView.SetViewAsync(myEnvelope.GetCenter(), 70000);
            }
            else
            {
                if (!(layerCol[0].Geometry is Esri.ArcGISRuntime.Geometry.Polygon))
                {
                    ZoomOnMap(((MapPoint)layerCol[0].Geometry).Y, ((MapPoint)layerCol[0].Geometry).X, 50000);
                }
            }

        }

        public event CanvasEventHandler CloseCanvas;
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
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyStoryboard");
            sb.Begin();
            await Task.Delay(1000);

            CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            OnCloseCanvas(canvasEventArgs);
        }

        public event GoToNextStepEventHandler GoToNextStep;
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

        private void DispatchButton_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                var vm = DataContext as PatrolDetailsViewModel;
                if (vm == null)
                    return;

                //var msgBox = new MessageBoxUserControl("سوف يتم إرسال الدورية . هل أنت متأكد؟", true);
                var msgBox = new MessageBoxUserControl(Properties.Resources.strDispatchPatrolConfirmation, true);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();

                var res = msgBox.GetResult();

                if (res == false)
                    return;

                bool saved = false;
                switch (vm.SOPSource)
                {

                    case SOPSources.Fog:
                        saved = AddDuty(vm);
                        break;
                    case SOPSources.DetectedAccident:
                        saved = AddIncident(vm);
                        break;
                    case SOPSources.WantedCar:
                        saved = AddDuty(vm);
                        break;
                }

                if (saved)
                {
                    vm.Patrol.IsAssigned = true;
                    OnGoToNextStep(new GoToNextStepEventArgs
                    {
                        Confirmation = true
                    });

                    ClearControls();

                    var msgBoxDone = new MessageBoxUserControl(Properties.Resources.strDispatchedPatrolConfirm, false);
                    msgBoxDone.Owner = Window.GetWindow(this);
                    msgBoxDone.ShowDialog();

                    var resCon = msgBoxDone.GetResult();

                    CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

                    OnCloseCanvas(canvasEventArgs);
                }
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
            CommentText.Text = "";
        }

        private bool AddIncident(PatrolDetailsViewModel ViewModel)
        {
            var serviceClient = new TFMIntegrationServiceClient();

            var result = serviceClient.AddIncidentAsync(ViewModel.Patrol.PatrolOriginalId, DateTime.Now, ViewModel.EventLatitude, ViewModel.EventLongitude, CommentText.Text);

            return result.Result;
        }

        private bool AddDuty(PatrolDetailsViewModel ViewModel)
        {
            var serviceClient = new TFMIntegrationServiceClient();

            if (!serviceClient.ValidateBeforeAssignPatrol(ViewModel.NotificationId, ViewModel.Patrol.PatrolId))
            {
                //TODO: Show Message to tell the user that the patrol is already assigned.
                var msgBox = new MessageBoxUserControl(Properties.Resources.strPatrolAssignment, false);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();
                return false;//Or true (Based on the scenario).
            }

            var result = serviceClient.AddDutyAsync(ViewModel.Patrol.PatrolOriginalId, CommentText.Text, DateTime.Now, ViewModel.EventLatitude, ViewModel.EventLongitude, ViewModel.NotificationId, (int)ViewModel.Patrol.PatrolId);

            if (result.Result > 0)
            {
                serviceClient.UpdatePatrolCurrentTask(ViewModel.Patrol.PatrolOriginalId, result.Result);

                return true;
            }

            return false;
        }

        private void CancelButton_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();

                ClearControls();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            EsriMapView.ZoomAsync(1.1);
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            EsriMapView.ZoomAsync(0.9);
        }
    }
}
