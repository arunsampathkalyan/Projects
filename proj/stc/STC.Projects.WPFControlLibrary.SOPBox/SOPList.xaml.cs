using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using Telerik.Windows;
using System;
using System.Reflection;
using System.Linq;
using STC.Projects.WPFControlLibrary.SOPBox.UserControls;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;
using System.Windows.Input;


namespace STC.Projects.WPFControlLibrary.SOPBox
{
    /// <summary>
    /// Interaction logic for SOPList.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "SOPList")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SOPList : UserControl, IUserControl
    {
        private SOPViewModel sopViewModel;
        private int _sopActiveStepIndex;
        public SOPList()
        {
            _sopActiveStepIndex = 0;
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();



            sopViewModel = new SOPViewModel();
            sopViewModel.LoadSteps += sopViewModel_LoadSteps;
            DataContext = sopViewModel;
            if (SopListPanel.Items.Count > 0)
                SopListPanel.SelectedIndex = 0;



            SetDimentionsForMediaCanvas();



            this.Unloaded += SOPList_Unloaded;
        }


        private void SetDimentionsForMediaCanvas()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            PopupMediaCanvas.Width = desktopWorkingArea.Width - 300;
            PopupMediaCanvas.Height = desktopWorkingArea.Height - 100;

            PopupMediaCanvas.Margin = new Thickness(-(PopupMediaCanvas.Width), 0, 0, 0);

            //borderImageMediaControl.Visibility = Visibility.Visible;
        }

        private void SOPList_Unloaded(object sender, RoutedEventArgs e)
        {
            sopViewModel.LoadSteps -= sopViewModel_LoadSteps;
        }

