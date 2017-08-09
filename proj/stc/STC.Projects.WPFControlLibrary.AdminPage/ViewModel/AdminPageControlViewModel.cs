using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System.ComponentModel;

namespace STC.Projects.WPFControlLibrary.AdminPage.ViewModel
{
    public class AdminPageControlViewModel : System.ComponentModel.INotifyPropertyChanged, IDataErrorInfo
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate RoleAdded;
        public AdminPageControlViewModel()
        {
            PriorityTypesList = new ObservableCollection<BusinessRulePriorityDTO>();
            OverSpeedList = new ObservableCollection<OverSpeedDTO>();
            TrafficCrossList = new ObservableCollection<TrafficCrossDTO>();
            VehicleTypesList = new ObservableCollection<VehicleTypeDTO>();
            _client = new ServiceLayerClient();
            AddBusinessRuleCommand = new Command(SaveBusinessRule);
            IsAddGridEnabled = true;

            NumOfViolnsInCity = 1;
            NumOfViolnsOutCity = 1;
            RedLgtCrosngNumOfViolnsValue = 1;
            RuleIntTimeValue = 1;

            LoadData();


        }

        private void LoadData()
        {
            LoadPriorties();
            LoadOverSpeed();
            LoadVehicleTypes();
            LoadTrafficCross();
        }

        private void LoadPriorties()
        {
            var list = _client.GetAllPriorities();
            if (list == null)
                return;
            foreach (var item in list)
            {
                PriorityTypesList.Add(item);
            }
        }

        private void LoadVehicleTypes()
        {
            var list = _client.GetAllVehicleTypes();
            if (list == null)
                return;
            VehicleTypesList.Add(new VehicleTypeDTO() { VehicleTypeEn = "Any", VehicleTypeAr = "اختر الكل" });
            this.SelectedIndexVehicleType = 0;
            foreach (var item in list)
            {
                if (item.VehicleTypeEn == "Car" || item.VehicleTypeEn == "Truck")
                    VehicleTypesList.Add(item);
            }

        }

        private void LoadOverSpeed()
        {
            var list = _client.GetAllOverSpeed();
            if (list == null)
                return;
            foreach (var item in list)
            {
                OverSpeedList.Add(item);
            }
        }

        private void LoadTrafficCross()
        {
            var list = _client.GetAllTrafficCrossTimes();
            if (list == null)
                return;
            foreach (var item in list)
            {
                TrafficCrossList.Add(item);
            }
        }

        private void SaveBusinessRule(object parameter)
        {
            if (IsAddGridEnabled)
            {
                IsAddGridEnabled = false;
                BusinessRulesDTO businessRule = new BusinessRulesDTO
                {
                    BusinessName = BusinessRuleName,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1,
                    InsideCityOverSpeedQty = NumOfViolnsInCity,
                    IsOverSpeedInsideCity = SpeedInsideCityAdded,
                    IsOverSpeedOutsideCity = SpeedOutsideCityAdded,
                    IsTrafficCross = RedLgtCrosdAdded,
                    OutsideCityOverSpeedQty = NumOfViolnsOutCity,
                    RuleInterval = RuleIntTimeValue,
                    TrafficCrossQty = RedLgtCrosngNumOfViolnsValue,
                    IsDeleted = IsRuleDisabled,
                };

                if (RulePriority != null)
                    businessRule.PriorityId = RulePriority.PriorityID;
                if (SpeedInsideCityAdded && SpeedThshldInCity != null)
                    businessRule.InsideCityOverSpeedId = SpeedThshldInCity.OverSpeedId;
                if (SpeedOutsideCityAdded && SpeedThshldOutCity != null)
                    businessRule.OutsideCityOverSpeedId = SpeedThshldOutCity.OverSpeedId;
                if (RedLgtCrosdAdded && RedLgtCrosngSecondsValue != null)
                    businessRule.TrafficCrossTimesId = RedLgtCrosngSecondsValue.TrafficCrossId;
                if (VehicleType != null && VehicleType.VehicleTypeEn != "Any")
                    businessRule.VehicleTypeId = VehicleType.VehicleTypeID;


                var res = _client.SaveBusinessRuleAsync(businessRule);
                res.ContinueWith(x => AddBusinessRuleResult(x.Result));
            }
        }

