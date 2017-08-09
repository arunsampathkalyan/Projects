using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using Microsoft.AspNet.SignalR.Client;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.ViolationsMapControl.ServiceLayerReference;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using STC.Projects.ClassLibrary.Common.Helper;
using System.Globalization;


namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.ViewModel
{
    public class ViolationsMapControlViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerClient _client = new ServiceLayerClient();
        const int SHAPE_Z_INDEX = 1;
        const int SHAPE2_Z_INDEX = 2;
        const int MARKER_Z_INDEX = 3;
        IHubProxy _vehicleHub;
        private ServiceLayerReference.PeriodType _periodType;

        #region properties

        public event ZoomOnMapEventHandler ZoomOnMapEvent;

        public string CurrentUsername { get; set; }

        public string PolygonForSearch { get; set; }
        public List<OfficersLastLocationViewDTO> Officers { get; set; }

        private Dictionary<string, ObservableCollection<Graphic>> _layersGraphicsDictionary;
        //layers dynamic graphic collections
        public Dictionary<string, ObservableCollection<Graphic>> LayersGraphicsDictionary
        {
            get { return _layersGraphicsDictionary; }
            set
            {
                _layersGraphicsDictionary = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        //filters dropdowns list
        public ObservableCollection<ViolationTypeDimDTO> ViolationTypesList { get; set; }
        public ObservableCollection<AssetStatusDimDTO> AssetStatusList { get; set; }
        public ObservableCollection<AssetTypeDimDTO> AssetTypesList { get; set; }
        public ObservableCollection<PatrolStatusDimDTO> PatrolStatusList { get; set; }
        public ObservableCollection<UsersDTO> AllUsers { get; set; }
        public ObservableCollection<NameValueModel> ViolationsByStatusData { get; set; }
        public ObservableCollection<NameValueModel> ViolationsByTypeData { get; set; }

        private AssetViolationDetailsDTO _assetViolationsDetails;

        public AssetViolationDetailsDTO AssetViolationsDetails
        {
            get { return _assetViolationsDetails; }
            set { _assetViolationsDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ViolationDetailsDTO _violationsOrAssetsToolTipDetailsOnGraphic;
        private IncidentDetailsDTO _incidentsToolTipDetailsOnGraphic;
        private PatrolFullDetailsDTO _patrolToolTipDetailsOnGraphic;
        private AssetDetailsForViolation _assetDetailsForViolation;

        private VehicleLiveTrackingDTO _vehicleDetail;

        public VehicleLiveTrackingDTO VehicleDetail
        {
            get { return _vehicleDetail; }
            set { _vehicleDetail = value; this.RaiseNotifyPropertyChanged(); }
        }

        public AssetDetailsForViolation AssetDetailsForViolation
        {
            get { return _assetDetailsForViolation; }
            set { _assetDetailsForViolation = value; this.RaiseNotifyPropertyChanged(); }
        }
        private OfficersLastLocationViewDTO _officer;

        public OfficersLastLocationViewDTO Officer
        {
            get { return _officer; }
            set { _officer = value; this.RaiseNotifyPropertyChanged(); }
        }
        //public ViolationDetailsDTO ViolationsOrAssetsToolTipDetailsOnGraphic
        //{
        //    get { return _violationsOrAssetsToolTipDetailsOnGraphic; }
        //    set { _violationsOrAssetsToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        //}
        public IncidentDetailsDTO IncidentsToolTipDetailsOnGraphic
        {
            get { return _incidentsToolTipDetailsOnGraphic; }
            set { _incidentsToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        }
        public PatrolFullDetailsDTO PatrolToolTipDetailsOnGraphic
        {
            get { return _patrolToolTipDetailsOnGraphic; }
            set { _patrolToolTipDetailsOnGraphic = value; this.RaiseNotifyPropertyChanged(); }
        }
        public Command FilterViolationsCommand { get; set; }
        public Command FilterPatrolsCommand { get; set; }
        public Command FilterAssetsCommand { get; set; }


        //selected dropdowns
        public Object SelectedAssetType { set; get; }
        public Object SelectedAssetStatus { set; get; }
        public Object SelectedViolationType { set; get; }
        public Object SelectedPatrolStatus { set; get; }

        public Object SelectedViolationDateFilter { set; get; }

        #endregion

        private string _checkBtn;

        public String CheckBtn
        {
            get { return _checkBtn; }
            set
            {
                _checkBtn = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private int _fromMonthValue;
        private int _toMonthValue;

        public Int32 FromMonthValue
        {
            get { return _fromMonthValue; }
            set
            {
                if (_fromMonthValue != value)
                {
                    _fromMonthValue = value;

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }
        public Int32 ToMonthValue
        {
            get { return _toMonthValue; }
            set
            {
                if (_toMonthValue != value)
                {
                    _toMonthValue = value;




                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<CubeDTO> _violationsCollection;
        public ObservableCollection<CubeDTO> ViolationsCollection
        {
            get { return _violationsCollection; }
            set
            {
                _violationsCollection = value;

                if (_violationsCollection == null || _violationsCollection.Count() == 0)
                    LoadSampleData();

                RaiseNotifyPropertyChanged("ViolationsCollection");
            }
        }

        private int _yearValue;

        public int YearValue
        {
            get
            {
                if (_yearValue == 0)
                {
                    if (this.YearValueColl != null && YearValueColl.Count > 0)
                        _yearValue = YearValueColl[0];
                    else
                        _yearValue = DateTime.Now.Year;
                    GetViolationData();
                }

                return _yearValue;
            }
            set
            {
                if (_yearValue != value)
                {
                    _yearValue = value;

                    GetViolationData();

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<int> _yearValueColl;

        public ObservableCollection<int> YearValueColl
        {
            get
            {
                return _yearValueColl;
            }

            set
            {
                _yearValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }



        private ObservableCollection<string> _fromMonthValueColl;
        private ObservableCollection<string> _toMonthValueColl;


        public ObservableCollection<String> FromMonthValueColl
        { get { return _fromMonthValueColl; } set { _fromMonthValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        public ObservableCollection<String> ToMonthValueColl
        { get { return _toMonthValueColl; } set { _toMonthValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        private void LoadBasicData()
        {
            if (YearValueColl == null)
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(5));

            if (FromMonthValueColl == null)
                FromMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


        }

        private void GetViolationData()
        {
            var callTask = _client.GetViolationsStaticsticalMonthlyAsync(YearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = new ObservableCollection<CubeDTO>(data); });
        }

        private double _sliderMaximum;

        public Double SliderMaximum
        {
            get { return _sliderMaximum; }
            set { _sliderMaximum = value; this.RaiseNotifyPropertyChanged(); }
        }

        private double _sliderMinimum;

        public Double SliderMinimum
        {
            get { return _sliderMinimum; }
            set { _sliderMinimum = value; this.RaiseNotifyPropertyChanged(); }
        }

        private double _sliderCurrValue;

        public Double SliderCurrValue
        {
            get { return _sliderCurrValue; }
            set { _sliderCurrValue = value; this.RaiseNotifyPropertyChanged(); }
        }


        private string _currentDateRange;

        public String CurrentDateRange
        {
            get { return _currentDateRange; }
            set { _currentDateRange = value; this.RaiseNotifyPropertyChanged(); }
        }


        private ServiceLayerReference.PeriodType _periodTypeFilter;

        public ServiceLayerReference.PeriodType PeriodTypeFilter
        {
            get { return _periodTypeFilter; }
            set
            {

                _periodTypeFilter = value;
                this.SliderMinimum = 0;

                this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(value);

                //switch (value)
                //{

                //    case ServiceLayerReference.PeriodType.Daily:
                //        {
                //            //CalculateNumberofMonths();

                //            //this.SliderMaximum = (HistoricalDaysCategViolationList != null && HistoricalDaysCategViolationList.Count > 0) ? 2 * (HistoricalDaysCategViolationList.Count - 1) : 2;
                //            this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(_periodTypeFilter);


                //            break;
                //        }
                //    case ServiceLayerReference.PeriodType.Weekly:
                //        {
                //            //CalculateNumberofMonths();

                //            this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(_periodTypeFilter);

                //            break;
                //        }
                //    case ServiceLayerReference.PeriodType.Monthly:
                //        {
                //            //CalculateNumberofMonths();
                //            this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(_periodTypeFilter);
                //            break;
                //        }
                //}

                this.SliderCurrValue = this.SliderMaximum / 2;

                this.RaiseNotifyPropertyChanged();

            }
        }

        private int GetCountsToDisplayUptoCurrentDateCategory(ServiceLayerReference.PeriodType periodType)
        {
            int count = 2;
            switch (periodType)
            {

                case ServiceLayerReference.PeriodType.Daily:
                    {
                        //CalculateNumberofMonths();

                        count = (HistoricalDaysCategViolationList != null && HistoricalDaysCategViolationList.Count > 0) ? 2 * (HistoricalDaysCategViolationList.Count - 1) : 2;

                        if (HistoricalDaysCategViolationList != null && HistoricalDaysCategViolationList.Count > 0)
                        {
                            for (int i = HistoricalDaysCategViolationList.Count - 1; i >= 0; i--)
                            {
                                string[] dateYear = (HistoricalDaysCategViolationList[i].DateElement.Split(new char[] { ' ' }));

                                if (dateYear.Length > 3 && dateYear[3].Length == 4
                                    && Convert.ToInt32(dateYear[3]) == DateTime.Now.Year
                                    && DateTime.ParseExact(dateYear[1], "MMMM", CultureInfo.CurrentCulture).Month == DateTime.Now.Month
                                    && Convert.ToInt32(dateYear[2]) == DateTime.Now.Day)
                                {
                                    count = i * 2;
                                    //count = i
                                    //count = count * 2;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                case ServiceLayerReference.PeriodType.Weekly:
                    {
                        //CalculateNumberofMonths();
                        count = (HistoricalWeeksCategViolationList != null && HistoricalWeeksCategViolationList.Count > 0) ? 2 * (HistoricalWeeksCategViolationList.Count - 1) : 2;

                        if (HistoricalWeeksCategViolationList != null && HistoricalWeeksCategViolationList.Count > 0)
                        {
                            for (int i = HistoricalWeeksCategViolationList.Count - 1; i >= 0; i--)
                            {
                                string[] weekYear = HistoricalWeeksCategViolationList[i].DateElement.Replace(" ", "").Split(new char[] { ',' });
                                if (weekYear.Length > 1 && weekYear[0].Length < 3 && weekYear[1].Length == 4)
                                {
                                    if (Convert.ToInt32(weekYear[0]) == ((DateTime.Now.DayOfYear + 1) / 7) + 1 && Convert.ToInt32(weekYear[1]) == DateTime.Now.Year)
                                    {
                                        count = i * 2;
                                        //count = i;
                                        //count = count * 2;
                                        break;
                                    }
                                }
                            }
                        }


                        break;
                    }
                case ServiceLayerReference.PeriodType.Monthly:
                    {
                        //CalculateNumberofMonths();
                        count = (HistoricalMonthsCategViolationList != null && HistoricalMonthsCategViolationList.Count > 0) ? 2 * (HistoricalMonthsCategViolationList.Count - 1) : 2;


                        if (HistoricalMonthsCategViolationList != null && HistoricalMonthsCategViolationList.Count > 0)
                        {
                            for (int i = HistoricalMonthsCategViolationList.Count - 1; i >= 0; i--)
                            {
                                string[] monthYear = HistoricalMonthsCategViolationList[i].DateElement.Split(new char[] { ' ' });
                                if (monthYear.Length > 1 && monthYear[1].Length == 4 && Convert.ToInt32(monthYear[1]) == DateTime.Now.Year)
                                {
                                    if (DateTime.ParseExact(monthYear[0], "MMMM", CultureInfo.CurrentCulture).Month == DateTime.Now.Month)
                                    {
                                        count = i * 2;
                                        //count = i;
                                        //count = count * 2;
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                    }
            }
            return count;
        }

        public void UpdateSliderValues()
        {
            this.SliderMinimum = 0;

            this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(this.PeriodTypeFilter);

            //switch (this.PeriodTypeFilter)
            //{

            //    case ServiceLayerReference.PeriodType.Daily:
            //        {
            //            //CalculateNumberofMonths();

            //            this.SliderMaximum = GetCountsToDisplayUptoCurrentDateCategory(PeriodTypeFilter);
            //            break;
            //        }
            //    case ServiceLayerReference.PeriodType.Weekly:
            //        {
            //            //CalculateNumberofMonths();
            //            this.SliderMaximum = (HistoricalWeeksCategViolationList != null && HistoricalWeeksCategViolationList.Count > 0) ? 2 * (HistoricalWeeksCategViolationList.Count - 1) : 2;
            //            break;
            //        }
            //    case ServiceLayerReference.PeriodType.Monthly:
            //        {
            //            //CalculateNumberofMonths();
            //            this.SliderMaximum = (HistoricalMonthsCategViolationList != null && HistoricalMonthsCategViolationList.Count > 0) ? 2 * (HistoricalMonthsCategViolationList.Count - 1) : 2;
            //            break;
            //        }
            //}

            this.SliderCurrValue = this.SliderMaximum / 2;
            GetAndPopulateCurrentIterationViolations((int)this.SliderCurrValue);
        }




        public void GetAndPopulateCurrentIterationViolations(int index)
        {
            switch (PeriodTypeFilter)
            {
                case ServiceLayerReference.PeriodType.Daily:
                    {
                        if (HistoricalDaysCategViolationList.Count > index)
                        {
                            Add_HistoricalViolationData(new ObservableCollection<ViolationsGroupedByLocationsDTO>(HistoricalDaysCategViolationList[index].AssetDetails));
                            CurrentDateRange = HistoricalDaysCategViolationList[index].DateElement;
                        }
                        break;
                    }
                case ServiceLayerReference.PeriodType.Weekly:
                    {
                        //CalculateNumberofMonths();
                        if (HistoricalWeeksCategViolationList.Count > index)
                        {
                            Add_HistoricalViolationData(new ObservableCollection<ViolationsGroupedByLocationsDTO>(HistoricalWeeksCategViolationList[index].AssetDetails));
                            CurrentDateRange = HistoricalWeeksCategViolationList[index].DateElement;

                            string[] weekYear = HistoricalWeeksCategViolationList[index].DateElement.Replace(" ", "").Split(new char[] { ',' });
                            if (weekYear.Length > 1 && weekYear[0].Length < 3 && weekYear[1].Length == 4)
                            {
                                int weekNum = Convert.ToInt32(weekYear[0]);
                                int year = Convert.ToInt32(weekYear[1]);
                                if (Convert.ToInt32(weekYear[0]) < 53)
                                {
                                    DateTime startDt = new DateTime(year, 1, 1);
                                    startDt = startDt.AddDays((weekNum - 1) * 7);
                                    CurrentDateRange = startDt.ToString("dd MMM - ") + startDt.AddDays(6).ToString("dd MMM") + weekYear[1];
                                }
                                else
                                    CurrentDateRange = new DateTime(year, 1, 1).AddDays((weekNum - 1) * 7).ToString("dd MMM - ") + new DateTime(year, 12, 31).ToString("dd MMM ") + weekYear[1];
                            }

                        }

                        break;
                    }
                case ServiceLayerReference.PeriodType.Monthly:
                    {
                        //CalculateNumberofMonths();
                        if (HistoricalMonthsCategViolationList.Count > index)
                        {
                            Add_HistoricalViolationData(new ObservableCollection<ViolationsGroupedByLocationsDTO>(HistoricalMonthsCategViolationList[index].AssetDetails));
                            CurrentDateRange = HistoricalMonthsCategViolationList[index].DateElement;


                        }
                        break;
                    }
            }

        }




        private ObservableCollection<ViolationsCountForMapDTO> _historicalWeeksCategViolationList;
        public ObservableCollection<ViolationsCountForMapDTO> HistoricalWeeksCategViolationList
        {
            get { return _historicalWeeksCategViolationList; }
            set
            {
                this._historicalWeeksCategViolationList = value;
                if (value != null && value.Count > 0 && this.PeriodTypeFilter == ServiceLayerReference.PeriodType.Weekly)
                    UpdateSliderValues();
                this.RaiseNotifyPropertyChanged();
            }
        }

        private async void GetViolationsForWeeksCategory()
        {
            HistoricalWeeksCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>();
            GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Weekly);
        }

        private ObservableCollection<ViolationsCountForMapDTO> _historicalMonthsCategViolationList;
        public ObservableCollection<ViolationsCountForMapDTO> HistoricalMonthsCategViolationList
        {
            get { return _historicalMonthsCategViolationList; }
            set
            {
                this._historicalMonthsCategViolationList = value;
                if (value != null && value.Count > 0 && this.PeriodTypeFilter == ServiceLayerReference.PeriodType.Monthly)
                    UpdateSliderValues();
                this.RaiseNotifyPropertyChanged();
            }
        }

        private async void GetViolationsForMonthsCategory()
        {
            HistoricalMonthsCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>();
            GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Monthly);
        }

        private ObservableCollection<ViolationsCountForMapDTO> _historicalDaysCategViolationList;
        public ObservableCollection<ViolationsCountForMapDTO> HistoricalDaysCategViolationList
        {
            get { return _historicalDaysCategViolationList; }
            set
            {
                this._historicalDaysCategViolationList = value;
                if (value != null && value.Count > 0 && this.PeriodTypeFilter == ServiceLayerReference.PeriodType.Daily)
                    UpdateSliderValues();
                this.RaiseNotifyPropertyChanged();
            }
        }

        private async void GetViolationsForDaysCategory()
        {
            HistoricalDaysCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>();
            GetViolationsCountPerAsset(ServiceLayerReference.PeriodType.Daily);
        }



        private void LoadSampleData()
        {
            ViolationsCollection = new ObservableCollection<CubeDTO>();
            for (int i = 1; i <= 4; i++)
            {
                CubeDTO cdto = new CubeDTO();
                cdto.LegendName = "C" + i.ToString();
                cdto.Details = new CubeDetailsDTO[12];
                for (int j = 1; j <= 12; j++)
                {
                    CubeDetailsDTO cddto = new CubeDetailsDTO();
                    cddto.Key = "M" + j.ToString();
                    cddto.Value = j * 5 % 3;
                    cdto.Details[j - 1] = cddto;
                }
                ViolationsCollection.Add(cdto);
            }
        }

        public ViolationsMapControlViewModel()
        {
            LayersGraphicsDictionary = new Dictionary<string, ObservableCollection<Graphic>>();

            Officers = new List<OfficersLastLocationViewDTO>();
            Officer = new OfficersLastLocationViewDTO();

            FilterViolationsCommand = new Command(FilterViolationsOnMap);
            FilterPatrolsCommand = new Command(FilterPatrolsOnMap);
            FilterAssetsCommand = new Command(FilterAssetsOnMap);

            GetLayerObservable(LayerTypeEnum.Incidents);
            GetLayerObservable(LayerTypeEnum.Violations);
            GetLayerObservable(LayerTypeEnum.Patrols);
            GetLayerObservable(LayerTypeEnum.Assets);
            GetLayerObservable(LayerTypeEnum.Notifications);
            GetLayerObservable(LayerTypeEnum.Officers);

            ViolationTypesList = new ObservableCollection<ViolationTypeDimDTO>();
            AssetStatusList = new ObservableCollection<AssetStatusDimDTO>();
            AssetTypesList = new ObservableCollection<AssetTypeDimDTO>();
            PatrolStatusList = new ObservableCollection<PatrolStatusDimDTO>();
            AllUsers = new ObservableCollection<UsersDTO>();
            //ViolationsOrAssetsToolTipDetailsOnGraphic = new ViolationDetailsDTO();

            ViolationsByStatusData = new ObservableCollection<NameValueModel>();
            ViolationsByTypeData = new ObservableCollection<NameValueModel>();
            AssetDetailsForViolation = new AssetDetailsForViolation();

            LoadData();

            LoadBasicData();

        }




        public void SendIncidentToUsers(List<UsersDTO> usersToSend)
        {
            //GetViolationsStaticsticalMonthly _cl

            string xmlToSend = IncidentsToolTipDetailsOnGraphic.XML;
            _client.SaveSendControlToUsersAsync(xmlToSend, usersToSend.Select(x => x.UserName).ToArray());
        }

        public void AddGraphicsToLayersGraphicsDictionary(LayerTypeEnum layerType)
        {
            switch (layerType)
            {
                case LayerTypeEnum.Violations:
                    GetViolationsData(null, null);
                    break;
                case LayerTypeEnum.Incidents:
                    GetIncidentsDTOData();
                    break;
                case LayerTypeEnum.Patrols:
                    GetPatrolLastLocationDTOData(null);
                    break;
                case LayerTypeEnum.Assets:
                    GetAssetLastStatusData(null, null);
                    break;
                case LayerTypeEnum.Officers:
                    GetOfficersData();
                    break;
            }
        }

        private void LoadData()
        {
            GetAvailableYearsViolationsAvailable();
            GetViolationsForMonthsCategory();
            GetViolationsForDaysCategory();
            GetViolationsForWeeksCategory();


            //load filters
            GetAssetStatusDimDTOList();
            GetAssetTypesList();
            GetViolationTypeDimDTOsList();
            GetPatrolStatusList();
            GetAllUsersList();


            //connect signalR
            Task connectTask = new Task(ConnectToSignalR);
            connectTask.Start();

        }

        public void ConnectToSignalR()
        {
            string url = Utility.GetSignalRUrl();

            HubConnection conn = new HubConnection(url);
            var violationsHub = conn.CreateHubProxy("ViolationsHub");
            var assetsHub = conn.CreateHubProxy("AssetsHub");
            var incidentHub = conn.CreateHubProxy("IncidentHub");
            var patrolHub = conn.CreateHubProxy("PatrolHub");
            _vehicleHub = conn.CreateHubProxy("VehicleLiveTrackingHub");
            violationsHub.On<ObservableCollection<ViolationNotificationDTO>>("NewViolations", UpdateViolationsBySignalR);
            assetsHub.On<ObservableCollection<AssetLastStatusDTO>>("NewAssets", UpdateAssetsBySignalR);
            incidentHub.On<ObservableCollection<IncidentsDTO>>("NewIncidents", UpdateIncidentsBySignalR);
            patrolHub.On<ObservableCollection<PatrolLastLocationDTO>>("NewLocations", UpdatePatrolsBySignalR);
            _vehicleHub.On<ObservableCollection<VehicleLiveTrackingDTO>>("NewVehicleLocations", UpdateWantedCarLocationBySignalR);
            conn.Start().Wait();
        }

        public void RegisterWantedCarSignalR(string plateNumber)
        {
            _vehicleHub.Invoke("RegisterDependency", plateNumber);
        }

        public void UnRegisterWantedCarSignalR()
        {
            try
            {
                _vehicleHub.Invoke("UnRegisterDependency");
            }
            catch
            {

            }
        }

        #region SignalR update

        private void UpdateWantedCarLocationBySignalR(ObservableCollection<VehicleLiveTrackingDTO> vehicles)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var vehicle in vehicles)
            {
                var markerGraphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_WantedCarCenter");
                var circleGraphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == vehicle.PlateNumber.ToString() + "_Circle_" + SHAPE2_Z_INDEX);

                if (markerGraphic != null)
                    Application.Current.Dispatcher.Invoke(() => markerGraphic.Geometry = new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)));

                if (circleGraphic != null)
                    Application.Current.Dispatcher.Invoke(() => DrawCircle(vehicle.PlateNumber, new MapPoint(vehicle.Longitude.Value, vehicle.Latitude.Value, new SpatialReference(4326)), (double)_client.GetWantedCarRadius(), circleGraphic.Symbol as SimpleFillSymbol, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, Colors.Red));

                if (ZoomOnMapEvent != null)
                    ZoomOnMapEvent(vehicle.Latitude, vehicle.Longitude, vehicle.PlateNumber);
            }
        }
        private void UpdatePatrolsBySignalR(ObservableCollection<PatrolLastLocationDTO> patrols)
        {
            patrols = new ObservableCollection<PatrolLastLocationDTO>(patrols.Where(x =>
                  SelectedPatrolStatus != null && ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId != 0 ? x.StatusId == ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId : true
                  ).ToList<PatrolLastLocationDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Patrols);
            foreach (var item in patrols)
            {
                var graphic = layerCol.FirstOrDefault(x => long.Parse(x.Attributes["Id"].ToString()) == item.PatrolId);
                if (graphic != null)
                    Application.Current.Dispatcher.Invoke(() => graphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
                else
                    Application.Current.Dispatcher.Invoke(() =>
                    {

                        layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item)));
                    });
            }
        }
        private void UpdateIncidentsBySignalR(ObservableCollection<IncidentsDTO> incidents)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Incidents);
            foreach (var item in incidents)
            {
                var graphic = layerCol.FirstOrDefault(x => long.Parse(x.Attributes["Id"].ToString()) == item.IncidentId);

                if (!item.EndTime.HasValue)
                {
                    if (graphic != null)
                        Application.Current.Dispatcher.Invoke(() => graphic.Geometry = new MapPoint(item.Longitude.Value, item.Latitude.Value, new SpatialReference(4326)));
                    else
                        Application.Current.Dispatcher.Invoke(() => layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item))));
                }
                else
                {
                    if (graphic != null)
                        Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
                }
            }
        }
        private void UpdateAssetsBySignalR(ObservableCollection<AssetLastStatusDTO> assets)
        {
            assets = new ObservableCollection<AssetLastStatusDTO>(assets.Where(x =>
                  SelectedAssetStatus != null && ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId != 0 ? x.AssetStatusId == ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId : true &&
                   SelectedAssetType != null && ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId != 0 ? x.AssetTypeId == ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId : true
                  ).ToList<AssetLastStatusDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Assets);
            foreach (var item in assets)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.AssetId);
                if (graphic != null)
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        graphic.Attributes["Status"] = item.AssetStatusName;

                        CompositeSymbol compositeSymbol = new CompositeSymbol();
                        PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();

                        pictureMarkerSymbol.SetSourceAsync(new Uri(GetMarkerImageUrl(MarkerType.Assets, item)));


                        compositeSymbol.Symbols.Add(pictureMarkerSymbol);

                        graphic.Symbol = compositeSymbol;
                    });
            }
        }
        private void UpdateViolationsBySignalR(ObservableCollection<ViolationNotificationDTO> violations)
        {
            violations = new ObservableCollection<ViolationNotificationDTO>(violations.Where(x =>
                  SelectedViolationDateFilter != null ? x.DateTaken > (((DateTime?)SelectedViolationDateFilter)).Value : true &&
                   SelectedViolationType != null && ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId != 0 ? x.ViolationTypeId == ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId : true
                  ).ToList<ViolationNotificationDTO>());

            var layerCol = GetLayerObservable(LayerTypeEnum.Violations);
            foreach (var item in violations)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.LocationCode);
                if (graphic != null)
                {
                    int count = graphic.Attributes["Count"] != null ? int.Parse(graphic.Attributes["Count"].ToString()) : 0;
                    count++;
                    Application.Current.Dispatcher.Invoke(() => graphic.Attributes["Count"] = count.ToString());
                    Application.Current.Dispatcher.Invoke(() => ((graphic.Symbol as CompositeSymbol).Symbols[1] as TextSymbol).Text = count.ToString());
                }
                else
                {
                    if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                        Application.Current.Dispatcher.Invoke(() => layerCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Violations, 1), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Violation, item), 1)));
                }
            }
        }

        #endregion

        #region AddLayerDataOnMap

        public void GetAssetViolationDetails(string ID)
        {
            var callTask = _client.GetAssetViolationsAsync(ID, Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetViolationsDetails(x));
        }

        private void Add_AssetViolationsDetails(AssetViolationDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => { AssetViolationsDetails = data; });
        }

        private void GetAvailableYearsViolationsAvailable()
        {
            var callTask = _client.GetViolationsAvailableYearsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsAvailableYears(x));
        }

        private void Add_ViolationsAvailableYears(int[] lst)
        {
            Application.Current.Dispatcher.Invoke(() => { YearValueColl = new ObservableCollection<int>(lst.OrderByDescending(x => x)); });
        }

        private void GetPatrolLastLocationDTOData(int? SelectedPatrolStatusId)
        {
            var callTask = _client.GetPatrolsListAsync(SelectedPatrolStatusId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolLastLocationDTOData(x == null ? null : x.ToList()));
        }
        private void Add_PatrolLastLocationDTOData(List<PatrolLastLocationDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Patrols);
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item))));
        }

        private void GetAssetLastStatusData(int? SelectedAssetStatusDimDTO, int? SelectedAssetType)
        {
            var callTask = _client.GetAssetsListAsync(SelectedAssetStatusDimDTO, SelectedAssetType);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetLastStatusData(x == null ? null : x.ToList()));
        }

        private void GetOfficersData()
        {
            var callTask = _client.GetOfficersListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe(x => Add_OfficersLastLocationDTOData(x.ToList()));
        }

        private void Add_OfficersLastLocationDTOData(List<OfficersLastLocationViewDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Officers);
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Officers), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Officers, item))));
            Officers = list;
        }

        private void Add_AssetLastStatusData(List<AssetsViewDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Assets);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
                }
            }
        }
        private void GetViolationsData(DateTime? dateTaken, int? ViolationTypeDimDTOId)
        {
            var callTask = _client.GetViolationsListGroupedByLocationAsync(dateTaken, ViolationTypeDimDTOId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationData(x != null ? new ObservableCollection<ViolationsGroupedByLocationsDTO>(x) : null));
        }

        public void GetViolationsDataByLocation(DateTime? fromDate, DateTime? toDate, int? ViolationTypeDimDTOId)
        {
            var callTask = _client.GetHistoricalViolationsListGroupedByLocationAsync(fromDate, toDate, null);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_HistoricalViolationData(x != null ? new ObservableCollection<ViolationsGroupedByLocationsDTO>(x) : null));
        }

        public void GetViolationsCountPerAsset(ServiceLayerReference.PeriodType periodType)
        {
            _periodType = periodType;
            HistoricalCategoryViolationList = null;
            var callTask = _client.GetViolationsCountPerAssetAsync(periodType);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_HistoricalCategoryViolationData(x != null ? new ObservableCollection<ViolationsCountForMapDTO>(x) : null, periodType));

            /*
            switch (PeriodTypeFilter)
            {
                case ServiceLayerReference.PeriodType.Daily:
                    {
                        if (HistoricalDaysCategViolationList == null || HistoricalDaysCategViolationList.Count == 0)
                        {
                            ViolationsCountForMapDTO[] tempData = _client.GetViolationsCountPerAsset(periodType);
                            Application.Current.Dispatcher.Invoke(() => { HistoricalDaysCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>(tempData); });
                        }
                        break;
                    }
                case ServiceLayerReference.PeriodType.Weekly:
                    {
                        if (HistoricalWeeksCategViolationList == null || HistoricalWeeksCategViolationList.Count == 0)
                        {
                            ViolationsCountForMapDTO[] tempData = _client.GetViolationsCountPerAsset(periodType);
                            Application.Current.Dispatcher.Invoke(() => { HistoricalWeeksCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>(tempData); });
                        }
                        break;
                    }
                case ServiceLayerReference.PeriodType.Monthly:
                    {
                        if (HistoricalMonthsCategViolationList == null || HistoricalMonthsCategViolationList.Count == 0)
                        {
                            ViolationsCountForMapDTO[] tempData = _client.GetViolationsCountPerAsset(periodType);
                            Application.Current.Dispatcher.Invoke(() => { HistoricalMonthsCategViolationList = new ObservableCollection<ViolationsCountForMapDTO>(tempData); });
                        }
                        break;
                    }
            }
            */

            //if (HistoricalCategoryViolationList == null || HistoricalCategoryViolationList.Count == 0)
            //{
            //    ViolationsCountForMapDTO[] tempData = _client.GetViolationsCountPerAsset(periodType);
            //    Application.Current.Dispatcher.Invoke(() => { HistoricalCategoryViolationList = new ObservableCollection<ViolationsCountForMapDTO>(tempData); });
            //}

        }

        private void Add_ViolationData(ObservableCollection<ViolationsGroupedByLocationsDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Violations);
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Violations, item.ViolationsCount), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Violation, item), item.ViolationsCount)));
        }

        public ObservableCollection<Graphic> _hioricalViolationList;
        public ObservableCollection<Graphic> HioricalViolationList
        {
            get { return _hioricalViolationList; }
            set
            {
                this._hioricalViolationList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        public void Add_HistoricalViolationData(ObservableCollection<ViolationsGroupedByLocationsDTO> list)
        {
            if (list == null || list.Count == 0)
                return;

            int threashouldValue = 0;
            switch (_periodType)
            {
                case ServiceLayerReference.PeriodType.Daily:
                    threashouldValue = 20;
                    break;
                case ServiceLayerReference.PeriodType.Weekly:
                    threashouldValue = 70;
                    break;
                case ServiceLayerReference.PeriodType.Monthly:
                    threashouldValue = 100;
                    break;
            }

            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.HistoricalViolations);
            graphicCol.Clear();
            foreach (var item in list)
            {
                Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.HistoricalViolations, item.ViolationsCount), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Violation, item, threashouldValue), item.ViolationsCount)));
            }
            HioricalViolationList = graphicCol;
        }

        private ObservableCollection<ViolationsCountForMapDTO> _historicalCategoryViolationList;
        public ObservableCollection<ViolationsCountForMapDTO> HistoricalCategoryViolationList
        {
            get { return _historicalCategoryViolationList; }
            set
            {
                this._historicalCategoryViolationList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        public void Add_HistoricalCategoryViolationData(ObservableCollection<ViolationsCountForMapDTO> data, ServiceLayerReference.PeriodType periodType)
        {
            switch (periodType)
            {
                case ServiceLayerReference.PeriodType.Daily:
                    {
                        Application.Current.Dispatcher.Invoke(() => { HistoricalDaysCategViolationList = data; });
                        break;
                    }
                case ServiceLayerReference.PeriodType.Weekly:
                    {
                        Application.Current.Dispatcher.Invoke(() => { HistoricalWeeksCategViolationList = data; });
                        break;
                    }
                case ServiceLayerReference.PeriodType.Monthly:
                    {
                        Application.Current.Dispatcher.Invoke(() => { HistoricalMonthsCategViolationList = data; });
                        break;
                    }
            }
            //Application.Current.Dispatcher.Invoke(() => { HistoricalCategoryViolationList = data; });
        }

        public void PopulateCurrentCategoryHistoricalData(int index)
        {
            if (HistoricalCategoryViolationList.Count > index)
                Add_HistoricalViolationData(new ObservableCollection<ViolationsGroupedByLocationsDTO>(HistoricalCategoryViolationList[index].AssetDetails));
        }


        public void GetHistoricalViolationsData(DateTime startDate, DateTime? endDate, PeriodCategory periodCategory)
        {
            var callTask = _client.GetViolationHistoricalViewByDateAsync(startDate, endDate, periodCategory);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_HistoricalViolationData(x != null ? new ObservableCollection<ViolationsHistoricalDTO>(x) : null));
        }

        private void Add_HistoricalViolationData(ObservableCollection<ViolationsHistoricalDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.HistoricalViolations);
            graphicCol.Clear();
            foreach (var itemList in list)
            {
                foreach (var item in itemList.ViolationsByLocations)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Violations, item.ViolationsCount), item.Latitude, item.Longitude, GetMarkerImageUrl(MarkerType.Violation, item), item.ViolationsCount)));
                }
            }
        }

        private void GetIncidentsDTOData()
        {
            var callTask = _client.GetActiveIncidentsListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_IncidentsDTOData(x != null ? new ObservableCollection<IncidentsDTO>(x) : null));
        }
        private void Add_IncidentsDTOData(ObservableCollection<IncidentsDTO> list)
        {
            if (list == null)
                return;
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Incidents);
            foreach (var item in list)
            {
                if (item.Latitude.HasValue != false && item.Longitude.HasValue != false)
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Incidents), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Incident, item))));
            }
        }
        public void AddNotificationViolationOnMap(ViolationToDraw violationToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == violationToDraw.Id && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Violations.ToString());
            if (graphic == null)
            {
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(violationToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Violations), violationToDraw.Latitude, violationToDraw.Longitude, GetMarkerImageUrl(MarkerType.ViolationsNotifications, violationToDraw)));
            }
        }
        public void AddNotificationIncidentOnMap(IncidentToDraw incidentToDraw)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == incidentToDraw.Id.ToString() && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Incidents.ToString());
            if (graphic == null)
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(incidentToDraw, LayerTypeEnum.Notifications, null, LayerTypeEnum.Incidents), incidentToDraw.Latitude, incidentToDraw.Longitude, GetMarkerImageUrl(MarkerType.IncidentNotification, incidentToDraw)));
        }
        public void ManageFogEventMessage(FogEventMessage fogEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(fogEventMessage.Longitude, fogEventMessage.Latitude, new SpatialReference(4326));
            DrawCircle(fogEventMessage.TowerId.ToString(), point, fogEventMessage.VisibilityRadius, BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, _client.GetRadiusForNearByAssets(), BufferSymbolCircleBig, SHAPE_Z_INDEX, LayerTypeEnum.Notifications);
            SearchNearByAssetsAndPatrolsForFogEvent(fogEventMessage);
        }

        //Manage Wanted Car Message
        public void ManageWantedCarEventMessage(WantedCarMessage wantedCarEventMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude, new SpatialReference(4326));
            DrawCircle(wantedCarEventMessage.VehiclePlateNumber.ToString(), point, (double)_client.GetWantedCarRadius(), BufferSymbolCircleSmall, SHAPE2_Z_INDEX, LayerTypeEnum.Notifications, color);
            //DrawCircle(fogEventMessage.TowerId.ToString(), point, _client.GetRadiusForNearByAssets(), BufferSymbolCircleBig, SHAPE_Z_INDEX, LayerTypeEnum.Notifications);
            SearchNearByAssetsAndPatrolsForWantedCarEvent(wantedCarEventMessage);
        }
        public void ManageDetectedAccidentEventMessage(DetectedAccidentMessage detectedMessage, SimpleFillSymbol BufferSymbolCircleSmall, SimpleFillSymbol BufferSymbolCircleBig, Color color)
        {
            MapPoint point = new MapPoint(detectedMessage.Longitude, detectedMessage.Latitude, new SpatialReference(4326));
            SearchNearByAssetsAndPatrolsForDetectedAccidentEvent(detectedMessage);
        }
        private bool AddFogTowerMarker(FogEventMessage fogEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (fogEventMessage.TowerId.ToString() + "_FogCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(fogEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), fogEventMessage.Latitude, fogEventMessage.Longitude, GetMarkerImageUrl(MarkerType.Fog, fogEventMessage));
                graphicCol.Add(graphic);
            }
            return isNew;
        }

        //Add Wanted Car Tower Marker
        private bool AddWantedCarTowerMarker(WantedCarMessage wantedCarEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (wantedCarEventMessage.VehiclePlateNumber.ToString() + "_WantedCarCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(wantedCarEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), wantedCarEventMessage.Latitude, wantedCarEventMessage.Longitude, GetMarkerImageUrl(MarkerType.WantedCar, wantedCarEventMessage));
                graphicCol.Add(graphic);
            }
            return isNew;
        }
        private bool AddDetectedAccidentMarker(DetectedAccidentMessage detectedAccidentEventMessage)
        {
            bool isNew = false;
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var graphic = layerCol.FirstOrDefault(x => x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (detectedAccidentEventMessage.TowerId.ToString() + "_AccidentCenter") && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString());
            if (graphic == null)
            {
                isNew = true;
                graphic = CreateGraphic(CreateGraphicDictionary(detectedAccidentEventMessage, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), detectedAccidentEventMessage.Latitude, detectedAccidentEventMessage.Longitude, GetMarkerImageUrl(MarkerType.DetectedAccident, detectedAccidentEventMessage));
                graphicCol.Add(graphic);
            }
            return isNew;
        }
        private void SearchNearByAssetsAndPatrolsForFogEvent(FogEventMessage fogEventMessage)
        {
            bool isNewTower = AddFogTowerMarker(fogEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByTowersByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByTowersForFogEvent(x == null ? null : x.ToList(), fogEventMessage.TowerId));
                //callTask = _client.GetNearByRadarsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForFogEvent(x == null ? null : x.ToList(), fogEventMessage));
                //callTask = _client.GetNearByCamerasByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByCamerasForFogEvent(x == null ? null : x.ToList(), fogEventMessage));

                //var callTask = _client.GetNearByPatrolsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
            }
        }

        //Search Near By Assets and Patrols For Wanted Car Event
        private void SearchNearByAssetsAndPatrolsForWantedCarEvent(WantedCarMessage wantedCarEventMessage)
        {
            bool isNewTower = AddWantedCarTowerMarker(wantedCarEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByRadarsByLatLonAsync(wantedCarEventMessage.Longitude, wantedCarEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForWantedCar(x == null ? null : x.ToList(), wantedCarEventMessage));
            }
        }

        private void SearchNearByAssetsAndPatrolsForDetectedAccidentEvent(DetectedAccidentMessage detectedAccidentEventMessage)
        {
            bool isNewTower = AddDetectedAccidentMarker(detectedAccidentEventMessage);
            if (isNewTower)
            {
                //var callTask = _client.GetNearByTowersByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //var obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByTowersForFogEvent(x == null ? null : x.ToList(), fogEventMessage.TowerId));
                //callTask = _client.GetNearByRadarsByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByRadarsForFogEvent(x == null ? null : x.ToList(), fogEventMessage));
                //callTask = _client.GetNearByCamerasByLatLonAsync(fogEventMessage.Longitude, fogEventMessage.Latitude);
                //obs = callTask.ToObservable();
                //obs.Subscribe((x) => Add_NearByCamerasForFogEvent(x == null ? null : x.ToList(), fogEventMessage));

                var callTask = _client.GetNearByPatrolsByLatLonAsync(detectedAccidentEventMessage.Longitude, detectedAccidentEventMessage.Latitude, 5);
                var obs = callTask.ToObservable();
                obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
            }
        }

        public void DrawPatrols(DrawPatrolsMessage PatrolsMessage)
        {
            var callTask = _client.GetNearByPatrolsByLatLonAsync(PatrolsMessage.Longitude, PatrolsMessage.Latitude, 5);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_NearByPatrolsForFogEvent(x == null ? null : x.ToList()));
        }

        private void Add_NearByPatrolsForFogEvent(List<PatrolLastLocationDTO> PatrolsList)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            foreach (var item in PatrolsList)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.PatrolId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Patrols), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Patrols, item))));
                }
            }
        }

        private void Add_NearByCamerasForFogEvent(List<AssetsViewDTO> list, FogEventMessage fogEventMessage)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
                }
            }
        }
        private void Add_NearByRadarsForFogEvent(List<AssetsViewDTO> list, FogEventMessage fogEventMessage)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);
            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
                }
            }
        }
        private void Add_NearByTowersForFogEvent(List<AssetsViewDTO> list, long SourceFogTowerId)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var sourceFog = list.FirstOrDefault(x => x.ItemId == SourceFogTowerId);
            if (sourceFog != null)
                list.Remove(sourceFog);

            foreach (var item in list)
            {
                var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == item.ItemId.ToString());
                if (graphic == null)
                {
                    Application.Current.Dispatcher.Invoke(() => graphicCol.Add(CreateGraphic(CreateGraphicDictionary(item, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), item.Latitude.Value, item.Longitude.Value, GetMarkerImageUrl(MarkerType.Assets, item))));
                }
            }
        }



        public void ClearHistoricalViolationsObjects()
        {
            var graphic = LayersGraphicsDictionary[LayerTypeEnum.HistoricalViolations.ToString()];
            graphic = new ObservableCollection<Graphic>();
            HioricalViolationList = new ObservableCollection<Graphic>();
            //var layerCol = GetLayerObservable(LayerTypeEnum.HistoricalViolations);
            //ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.HistoricalViolations);

            ////TODO: Edit here
            //for (int i = 0; i < graphicCol.Count; i++)
            //{
            //    var graphic = graphicCol[0];

            //    //if (graphic != null && graphic.Attributes.ContainsKey("Id") && (graphic.Attributes["Id"].ToString().Contains("_FogCenter") || graphic.Attributes["Id"].ToString().Contains("_Circle_")))
            //    //{

            //    //    continue;
            //    //}

            //    Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            //}
        }


        public void ClearViolationsObjects()
        {
            var graphic = LayersGraphicsDictionary[LayerTypeEnum.Violations.ToString()];
            graphic = new ObservableCollection<Graphic>();

        }

        public void ClearSOPObjects()
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var tmpColl = graphicCol.Where(graphic => graphic.Attributes.ContainsKey("Id") && !graphic.Attributes["Id"].ToString().Contains("_FogCenter") && !graphic.Attributes["Id"].ToString().Contains("_Circle_")).ToList();

            var count = tmpColl.Count;

            //TODO: Edit here
            for (int i = 0; i < count; i++)
            {
                var graphic = tmpColl[0];

                if (graphic != null && graphic.Attributes.ContainsKey("Id") && (graphic.Attributes["Id"].ToString().Contains("_FogCenter") || graphic.Attributes["Id"].ToString().Contains("_Circle_")))
                {

                    continue;
                }

                Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            }

            //foreach (var graphic in graphicCol)
            //{

            //    if (graphic.Attributes.ContainsKey("Id") && graphic.Attributes["Id"].ToString().Contains("_FogCenter") && graphic.Attributes["LayerType"].ToString() == LayerTypeEnum.Assets.ToString())
            //    {
            //        continue;
            //    }

            //    Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));
            //}

        }

        public void DrawSOPMapObject(SOPMapDraw SOPMapDrawObject)
        {

            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var assetTemp = SOPMapDrawObject.ObjectToDraw as dynamic;

            var asset = new AssetsViewDTO
            {
                ItemCategoryName = assetTemp.ItemCategoryName,
                ItemCategoryId = assetTemp.ItemCategoryId,
                ItemId = assetTemp.ItemId,
                ItemImage = assetTemp.ItemImage,
                ItemName = assetTemp.ItemName,
                ItemStatusId = assetTemp.ItemStatusId,
                Latitude = assetTemp.Latitude,
                ItemStatusName = assetTemp.ItemStatusName,
                LocationCode = assetTemp.LocationCode,
                Longitude = assetTemp.Longitude,
                OriginalIdent = assetTemp.OriginalIdent,
                SerialNo = assetTemp.SerialNo
            };

            var graphic = layerCol.FirstOrDefault(x => x.Attributes["Id"].ToString() == asset.ItemId.ToString() && x.Attributes["LayerType"].ToString() == LayerTypeEnum.Notifications.ToString());

            if (graphic == null)
                graphicCol.Add(CreateGraphic(CreateGraphicDictionary(asset, LayerTypeEnum.Notifications, null, LayerTypeEnum.Assets), SOPMapDrawObject.Lat, SOPMapDrawObject.Lon, GetMarkerImageUrl(MarkerType.Assets, asset)));

        }

        public void ClearNotificationsLayer()
        {

            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            ObservableCollection<Graphic> graphicCol = GetLayerObservable(LayerTypeEnum.Notifications);

            var count = graphicCol.Count;

            for (int i = 0; i < count; i++)
            {

                var graphic = graphicCol[0];

                Application.Current.Dispatcher.Invoke(() => layerCol.Remove(graphic));

            }

        }

        #endregion

        #region MarkersDetailsToolTip
        public void GetVehicleDetails(string plateNumber)
        {
            var callTask = _client.GetVehicleDetailsByPlateNumberAsync(plateNumber);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_VehicleDetails(x));
        }
        private void Add_VehicleDetails(VehicleLiveTrackingDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => VehicleDetail = data);
        }
        public void GetIncidentDetails(int selectedIncidentId)
        {
            var callTask = _client.GetIncidentFullDetailsAsync(selectedIncidentId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_IncidentDetails(x));
        }
        private void Add_IncidentDetails(IncidentDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => IncidentsToolTipDetailsOnGraphic = data);
        }
        public void GetViolationDetails(int selectedLocationCode)
        {
            int? violationTypeId = null;
            if (SelectedViolationType != null)
                if (((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId == 0)
                    violationTypeId = null;

            var callTask = _client.GetViolationDetailsByAssetAsync(selectedLocationCode.ToString(), (DateTime?)SelectedViolationDateFilter, violationTypeId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationDetails(x));
        }
        private void Add_ViolationDetails(ViolationDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => AddViolationTotalsbyStatus(data.TotalsByStatus));
            Application.Current.Dispatcher.Invoke(() => AddViolationTotalsbyType(data.TotalsByTypes));
            Application.Current.Dispatcher.Invoke(() => AssetDetailsForViolation = data.AssetsDetails);
        }

        private void AddViolationTotalsbyType(TotalViolationValuesByTypes[] totalViolationValuesByTypes)
        {
            ViolationsByTypeData.Clear();
            this.RaiseNotifyPropertyChanged("ViolationsByTypeData");
            if (totalViolationValuesByTypes == null)
                return;
            for (int i = 0; i < totalViolationValuesByTypes.Length; i++)
            {
                ViolationsByTypeData.Add(new NameValueModel() { Name = totalViolationValuesByTypes[i].VioltionTypeName, Value = totalViolationValuesByTypes[i].TotalCountOfViolations });
            }
        }

        private void AddViolationTotalsbyStatus(TotalViolationValuesByStatus[] totalViolationValuesByStatus)
        {
            ViolationsByStatusData.Clear();
            this.RaiseNotifyPropertyChanged("ViolationsByStatusData");
            if (totalViolationValuesByStatus == null)
                return;
            for (int i = 0; i < totalViolationValuesByStatus.Length; i++)
            {
                ViolationsByStatusData.Add(new NameValueModel() { Name = totalViolationValuesByStatus[i].VioltionStatusName, Value = totalViolationValuesByStatus[i].TotalCountOfViolations });
            }
        }

        public void GetPatrolDetails(int patrolId)
        {
            var callTask = _client.GetPatrolDetailsAsync(patrolId);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolDetails(x));
        }

        private void Add_PatrolDetails(PatrolFullDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() => PatrolToolTipDetailsOnGraphic = data);
        }

        public void GetOfficerTipDetails(int officerId)
        {
            if (Officers != null && Officers.Any(x => x.OfficerId == officerId))
                Officer = Officers.FirstOrDefault(x => x.OfficerId == officerId);
        }



        #endregion

        #region Fill Dropdowns
        private void GetPatrolStatusList()
        {
            var callTask = _client.GetPatrolStatusListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_PatrolStatusList(x == null ? null : x.ToList()));
        }
        private void Add_PatrolStatusList(List<PatrolStatusDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new PatrolStatusDimDTO() { PatrolStatus = "All", PatrolStatusId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => PatrolStatusList.Add(item));
        }
        private void GetAllUsersList()
        {
            var callTask = _client.GetAllUsersListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_UsersList(x == null ? null : x.ToList()));
        }
        private void Add_UsersList(List<UsersDTO> list)
        {
            if (list == null)
                return;
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AllUsers.Add(item));
        }
        private void GetAssetTypesList()
        {
            var callTask = _client.GetAssetTypesListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetTypesList(x == null ? null : x.ToList()));
        }
        private void Add_AssetTypesList(List<AssetTypeDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new AssetTypeDimDTO() { AssetType = "All", AssetTypeId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AssetTypesList.Add(item));
        }
        private void GetAssetStatusDimDTOList()
        {
            var callTask = _client.GetAssetStatusListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AssetStatusDimDTOList(x == null ? null : x.ToList()));
        }
        private void Add_AssetStatusDimDTOList(List<AssetStatusDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new AssetStatusDimDTO() { AssetStatus = "All", AssetStatusId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => AssetStatusList.Add(item));
        }
        private void GetViolationTypeDimDTOsList()
        {
            var callTask = _client.GetViolationTypesListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationTypeDimDTOsList(x == null ? null : x.ToList()));
        }
        private void Add_ViolationTypeDimDTOsList(List<ViolationTypeDimDTO> list)
        {
            if (list == null)
                return;
            list.Insert(0, new ViolationTypeDimDTO() { ViolationType = "All", ViolationTypeId = 0 });
            foreach (var item in list)
                Application.Current.Dispatcher.Invoke(() => ViolationTypesList.Add(item));
        }

        #endregion

        #region Filters
        private void FilterAssetsOnMap(object parameter)
        {
            int? assetStatus = null;
            int? assetType = null;

            if (SelectedAssetStatus != null && ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId != 0)
                assetStatus = ((AssetStatusDimDTO)SelectedAssetStatus).AssetStatusId;
            if (SelectedAssetType != null && ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId != 0)
                assetType = ((AssetTypeDimDTO)SelectedAssetType).AssetTypeId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Assets).Clear());
            GetAssetLastStatusData(assetStatus, assetType);
        }


        private void FilterViolationsOnMap(object parameter)
        {
            int? violationType = null;
            if (SelectedViolationType != null && ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId != 0)
                violationType = ((ViolationTypeDimDTO)SelectedViolationType).ViolationTypeId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Violations).Clear());
            GetViolationsData(SelectedViolationDateFilter == null ? (DateTime?)null : (DateTime?)SelectedViolationDateFilter, violationType);
        }
        private void FilterPatrolsOnMap(object parameter)
        {
            int? patrolStatus = null;
            if (SelectedPatrolStatus != null && ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId != 0)
                patrolStatus = ((PatrolStatusDimDTO)SelectedPatrolStatus).PatrolStatusId;

            Application.Current.Dispatcher.Invoke(() => GetLayerObservable(LayerTypeEnum.Patrols).Clear());
            GetPatrolLastLocationDTOData(patrolStatus);
        }
        #endregion

        #region HelperFunctions
        public ObservableCollection<Graphic> GetLayerObservable(LayerTypeEnum layerType)
        {
            ObservableCollection<Graphic> graphicCol = null;
            if (LayersGraphicsDictionary.ContainsKey(layerType.ToString()))
                graphicCol = LayersGraphicsDictionary[layerType.ToString()];
            else
            {
                graphicCol = new ObservableCollection<Graphic>();
                LayersGraphicsDictionary.Add(layerType.ToString(), graphicCol);
            }
            return graphicCol;
        }
        private Graphic CreateGraphic(Dictionary<string, object> attributes, double latitude, double longitude, string iconPath, int? countSymbol = 0)
        {
            CompositeSymbol compositeSymbol = new CompositeSymbol();
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol();
            TextSymbol textSymbol = new TextSymbol();

            if (!string.IsNullOrEmpty(iconPath))
            {
                pictureMarkerSymbol.SetSourceAsync(new Uri(iconPath));
                compositeSymbol.Symbols.Add(pictureMarkerSymbol);
            }


            if (countSymbol.HasValue && countSymbol != 0)
            {
                textSymbol.Text = countSymbol.ToString();
                textSymbol.HorizontalTextAlignment = HorizontalTextAlignment.Center;
                textSymbol.VerticalTextAlignment = VerticalTextAlignment.Middle;
                textSymbol.Font.FontSize = 14;
                textSymbol.Font.FontWeight = SymbolFontWeight.Bold;
                textSymbol.YOffset = 5;
                textSymbol.Color = (Color)ColorConverter.ConvertFromString("#181818");
                //textSymbol.Color = (Color)ColorConverter.ConvertFromString("#d91e18");
                compositeSymbol.Symbols.Add(textSymbol);
            }

            var graphics = new Graphic(new MapPoint(longitude, latitude, new SpatialReference(4326)), compositeSymbol);
            foreach (KeyValuePair<string, object> item in attributes)
                graphics.Attributes.Add(item.Key, item.Value);
            graphics.ZIndex = MARKER_Z_INDEX;
            return graphics;
        }
        private Dictionary<string, object> CreateGraphicDictionary(dynamic item, LayerTypeEnum layerType, int? violationCount = 0, LayerTypeEnum? targetNotificatoinLayerType = null)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            switch (layerType)
            {
                case LayerTypeEnum.HistoricalViolations:
                case LayerTypeEnum.Violations:
                    attributes.Add("Id", item.LocationCode);
                    attributes.Add("Count", violationCount);

                    if (item is ViolationNotificationDTO)
                        attributes.Add("ViolationObj", item);
                    else if (item is ViolationToDraw)
                        attributes.Add("ViolationObj", item.ViolationObj);
                    else if (item is ViolationsGroupedByLocationsDTO)
                        attributes.Add("ViolationObj", item);
                    break;
                case LayerTypeEnum.Incidents:
                    attributes.Add("Id", item.IncidentId);
                    attributes.Add("Name", item.IncidentTypeName);
                    attributes.Add("Address", item.IncidentAddress);

                    if (item is IncidentsDTO)
                        attributes.Add("IncidentObj", item);
                    else if (item is IncidentToDraw)
                        attributes.Add("IncidentObj", item.IncidentObj);
                    break;
                case LayerTypeEnum.Patrols:
                    attributes.Add("Id", item.PatrolId);
                    attributes.Add("Speed", item.Speed);
                    attributes.Add("Date", item.LocationDate);
                    attributes.Add("Name", item.PatrolCode);
                    break;
                case LayerTypeEnum.Officers:
                    attributes.Add("Id", item.OfficerId);
                    attributes.Add("Speed", item.Speed);
                    attributes.Add("Date", item.LocationDate);
                    attributes.Add("Name", item.OfficerName);
                    break;
                case LayerTypeEnum.Assets:
                    attributes.Add("Id", item.OriginalIdent);
                    attributes.Add("Name", item.ItemName);
                    attributes.Add("Status", item.ItemStatusName);
                    attributes.Add("LocationCode", item.LocationCode);
                    attributes.Add("AssetsType", item.ItemCategoryName);
                    break;
                case LayerTypeEnum.Notifications:
                    if (item is AssetsViewDTO)
                    {
                        attributes.Add("Id", item.ItemId);
                        attributes.Add("LocationCode", item.LocationCode);
                    }
                    else if (item is FogEventMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_FogCenter");
                    }
                    else if (item is WantedCarMessage)
                    {
                        attributes.Add("Id", item.VehiclePlateNumber + "_WantedCarCenter");
                    }
                    else if (item is DetectedAccidentMessage)
                    {
                        attributes.Add("Id", item.TowerId + "_AccidentCenter");
                        attributes.Add("DetectedEventMessageObj", item);
                    }
                    else if (item is PatrolLastLocationDTO)
                    {
                        attributes.Add("Id", item.PatrolId + "_PatrolCenter");
                    }
                    else
                    {
                        attributes.Add("Id", item.Id);
                    }

                    attributes.Add("LayerType", targetNotificatoinLayerType.Value.ToString());

                    if (item is ViolationToDraw)
                        attributes.Add("ViolationObj", item.ViolationObj);
                    else if (item is IncidentToDraw)
                        attributes.Add("IncidentObj", item.IncidentObj);
                    else if (item is FogEventMessage)
                        attributes.Add("FogEventMessageObj", item);
                    else if (item is WantedCarMessage)
                        attributes.Add("WantedCarMessageObj", item);
                    break;
            }
            return attributes;
        }
        private void DrawCircle(string id, MapPoint point, double radius, SimpleFillSymbol circleFillSymbol, int ZIndex, LayerTypeEnum layerType, Color color)
        {
            var layerCol = GetLayerObservable(LayerTypeEnum.Notifications);
            var bufferGraphic = layerCol.Where(x => x.Attributes.Count > 0 && x.Attributes.ContainsKey("Id") && x.Attributes["Id"].ToString() == (id + "_Circle_" + ZIndex).ToString()).FirstOrDefault();
            if (bufferGraphic != null)
                layerCol.Remove(bufferGraphic);

            var buffer = GeometryEngine.Buffer(point, radius * 0.0175);
            bufferGraphic = new Graphic { Geometry = buffer, Symbol = circleFillSymbol, ZIndex = ZIndex };

            //bufferGraphic = DrawCircle(point, radius * 0.0175, color);
            bufferGraphic.Attributes.Add("Id", id + "_Circle_" + ZIndex);
            bufferGraphic.Attributes.Add("LayerType", layerType.ToString());
            layerCol.Add(bufferGraphic);
        }

        public Graphic DrawCircle(MapPoint mapPoint, double radius, Color color)//GraphicsLayer myGraphicLayer, 
        {
            //myGraphicLayer.Graphics.Clear();
            //symbole that make a outline polygon circle. 
            SimpleLineSymbol symbolLine = new SimpleLineSymbol();
            symbolLine.Width = 1;
            symbolLine.Color = color;
            //symbole that make a polygon properties.
            SimpleFillSymbol myFillSymbol = new SimpleFillSymbol();
            myFillSymbol.Color = color;
            myFillSymbol.Outline = symbolLine;
            myFillSymbol.Style = SimpleFillStyle.Vertical;

            List<MapPoint> pointCollection = new List<MapPoint>();
            // point that make up the circle
            //MapPoint pp = new MapPoint(55.00206, 24.98167);
            var PointCount = 100; // number of points on the circle
            var angle = 360 / PointCount; // used to compute points on the circle

            for (var i = 1; i <= PointCount; i++)
            {
                // convert angle to raidans
                // var radians = i * angle * Math.PI / 180;
                double radians = 2 * Math.PI / PointCount * i;
                // add point to the circle
                double x = (mapPoint.X + radius * Math.Cos(radians));
                double y = (mapPoint.Y + radius * Math.Sin(radians));
                pointCollection.Add(new MapPoint(x, y));
            }

            Polygon myPolygon = new Polygon(pointCollection, SpatialReferences.Wgs84);
            Graphic myGraphic = new Graphic();
            myGraphic.Geometry = myPolygon;
            myGraphic.Symbol = myFillSymbol;
            //myGraphicLayer.Graphics.Add(myGraphic);

            return myGraphic;
        }

        private string GetMarkerImageUrl(MarkerType MarkerType, object MarkerObject, int threashould = 0)
        {
            //@"pack://application:,,,/STC.Projects.WPFControlLibrary.MapControl;component/" 
            switch (MarkerType)
            {
                case MarkerType.Officers:
                    {
                        #region Officers
                        var officerLocation = MarkerObject as OfficersLastLocationViewDTO;

                        if (officerLocation != null)
                        {
                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.SmartOfficerMarker;

                        }
                        #endregion
                    }
                    break;
                case MarkerType.Patrols:
                    {
                        #region Patrols

                        var patrolLocation = MarkerObject as PatrolLastLocationDTO;

                        if (patrolLocation != null)
                        {

                            var patrolStatus = (PatrolStatusEnum)patrolLocation.StatusId.Value;

                            switch (patrolStatus)
                            {
                                case PatrolStatusEnum.Acknowledged:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolAcknowledgedMarker;

                                case PatrolStatusEnum.ArrivedToLocation:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolArrivedMarker;

                                case PatrolStatusEnum.AssignedToEvent:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolAssignedMarker;

                                case PatrolStatusEnum.Available:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolAvailableMarker;

                                case PatrolStatusEnum.Dispatched:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolDispatchedMarker;

                                case PatrolStatusEnum.OnTheWay:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolOnTheWayMarker;

                                case PatrolStatusEnum.UnderMaintenance:
                                    return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.PatrolUnderMaintenanceMarker;

                            }

                        }

                        #endregion
                    }
                    break;

                case MarkerType.Assets:
                    {
                        #region Assets

                        var assetLastStatus = MarkerObject as AssetLastStatusDTO;

                        if (assetLastStatus != null)
                        {

                            var assetStatus = (AssetStatusEnum)assetLastStatus.AssetStatusId;

                            var assetType = (AssetTypesEnum)assetLastStatus.AssetTypeId;

                            switch (assetType)
                            {
                                case AssetTypesEnum.DOTcounters:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcounters;
                                //}
                                //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                //}
                                //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadars;
                                    }
                                    break;
                            }

                        }

                        var assetsView = MarkerObject as AssetsViewDTO;

                        if (assetsView != null)
                        {

                            var assetStatus = (AssetStatusEnum)assetsView.ItemStatusId;

                            var assetType = (AssetTypesEnum)assetsView.ItemCategoryId;

                            switch (assetType)
                            {
                                case AssetTypesEnum.DOTcounters:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcountersUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerDOTcounters;
                                //}
                                //break;

                                case AssetTypesEnum.EkinRedLightCamera:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCameraUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerEkinRedLightCamera;
                                    }
                                    break;

                                case AssetTypesEnum.SmartTowers:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowersUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSmartTowers;
                                    }
                                    break;

                                case AssetTypesEnum.SpeedGuns:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGunsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerSpeedGuns;
                                    }
                                    break;

                                case AssetTypesEnum.VitronicMobileRadars:
                                    return "";
                                //switch (assetStatus)
                                //{
                                //    case AssetStatusEnum.NotWorking:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsDisabled;

                                //    case AssetStatusEnum.UnderMaintenance:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadarsUnderMaintenance;

                                //    case AssetStatusEnum.Working:
                                //        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicMobileRadars;
                                //}
                                //break;

                                case AssetTypesEnum.VitronicRadars:
                                    switch (assetStatus)
                                    {
                                        case AssetStatusEnum.NotWorking:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsDisabled;

                                        case AssetStatusEnum.UnderMaintenance:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadarsUnderMaintenance;

                                        case AssetStatusEnum.Working:
                                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.AssetMarkerVitronicRadars;
                                    }
                                    break;
                            }

                        }

                        #endregion
                    }
                    break;

                case MarkerType.Incident:
                    {
                        #region Incident

                        var newIncident = MarkerObject as IncidentsDTO;

                        var incidentType = (IncidentTypeEnum)newIncident.IncidentTypeId;

                        switch (incidentType)
                        {
                            case IncidentTypeEnum.HighPriorityIncident:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerMed;
                        }

                        #endregion
                    }
                    break;

                case MarkerType.IncidentNotification:
                    {
                        #region IncidentNotification

                        var newIncident = MarkerObject as IncidentToDraw;

                        var incidentType = (IncidentTypeEnum)newIncident.IncidentTypeId;

                        switch (incidentType)
                        {
                            case IncidentTypeEnum.HighPriorityIncident:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;

                            case IncidentTypeEnum.Low:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerLow;

                            case IncidentTypeEnum.Medium:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerMed;
                        }

                        #endregion
                    }
                    break;

                case MarkerType.Violation:
                    {
                        if (MarkerObject is ViolationsGroupedByLocationsDTO)
                        {
                            if (_periodType == 0)
                                threashould = 20;
                            if (((ViolationsGroupedByLocationsDTO)MarkerObject).ViolationsCount >= threashould)
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationCountHigh;
                            else if (((ViolationsGroupedByLocationsDTO)MarkerObject).ViolationsCount >= (threashould - ((threashould * 10) / 100)))
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationCountLow;
                            else if (((ViolationsGroupedByLocationsDTO)MarkerObject).ViolationsCount > 0)
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationCountNormal;
                        }
                        else
                            return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarker;
                        break;
                    }

                case MarkerType.ViolationsNotifications:
                    {
                        #region ViolationsNotifications

                        var newViolation = MarkerObject as ViolationToDraw;

                        var violationType = (ViolationTypesEnum)newViolation.ViolationTypeId;

                        switch (violationType)
                        {
                            case ViolationTypesEnum.DirectionReversing:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerDirectionReversing;

                            case ViolationTypesEnum.P2P:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerP2P;

                            case ViolationTypesEnum.RedLight:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerRedLight;

                            case ViolationTypesEnum.RouteDeviation:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerRouteDeviation;

                            case ViolationTypesEnum.ShoulderSpeeding:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerShoulderSpeeding;

                            case ViolationTypesEnum.Speed:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerSpeed;

                            case ViolationTypesEnum.Tailgating:
                                return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.ViolationMarkerTailgating;
                        }

                        #endregion
                    }
                    break;
                case MarkerType.Fog:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.FogTowerMarker;
                    }
                case MarkerType.DetectedAccident:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.IncidentMarkerHigh;
                    }
                case MarkerType.WantedCar:
                    {
                        return @"pack://application:,,,/STC.Projects.ClassLibrary.Common;component/" + ImagesResx.WantedCarIcon;
                    }
            }

            return "";
        }
        #endregion

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
