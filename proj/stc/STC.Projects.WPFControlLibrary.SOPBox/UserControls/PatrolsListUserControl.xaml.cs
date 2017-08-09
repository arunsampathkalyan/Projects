using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using Telerik.Windows.Controls;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Geometry;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using STC.Projects.WPFControlLibrary.SOPBox.TFMServiceReference;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for PatrolsListUserControl.xaml
    /// </summary>
    public partial class PatrolsListUserControl : UserControl, IUserControl
    {
        PatrolsListViewModel vm = null;
        public PatrolsListUserControl()
        {

            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();
            vm = new PatrolsListViewModel();
            DataContext = vm;
            listViewPatrolsList.Loaded += listViewPatrolsList_Loaded;

        }

        void listViewPatrolsList_Loaded(object sender, RoutedEventArgs e)
        {


            SendZoomMessage();

            if (vm != null)
            {
                vm.PatrolsAdded += vm_PatrolsAdded;
            }
        }

        public void SetNotificationID(long notificationId)
        {
            if (vm != null)
                vm.NotificationId = notificationId;
        }


        public void ProcessMessage(FogLocationModel Location)
        {
            vm.Latitude = Location.Latitude;
            vm.Longitude = Location.Longitude;

            vm.SOPSource = SOPSources.Fog;

            vm.GetAllPatrolsAroundPoint();
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            vm.Latitude = Location.Latitude;
            vm.Longitude = Location.Longitude;

            vm.SOPSource = SOPSources.DetectedAccident;

            vm.GetAllPatrolsAroundPoint();
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            vm.Latitude = Location.Latitude;
            vm.Longitude = Location.Longitude;

            vm.SOPSource = SOPSources.WantedCar;

            vm.GetAllPatrolsAroundPoint();
        }

        public void SendZoomMessage()
        {
            var message = new ZoomToExtend();


            if (vm == null)
                return;

            foreach (PatrolLastLocationDTO item in vm.PatrolsList)
            {
                if (item.Longitude.HasValue && item.Latitude.HasValue)
                    message.points.Add(new CustomPoints() { x = item.Latitude.Value, y = item.Longitude.Value });
            }
            message.points.Add(new CustomPoints() { x = vm.Latitude, y = vm.Longitude });
            GetParent().Publish(message);
        }

        void vm_PatrolsAdded(object sender, EventArgs e)
        {
            SendZoomMessage();
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
        void detailsCtrl_CloseCanvas(object sender, CanvasEventArgs e)
        {
            try
            {
                OnCloseCanvas(e);


            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnClosePopups_Click(object Sender, RoutedEventArgs E)
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


        public event CanvasEventHandler AddChildContent;
        protected virtual void OnAddChildContent(CanvasEventArgs E)
        {
            try
            {
                var handler = AddChildContent;
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

        private void detailsCtrl_GoToNextStep(object sender, GoToNextStepEventArgs e)
        {
            try
            {
                OnGoToNextStep(e);

                var selectedItem = vm.SelectedPatrol;
                if (selectedItem == null)
                    return;
                selectedItem.ImgCheckedSource = "../images/true.png";

                vm.SelectedPatrol = null;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void PatrolsList_OnSelectionChanged(object Sender, SelectionChangedEventArgs E)
        {
            try
            {
                var selectedItem = vm.SelectedPatrol;
                if (selectedItem == null)
                    return;

                //PatrolsListViewModel vm = DataContext as PatrolsListViewModel;
                if (vm == null)
                    return;

                //var detailsCtrl = new PatrolDetailsUserControl(selectedItem, vm.SOPSource, vm.Latitude, vm.Longitude,vm.NotificationId);

                //detailsCtrl.CloseCanvas += detailsCtrl_CloseCanvas;
                //detailsCtrl.GoToNextStep += detailsCtrl_GoToNextStep;

                //CanvasEventArgs canvasEventArgs = new CanvasEventArgs
                //{
                //    ChildControl = detailsCtrl,
                //    Width = 730
                //};

                //OnAddChildContent(canvasEventArgs);





                ZoomOnMap(vm.Latitude, vm.Longitude, 1);
                AddGrphicLayer();

                if (selectedItem.Latitude != null && selectedItem.Longitude != null)
                    vm.AddLayerContent("patrol", selectedItem.Latitude.Value, selectedItem.Longitude.Value);

                vm.AddLayerContent("event", vm.Latitude, vm.Longitude);

                ZoomToExtent();


                //vm.SelectedPatrol = null;
                if (selectedItem.Latitude.HasValue && selectedItem.Longitude.HasValue)
                {
                    var zoomMessage = new SOPMapZoom() { Lat = selectedItem.Latitude.Value, Lon = selectedItem.Longitude.Value };
                    //var zoomMessage = new ZoomToExtend();
                    //zoomMessage.points.Add(new CustomPoints(){x = vm.Latitude,y=vm.Longitude});
                    //zoomMessage.points.Add(new CustomPoints(){x = selectedItem.Latitude.Value,y=selectedItem.Longitude.Value});//{ Lat = selectedItem.Latitude.Value, Lon = selectedItem.Longitude.Value };
                    var parent = GetParent();
                    if (parent == null)
                        return;
                    parent.Publish(zoomMessage);
                }

                //selectedItem.ImgCheckedSource = "../images/true.png";

                //var parent = GetParent() as SOPList;
                //if (parent == null)
                //    return;
                //var parentVM = parent.DataContext as SOPViewModel;
                //if (parentVM == null)
                //    return;
                //parentVM.SetStepChecked(vm);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private UserControl GetParent()
        {
            var parent = this.VisualParent.ParentOfType<UserControl>() as UserControl;
            return parent;
        }
        //private void PublishMessages(PatrolLastLocationDTO SelectedPatrol)
        //{
        //    if (SelectedPatrol.Longitude.HasValue && SelectedPatrol.Latitude.HasValue)
        //    {
        //        var clearNotificationLayer = new SOPMapClearObjects();

        //        var drawMessage = new SOPMapDraw() { Lat = SelectedPatrol.Latitude.Value, Lon = SelectedPatrol.Longitude.Value, ObjectTypeToDraw = (int)MarkerType.Assets, ObjectToDraw = SelectedPatrol };
        //        var zoomMessage = new SOPMapZoom() { Lat = SelectedPatrol.Latitude.Value, Lon = SelectedPatrol.Longitude.Value };

        //        var parent = GetParent();
        //        if (parent == null)
        //            return;

        //        //parent.Publish(clearNotificationLayer);
        //        parent.Publish(drawMessage);
        //        parent.Publish(zoomMessage);
        //    }
        //}


        #region SelectedPatrol


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



        private void DispatchButton_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                var vm = DataContext as PatrolsListViewModel;
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
                    vm.SelectedPatrol.IsAssigned = true;
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
            txtBlkComments.Text = "";
        }

        private bool AddIncident(PatrolsListViewModel ViewModel)
        {
            var serviceClient = new TFMIntegrationServiceClient();

            var result = serviceClient.AddIncidentAsync(ViewModel.SelectedPatrol.PatrolOriginalId, DateTime.Now, ViewModel.Latitude, ViewModel.Longitude, txtBlkComments.Text);

            return result.Result;
        }

        private bool AddDuty(PatrolsListViewModel ViewModel)
        {
            var serviceClient = new TFMIntegrationServiceClient();

            if (!serviceClient.ValidateBeforeAssignPatrol(ViewModel.NotificationId, ViewModel.SelectedPatrol.PatrolId))
            {
                //TODO: Show Message to tell the user that the patrol is already assigned.
                var msgBox = new MessageBoxUserControl(Properties.Resources.strPatrolAssignment, false);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();
                return false;//Or true (Based on the scenario).
            }

            var result = serviceClient.AddDutyAsync(ViewModel.SelectedPatrol.PatrolOriginalId, txtBlkComments.Text, DateTime.Now, ViewModel.Latitude, ViewModel.Longitude, ViewModel.NotificationId, (int)ViewModel.SelectedPatrol.PatrolId);

            if (result.Result > 0)
            {
                serviceClient.UpdatePatrolCurrentTask(ViewModel.SelectedPatrol.PatrolOriginalId, result.Result);

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

        #endregion

    }
}
