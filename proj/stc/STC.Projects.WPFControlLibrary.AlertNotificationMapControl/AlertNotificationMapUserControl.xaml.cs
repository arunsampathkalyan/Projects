using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl
{
    /// <summary>
    /// Interaction logic for AlertNotificationMapUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "AlertNotificationMapUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AlertNotificationMapUserControl : UserControl, IUserControl
    {
        AlertNotificationMapViewModel _AlertNotificationMapViewModel = null;

        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 13) };


        #region Constractor
        public AlertNotificationMapUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();



            //esriMapView.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            //mapTipAssetsOrViolations.FlowDirection = Utility.GetLang() == "ar" ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;


            _AlertNotificationMapViewModel = new AlertNotificationMapViewModel();
            _AlertNotificationMapViewModel.NotificationRecieved += AlertNotificationMapViewModel_NotificationRecieved;


            //AddGrphicLayer(LayerTypeEnum.Traffic);

            this.DataContext = _AlertNotificationMapViewModel;

            CultureInfo cul = Utility.GetLang() == "ar" ? new CultureInfo("ar-Eg") : new CultureInfo(Utility.GetLang());


            timer.Tick += timer_Tick;

            //DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            //{

            //    this.TxtTime.Text = DateTime.Now.ToString("t", cul);

            //    this.TxtDate.Text = DateTime.Now.ToString("D", cul);

            //}, this.Dispatcher);



        }

        void AlertNotificationMapViewModel_NotificationRecieved(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    NotifyNewAlert();
                    //NotifyRemainingAlerts();
                    timer.Start();
                    //throw new NotImplementedException();
                });

        }


        #endregion

        #region Event Handelers

        void timer_Tick(object sender, EventArgs e)
        {
            if (!_AlertNotificationMapViewModel.IsAlertProcessing && _AlertNotificationMapViewModel.NotificationsToNotify != null && _AlertNotificationMapViewModel.NotificationsToNotify.Count > 0)
                NotifyRemainingAlerts();
            else
                timer.Stop();
        }

        void alertControl_AddChildContent(object sender, CanvasEventArgs e)
        {
            OnAddChildContent(this, e);
        }


        void alertControl_ProcessNextAlert(object sender, ProcessNextItemEventArgs e)
        {
            try
            {
                _AlertNotificationMapViewModel.IsAlertProcessing = !e.CanProcessNextItem;
                if (e.CanProcessNextItem)
                {
                    if (_AlertNotificationMapViewModel.NotificationsToNotify != null
                        && _AlertNotificationMapViewModel.NotificationsToNotify.Count > 0)
                    {
                        NotifyRemainingAlerts();
                        timer.Start();
                        //Approve
                        //
                        //_AlertNotificationMapViewModel.SelectedSupervisorNotificationDTO
                    }

                }
                else
                {

                    timer.Stop();
                }

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void radPanelBarNotfications_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var selectedSupervisorNotificantion = (e.OriginalSource as RadPanelBarItem).DataContext as SupervisorNotificationDTO;
            //NotifiedAlertViolatorDetailsUserControlViewModel selectedSupervisorNotificantionVM = new NotifiedAlertViolatorDetailsUserControlViewModel(selectedSupervisorNotificantion);
            //ucNotifiedAlertDetail.DataContext = selectedSupervisorNotificantionVM;

            //Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
            //OpenNotifiedAlertDetailsPopup.Begin();

            ShowNotifiedAlertDetails(selectedSupervisorNotificantion);
            alertControl_ProcessNextAlert(this, new ProcessNextItemEventArgs() { CanProcessNextItem = false });
        }



        public event CanvasEventHandler AddChildContent;

        public event GoToNextStepEventHandler GoToNextStep;

        //public event ProcessNextItemEventHandler ProcessNextAlert;

        protected virtual void OnProcessNextAlert(ProcessNextItemEventArgs E)
        {

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



        private void OnAddChildContent(object Sender, CanvasEventArgs E)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AddChildItem(E.ChildControl, E.Width);
                    ScaleGridBasicInfo();
                }
                );
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void AddChildItem(UIElement childControl, int width)
        {
          //  PopupCanvas.Children.Clear();

            PopupCanvas.Content = childControl;

            this.Publish(new ScaleMessage() { Width = 490 });

            //Canvas.SetRight(PopupCanvas, width);
            SubListPopup.Visibility = Visibility.Visible;

            // PopupCanvas.Visibility = Visibility.Visible;

        }

        private void CloseCanvas()
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    //var items = PopupCanvas.Children;
                    //foreach (var item in items)
                    //{
                    //    UnRegisterReqEvents(item);
                    //}
                    //PopupCanvas.Children.Clear();
                    SubListPopup.Visibility =  Visibility.Collapsed;
                  //  PopupCanvas.Visibility = Visibility.Hidden;
                    this.Publish(new ScaleMessage() { Width = 220 });
                });


            alertControl_ProcessNextAlert(this, new ProcessNextItemEventArgs() { CanProcessNextItem = true });
        }


        private void ScaleGridBasicInfo()
        {

            //gridItemBasicInfo.Width = 270;
        }

        private void ReturnToDefaultScaleGridBasicInfo()
        {


            //gridItemBasicInfo.Width = 0;
        }

        private void ScaleSOP()
        {
            DoubleAnimation da = (DoubleAnimation)TryFindResource("ScaleSOPControl");

            //radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);
            //this.Publish(new ScaleMessage() { Width = 490 });
        }

        private void ReturnToDefaultScaleSOP()
        {
            //DoubleAnimation da = (DoubleAnimation)TryFindResource("ReturnToDefaultScaleSOPControl");

            //radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);
            //this.Publish(new ScaleMessage() { Width = 0 });
        }

        private void OnCloseCanvas(object Sender, CanvasEventArgs E)
        {
            try
            {
                CloseCanvas();
                ReturnToDefaultScaleGridBasicInfo();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void RegisterReqEvents(object obj)
        {
            Utility.RegisterEvent(obj, this, "CloseCanvas", "OnCloseCanvas"); // register close event
            //Utility.RegisterEvent(obj, this, "GoToNextStep", "SOPList_GoToNextStep"); // register next event
            //Utility.RegisterEvent(obj, this, "CancelSOP", "SOPList_CancelSOP"); // register cancel event
        }

        private void UnRegisterReqEvents(object obj)
        {
            Utility.UnRegisterEvent(obj, this, "CloseCanvas", "OnCloseCanvas"); // register close event
            //Utility.UnRegisterEvent(obj, this, "GoToNextStep", "SOPList_GoToNextStep"); // register next event
            //Utility.UnRegisterEvent(obj, this, "CancelSOP", "SOPList_CancelSOP"); // register cancel event
        }


        #endregion

        #region Methods


        private  void NotifyNewAlert()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //timer.Start();
                //bool canDelay = false;
                if (_AlertNotificationMapViewModel.NewNotificationToNotify != null)
                {

                    //NotifyAlert(_AlertNotificationMapViewModel.NewNotificationToNotify);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (!_AlertNotificationMapViewModel.SupervisorNotifications.Contains(_AlertNotificationMapViewModel.NewNotificationToNotify))
                          _AlertNotificationMapViewModel.SupervisorNotifications.Add(_AlertNotificationMapViewModel.NewNotificationToNotify);
                        _AlertNotificationMapViewModel.NotificationsListHeaderCount =  _AlertNotificationMapViewModel.SupervisorNotifications.Count +string.Empty ;
                        _AlertNotificationMapViewModel.NewNotificationToNotify = null;
                    }
                    );

                    //canDelay = true;
                }

            }
            );


        }


        private void NotifyRemainingAlerts()
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    //timer.Start();
                    bool canDelay = false;
                    if (!_AlertNotificationMapViewModel.IsAlertProcessing
                        && _AlertNotificationMapViewModel.NotificationsToNotify != null && _AlertNotificationMapViewModel.NotificationsToNotify.Count > 0)
                    {
                        //if (canDelay) await Task.Delay(13000);
                        //SupervisorNotificationDTO alertToNotify = new SupervisorNotificationDTO(); //get next alert from the list
                        if (_AlertNotificationMapViewModel.NotificationsToNotify.Count > 0)
                        {
                 //           NotifyAlert(_AlertNotificationMapViewModel.NotificationsToNotify[0]);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                _AlertNotificationMapViewModel.SupervisorNotifications.Add(_AlertNotificationMapViewModel.NotificationsToNotify[0]);
                                _AlertNotificationMapViewModel.NotificationsListHeaderCount =   _AlertNotificationMapViewModel.SupervisorNotifications.Count + string.Empty;
                                _AlertNotificationMapViewModel.NotificationsToNotify.RemoveAt(0); // check for error
                            }
                            );

                            //canDelay = true;
                        }

                    }
                });


        }

        //private int GetSupervisorID()
        //{
        //    return _AlertNotificationMapViewModel.GetSuperVisorID();
        //}

        private int GetCurrentUserID()
        {
            return this.GetCurrentUserId();
        }

        private void NotifyAlert(SupervisorNotificationDTO alertToNotify)
        {
            CanvasEventArgs canvasEventArgs = null;
            if (_AlertNotificationMapViewModel.CurrentUserId == alertToNotify.ReceiverId && alertToNotify.ResponseToId != null && alertToNotify.ResponseToId > 0)
            {
                var alertControl = new AlertOperatorUserControl(alertToNotify);

                alertControl.ProcessNextAlert += alertControl_ProcessNextAlert;
                alertControl.AddChildContent += alertControl_AddChildContent;
                alertControl.GoToNextStep += alertControl_GoToNextStep;

                canvasEventArgs = new CanvasEventArgs
                {
                    ChildControl = alertControl,
                    Width = 400
                };
            }
            else
            {
                var alertControl = new AlertSuperVisorUserControl(alertToNotify);

                alertControl.ProcessNextAlert += alertControl_ProcessNextAlert;
                alertControl.AddChildContent += alertControl_AddChildContent;

                canvasEventArgs = new CanvasEventArgs
                {
                    ChildControl = alertControl,
                    Width = 400
                };
            }

            OnAddChildContent(this, canvasEventArgs);

        }

        void alertControl_GoToNextStep(object sender, GoToNextStepEventArgs e)
        {
            if (e.Notification.Status != SupervisorNotificationStatus.Pending)
            {
                var notification = _AlertNotificationMapViewModel.SupervisorNotifications.Where(x => x.DangerousViolatorDetails.PlateNumber == e.Notification.DangerousViolatorDetails.PlateNumber).FirstOrDefault();
                if (notification != null)
                {
                    Application.Current.Dispatcher.Invoke(() => _AlertNotificationMapViewModel.SupervisorNotifications.Remove(notification));
                    _AlertNotificationMapViewModel.NotificationsListHeaderCount =  _AlertNotificationMapViewModel.SupervisorNotifications.Count + string.Empty;
                }
            }
        }


        private void ShowNotifiedAlertDetails(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            var detailsCtrl = new NotifiedAlertViolatorDetailsUserControl(supervisorNotificationDTO);

            detailsCtrl.CloseCanvas += OnCloseCanvas;
            detailsCtrl.GoToNextStep += alertControl_GoToNextStep;


            CanvasEventArgs canvasEventArgs = new CanvasEventArgs
            {
                ChildControl = detailsCtrl,
                Width = 400
            };

            OnAddChildContent(this, canvasEventArgs);
        }
        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            Storyboard CloseDetailsPopup = (Storyboard)TryFindResource("CloseViolatorDetails");
            CloseDetailsPopup.Begin();

            //Remove this later

            Storyboard showAlert = (Storyboard)TryFindResource("showAlert");
            showAlert.Begin();
        }

        private void gridSearchDangerousViolator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard OpenDetailsPopup = (Storyboard)TryFindResource("OpenViolatorDetailsBottomToTop");
            OpenDetailsPopup.Begin();

        }

        private void btnRejectReportedAlert_Click(object sender, RoutedEventArgs e)
        {
            //Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
            //OpenNotifiedAlertDetailsPopup.Begin();

            Storyboard CloseNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("CloseNotifiedViolatorDetailsinRight");
            CloseNotifiedAlertDetailsPopup.Begin();
        }

        private void btnViewDetailsReportedAlert_Click(object sender, RoutedEventArgs e)
        {
            //Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
            //OpenNotifiedAlertDetailsPopup.Begin();
        }



        private void RadExpander_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void expanderNotificationList_Collapsed(object sender, RoutedEventArgs e)
        {
            //Remove this later

            //Storyboard showAlert = (Storyboard)TryFindResource("showAlert");
            //showAlert.Begin();

            //NotifyAlert(new SupervisorNotificationDTO());

            //NotifyRemainingAlerts();
            //timer.Start();

        }

        #endregion

        private void AlertNotificationUIcontorl_Loaded(object sender, RoutedEventArgs e)
        {
            _AlertNotificationMapViewModel.CurrentUserId = Utility.GetCurrentUserId(this);
            //_AlertNotificationMapViewModel.CurrentUserId = this.GetCurrentUserID();
            //if (_AlertNotificationMapViewModel.CurrentUserId == 0)
            //    _AlertNotificationMapViewModel.CurrentUserId = 1;
            _AlertNotificationMapViewModel.GetUnNoticedNotifications();
        }


        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_AlertNotificationMapViewModel.SupervisorNotifications.Count > 0)
            {
                ListPopup.Visibility = ListPopup.Visibility== Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
                
                if(SubListPopup.Visibility== System.Windows.Visibility.Visible)
                {
                    SubListPopup.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            // SubListPopup.IsOpen = true;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selecteditem = SupervisorListView.SelectedItem;
            //var selectedSupervisorNotificantion = selecteditem as SupervisorNotificationDTO;
            ////NotifiedAlertViolatorDetailsUserControlViewModel selectedSupervisorNotificantionVM = new NotifiedAlertViolatorDetailsUserControlViewModel(selectedSupervisorNotificantion);
            ////ucNotifiedAlertDetail.DataContext = selectedSupervisorNotificantionVM;

            ////Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
            ////OpenNotifiedAlertDetailsPopup.Begin();

            //ShowNotifiedAlertDetails(selectedSupervisorNotificantion);
            //alertControl_ProcessNextAlert(this, new ProcessNextItemEventArgs() { CanProcessNextItem = false });
        }

      

        private void ListItemSelection(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Grid;
            if (border != null)
            {
                var selecteditem = border.DataContext;
                var selectedSupervisorNotificantion = selecteditem as SupervisorNotificationDTO;
                //NotifiedAlertViolatorDetailsUserControlViewModel selectedSupervisorNotificantionVM = new NotifiedAlertViolatorDetailsUserControlViewModel(selectedSupervisorNotificantion);
                //ucNotifiedAlertDetail.DataContext = selectedSupervisorNotificantionVM;

                //Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
                //OpenNotifiedAlertDetailsPopup.Begin();

                ShowNotifiedAlertDetails(selectedSupervisorNotificantion);
            }
            alertControl_ProcessNextAlert(this, new ProcessNextItemEventArgs() { CanProcessNextItem = false });

        }
    }


}