        void SOPList_CancelSOP(object sender, CancelSOPEventArgs e)
        {
            try
            {
                FinishSOP(sopViewModel);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        void SOPList_GoToNextStep(object sender, GoToNextStepEventArgs e)
        {
            try
            {
                var vm = DataContext as SOPViewModel;
                //var sopId = sopViewModel.SopList[_sopActiveStepIndex].SOPId;
                //vm.SaveSopStep(sopId, this.GetCurrentUserId());
                Application.Current.Dispatcher.Invoke(() => OpenNextStep(e));
                this.Publish(new ChangeNotificationStatus { MessageId = vm.MessageId });

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        void sopViewModel_LoadSteps(object sender, CanvasEventArgs e)
        {
            try
            {
                if (sopViewModel.SopList.Count > 0)
                {
                    Application.Current.Dispatcher.Invoke(() => OpenDetails(sopViewModel.SopList[0]));
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        //public void Go(IncidentSOPMessages Message)
        //{
        //    sopViewModel.GetIncidentSOP(Message.IncidentObj);
        //}

        //public void Go(ViolationSOPMessages Message)
        //{
        //    sopViewModel.GetViolationSOP(Message.ViolationObj);
        //}

        public void Go(SOPGeneralMessage Message)
        {
            _sopActiveStepIndex = 0;

            sopViewModel.GetSOP(Message);
        }

        public void Go(FogEventToSOPMessage Message)
        {
            ScaleSOP();

            _sopActiveStepIndex = 0;
            var client = new ServiceLayerReference.ServiceLayerClient();
            bool isArabic = Utility.GetLang() == "ar";
            sopViewModel.EventDetailsList = new System.Collections.ObjectModel.ObservableCollection<EventDetailModel>() {
                new EventDetailModel
                {
                    Name = isArabic ? "التاريخ والوقت:" : "Date & Time:",
                    Value = Message.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                }, new EventDetailModel
                {
                    Name = isArabic ? "سبب إنعدام الرؤية:" : "Low Visability Reason:",
                    Value = isArabic ? "ضباب":"Fog"
                }, new EventDetailModel
                {
                    Name = isArabic ? "المكان:" :  "Location:",
                    Value = client.GetAssetLocation((int)Message.TowerId,isArabic)
                } };

            sopViewModel.GetSOP(new SOPGeneralMessage
            {
                MessageId = Message.MessageId,
                GeneralType = SOPSources.Fog,
                OriginalObject = Message,
                NotificationId = Message.NotificationId
            });
        }

        public void Go(DetectedAccidentSOPMessage Message)
        {
            ScaleSOP();

            _sopActiveStepIndex = 0;

            sopViewModel.GetSOP(new SOPGeneralMessage
            {
                MessageId = Message.MessageId,
                GeneralType = SOPSources.DetectedAccident,
                OriginalObject = Message,
                NotificationId = Message.NotificationId
            });
        }

        public void Go(WantedCarToSOPMessage Message)
        {
            ScaleSOP();

            ScaleGridBasicInfo();

            _sopActiveStepIndex = 0;
            var client = new ServiceLayerReference.ServiceLayerClient();
            bool isArabic = Utility.GetLang() == "ar";
            long messageId = 0;
            string type = isArabic ? Message.Discription : Message.EnglishDiscription;
            if (long.TryParse(Message.MessageId, out messageId))
            {
                var ruleDTO = client.GetMessageBusinessRule(messageId);
                if (ruleDTO != null)
                {
                    type = ruleDTO.BusinessName;
                }
            }
            sopViewModel.EventDetailsList = new System.Collections.ObjectModel.ObservableCollection<EventDetailModel>() {
                new EventDetailModel
                {
                    Name = isArabic ? "نوع المخالفة" :  "Violation Type",
                    Value = type
                }, new EventDetailModel
                {
                    Name =  isArabic ? "التاريخ والوقت" :  "Date & Time",
                    Value = Message.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                }, new EventDetailModel
                {
                    Name = isArabic ? "نوع المركبة" :  "Vehicle Type",
                    Value = isArabic ? "سيارة":"Car"
                }, new EventDetailModel
                {
                    Name = Properties.Resources.strPlateNum,
                    Value = Message.VehiclePlateNumber
                }, new EventDetailModel
                {
                    Name = isArabic ? "المكان" :  "Location",
                    Value = client.GetAssetLocation((int)Message.TowerId,isArabic)
                } , new EventDetailModel {
                    Name = isArabic ? "رقم الرادار" :  "Radar Code",
                    Value = client.GetAssetSerialNumber(Message.TowerId)
                } };

            var assignedPatrols = sopViewModel.GetAssignedPatrols(Message.NotificationId);
            if (assignedPatrols != null && assignedPatrols.Any())
            {
                foreach (var assignedPatrol in assignedPatrols)
                {
                    sopViewModel.EventDetailsList.Add(new EventDetailModel()
                    {
                        Name = isArabic ? "رقم الدورية المدرجة" : "Assigned patrol code",
                        Value = assignedPatrol.PatrolCode
                    });
                }
            }
            sopViewModel.GetSOP(new SOPGeneralMessage
            {
                MessageId = Message.MessageId,
                GeneralType = SOPSources.WantedCar,
                OriginalObject = Message,
                NotificationId = Message.NotificationId
            });
        }
        public void Go(TruckViolationToSOPMessage message)
        {
            ScaleSOP();
            _sopActiveStepIndex = 0;
            var client = new ServiceLayerReference.ServiceLayerClient();
            bool isArabic = Utility.GetLang() == "ar";
            sopViewModel.EventDetailsList = new System.Collections.ObjectModel.ObservableCollection<EventDetailModel>() {
                new EventDetailModel
                {
                    Name = isArabic ? "التاريخ والوقت" :  "Date & Time",
                    Value = message.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                }, new EventDetailModel
                {
                    Name = isArabic ? "نوع المركبة" :  "Vehicle Type",
                    Value = isArabic ? "شاحنة" : "Truck"
                }, new EventDetailModel
                {
                    Name = Properties.Resources.strPlateNum,
                    Value = message.TruckPlateNumber
                }, new EventDetailModel
                {
                    Name = isArabic ? "المكان" :  "Location",
                    Value = client.GetAssetLocation((int)message.TowerId,isArabic)
                } , new EventDetailModel {
                    Name = isArabic ? "رقم الرادار" :  "Radar Code",
                    Value = client.GetAssetSerialNumber(message.TowerId)
                } };

            var assignedPatrols = sopViewModel.GetAssignedPatrols(message.NotificationId);
            if (assignedPatrols != null && assignedPatrols.Any())
            {
                foreach (var assignedPatrol in assignedPatrols)
                {
                    sopViewModel.EventDetailsList.Add(new EventDetailModel()
                    {
                        Name = isArabic ? "رقم الدورية المدرجة" : "Assigned patrol code",
                        Value = assignedPatrol.PatrolCode
                    });
                }
            }
            sopViewModel.GetSOP(new SOPGeneralMessage
            {
                MessageId = message.MessageId,
                GeneralType = SOPSources.TruckViolation,
                OriginalObject = new WantedCarToSOPMessage
                {
                    CreatedDate = message.CreatedDate,
                    Discription = message.Discription,
                    Latitude = message.Latitude,
                    Longitude = message.Longitude,
                    MessageId = message.MessageId,
                    NotificationId = message.NotificationId,
                    TowerId = message.TowerId,
                    VehiclePlateNumber = message.TruckPlateNumber
                },
                NotificationId = message.NotificationId
            });
        }
        public void Go(WantedCarLocationChanged message)
        {
            //ScaleSOP();

            var vm = this.DataContext as SOPViewModel;
            MessageTypeSOPParentModel obj = null;
            MessageTypeSOPModel SOPItem = null;

            if (vm != null && vm.SopList.Count > 0)
            {
                var currentMessage = vm.GetCurrentMessageObject();

                if (currentMessage != null && (currentMessage as SOPGeneralMessage).OriginalObject != null)
                {
                    WantedCarToSOPMessage wantedcar = ((SOPGeneralMessage)currentMessage).OriginalObject as WantedCarToSOPMessage;

                    if (wantedcar != null && wantedcar.VehiclePlateNumber == message.PlateNumber)
                    {
                        wantedcar.Latitude = message.Lat.Value;
                        wantedcar.Longitude = message.Lon.Value;
                    }
                    else
                        return;

                    foreach (var item in vm.SopList.Where(x => x.SOPItems.Any(y => y.UserControlModel != null && y.UserControlModel.Any() && y.UserControlModel.FirstOrDefault() is PatrolsListViewModel)).ToList())
                    {
                        var patrolListVM = item.SOPItems.FirstOrDefault().UserControlModel.FirstOrDefault();
                        Utility.CallMethodUsingReflector(patrolListVM, "ProcessMessage", new WantedCarModel { Latitude = wantedcar.Latitude, Longitude = wantedcar.Longitude, TowerId = wantedcar.TowerId, VehiclePlateNumber = wantedcar.VehiclePlateNumber, PlateKind = wantedcar.VehiclePlateKind, PlateType = wantedcar.VehiclePlateType, PlateSource = wantedcar.VehiclePlateSource, PlateColor = wantedcar.VehiclePlateColor });
                        //if (item.SOPItems.Count > 0)
                        //{
                        //    MessageTypeSOPModel m = item.SOPItems.FirstOrDefault(x => x.ListUserControlMessageType == "PatrolsListViewModel");
                        //    if(m != null)
                        //    {
                        //        Utility.CallMethodUsingReflector(m, "ProcessMessage", new WantedCarModel { Latitude = wantedcar.Latitude,Longitude = wantedcar.Longitude,TowerId = wantedcar.TowerId,VehiclePlateNumber = wantedcar.VehiclePlateNumber});
                        //        return;
                        //    }
                        //    //MessageTypeSOPModel m = item.SOPItems.Where(x => x.UserControlDetailsControl == "CamerasListActionPanelUserControl").FirstOrDefault();

                        //    //if (m != null)
                        //    //{
                        //    //    obj = item;
                        //    //    SOPItem = m;
                        //    //    break;
                        //    //}
                        //}
                    }

                    if (obj != null)
                    {
                        OpenDetails(obj);

                        //PopupCanvas.Children.Clear();

                        //if (SOPItem.UserControlDetailsControl != null)
                        //{
                        //    var usrCnt = sopViewModel.GetUserControl(SOPItem.UserControlDetailsControl, SOPItem.DetailsUserControlMessageType, SOPItem.DetailsMessageXSLT) as UserControl;
                        //    if (usrCnt != null)
                        //    {
                        //        Utility.CallMethodUsingReflector(usrCnt, "UpdateCameras", message);
                        //    }
                        //}
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = DataContext as SOPViewModel;
                if (vm == null)
                    return;
                if (!vm.IsAllStepsChecked())
                {
                    var msgBox = new MessageBoxUserControl(Properties.Resources.strEndSOP, true);
                    msgBox.Owner = Window.GetWindow(this);
                    msgBox.ShowDialog();

                    var res = msgBox.GetResult();

                    //MessageBoxResult messageBoxResult = MessageBox.Show("Not all steps are checked . Are you sure you want to finish the SOP?", "Finish Confirmation", MessageBoxButton.YesNo);
                    if (res == true)
                    {
                        FinishSOP(vm);
                    }
                }
                else
                {
                    FinishSOP(vm);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = DataContext as SOPViewModel;
                if (vm == null)
                    return;

                FinishSOP(vm, true);

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void FinishSOP(SOPViewModel vm, bool IsCancle = false)
        {

            // VlcContext.CloseAll();
            this.Publish(new ClearAllNotificationLayer() { MessageId = vm.MessageId, IsCancle = IsCancle });
            this.Publish(new ShowAllLayers());
            this.Publish(new ShowNotificationBox());

            if (!IsCancle)
            {
                if (vm._currentMessage.GeneralType == SOPSources.WantedCar || vm._currentMessage.GeneralType == SOPSources.TruckViolation)
                    this.Publish(new UnregisterLiveTrackingDependency());

                this.Publish(new FinishEvent() { EventId = vm.MessageId });
            }
            //radExpanderSOP.IsExpanded = false;
            //radExpanderSOP.IsEnabled = false;

            vm.IsExpanded = false;
            vm.IsEnabled = false;

            PopupCanvas.Children.Clear();

            ReturnToDefaultScaleGridBasicInfo();

            ReturnToDefaultScaleSOP();
        }

        private void OnAddChildContent(object Sender, CanvasEventArgs E)
        {
            try
            {
                AddChildItem(E.ChildControl, E.Width);
                //ScaleGridBasicInfo();
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
            PopupCanvas.Children.Clear();

            PopupCanvas.Children.Add(childControl);

            //this.Publish(new ScaleMessage() { Width = 490 });

            //Canvas.SetRight(PopupCanvas, width);

            PopupCanvas.Visibility = Visibility.Visible;

        }

        private void CloseCanvas()
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var items = PopupCanvas.Children;
                    foreach (var item in items)
                    {
                        UnRegisterReqEvents(item);
                    }
                    PopupCanvas.Children.Clear();
                    PopupCanvas.Visibility = Visibility.Hidden;
                    this.Publish(new ScaleMessage() { Width = 220 });
                });
        }


        private void ScaleGridBasicInfo()
        {
            //DoubleAnimation da = (DoubleAnimation)TryFindResource("ScaleGridBasicInfo");

            //radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);


            gridItemBasicInfo.Width = 210;
        }

        private void ReturnToDefaultScaleGridBasicInfo()
        {
            //DoubleAnimation da = (DoubleAnimation)TryFindResource("ReturnToDefaultScaleGridBasicInfo");

            //radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);
            //this.Publish(new ScaleMessage() { Width = 0 });

            gridItemBasicInfo.Width = 0;
        }

        private void ScaleSOP()
        {
            CloseCanvas();

            DoubleAnimation da = (DoubleAnimation)TryFindResource("ScaleSOPControl");

            radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);
            this.Publish(new ScaleMessage() { Width = 220 });
        }

        private void ReturnToDefaultScaleSOP()
        {
            DoubleAnimation da = (DoubleAnimation)TryFindResource("ReturnToDefaultScaleSOPControl");

            radExpanderSOP.BeginAnimation(Grid.WidthProperty, da);
            this.Publish(new ScaleMessage() { Width = 0 });
        }

        private void OnCloseCanvas(object Sender, CanvasEventArgs E)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CloseCanvas();
                    //ReturnToDefaultScaleGridBasicInfo();
                });

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
        private void SopListPanel_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var obj = ((Telerik.Windows.Controls.RadPanelBarItem)e.OriginalSource).DataContext as MessageTypeSOPParentModel;

            if (obj != null)
            {
                if (obj.SOPItems[0].UserControlModel == null)
                {
                    obj.IsExpanded = false;
                    // e.Handled = true;
                }
                if (obj.IsExpanded)
                {
                    var item = obj.SOPItems[0].UserControlModel.FirstOrDefault();
                    if (item is AssignedPatrolsViewModel)
                    {
                        ((AssignedPatrolsViewModel)item).GetAssignedPatrols();
                        if (!((AssignedPatrolsViewModel)item).AssignedPatrols.Any())
                        {
                            obj.IsExpanded = false;
                        }
                    }
                }
            }
        }
        private void SopListPanel_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                var obj = ((Telerik.Windows.Controls.RadPanelBarItem)e.OriginalSource).DataContext as MessageTypeSOPParentModel;
                if (obj != null)
                {
                    var sopObjIndex = sopViewModel.SopList.IndexOf(sopViewModel.SopList.FirstOrDefault(x => x.SOPId == obj.SOPId));
                    if (sopObjIndex != _sopActiveStepIndex)
                    {
                        _sopActiveStepIndex = sopObjIndex;
                    }
                    //else
                    //{
                    //    // obj.IsExpanded = true;
                    //    if (obj.SOPItems != null && obj.SOPItems.Count > 0)
                    //        HandlePatrolClick(obj.SOPItems[0]);
                    //}
                    OpenDetails(obj);

                    if (obj.SOPItems[0].UserControlModel == null)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void OpenNextStep(GoToNextStepEventArgs E)
        {
            try
            {
                if (E.Confirmation && sopViewModel.SopList.Count > _sopActiveStepIndex)
                {
                    sopViewModel.SopList[_sopActiveStepIndex].ImgCheckedSource = "images/true.png";
                    sopViewModel.SopList[_sopActiveStepIndex].IsExpanded = false;
                    sopViewModel.SopList[_sopActiveStepIndex].IsChecked = true;
                }

                if (_sopActiveStepIndex >= 0 && _sopActiveStepIndex < sopViewModel.SopList.Count - 1)
                {
                    _sopActiveStepIndex++;
                    if (sopViewModel.SopList.Count > _sopActiveStepIndex)
                    {
                        sopViewModel.SopList[_sopActiveStepIndex].IsExpanded = true;
                        OpenDetails(sopViewModel.SopList[_sopActiveStepIndex]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void HandlePatrolClick(MessageTypeSOPModel sopItem)
        {
            if (sopItem.UserControlModel != null)
            {
                if (sopItem.UserControlModel.Count > 0)
                {
                    var model = sopItem.UserControlModel[0];
                    if (model as PatrolsListViewModel != null)
                    {
                        var pModel = model as PatrolsListViewModel;
                        pModel.HandleClick();
                    }

                }
            }
        }
        private void OpenDetails(MessageTypeSOPParentModel obj)
        {
            if (obj != null && obj.SOPItems.Count > 0)
            {

                var sopItem = obj.SOPItems[0];

                if (sopItem != null)
                {
                    HandlePatrolClick(sopItem);

                    CloseCanvas();
                    //ReturnToDefaultScaleGridBasicInfo();
                    if (sopItem.UserControlDetailsControl != null)
                    {
                        var usrCnt = sopViewModel.GetUserControl(sopItem.UserControlDetailsControl, sopItem.DetailsUserControlMessageType, sopItem.DetailsMessageXSLT) as UserControl;
                        if (usrCnt != null)
                        {
                            try
                            {
                                Utility.CallMethodUsingReflector(usrCnt, "SetParentUserControl", this);
                                if (sopItem.UserControlDetailsControl == "PatrolsListUserControl")
                                    this.Publish(new ViewPatrolLayer());
                            }
                            catch (Exception ex)
                            {

                            }
                            RegisterReqEvents(usrCnt);
                            AddChildItem(usrCnt, 730);
                            //ScaleGridBasicInfo();
                            var pItem = SopListPanel.Items[0] as MessageTypeSOPParentModel;
                            if (pItem != null)
                            {
                                //if (pItem.SOPItems[0].UserControlModel == null)
                                //{
                                //    pItem.IsExpanded = false;
                                //}
                                //else
                                //{
                                // pItem.IsChecked = true;
                                pItem.IsExpanded = true;
                                // }
                            }
                        }
                    }
                }
            }
        }

        private void RegisterReqEvents(object obj)
        {
            Utility.RegisterEvent(obj, this, "CloseCanvas", "OnCloseCanvas"); // register close event
            Utility.RegisterEvent(obj, this, "GoToNextStep", "SOPList_GoToNextStep"); // register next event
            Utility.RegisterEvent(obj, this, "CancelSOP", "SOPList_CancelSOP"); // register cancel event
        }

        private void UnRegisterReqEvents(object obj)
        {
            Utility.UnRegisterEvent(obj, this, "CloseCanvas", "OnCloseCanvas"); // register close event
            Utility.UnRegisterEvent(obj, this, "GoToNextStep", "SOPList_GoToNextStep"); // register next event
            Utility.UnRegisterEvent(obj, this, "CancelSOP", "SOPList_CancelSOP"); // register cancel event
        }

        private void PatrolsListUserControl_OnLoaded(object Sender, RoutedEventArgs E)
        {
            try
            {
                var sender = Sender as PatrolsListUserControl;
                if (sender == null)
                    return;

                PatrolsListViewModel vm = sender.DataContext as PatrolsListViewModel;
                if (vm == null)
                    return;
                var drawPatrolMessage = new DrawPatrolsMessage()
                {
                    Latitude = vm.Latitude,
                    Longitude = vm.Longitude,
                };

                drawPatrolMessage.PatrolsList = new ObservableCollection<PatrolLastLocationDTO>();
                foreach (var patrolLastLocationDto in vm.PatrolsList)
                {
                    drawPatrolMessage.PatrolsList.Add(new PatrolLastLocationDTO()
                    {
                        Latitude = patrolLastLocationDto.Latitude,
                        Altitude = patrolLastLocationDto.Altitude,
                        Longitude = patrolLastLocationDto.Longitude,
                        NumberOfAssignedIncident = patrolLastLocationDto.NumberOfAssignedIncident,
                        PatrolId = patrolLastLocationDto.PatrolId,
                        PatrolPlateNo = patrolLastLocationDto.PatrolPlateNo,
                        PatrolCode = patrolLastLocationDto.PatrolCode,
                        IsNoticed = patrolLastLocationDto.IsNoticed,
                        LocationDate = patrolLastLocationDto.LocationDate,
                        PatrolLatLocationId = patrolLastLocationDto.PatrolLatLocationId,
                        Speed = patrolLastLocationDto.Speed,
                        StatusName = patrolLastLocationDto.StatusName,
                        PatrolOriginalId = patrolLastLocationDto.PatrolOriginalId,
                        StatusId = patrolLastLocationDto.StatusId,
                        IsRecommended = patrolLastLocationDto.IsRecommended,
                        ETATime = patrolLastLocationDto.ETATime,
                        CreationDate = patrolLastLocationDto.CreationDate,
                        OfficerName = patrolLastLocationDto.OfficerName,
                        isPatrol = patrolLastLocationDto.isPatrol,
                        PatrolImage = patrolLastLocationDto.PatrolImage,
                        isDeleted = patrolLastLocationDto.isDeleted
                    });
                }

                this.Publish(drawPatrolMessage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void PopupCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                this.Publish(new AccessNotificationFromMapClick());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void SopListPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SopListPanel.SelectedIndex != -1)
                {
                    var obj = (((ListView)e.OriginalSource).SelectedItem as MessageTypeSOPParentModel);

                    //var obj = ((ListViewItem)(((ListView)e.OriginalSource).SelectedItem)).DataContext as MessageTypeSOPParentModel;
                    if (obj != null)
                    {
                        var sopObjIndex = sopViewModel.SopList.IndexOf(sopViewModel.SopList.FirstOrDefault(x => x.SOPId == obj.SOPId));
                        if (sopObjIndex != _sopActiveStepIndex)
                        {
                            _sopActiveStepIndex = sopObjIndex;
                        }
                        //else
                        //{
                        //    // obj.IsExpanded = true;
                        //    if (obj.SOPItems != null && obj.SOPItems.Count > 0)
                        //        HandlePatrolClick(obj.SOPItems[0]);
                        //}
                        OpenDetails(obj);

                        if (obj.SOPItems[0].UserControlModel == null)
                        {
                            e.Handled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }




        private void SopListPanelItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                if (SopListPanel.SelectedIndex != -1)
                {
                    var obj = item.DataContext as MessageTypeSOPParentModel;


                    //var obj = ((ListViewItem)(((ListView)e.OriginalSource).SelectedItem)).DataContext as MessageTypeSOPParentModel;
                    if (obj != null)
                    {

                        var sopObjIndex = sopViewModel.SopList.IndexOf(sopViewModel.SopList.FirstOrDefault(x => x.SOPId == obj.SOPId));

                        if (sopObjIndex == _sopActiveStepIndex)
                        {
                            OpenDetails(obj);

                            if (obj.SOPItems[0].UserControlModel == null)
                            {
                                e.Handled = true;
                            }
                        }

                    }
                }
            }
        }



    }


}
