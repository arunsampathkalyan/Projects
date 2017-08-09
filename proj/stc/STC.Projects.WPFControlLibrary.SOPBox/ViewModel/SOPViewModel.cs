using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.ClassLibrary.Common;
using System.Reflection;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;

namespace STC.Projects.WPFControlLibrary.SOPBox.ViewModel
{
    class SOPViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MessageTypeSOPParentModel> SopList { get; set; }

        private ObservableCollection<EventDetailModel> _eventDetailsList;
        public ObservableCollection<EventDetailModel> EventDetailsList
        {
            get { return _eventDetailsList; }
            set
            {
                _eventDetailsList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EventDetailsList"));
            }
        }

        private bool isEnabled;
        public event CanvasEventHandler LoadSteps;

        public SOPGeneralMessage _currentMessage;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEnabled"));
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsExpanded"));
            }
        }

        public string MessageId { get; set; }

        public SOPViewModel()
        {
            SopList = new ObservableCollection<MessageTypeSOPParentModel>();
            EventDetailsList = new ObservableCollection<EventDetailModel>();
            _currentMessage = null;
        }

        //internal void GetIncidentSOP(dynamic Incident)
        //{
        //    if (Incident != null && Incident.Latitude != null && Incident.Longitude != null && Incident.CreatedTime != null) {
        //        SopSource = new SOPModel {
        //            Text = Incident.MessageText,
        //            Date = Incident.CreatedTime,
        //            Latitude = Incident.Latitude,
        //            Longitude = Incident.Longitude
        //        };
        //    }
        //    else
        //        SopSource = null;

        //    ServiceLayerReference.ServiceLayerClient _client = new ServiceLayerReference.ServiceLayerClient();

        //    var lstSopAsync = _client.GetMessageTypeSOPAsync("IncidentSOPMessages");

        //    var obs = lstSopAsync.ToObservable();
        //    obs.Subscribe((x) => AddNewSOP(x == null ? null : x.ToList()));

        //}

        //internal void GetViolationSOP(dynamic Violation)
        //{
        //    if (Violation != null && Violation.Latitude != null && Violation.Longitude != null) {
        //        SopSource = new SOPModel {
        //            Text = Violation.MessageText,
        //            Date = Violation.DateTaken,
        //            Latitude = Violation.Latitude,
        //            Longitude = Violation.Longitude
        //        };
        //    }
        //    else
        //        SopSource = null;

        //    ServiceLayerReference.ServiceLayerClient _client = new ServiceLayerReference.ServiceLayerClient();

        //    var lstSopAsync = _client.GetMessageTypeSOPAsync("ViolationSOPMessages");

        //    var obs = lstSopAsync.ToObservable();
        //    obs.Subscribe((x) => AddNewSOP(x == null ? null : x.ToList()));
        //}

        public void SaveSopStep(int sopStepId, int userId)
        {
            try
            {
                var client = new ServiceLayerClient();
                client.SaveSOPNotificationLogAsync(sopStepId, _currentMessage.NotificationId, null, userId, "", "");
            }
            catch(Exception ex)
            {

            }
        }

        public bool IsAllStepsChecked()
        {
            return SopList.All(x => x.IsChecked);
        }

        public void SetStepChecked(object UserControlViewModel)
        {
            var item = SopList.FirstOrDefault(x => x.SOPItems.Any(y => y.UserControlModel.Any(z => z == UserControlViewModel)));
            if (item == null)
                return;
            item.ImgCheckedSource = "images/true.png";
            item.IsChecked = true;
        }

        public void UpdateCurrentMessageObject(object newObject)
        {
            if (_currentMessage != null)
            {
                _currentMessage.OriginalObject = newObject;
            }
        }

        public object GetCurrentMessageObject()
        {
            return _currentMessage;
        }

        private void AddNewSOP(List<MessageTypeSOPDTO> List)
        {
            if (List == null || List.Count == 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (SopList.Count > 0)
                    {
                        IsExpanded = true;
                        IsEnabled = true;
                    }
                    else
                    {
                        IsExpanded = false;
                        IsEnabled = false;
                    }
                });

                return;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                int count = SopList.Count;

                for (int i = 0; i < count; i++)
                {
                    SopList.RemoveAt(0);
                }
            });

           
            foreach (var item in List)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var sop = new MessageTypeSOPParentModel
                    {
                        IsChecked = false,
                        BorderBackground = "#252525",
                        ImgCheckedSource = "images/false.png",
                        RankForeground = "#FFFFFF",
                        MessageTypeId = item.MessageTypeId,
                        MessageTypeName = item.MessageTypeName,
                        Rank = item.Rank,
                        SOPContent = item.SOPContent,
                        SOPId = item.SOPId,
                        IsParent = true,
                        IsExpanded = false,
                        SOPItems = new List<MessageTypeSOPModel> {
                            new MessageTypeSOPModel {
                                BorderBackground = "#252525",
                                IsParent = false,
                                
                               UserControlDetailsControl = item.SOPDetailsControlName,
                               DetailsUserControlMessageType = item.SOPDetailsDataMessageType,
                                DetailsMessageXSLT = item.SOPDetailsXSLT,
                                ListMessageXSLT = item.SOPListXSLT,
                                ListUserControlMessageType = item.SOPListDataMessageType

                            }
                        }
                    };
                    
                    if(!string.IsNullOrEmpty(item.SOPControlName) && sop.SOPItems.Count > 0)
                    {
                        sop.SOPItems[0].UserControlModel = new List<object> {
                                    GetUserControlObject(item.SOPControlName,item.SOPListDataMessageType,item.SOPListXSLT)
                                };
                    }
                    SopList.Add(sop);
                });
            }


            Application.Current.Dispatcher.Invoke(() =>
            {
                if (SopList.Count > 0)
                {
                    IsExpanded = true;
                    IsEnabled = true;
                }
                else
                {
                    IsExpanded = false;
                    IsEnabled = false;
                }
            });

            var handler = LoadSteps;
            if (handler != null)
                handler(this, new CanvasEventArgs());
        }

        public object GetUserControlObject(string userControlObjectName, string userControlMessage, string userControlXSLT)
        {
            try
            {
                var fullTypeName = string.Format("STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel.{0}", userControlObjectName);
                var instance = Activator.CreateInstance("STC.Projects.WPFControlLibrary.SOPBox", fullTypeName);
                if (instance != null)
                {
                    try
                    {
                        var ins = instance.Unwrap();
                        if (ins != null)
                        {
                            if (!String.IsNullOrEmpty(userControlMessage) && !String.IsNullOrEmpty(userControlXSLT) && _currentMessage != null)
                            {
                                Assembly asm = typeof(SOPList).Assembly;

                                var messageDataObject = Utility.MapObjectsUsingXSLT(_currentMessage.OriginalObject, userControlXSLT, string.Format("{0}.{1}", "STC.Projects.WPFControlLibrary.SOPBox.Model", userControlMessage), asm);
                                if (messageDataObject != null)
                                {
                                    Utility.CallMethodUsingReflector(ins, "ProcessMessage", messageDataObject);
                                    try
                                    {
                                        Utility.CallMethodUsingReflector(ins, "SetNotificationID", _currentMessage.NotificationId);
                                    }
                                    catch(Exception ex)
                                    {

                                    }
                                }
                            }
                            return ins;
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                    //var ins = instance.Unwrap() as ISOPControlObject;
                    //if (ins != null) {//do the refactoring here
                    //    //ins.Process(lat, lon);
                    //}
                    //return ins;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public object GetUserControl(string userControlObjectName, string userControlMessage, string userControlXSLT)
        {

            try
            {
                var fullTypeName = string.Format("STC.Projects.WPFControlLibrary.SOPBox.UserControls.{0}", userControlObjectName);
                var instance = Activator.CreateInstance("STC.Projects.WPFControlLibrary.SOPBox", fullTypeName);
                if (instance != null)
                {
                    var ins = instance.Unwrap() as UserControl;
                    if (ins != null)
                    {
                        try
                        {
                            if (!String.IsNullOrEmpty(userControlMessage) && !String.IsNullOrEmpty(userControlXSLT) && _currentMessage != null)
                            {
                                Assembly asm = typeof(SOPList).Assembly;

                                var messageDataObject = Utility.MapObjectsUsingXSLT(_currentMessage.OriginalObject, userControlXSLT, string.Format("{0}.{1}", "STC.Projects.WPFControlLibrary.SOPBox.Model", userControlMessage), asm);
                                if (messageDataObject != null)
                                {
                                    Utility.CallMethodUsingReflector(ins, "ProcessMessage", messageDataObject);
                                    try
                                    {
                                        Utility.CallMethodUsingReflector(ins, "SetNotificationID", _currentMessage.NotificationId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    //      ins.Process(lat, lon);
                    return ins;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        internal void GetSOP(SOPGeneralMessage Message)
        {
            if (Message.OriginalObject == null)
            {
                IsExpanded = false;
                IsEnabled = false;

                return;
            }

            _currentMessage = Message;

            MessageId = Message.MessageId;
            var msgId = 0;
            int.TryParse(Message.MessageId, out msgId);
            ServiceLayerClient _client = new ServiceLayerClient();

            var lstSopAsync = _client.GetMessageTypeSOPsAsync((int)Message.GeneralType,msgId);

            var obs = lstSopAsync.ToObservable();
            obs.Subscribe((x) => AddNewSOP(x == null ? null : x.ToList()));
        }



        public  ObservableCollection<PatrolDTO>  GetAssignedPatrols(long eventId)
        {
            var _client = new ServiceLayerClient();
            return new ObservableCollection<PatrolDTO>(( _client.GetAssignedPatrols(eventId)).ToList());

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
