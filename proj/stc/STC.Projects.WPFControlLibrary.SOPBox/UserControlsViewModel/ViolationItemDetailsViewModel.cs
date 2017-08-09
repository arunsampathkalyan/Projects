using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using System.Windows.Media.Imaging;
using STC.Projects.WPFControlLibrary.SOPBox.ADPUTSserviceReference;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class ViolationItemDetailsViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate ImageMediaSourceUpdated;
        public int itemsCount { get; set; }


        ADPUTSServiceClient adpUTSclient = null;
        ServiceLayerClient _client = null;

        public event PublishDelegate ViolatorSearched;

        public ViolationItemDetailsViewModel()
        {
            //PlateNumber = plateNum;
            adpUTSclient = new ADPUTSServiceClient();
            _client = new ServiceLayerClient();
            //_activeIndex = -1;
            LoadData();
        }

        //public ViolationItemDetailsViewModel(string plateNum)
        //{
        //    PlateNumber = plateNum;
        //    adpUTSclient = new ADPUTSServiceClient();
        //    _client = new ServiceLayerClient();
        //    _activeIndex = -1;
        //    LoadData();
        //}


        private bool _isControlBusy;

        public bool IsControlBusy
        {
            get { return _isControlBusy; }
            set { _isControlBusy = value; this.RaiseNotifyPropertyChanged(); }
        }


        private bool _visiblePlateNumberError;

        public bool VisiblePlateNumberError
        {
            get { return _visiblePlateNumberError; }
            set
            {
                _visiblePlateNumberError = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _visiblePlateTypeError;

        public bool VisiblePlateTypeError
        {
            get { return _visiblePlateTypeError; }
            set
            {
                _visiblePlateTypeError = value;

                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _visiblePlateKindError;

        public bool VisiblePlateKindError
        {
            get { return _visiblePlateKindError; }
            set
            {
                _visiblePlateKindError = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _visiblePlateColorError;

        public bool VisiblePlateColorError
        {
            get { return _visiblePlateColorError; }
            set
            {
                _visiblePlateColorError = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _visiblePlateSourceError;

        public bool VisiblePlateSourceError
        {
            get { return _visiblePlateSourceError; }
            set
            {
                _visiblePlateSourceError = value;

                this.RaiseNotifyPropertyChanged();
            }
        }


        private void LoadLookupVehiclePlateClassifications()
        {
            var callTask = adpUTSclient.GetVehiclePlateClassificationsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateClassificationsList(x));

        }

        private void Add_VehiclePlateClassificationsList(VehiclePlateClassificationsDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehiclePlateClassficationsList = new ObservableCollection<VehiclePlateClassificationsDTO>(data);
                //VehiclePlateClassficationsList.Insert(0, new VehiclePlateClassificationsDTO() { Id = 0, Name = Properties.Resources.strSelect });
            });
        }

        private void LoadLookupVehiclePlateColors()
        {
            var callTask = adpUTSclient.GetVehiclePlateColorAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateColorsList(x));

        }

        private void Add_VehiclePlateColorsList(VehiclePlateColorDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehiclePlateColorList = new ObservableCollection<VehiclePlateColorDTO>(data);
                //VehiclePlateColorList.Insert(0, new VehiclePlateColorDTO() { Id = 0, Name = Properties.Resources.strSelect });
            });
        }

        private void LoadLookupVehiclePlateKind()
        {
            var callTask = adpUTSclient.GetVehiclePlateKindAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateKindList(x));

        }

        private void Add_VehiclePlateKindList(VehiclePlateKindDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehiclePlateKindList = new ObservableCollection<VehiclePlateKindDTO>(data);
                //VehiclePlateKindList.Insert(0, new VehiclePlateKindDTO() { Id = 0, Name = Properties.Resources.strSelect }); 
            });
        }

        private void LoadLookupVehiclePlateSource()
        {
            var callTask = adpUTSclient.GetVehiclePlateSourceAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehiclePlateSourceList(x));

        }

        private void Add_VehiclePlateSourceList(VehiclePlateSourceDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                VehiclePlateSourceList = new ObservableCollection<VehiclePlateSourceDTO>(data);
                //VehiclePlateSourceList.Insert(0, new VehiclePlateSourceDTO() { Id = 0, Name = Properties.Resources.strSelect }); 
            });
        }



        private ObservableCollection<VehiclePlateClassificationsDTO> _vehiclePlateClassficationsList;

        public ObservableCollection<VehiclePlateClassificationsDTO> VehiclePlateClassficationsList
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


        private ObservableCollection<VehiclePlateColorDTO> _vehiclePlateColorList;

        public ObservableCollection<VehiclePlateColorDTO> VehiclePlateColorList
        {
            get { return _vehiclePlateColorList; }
            set
            {
                _vehiclePlateColorList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<VehiclePlateKindDTO> _vehiclePlateKindList;

        public ObservableCollection<VehiclePlateKindDTO> VehiclePlateKindList
        {
            get { return _vehiclePlateKindList; }
            set
            {
                _vehiclePlateKindList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private ObservableCollection<VehiclePlateSourceDTO> _vehiclePlateSourceList;

        public ObservableCollection<VehiclePlateSourceDTO> VehiclePlateSourceList
        {
            get { return _vehiclePlateSourceList; }
            set
            {
                _vehiclePlateSourceList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }




        private string _plateNumber;

        public string PlateNumber
        {
            get { return _plateNumber; }
            set
            {
                _plateNumber = value;
                if (string.IsNullOrEmpty(value))
                    VisiblePlateNumberError = true;
                else
                    VisiblePlateNumberError = false;
                RaiseNotifyPropertyChanged();
            }
        }

        private string _errorRecordFoundStatus;
        public string ErrorRecordFoundStatus
        {
            get { return _errorRecordFoundStatus; }
            set { _errorRecordFoundStatus = value; RaiseNotifyPropertyChanged(); }
        }

        private bool _canEnablePreviousButton;

        public bool CanEnablePreviousButton
        {
            get { return _canEnablePreviousButton; }
            set { _canEnablePreviousButton = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _canEnableNextButton;

        public bool CanEnableNextButton
        {
            get { return _canEnableNextButton; }
            set { _canEnableNextButton = value; this.RaiseNotifyPropertyChanged(); }
        }

        private int _activeIndex;

        public int ActiveIndex
        {
            get { return _activeIndex; }
            set
            {
                CanEnablePreviousButton = true;
                CanEnableNextButton = true;

                if (value == 0) { CanEnablePreviousButton = false; }
                if (itemsCount > 0)
                {
                    if (value <= itemsCount)
                        _activeIndex = value;

                    if (value >= itemsCount - 1)
                        CanEnableNextButton = false;
                    if (_activeIndex != -1)
                        ImgSource = ImageURLList[_activeIndex];
                }
                else
                {
                    CanEnableNextButton = false;
                    CanEnablePreviousButton = false;
                    _activeIndex = value;
                }



                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _videoURL;
        public string VideoURL
        {
            get { return _videoURL; }
            set
            {
                _videoURL = value;
                if (value != null)
                {
                    var handler = ImageMediaSourceUpdated;
                    if (handler != null)
                        ImageMediaSourceUpdated(this, new EventArgs());
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<string> _imageURLList;
        public List<string> ImageURLList
        {
            get { return _imageURLList; }
            set
            {
                _imageURLList = value;
                if (value != null)
                {
                    itemsCount = value.Count;
                    if (value.Count > 0)
                        ActiveIndex = 0;
                    ImageSrcType = "StringPath";
                    var handler = ImageMediaSourceUpdated;
                    if (handler != null)
                        ImageMediaSourceUpdated(this, new EventArgs());
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<BitmapImage> _imageURLBitmap;

        public List<BitmapImage> ImageURLBitmap
        {
            get { return _imageURLBitmap; }
            set
            {
                _imageURLBitmap = value;
                //if (value != null)
                //{
                itemsCount = value.Count;
                ImageSrcType = "BitmapImage";
                var handler = ImageMediaSourceUpdated;
                if (handler != null)
                    ImageMediaSourceUpdated(this, new EventArgs());
                //}
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _sourceURL;
        public string SourceURL
        {
            get { return _sourceURL; }
            set
            {
                _sourceURL = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _imgSource;
        public string ImgSource
        {
            get { return _imgSource; }
            set
            {
                _imgSource = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _imageSrcType;
        public string ImageSrcType
        {
            get { return _imageSrcType; }
            set
            {
                _imageSrcType = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _isVideoStreamingVisible;
        public bool IsVideoStreamingVisible
        {
            get { return _isVideoStreamingVisible; }
            set
            {
                _isVideoStreamingVisible = value;
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

                this.RaiseNotifyPropertyChanged();
            }
        }

        public Command SearchDangerousViolatorCommand { get { return new Command((SearchDangerousViolatorExecute)); } }

        public void ShowStream()
        {
            if (SourceURL == "Image")
            {
                IsVideoStreamingVisible = false;
            }
            else if (SourceURL == "Video")
            {
                IsVideoStreamingVisible = true;
            }
        }

        private void LoadData()
        {
            LoadLookupVehiclePlateClassifications();
            LoadLookupVehiclePlateColors();
            LoadLookupVehiclePlateKind();
            LoadLookupVehiclePlateSource();
            //LoadBusinessRules();
            //LoadVehicleTypes();
        }

        private void SearchDangerousViolatorExecute()
        {

            try
            {
                IsControlBusy = true;

                VehicleDetailsRequest req = new VehicleDetailsRequest();
                bool canSearch = true;
                ErrorRecordFoundStatus = string.Empty;

                //var reportedDangViolatorHandler = ViolatorSearched;
                //GoToNextStepEventArgs e = new GoToNextStepEventArgs();
                //if (reportedDangViolatorHandler != null)
                //    reportedDangViolatorHandler(this, e);

                ErrorRecordFoundStatus = string.Empty;

                if (!string.IsNullOrEmpty(PlateNumber))
                    req.PlateNo = PlateNumber;
                else
                    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Enter Plate Number" : ErrorRecordFoundStatus + ", " + "Enter Plate Number";

                if (SelectedPlateType != null && SelectedPlateType.Name != Properties.Resources.strSelect)
                    req.PlateTypeCode = Convert.ToInt32(SelectedPlateType.Id);
                else
                    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Select Plate Type" : ErrorRecordFoundStatus + ", " + "Select Plate Type";

                if (SelectedColor != null && SelectedColor.Name != Properties.Resources.strSelect)
                    req.PlateColorCode = Convert.ToInt32(SelectedColor.Id);
                else
                    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Select Plate Color" : ErrorRecordFoundStatus + ", " + "Select Plate Color";

                if (SelectedKind != null && SelectedKind.Name != Properties.Resources.strSelect)
                    req.PlateKindCode = Convert.ToInt32(SelectedKind.Id);
                else
                    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Select Plate Kind" : ErrorRecordFoundStatus + ", " + "Select Plate Kind";

                if (SelectedSource != null && SelectedSource.Name != Properties.Resources.strSelect)
                    req.PlateSourceCode = Convert.ToInt32(SelectedSource.Id);
                else
                    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Select Plate Source" : ErrorRecordFoundStatus + ", " + "Select Plate Source";

                //if (SelectedBusinessRule == null)
                //    ErrorRecordFoundStatus = (string.IsNullOrEmpty(ErrorRecordFoundStatus)) ? "Select Business Rule" : ErrorRecordFoundStatus + ", " + "Select Business Rule";

                if (string.IsNullOrEmpty(ErrorRecordFoundStatus))
                {

                    //req.SystemCode = 4;
                    //req.UserId = "nn";
                    req.ChassisNoExist = true;

                    var searchResult = adpUTSclient.GetVehicleDetailsAsync(req);

                    searchResult.ContinueWith(x => SearchedDangerousViolator(x.Result));

                    //await Task.Delay(1500);

                    //req.PlateNo = "48724";
                    //req.PlateColorCode = 21;
                    //req.PlateKindCode = 1;
                    //req.PlateOrgNo = 0;
                    //req.PlateSourceCode = 2;
                    //req.PlateTypeCode = 1;
                    //req.SystemCode = 4;
                    //req.UserId = "nn";
                    //req.ChassisNoExist = true;
                }
                else
                {
                    ErrorRecordFoundStatus = "";
                    IsControlBusy = false;
                }
            }
            catch (Exception ex)
            {
                IsControlBusy = false;
                throw ex;
            }


        }



        private void SearchedDangerousViolator(VehicleDetailsResponse resp)
        {
            IsControlBusy = false;

            var searchedDangViolatorHandler = ViolatorSearched;

            VehicleDetailsResponse = resp;

            GoToNextStepEventArgs e = new GoToNextStepEventArgs();

            if (resp != null)
                //e.Confirmation = (resp != null);
                if (searchedDangViolatorHandler != null)
                    searchedDangViolatorHandler(this, new EventArgs());
        }

        #region INotifyPropertyChanged interface
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}