using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using System.Windows.Media.Animation;
using STC.Projects.ClassLibrary.DTO;
using System.Reactive.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ADPUTSserviceReference;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ServiceLayerReference;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel
{
    public class NotifiedAlertViolatorDetailsUserControlViewModel : System.ComponentModel.INotifyPropertyChanged, IDataErrorInfo
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate NotificationHandled;

        ADPUTSServiceClient _ADPUTSclient;
        ServiceLayerClient client;
        public int currentUserid { get; set; }

        public NotifiedAlertViolatorDetailsUserControlViewModel(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            _supervisorNotificationDTO = supervisorNotificationDTO;
            _ADPUTSclient = new ADPUTSServiceClient();
            client = new ServiceLayerClient();

            if (supervisorNotificationDTO.DangerousViolatorDetails != null && !string.IsNullOrEmpty(supervisorNotificationDTO.DangerousViolatorDetails.PlateNumber))
                GetDangerousVehicleDetails(supervisorNotificationDTO.DangerousViolatorDetails.PlateNumber);


            GetLicenseDetails();
            GetTrafficProfile();


            if (supervisorNotificationDTO.DangerousViolatorDetails != null && supervisorNotificationDTO.DangerousViolatorDetails.BusinessRuleId != null)
                GetBusinessRuleName(Convert.ToInt32(supervisorNotificationDTO.DangerousViolatorDetails.BusinessRuleId));

            if (supervisorNotificationDTO.DangerousViolatorDetails != null && !string.IsNullOrEmpty(supervisorNotificationDTO.DangerousViolatorDetails.MediaURL))
                IsAttachmentAvailable = true;

            GetVehicleDetails();

            _violatorDetailsExpanded = true;
        }


        private void GetBusinessRuleName(int id)
        {
            var resp = client.GetBusinessRuleByIDAsync(id);
            resp.ContinueWith(x => businessRuleName = x.Result.BusinessName);
        }

        private string businessRuleName;
        private void GetLicenseDetails()
        {
            if (VehicleDetailsResponse != null && !string.IsNullOrEmpty(VehicleDetailsResponse.OwnerTcfNo))
            {
                var res = _ADPUTSclient.GetLicenseDetailsAsync(new LicenseDetailsRequest() { TcfNo = Convert.ToInt64(VehicleDetailsResponse.OwnerTcfNo) });
                LicenseDetailsResponse = res.Result;
            }
        }
        private void GetTrafficProfile()
        {
            if (VehicleDetailsResponse != null && !string.IsNullOrEmpty(VehicleDetailsResponse.OwnerTcfNo))
            {
                var res = _ADPUTSclient.GetTrafficProfileAsync(new TrafficProfileRequest() { TcfNo = Convert.ToInt64(VehicleDetailsResponse.OwnerTcfNo) });
                TrafficProfileResponse = res.Result;
            }
        }

        private DangerousVehicleDetailsDTO _dangerousVehicleDetails;

        public DangerousVehicleDetailsDTO DangerousVehicleDetails
        {
            get { return _dangerousVehicleDetails; }
            set
            {
                _dangerousVehicleDetails = value;


                this.RaiseNotifyPropertyChanged();
            }
        }

        public void GetDangerousVehicleDetails(string plateNumber)
        {
            //client.SaveSupervisorNotificationAsync(new SupervisorNotificationDTO() {  DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO() {    } });
            var callTask = client.GetDangerousVehicleDetailsByPlateNumberAsync(plateNumber, Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_DangerousVehicleDetails(x));
        }

        private void Add_DangerousVehicleDetails(DangerousVehicleDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DangerousVehicleDetails = data;

            });
        }

        private ImagePopupViewModel _imagePoupVM;

        public ImagePopupViewModel ImagePoupVM
        {
            get { return _imagePoupVM; }
            set { _imagePoupVM = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _isAttachmentAvailable;

        public bool IsAttachmentAvailable
        {
            get { return _isAttachmentAvailable; }
            set { _isAttachmentAvailable = value; this.RaiseNotifyPropertyChanged(); }
        }

        private int _driverAge;

        public int DriverAge
        {
            get { return _driverAge; }
            set
            {
                _driverAge = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private LicenseDetailsResponse _licenseDetailsResponse;

        public LicenseDetailsResponse LicenseDetailsResponse
        {
            get { return _licenseDetailsResponse; }
            set
            {
                _licenseDetailsResponse = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private TrafficProfileResponse _trafficProfileResponse;

        public TrafficProfileResponse TrafficProfileResponse
        {
            get { return _trafficProfileResponse; }
            set
            {
                _trafficProfileResponse = value;
                if (value != null)
                {
                    DriverAge = (DateTime.Now.Year - value.BirthDate.Year);
                    if (value.BirthDate > DateTime.Now.AddYears(DriverAge))
                        DriverAge--;
                }

                this.RaiseNotifyPropertyChanged();
            }
        }

        private SupervisorNotificationDTO _supervisorNotificationDTO;

        public SupervisorNotificationDTO SupervisorNotificationDTO
        {
            get { return _supervisorNotificationDTO; }
            set { _supervisorNotificationDTO = value; RaiseNotifyPropertyChanged(); }
        }


        private bool _violatorDetailsExpanded;

        public bool ViolatorDetailsExpanded
        {
            get { return _violatorDetailsExpanded; }
            set
            {
                _violatorDetailsExpanded = value;
                if (_violatorDetailsExpanded)
                    ViolatorHisotoryExpanded = false;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _violatorHistoryExpanded;

        public bool ViolatorHisotoryExpanded
        {
            get { return _violatorHistoryExpanded; }
            set
            {
                _violatorHistoryExpanded = value;
                if (_violatorHistoryExpanded)
                    ViolatorDetailsExpanded = false;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; RaiseNotifyPropertyChanged(); }
        }

        private bool _showApproveRejectButtons;

        public bool ShowApproveRejectButtons
        {
            get { return _showApproveRejectButtons; }
            set { _showApproveRejectButtons = value; RaiseNotifyPropertyChanged(); }
        }


        private VehicleDetailsResponse _vehicleDetailsResponse;
        public VehicleDetailsResponse VehicleDetailsResponse
        {
            get { return _vehicleDetailsResponse; }
            set
            {
                _vehicleDetailsResponse = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        public void SetSupervisorNotificationNoticed(SupervisorNotificationDTO notification, bool isNoticed)
        {
            client.SetSupervisorNotificationNoticedAsync(notification.SupervisorNoticationId, isNoticed);
        }


        private void GetVehicleDetails()
        {
            VehicleDetailsRequest req = new VehicleDetailsRequest();
            int colorId, kindId, authorityId, plateNum;

            if (SupervisorNotificationDTO.DangerousViolatorDetails != null
             && int.TryParse(SupervisorNotificationDTO.DangerousViolatorDetails.PlateColor, out colorId)
             && int.TryParse(SupervisorNotificationDTO.DangerousViolatorDetails.PlateKind, out kindId)
             && int.TryParse(SupervisorNotificationDTO.DangerousViolatorDetails.PlateAuthority, out authorityId)
             && int.TryParse(SupervisorNotificationDTO.DangerousViolatorDetails.PlateNumber, out plateNum))
            {

                req.PlateColorCode = colorId;
                req.PlateKindCode = kindId;
                req.PlateNo = SupervisorNotificationDTO.DangerousViolatorDetails.PlateNumber;
                req.PlateSourceCode = authorityId;


                //req.SystemCode = 4;
                req.ChassisNoExist = true;

                req.PlateTypeCode = kindId;

                //req.UserId = "nn";

                //req.PlateColorCode = 21;
                //req.PlateKindCode = 1;
                //req.PlateNo = "1234";
                //req.PlateSourceCode = 2;
                //req.PlateTypeCode = 1;



                var resp = _ADPUTSclient.GetVehicleDetailsAsync(req);
                resp.ContinueWith(x => UpdateVehicleDetails(resp.Result));

            }

        }


        private void UpdateVehicleDetails(VehicleDetailsResponse resp)
        {
            Application.Current.Dispatcher.Invoke(() => { VehicleDetailsResponse = resp; });
        }


        public void SendNotificationToReporter(bool isApproved)
        {
            SupervisorNotificationDTO req = new ClassLibrary.DTO.SupervisorNotificationDTO();
            req.SenderId = (currentUserid != 0) ? currentUserid : 1;
            req.ReceiverId = SupervisorNotificationDTO.SenderId;
            req.ResponseToId = SupervisorNotificationDTO.SupervisorNoticationId;

            DateTime dtNow = DateTime.Now;
            req.NotificationTime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow.Millisecond);
            req.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO()
            {
                PlateNumber = SupervisorNotificationDTO.DangerousViolatorDetails.PlateNumber,
                PlateAuthority = SupervisorNotificationDTO.DangerousViolatorDetails.PlateAuthority,
                PlateColor = SupervisorNotificationDTO.DangerousViolatorDetails.PlateColor,
                PlateKind = SupervisorNotificationDTO.DangerousViolatorDetails.PlateKind,
                BusinessRuleId = SupervisorNotificationDTO.DangerousViolatorDetails.BusinessRuleId,
                NotificationText = Comments,
                Lat = SupervisorNotificationDTO.DangerousViolatorDetails.Lat,
                Lon = SupervisorNotificationDTO.DangerousViolatorDetails.Lon,
            };

            req.Status = isApproved ? SupervisorNotificationStatus.Approved : SupervisorNotificationStatus.Rejected;
            var saveNotification = client.SaveSupervisorNotificationAsync(req);

            saveNotification.ContinueWith(x => SaveNotificationResult(x.Result, isApproved, req));

        }

        private void SaveNotificationResult(bool result, bool isApprove, SupervisorNotificationDTO req)
        {
            if (result)
            {
                SupervisorNotificationDTO.Status = isApprove ? SupervisorNotificationStatus.Approved : SupervisorNotificationStatus.Rejected;

                if (isApprove)
                {
                    //string plateDetails = req.DangerousViolatorDetails.PlateNumber + "," + req.DangerousViolatorDetails.PlateKind + "," + req.DangerousViolatorDetails.PlateAuthority + "," + req.DangerousViolatorDetails.PlateColor;

                    //var wantedCar = client.AddWantedCarEventAsync(plateDetails, SupervisorNotificationDTO.NotificationTime, Utility.GetLang(), businessRuleName, req.DangerousViolatorDetails.BusinessRuleId.ToString());
                    var wantedCar = client.AddWantedCarEventManualyAsync(req.DangerousViolatorDetails.PlateNumber, req.DangerousViolatorDetails.PlateKind, req.DangerousViolatorDetails.PlateAuthority, req.DangerousViolatorDetails.PlateColor,
                        req.NotificationTime, Utility.GetLang(), businessRuleName, req.DangerousViolatorDetails.BusinessRuleId.ToString(), Convert.ToDouble(req.DangerousViolatorDetails.Lat), Convert.ToDouble(req.DangerousViolatorDetails.Lon));

                    wantedCar.ContinueWith(x => WantedCarResult(x.Status, x.IsCompleted));
                }

            }
            else
                SupervisorNotificationDTO.Status = SupervisorNotificationStatus.Pending;

            var handler = NotificationHandled;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void WantedCarResult(TaskStatus status, bool iscompleted)
        {
            if (status == TaskStatus.RanToCompletion && iscompleted == true)
            { }
        }

        //public Command ViewDetailsCommand { get { return new Command((ViewDetailsDangerousViolatorExecute)); } }
        //public Command ViolationHistoryCommand { get { return new Command((ViolationHistoryOfDangerousViolatorExecute)); } }
        //public Command ReportDangerousViolatorCommand { get { return new Command((ReportDangerousViolatorExecute)); } }

        public Command CloseDetailsPopupCommand { get { return new Command((CloseDangerousViolatorPopupExecute)); } }


        private void CloseDangerousViolatorPopupExecute()
        {
            //Storyboard CloseDetailsPopup = (Storyboard)TryFindResource("CloseViolatorDetails");
        }


        public void SetMediaSource()
        {
            if (SupervisorNotificationDTO.DangerousViolatorDetails != null && !string.IsNullOrEmpty(SupervisorNotificationDTO.DangerousViolatorDetails.MediaURL))
            {
                if (SupervisorNotificationDTO.DangerousViolatorDetails.MediaType == MediaTypes.Image)
                {
                    ImagePoupVM.SourceURL = "Image";
                    ImagePoupVM.ShowStream();
                    ImagePoupVM.ImageURLList = new List<string>() { SupervisorNotificationDTO.DangerousViolatorDetails.MediaURL };
                }
                else if (SupervisorNotificationDTO.DangerousViolatorDetails.MediaType == MediaTypes.Video)
                {
                    ImagePoupVM.SourceURL = "Video";
                    ImagePoupVM.ShowStream();
                    ImagePoupVM.VideoURL = SupervisorNotificationDTO.DangerousViolatorDetails.MediaURL;
                }
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