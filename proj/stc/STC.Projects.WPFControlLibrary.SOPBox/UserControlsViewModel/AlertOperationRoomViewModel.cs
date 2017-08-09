using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class AlertOperationRoomViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerClient client = null;

        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate OperationRoomAlerted;

        private string _responseMsg;
        public string ResponseMsg
        {
            get { return _responseMsg; }
            set
            {
                _responseMsg = value;

                RaiseNotifyPropertyChanged();
            }
        }
        public int currentUserId { get; set; }
        public AlertOperationRoomViewModel()
        {
            client = new ServiceLayerClient();

            //DangerousVehicleDetailsDTOobj = new DangerousVehicleDetailsDTO();

            //GetDangerousVehicleDetails("1234");
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            //Latitude = Location.Latitude;
            //Longitude = Location.Longitude;

            //SOPSource = SOPSources.WantedCar;

            //GetAllPatrolsAroundPoint();
            GetDangerousVehicleDetails(Location.VehiclePlateNumber);
        }


        public void GetDangerousVehicleDetails(string plateNumber)
        {
            var callTask = client.GetDangerousVehicleDetailsByPlateNumberAsync(plateNumber, Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_DangerousVehicleDetails(x));
        }

        private void Add_DangerousVehicleDetails(DangerousVehicleDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DangerousVehicleDetailsDTOobj = data;

            });
        }



        private DangerousVehicleDetailsDTO _dangerousVehicleDetailsDTOobj;


        public DangerousVehicleDetailsDTO DangerousVehicleDetailsDTOobj
        {
            get { return _dangerousVehicleDetailsDTOobj; }
            set
            {
                _dangerousVehicleDetailsDTOobj = value;

                RaiseNotifyPropertyChanged();
            }
        }


        private string _reportMessage;


        public string ReportMessage
        {
            get { return _reportMessage; }
            set
            {
                _reportMessage = value;

                RaiseNotifyPropertyChanged();
            }
        }

        public Command SubmitCommand { get { return new Command((SubmitExecute)); } }

        private void SubmitExecute()
        {
            ResponseMsg = string.Empty;
            string closeMsg = Properties.Resources.strAlertOpRoomConfMsg;
            //string closeMsg = Utility.GetLang() == "en" ? "Are you sure, want to Alert the Operation Room?" : "؟";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            //closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                SupervisorNotificationDTO req = new SupervisorNotificationDTO();
                req.SenderId = currentUserId;
                req.ReceiverId = client.GetSupervisorId();

                DateTime dtNow = DateTime.Now;
                //req.NotificationTime = dtNow;
                req.NotificationTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow.Millisecond);
                req.IsNoticed = false;
                if (DangerousVehicleDetailsDTOobj != null)
                {
                    req.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO();
                    req.DangerousViolatorDetails.NotificationText = ReportMessage;
                    req.DangerousViolatorDetails.PlateNumber = DangerousVehicleDetailsDTOobj.PlateNumber;
                    req.DangerousViolatorDetails.PlateKind = DangerousVehicleDetailsDTOobj.PlateKind;
                    req.DangerousViolatorDetails.PlateColor = DangerousVehicleDetailsDTOobj.PlateColor;
                    req.DangerousViolatorDetails.PlateAuthority = DangerousVehicleDetailsDTOobj.PlateSource;
                }
                var saveRes = client.SaveSupervisorNotificationAsync(req);

                saveRes.ContinueWith(x => AlertOperationRoomResult(x.Result));
                //AddBusinessRuleResult(true);

            }
        }

        private void AlertOperationRoomResult(bool result)
        {
            if (result)
            {
                ResponseMsg = "Successfully Alerted Operation Room";
                var handler = OperationRoomAlerted;
                if (OperationRoomAlerted != null)
                    handler(this, new EventArgs());
            }
            else
            {
                ResponseMsg = "Alert Operation Room - Failed";
            }
        }


        #region IDataErrorInfo

        public string Error
        {
            get { return string.Empty; }
        }

        public String this[string columnName]
        {
            get
            {
                string result = string.Empty;

                switch (columnName)
                {
                    case "ReportMessage":
                        {
                            if (string.IsNullOrEmpty(ReportMessage))
                                result = Utility.GetLang() == "ar" ? "Alert Report Message is mandatory to alert Operation Room" : "Alert Report Message is mandatory to alert Operation Room";
                        }
                        break;
                }
                return result;
            }
        }



        #endregion

        #region INotifyPropertyChanged interface

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaiseNotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}