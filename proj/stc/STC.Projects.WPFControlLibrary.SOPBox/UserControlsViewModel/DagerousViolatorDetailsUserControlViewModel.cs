using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using STC.Projects.WPFControlLibrary.SOPBox.ADPUTSserviceReference;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System.Windows;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.UserControls;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using System.Drawing;
using STC.Projects.WPFControlLibrary.SOPBox.Converters;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class DagerousViolatorDetailsUserControlViewModel : System.ComponentModel.INotifyPropertyChanged, IDataErrorInfo
    {
        ADPUTSServiceClient adpUTSclient;
        ServiceLayerClient client;



        public Command EditDetailsCommand { get { return new Command((EditDetailsExecute)); } }
        public Command ShowVideoCommand { get; set; }
        public Command ShowImageCommand { get; set; }

        private ImagePopupViewModel _imagePoupVM;

        public ImagePopupViewModel ImagePoupVM
        {
            get { return _imagePoupVM; }
            set { _imagePoupVM = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ViolationItemDetailsViewModel _violationImageDetailsVM;

        public ViolationItemDetailsViewModel ViolationImageDetailsVM
        {
            get { return _violationImageDetailsVM; }
            set { _violationImageDetailsVM = value; this.RaiseNotifyPropertyChanged(); }
        }

        public WantedCarModel _currentWantedModel { get; set; }

        public DagerousViolatorDetailsUserControlViewModel()
        {

            adpUTSclient = new ADPUTSServiceClient();
            client = new ServiceLayerClient();

            ShowVideoCommand = new Command(ShowVideo);
            ShowImageCommand = new Command(ShowImage);

            //ImagePoupVM = new ImagePopupViewModel();

            //DangerousVehicleDetails = new DangerousVehicleDetailsDTO();
            //VehicleDetailResponse = new ADPUTSserviceReference.VehicleDetailsResponse();

            //GetDangerousVehicleDetails(vehicleResp.PlateInfo.PlateNo);
            _violatorDetailsExpanded = true;



            //ImagePoupVM = new ImagePopupViewModel(); 

            //this.PlateNumber="1234";
            //GetDangerousVehicleDetails(this.PlateNumber);

        }

        public void ViolationImageDetailsVM_ViolatorSearched(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            VehicleDetailsResponse = ViolationImageDetailsVM.VehicleDetailsResponse;
        }

        private void EditDetailsExecute(object violationNotificationDTO)
        {
            if (ViolationImageDetailsVM != null)
            {
                //_violationImageDetailsVM.ViolatorSearched += _violationImageDetailsVM_ViolatorSearched;
                ViolationImageDetailsVM.PlateNumber = PlateNumber;
                ViolationImageDetailsVM.ImageURLList = null;

                if (DangerousVehicleDetails.VehicleViolations != null && DangerousVehicleDetails.VehicleViolations.Count() > 0)
                {
                    violationNotificationDTO = DangerousVehicleDetails.VehicleViolations[0];

                    var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;


                    List<string> imagepathList = client.GetViolationImageURLsById(violationNotification.ViolationNotificationId).ToList();

                    ViolationImageDetailsVM.SourceURL = "Image";
                    ViolationImageDetailsVM.ShowStream();

                    if (imagepathList != null && imagepathList.Count > 0)
                    {
                        ViolationImageDetailsVM.ImageURLList = imagepathList;
                    }
                }
            }
            //else
            //    VolationImageDetailsVM = new ViolationItemDetailsViewModel(PlateNumber);
        }

        private bool _isControlBusy;

        public bool IsControlBusy
        {
            get { return _isControlBusy; }
            set { _isControlBusy = value; this.RaiseNotifyPropertyChanged(); }
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
        private void GetLicenseDetails()
        {
            var res = adpUTSclient.GetLicenseDetailsAsync(new LicenseDetailsRequest() { TcfNo = Convert.ToInt64(VehicleDetailsResponse.OwnerTcfNo) });
            res.ContinueWith(x => LicenseDetailsResponse = x.Result);

            //LicenseDetailsResponse = res.Result;
        }
        private void GetTrafficProfile()
        {
            var res = adpUTSclient.GetTrafficProfileAsync(new TrafficProfileRequest() { TcfNo = Convert.ToInt64(VehicleDetailsResponse.OwnerTcfNo) });
            res.ContinueWith(x => TrafficProfileResponse = x.Result);
            //TrafficProfileResponse = res.Result;
        }

        private STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO _selectedSource;

        public STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO SelectedSource
        {
            get { return _selectedSource; }
            set { _selectedSource = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ObservableCollection<STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO> _vehiclePlateSourceList;

        public ObservableCollection<STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO> VehiclePlateSourceList
        {
            get { return _vehiclePlateSourceList; }
            set
            {
                _vehiclePlateSourceList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private void LoadLookupVehiclePlateSource()
        {
            var callTask = adpUTSclient.GetVehiclePlateSourceAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateSourceList(x));

        }

        private void Add_VehiclePlateSourceList(STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehiclePlateSourceList = new ObservableCollection<STC.Projects.ClassLibrary.DTO.VehiclePlateSourceDTO>(data);
                if (VehiclePlateSourceList != null && VehiclePlateSourceList.Count > 0 && VehicleDetailsResponse.PlateInfo.PlateSourceCode != null)
                {
                    SelectedSource = VehiclePlateSourceList.Where(s => s.Id == VehicleDetailsResponse.PlateInfo.PlateSourceCode).FirstOrDefault();
                }

                //VehiclePlateSourceList.Insert(0, new VehiclePlateSourceDTO() { Id = 0, Name = Properties.Resources.strSelect }); 
            });
        }

        private void ShowVideo(object violationNotificationDTO)
        {
            try
            {
                IsControlBusy = true;
                var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;


                string videopath = client.GetViolationVideoURLById(violationNotification.ViolationNotificationId);

                IsControlBusy = false;

                ImagePoupVM.SourceURL = "Video";
                ImagePoupVM.ShowStream();
                //(imagePopup.DataContext as ImagePopupViewModel).VideoURL = "http://hubblesource.stsci.edu/sources/video/clips/details/images/hst_1.mpg";
                if (videopath != null)
                {

                    //videopath = @"D:\split.mp4";
                    ImagePoupVM.VideoURL = videopath;


                    //MediaPlayerWindow mplayer = new MediaPlayerWindow(videopath);
                    //mplayer.Show();

                    //mplayer.Activate();
                    //mplayer.Focus();
                    //mplayer.Topmost = true;
                    //imagePopup.Show();
                }
            }
            catch (Exception ex)
            {
                IsControlBusy = false;
                throw ex;
            }

        }
        private void ShowImage(object violationNotificationDTO)
        {
            try
            {
                IsControlBusy = true;

                var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;


                List<string> imagepathList = client.GetViolationImageURLsById(violationNotification.ViolationNotificationId).ToList();

                ImagePoupVM.SourceURL = "Image";
                ImagePoupVM.ShowStream();

                IsControlBusy = false;
                // Base64ImageConverter base64Image = new Base64ImageConverter();
                if (imagepathList != null && imagepathList.Count > 0)
                {
                    //List<BitmapImage> imageBitmapList = new List<BitmapImage>();
                    //BitmapImage btm;
                    //imagepathList = new List<string>();
                    //for (int i = 0; i < 7; i++)
                    //{
                    //    imagepathList.Add(@"D:\STC.Projects.WPFControlLibrary.DrawPolygonMapControl\STC.Projects.WPFControlLibrary.DrawPolygonMapControl\Images\google-active.png");
                    //}                    

                    //foreach (string base64Item in imagepathList)
                    //{
                    //    btm = (BitmapImage)base64Image.Convert(base64Item, null, null, null);
                    //    imageBitmapList.Add(btm);
                    //}

                    // ImagePoupVM.ImageURLBitmap = imageBitmapList;
                    ImagePoupVM.ImageURLList = imagepathList;

                    //imagePopup.Show();


                }
            }
            catch (Exception ex)
            {
                IsControlBusy = false;
                throw ex;
            }


        }


        private DangerousVehicleDetailsDTO _dangerousVehicleDetails;

        public DangerousVehicleDetailsDTO DangerousVehicleDetails
        {
            get { return _dangerousVehicleDetails; }
            set
            {
                _dangerousVehicleDetails = value;
                //if (value != null)
                //    GetVehicleDetails(_dangerousVehicleDetails);
                this.RaiseNotifyPropertyChanged();
            }
        }

        public void GetDangerousVehicleDetails(string plateNumber)
        {
            try
            {
                var callTask = client.GetDangerousVehicleDetailsByPlateNumberAsync(plateNumber, Utility.GetLang()).ToObservable();

                callTask.Subscribe(x => Add_DangerousViolator(x));
            }
            catch (Exception ex)
            {

            }

        }


        private void Add_DangerousViolator(DangerousVehicleDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DangerousVehicleDetails = data;
                //GetVehicleDetails(DangerousVehicleDetails);
                if (DangerousVehicleDetails.VehicleViolations != null && DangerousVehicleDetails.VehicleViolations.Count() > 0)
                    DangerousVehicleDetails.VehicleViolations = new List<ViolationNotificationDTO>() { DangerousVehicleDetails.VehicleViolations[0] }.ToArray();
            }
            );
        }

        public void RetriveVehicleDetails()
        {
            if (_currentWantedModel == null)
                return;

            DangerousVehicleDetailsDTO dangerousVehicleDTO = new DangerousVehicleDetailsDTO();

            dangerousVehicleDTO.PlateNumber = _currentWantedModel.VehiclePlateNumber;
            dangerousVehicleDTO.PlateColor = _currentWantedModel.PlateColor;
            dangerousVehicleDTO.PlateKind = _currentWantedModel.PlateKind;
            dangerousVehicleDTO.PlateSource = _currentWantedModel.PlateSource;

            GetVehicleDetails(dangerousVehicleDTO);
        }

        public void SetCurrentWantedVehicle(WantedCarModel currentModel)
        {
            _currentWantedModel = currentModel;
        }

        private void GetVehicleDetails(DangerousVehicleDetailsDTO dangerousVehicleDetails)
        {
            GetDangerousVehicleDetails(dangerousVehicleDetails.PlateNumber);

            VehicleDetailsRequest req = new VehicleDetailsRequest();

            int colorId, kindId, authorityId, plateNum;

            if (dangerousVehicleDetails != null
             && int.TryParse(dangerousVehicleDetails.PlateColor, out colorId)
             && int.TryParse(dangerousVehicleDetails.PlateKind, out kindId)
             && int.TryParse(dangerousVehicleDetails.PlateSource, out authorityId)
             && int.TryParse(dangerousVehicleDetails.PlateNumber, out plateNum))
            {

                req.PlateColorCode = colorId;
                req.PlateKindCode = kindId;
                req.PlateNo = dangerousVehicleDetails.PlateNumber;
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


                try
                {
                    var response = adpUTSclient.GetVehicleDetailsAsync(req).ToObservable();
                    response.Subscribe(x => Add_DangerousViolator(x));
                }
                catch (Exception ex)
                {

                }
            }
        }


        private void Add_DangerousViolator(VehicleDetailsResponse data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                VehicleDetailsResponse = data;
                LoadLookupVehiclePlateSource();
            }
            );
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

        private VehicleDetailsResponse _vehicleDetailsResponse;

        public VehicleDetailsResponse VehicleDetailsResponse
        {
            get { return _vehicleDetailsResponse; }
            set
            {
                _vehicleDetailsResponse = value;

                if (value != null)
                {
                    GetLicenseDetails();
                    GetTrafficProfile();
                }

                this.RaiseNotifyPropertyChanged();
            }
        }


        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; RaiseNotifyPropertyChanged(); }
        }



        public Command ViewDetailsCommand { get { return new Command((ViewDetailsDangerousViolatorExecute)); } }
        public Command ViolationHistoryCommand { get { return new Command((ViolationHistoryOfDangerousViolatorExecute)); } }
        public Command ReportDangerousViolatorCommand { get { return new Command((ReportDangerousViolatorExecute)); } }

        public Command CloseDetailsPopupCommand { get { return new Command((CloseDangerousViolatorPopupExecute)); } }

        private void ReportDangerousViolatorExecute()
        {

        }

        private void ViewDetailsDangerousViolatorExecute()
        {

        }

        private void ViolationHistoryOfDangerousViolatorExecute()
        {

        }

        private void CloseDangerousViolatorPopupExecute()
        {
            //Storyboard CloseDetailsPopup = (Storyboard)TryFindResource("CloseViolatorDetails");
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