        public void AddBusinessRuleResult(bool result)
        {
            BusinessRuleName = "";
            RulePriority = null;
            if (VehicleTypesList != null && VehicleTypesList.Count > 0)
                VehicleType = VehicleTypesList[0];
            SpeedInsideCityAdded = false;
            SpeedThshldInCity = null;
            NumOfViolnsInCity = 1;
            SpeedOutsideCityAdded = false;
            SpeedThshldOutCity = null;
            NumOfViolnsOutCity = 1;
            RedLgtCrosdAdded = false;
            RedLgtCrosdValue = 1;
            //RedLgtCrosngTimeAdded = false;
            RedLgtCrosngSecondsValue = null;
            RedLgtCrosngNumOfViolnsValue = 1;
            RuleIntTimeValue = 1;
            /// inform the user that the rule has been added
            var handler = RoleAdded;
            if (handler != null)
                handler(this, new EventArgs());
            IsAddGridEnabled = true;
        }

        private void UpdateBusinessRuleExecute()
        {

        }

        private void GoBackExecute()
        {
            string closeMsg = Utility.GetLang() == "en" ? "Are you sure, want to close the Add Business Rule Screen?" : "سيتم الغاء أي تعديلات, هل انت متأكد؟";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            //closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                AddBusinessRuleResult(true);
                //this.Visibility = Visibility.Collapsed;
                //Window.GetWindow(this).Close();
            }
        }

        #region privateVariable

        private ServiceReference1.ServiceLayerClient _client;
        private string _businessRuleName;
        private BusinessRulePriorityDTO _rulePriority;
        private VehicleTypeDTO _vehicleType;

        private bool _speedInsideCityAdded;
        private OverSpeedDTO _speedThshldInCity;
        private int _numOfViolnsInCity;

        private bool _speedOutsideCityAdded;
        private OverSpeedDTO _speedThshldOutCity;
        private int _numOfViolnsOutCity;

        private bool _redLgtCrosdAdded;
        private int _redLgtCrosdValue;

        private bool _redLgtCrosngTimeAdded;
        private TrafficCrossDTO _redLgtCrosngSecondsValue;
        private int _redLgtCrosngNumOfViolnsValue;

        private string _ruleIntTimeCategory;
        private int _ruleIntTimeValue;

        private bool _isRuleDisabled;
        #endregion

        #region Public Properties

        public String BusinessRuleName
        {
            get { return _businessRuleName; }
            set { _businessRuleName = value; this.RaiseNotifyPropertyChanged(); }
        }

        public BusinessRulePriorityDTO RulePriority
        {
            get { return _rulePriority; }
            set { _rulePriority = value; this.RaiseNotifyPropertyChanged(); }
        }

        public VehicleTypeDTO VehicleType
        {
            get { return _vehicleType; }
            set { _vehicleType = value; this.RaiseNotifyPropertyChanged(); }
        }


        private int _selectedIndexVehicleType;

        public int SelectedIndexVehicleType
        {
            get
            {
                if (VehicleTypesList != null && VehicleTypesList.Count > 0 && _selectedIndexVehicleType == -1)
                    _selectedIndexVehicleType = 0;
                return _selectedIndexVehicleType;
            }
            set { _selectedIndexVehicleType = value; this.RaiseNotifyPropertyChanged(); }
        }
        public Boolean SpeedInsideCityAdded
        {
            get { return _speedInsideCityAdded; }
            set
            {
                _speedInsideCityAdded = value;
                if (value)
                {
                    if (SpeedThshldInCity == null && OverSpeedList != null && OverSpeedList.Count > 0)
                        SpeedThshldInCity = OverSpeedList[0];
                }
                else
                    SpeedThshldInCity = null;


                this.RaiseNotifyPropertyChanged();
            }
        }


        public OverSpeedDTO SpeedThshldInCity
        {
            get
            {
                //if (_speedInsideCityAdded == true && _speedThshldInCity == null && OverSpeedList != null && OverSpeedList.Count > 0)
                //    _speedThshldInCity = OverSpeedList[0];
                return _speedThshldInCity;
            }
            set { _speedThshldInCity = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 NumOfViolnsInCity
        {
            get { return _numOfViolnsInCity; }
            set
            {
                //if (value > 0)
                _numOfViolnsInCity = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        public Boolean SpeedOutsideCityAdded
        {
            get { return _speedOutsideCityAdded; }
            set
            {
                _speedOutsideCityAdded = value;
                if (value)
                {
                    if (SpeedThshldOutCity == null && OverSpeedList != null && OverSpeedList.Count > 0)
                        SpeedThshldOutCity = OverSpeedList[0];
                }
                else
                    SpeedThshldOutCity = null;
                this.RaiseNotifyPropertyChanged();
            }
        }



        public OverSpeedDTO SpeedThshldOutCity
        {
            get
            {
                //if (_speedOutsideCityAdded == true && _speedThshldOutCity == null && OverSpeedList != null && OverSpeedList.Count > 0)
                //    _speedThshldOutCity = OverSpeedList[0];
                return _speedThshldOutCity;
            }
            set { _speedThshldOutCity = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 NumOfViolnsOutCity
        {
            get { return _numOfViolnsOutCity; }
            set
            {
                //if (value > 0)
                _numOfViolnsOutCity = value;
                this.RaiseNotifyPropertyChanged();
            }
        }




        public Boolean RedLgtCrosdAdded
        {
            get { return _redLgtCrosdAdded; }
            set
            {
                _redLgtCrosdAdded = value;

                if (value)
                {
                    if (RedLgtCrosngSecondsValue == null && TrafficCrossList != null && TrafficCrossList.Count > 0)
                        RedLgtCrosngSecondsValue = TrafficCrossList[0];
                }
                else
                    RedLgtCrosngSecondsValue = null;

                this.RaiseNotifyPropertyChanged();
            }
        }


        public Int32 RedLgtCrosdValue
        {
            get { return _redLgtCrosdValue; }
            set
            {
                //if (value > 0)
                _redLgtCrosdValue = value;
                this.RaiseNotifyPropertyChanged();
            }
        }



        public Boolean RedLgtCrosngTimeAdded
        {
            get { return _redLgtCrosngTimeAdded; }
            set
            {
                _redLgtCrosngTimeAdded = value;
                if (value)
                {
                    if (RedLgtCrosngSecondsValue == null && TrafficCrossList != null && TrafficCrossList.Count > 0)
                        RedLgtCrosngSecondsValue = TrafficCrossList[0];
                }
                else
                    RedLgtCrosngSecondsValue = null;

                this.RaiseNotifyPropertyChanged();
            }
        }

        public TrafficCrossDTO RedLgtCrosngSecondsValue
        {
            get { return _redLgtCrosngSecondsValue; }
            set { _redLgtCrosngSecondsValue = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 RedLgtCrosngNumOfViolnsValue
        {
            get { return _redLgtCrosngNumOfViolnsValue; }
            set
            {
                //if (value > 0)
                _redLgtCrosngNumOfViolnsValue = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        public String RuleIntTimeCategory
        {
            get { return _ruleIntTimeCategory; }
            set { _ruleIntTimeCategory = value; this.RaiseNotifyPropertyChanged(); }
        }

        public int RuleIntTimeValue
        {
            get { return _ruleIntTimeValue; }
            set
            {
                //if (value > 0)
                _ruleIntTimeValue = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        public Boolean IsRuleDisabled
        {
            get { return _isRuleDisabled; }
            set { _isRuleDisabled = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _isAddGridEnabled;

        public bool IsAddGridEnabled
        {
            get { return _isAddGridEnabled; }
            set
            {
                _isAddGridEnabled = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        public ObservableCollection<VehicleTypeDTO> VehicleTypesList { get; set; }
        public ObservableCollection<OverSpeedDTO> OverSpeedList { get; set; }
        public ObservableCollection<TrafficCrossDTO> TrafficCrossList { get; set; }
        public ObservableCollection<BusinessRulePriorityDTO> PriorityTypesList { get; set; }

        #endregion
        public Command AddBusinessRuleCommand { get; set; }

        public Command ClickedCellCommand { get { return new Command((UpdateBusinessRuleExecute)); } }
        public Command GoBackCommand { get { return new Command((GoBackExecute)); } }



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
                    //case "BusinessRuleName":
                    //    {
                    //        if (string.IsNullOrWhiteSpace(BusinessRuleName))
                    //        {
                    //            result = "Rule Name cannot be empty.";
                    //        }
                    //        break;
                    //    }

                    case "NumOfViolnsInCity":
                        {
                            if (NumOfViolnsInCity <= 0)
                            {
                                result = Utility.GetLang() == "ar" ? "0 اللائحة يجب ان تزيد عن " : "Number of Violations should be greater than 0.";
                            }
                            break;
                        }

                    case "NumOfViolnsOutCity":
                        {
                            if (NumOfViolnsOutCity <= 0)
                            {
                                result = Utility.GetLang() == "ar" ? "0 اللائحة يجب ان تزيد عن " : "Number of Violations should be greater than 0.";
                            }
                            break;
                        }

                    case "RedLgtCrosngNumOfViolnsValue":
                        {
                            if (RedLgtCrosngNumOfViolnsValue <= 0)
                            {
                                result = Utility.GetLang() == "ar" ? "0 اللائحة يجب ان تزيد عن " : "Number of Violations should be greater than 0.";
                            }
                            break;
                        }
                    case "RuleIntTimeValue":
                        {
                            if (RuleIntTimeValue <= 0)
                            {
                                result = Utility.GetLang() == "ar" ? "عدد المخالفات يجب ان يزيد عن 0" : "Rule Interval time should be greater than 0.";
                            }
                            break;
                        }



                }

                return result;
            }
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

