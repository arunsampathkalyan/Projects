using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.MessageBoxControl;

using STC.Projects.WPFControlLibrary.SOPBox.ADPUTSserviceReference;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.ClassLibrary.DTO;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class IssueFineViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerReference.ServiceLayerClient client = null;
        ADPUTSServiceClient adpUTSclient = null;

        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate NewTicketCreated;

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
        public IssueFineViewModel()
        {
            client = new ServiceLayerReference.ServiceLayerClient();
            adpUTSclient = new ADPUTSServiceClient();


            LoadData();
            GetVehicleViolationTypes();


        }

        public void ProcessMessage(WantedCarModel Location)
        {
            //Latitude = Location.Latitude;
            //Longitude = Location.Longitude;

            //SOPSource = SOPSources.WantedCar;

            //GetAllPatrolsAroundPoint();

            this.PlateNumber = Location.VehiclePlateNumber;
            GetDangerousVehicleDetails(this.PlateNumber);


        }


        public void GetDangerousVehicleDetails(string plateNumber)
        {

            //client.SaveSupervisorNotificationAsync(new SupervisorNotificationDTO() {  DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO() {    } });
            var callTask = client.GetDangerousVehicleDetailsByPlateNumberAsync(plateNumber, Utility.GetLang());

            callTask.ContinueWith(x => Add_DangerousVehicleDetails(x.Result));
        }


        private void Add_DangerousVehicleDetails(ServiceLayerReference.DangerousVehicleDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (data == null) return;
                DangerousVehicleDetails = data;

            });
        }

        private ServiceLayerReference.DangerousVehicleDetailsDTO _dangerousVehicleDetails;

        public ServiceLayerReference.DangerousVehicleDetailsDTO DangerousVehicleDetails
        {
            get { return _dangerousVehicleDetails; }
            set
            {
                _dangerousVehicleDetails = value;

                if (value != null)
                    GetVehicleDetails();

                this.RaiseNotifyPropertyChanged();
            }
        }
        private async void GetVehicleDetails()
        {
            int colorId;
            if (DangerousVehicleDetails != null && !string.IsNullOrEmpty(DangerousVehicleDetails.PlateColor) && Int32.TryParse(DangerousVehicleDetails.PlateColor, out colorId))
            {
                SelectedColor = VehiclePlateColorList.Where(x => x.Id == colorId).FirstOrDefault();
            }
            else
                SelectedColor = VehiclePlateColorList[0];

            int kindId;
            if (DangerousVehicleDetails != null && !string.IsNullOrEmpty(DangerousVehicleDetails.PlateKind) && Int32.TryParse(DangerousVehicleDetails.PlateKind, out kindId))
            {
                SelectedKind = VehiclePlateKindList.Where(x => x.Id == kindId).FirstOrDefault();
                SelectedPlateType = VehiclePlateClassficationsList.Where(x => x.Id == kindId).FirstOrDefault();
            }
            else
            {
                SelectedKind = VehiclePlateKindList[0];
                SelectedPlateType = VehiclePlateClassficationsList[0];
            }

            int sourceId;
            if (DangerousVehicleDetails != null && !string.IsNullOrEmpty(DangerousVehicleDetails.PlateSource) && Int32.TryParse(DangerousVehicleDetails.PlateSource, out sourceId))
            {
                SelectedSource = VehiclePlateSourceList.Where(x => x.Id == sourceId).FirstOrDefault();
            }
            else
                SelectedSource = VehiclePlateSourceList[0];

            VehicleDetailsRequest req = new VehicleDetailsRequest();

            req.PlateNo = this.PlateNumber;
            //req.PlateColorCode = Convert.ToInt32(!string.IsNullOrEmpty(DangerousVehicleDetails.PlateColor) ? DangerousVehicleDetails.PlateColor : SelectedColor.Id);
            //req.PlateKindCode = Convert.ToInt32(!string.IsNullOrEmpty(DangerousVehicleDetails.PlateKind) ? DangerousVehicleDetails.PlateKind : SelectedKind.Id);

            //req.PlateSourceCode = Convert.ToInt32(!string.IsNullOrEmpty(DangerousVehicleDetails.PlateSource) ? DangerousVehicleDetails.PlateSource : SelectedSource.Id);

            req.PlateColorCode = Convert.ToInt32(SelectedColor.Id);
            req.PlateKindCode = Convert.ToInt32(SelectedKind.Id);
            req.PlateSourceCode = Convert.ToInt32(SelectedSource.Id);
            req.PlateTypeCode = Convert.ToInt32(SelectedPlateType.Id);


            //req.SystemCode = 4;
            req.ChassisNoExist = true;

            //req.PlateTypeCode = Convert.ToInt32(!string.IsNullOrEmpty(DangerousVehicleDetails.PlateKind) ? DangerousVehicleDetails.PlateKind : SelectedPlateType.Id);

            //req.UserId = "nn";

            //req.PlateColorCode = 21;
            //req.PlateKindCode = 1;
            //req.PlateNo = "1234";
            //req.PlateSourceCode = 2;
            //req.PlateTypeCode = 1;




            var resp = adpUTSclient.GetVehicleDetailsAsync(req);
            Application.Current.Dispatcher.Invoke(() => { VehicleDetailResponse = resp.Result; });

        }

        private VehicleDetailsResponse _vehicleDetailResponse;

        public VehicleDetailsResponse VehicleDetailResponse
        {
            get { return _vehicleDetailResponse; }
            set
            {
                _vehicleDetailResponse = value;
                this.RaiseNotifyPropertyChanged();
            }
        }



        private VehicleViolationsTypesDTO[] _violationTypeList;

        public VehicleViolationsTypesDTO[] ViolationTypeList
        {
            get { return _violationTypeList; }
            set
            {
                _violationTypeList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehicleViolationsTypesDTO _selectedViolationType;

        public VehicleViolationsTypesDTO SelectedViolationType
        {
            get { return _selectedViolationType; }
            set
            {
                _selectedViolationType = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _plateNumber;

        public string PlateNumber
        {
            get
            {
                return _plateNumber;
            }
            set
            {
                _plateNumber = value;
                this.RaiseNotifyPropertyChanged();
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


        private void GetVehicleViolationTypes()
        {
            var callTask = adpUTSclient.GetVehicleViolationsTypesAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => AddViolationTypes(x));
        }

        private void AddViolationTypes(VehicleViolationsTypesDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() => ViolationTypeList = data);
        }


        private void LoadData()
        {
            LoadLookupVehiclePlateClassifications();
            LoadLookupVehiclePlateColors();
            LoadLookupVehiclePlateKind();
            LoadLookupVehiclePlateSource();

        }

        private void LoadLookupVehiclePlateClassifications()
        {
            var callTask = adpUTSclient.GetVehiclePlateClassificationsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateClassificationsList(x));

        }

        private void Add_VehiclePlateClassificationsList(VehiclePlateClassificationsDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { VehiclePlateClassficationsList = data; });
        }

        private void LoadLookupVehiclePlateColors()
        {
            var callTask = adpUTSclient.GetVehiclePlateColorAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateColorsList(x));

        }

        private void Add_VehiclePlateColorsList(VehiclePlateColorDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { VehiclePlateColorList = data; });
        }

        private void LoadLookupVehiclePlateKind()
        {
            var callTask = adpUTSclient.GetVehiclePlateKindAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateKindList(x));

        }

        private void Add_VehiclePlateKindList(VehiclePlateKindDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { VehiclePlateKindList = data; });
        }

        private void LoadLookupVehiclePlateSource()
        {
            var callTask = adpUTSclient.GetVehiclePlateSourceAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateSourceList(x));

        }

        private void Add_VehiclePlateSourceList(VehiclePlateSourceDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { VehiclePlateSourceList = data; });
        }



        private VehiclePlateClassificationsDTO[] _vehiclePlateClassficationsList;

        public VehiclePlateClassificationsDTO[] VehiclePlateClassficationsList
        {
            get { return _vehiclePlateClassficationsList; }
            set
            {
                _vehiclePlateClassficationsList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private VehiclePlateClassificationsDTO _selectedPlateType;

        public VehiclePlateClassificationsDTO SelectedPlateType
        {
            get { return _selectedPlateType; }
            set
            {
                _selectedPlateType = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehiclePlateColorDTO _selectedColor;

        public VehiclePlateColorDTO SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehiclePlateKindDTO _selectedKind;

        public VehiclePlateKindDTO SelectedKind
        {
            get { return _selectedKind; }
            set
            {
                _selectedKind = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehiclePlateSourceDTO _selectedSource;

        public VehiclePlateSourceDTO SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                _selectedSource = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehiclePlateColorDTO[] _vehiclePlateColorList;

        public VehiclePlateColorDTO[] VehiclePlateColorList
        {
            get { return _vehiclePlateColorList; }
            set
            {
                _vehiclePlateColorList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private VehiclePlateKindDTO[] _vehiclePlateKindList;

        public VehiclePlateKindDTO[] VehiclePlateKindList
        {
            get { return _vehiclePlateKindList; }
            set
            {
                _vehiclePlateKindList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private VehiclePlateSourceDTO[] _vehiclePlateSourceList;

        public VehiclePlateSourceDTO[] VehiclePlateSourceList
        {
            get { return _vehiclePlateSourceList; }
            set
            {
                _vehiclePlateSourceList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }
        public Command SubmitCommand { get { return new Command((SubmitExecute)); } }

        private void SubmitExecute()
        {
            ResponseMsg = string.Empty;

            string closeMsg = Utility.GetLang() == "en" ? "Are you sure, want to report Issue/Fine?" : "؟";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            //closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true && VehicleDetailResponse != null)
            {

                NewTicketRequest req = new NewTicketRequest();

                req.VehicleColorCode = Convert.ToInt32(VehicleDetailResponse.ColorCode);
                req.VehicleOwnerTcfNo = Convert.ToInt64(VehicleDetailResponse.OwnerTcfNo);
                req.VehicleTypeCode = Convert.ToInt32(VehicleDetailResponse.TypeCode);
                req.VehicleModelCode = Convert.ToInt32(VehicleDetailResponse.ModelCode);



                req.PlateInfo = VehicleDetailResponse.PlateInfo;
                if (SelectedViolationType != null)
                    req.TicketType = SelectedViolationType.Id.ToString();


                var createticketResponse = adpUTSclient.CreateNewTicketAsync(req);

                createticketResponse.ContinueWith(x => CreateNewTicketResult(x.Result));

            }
        }


        private void CreateNewTicketResult(NewTicketResponse response)
        {
            if (response != null)
            {
                ResponseMsg = "New Ticket Issued Successfully";
                var handler = NewTicketCreated;
                if (NewTicketCreated != null)
                    handler(this, new EventArgs());
            }
            else
                ResponseMsg = "New Ticket Issue - Failed";
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