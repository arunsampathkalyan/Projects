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

namespace STC.Projects.WPFControlLibrary.AdminPage.ViewModel
{
    public class UpdateAdminPageControlViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate RoleUpdated;

        private BusinessRulesDTO _businessRulesDTO;
        public UpdateAdminPageControlViewModel(BusinessRulesDTO businessRulesDto)
        {
            _businessRulesDTO = businessRulesDto;

            PriorityTypesList = new ObservableCollection<BusinessRulePriorityDTO>();
            OverSpeedList = new ObservableCollection<OverSpeedDTO>();
            TrafficCrossList = new ObservableCollection<TrafficCrossDTO>();
            VehicleTypesList = new ObservableCollection<VehicleTypeDTO>();
            _client = new ServiceLayerClient();
            AddBusinessRuleCommand = new Command(SaveBusinessRule);
            LoadData();

            SetSelectedRuleDetails();

            IsUpdateGridEnabled = true;
            //IsRuleEnabled = true;
        }

        private void LoadData()
        {
            LoadPriorties();
            LoadOverSpeed();
            LoadVehicleTypes();
            LoadTrafficCross();
        }

        private void SetSelectedRuleDetails()
        {
            this.BusinessRuleName = _businessRulesDTO.BusinessName;
            if (!string.IsNullOrEmpty(_businessRulesDTO.VehicleType) && this.VehicleTypesList != null && this.VehicleTypesList.Count > 0)
                foreach (var item in this.VehicleTypesList)
                {
                    if (item.VehicleTypeID == _businessRulesDTO.VehicleTypeId && item.VehicleTypeEn == _businessRulesDTO.VehicleType)
                    { this.VehicleType = item; break; }
                }
            if (this.VehicleType == null)
                this.VehicleType = VehicleTypesList.Where(a => a.VehicleTypeEn == "Any").FirstOrDefault();


            if (_businessRulesDTO.IsOverSpeedInsideCity)
            {
                this.SpeedInsideCityAdded = _businessRulesDTO.IsOverSpeedInsideCity;

                this.SpeedThshldInCity = OverSpeedList.Where(a => a.OverSpeedId == _businessRulesDTO.InsideCityOverSpeedId && a.OverSpeedValue == _businessRulesDTO.InsideCityOverSpeedValue).FirstOrDefault();

                //foreach (var item in OverSpeedList)
                //{
                //    if (item.OverSpeedId == _businessRulesDTO.InsideCityOverSpeedId
                //        && item.OverSpeedValue == _businessRulesDTO.InsideCityOverSpeedValue)
                //    {
                //        this.SpeedThshldInCity = item;
                //        break;
                //    }
                //}

            }


            if (_businessRulesDTO.IsOverSpeedOutsideCity)
            {
                this.SpeedOutsideCityAdded = _businessRulesDTO.IsOverSpeedOutsideCity;
                this.SpeedThshldOutCity = OverSpeedList.Where(a => a.OverSpeedId == _businessRulesDTO.OutsideCityOverSpeedId && a.OverSpeedValue == _businessRulesDTO.OutsideCityOverSpeedValue).FirstOrDefault();
            }

            this.NumOfViolnsInCity = _businessRulesDTO.InsideCityOverSpeedQty;


            this.NumOfViolnsOutCity = _businessRulesDTO.OutsideCityOverSpeedQty;

            this.RedLgtCrosdAdded = _businessRulesDTO.IsTrafficCross;
            this.RedLgtCrosdValue = _businessRulesDTO.TrafficCrossQty;
            if (_businessRulesDTO.IsTrafficCross && _businessRulesDTO.TrafficCrossTimesId != null)
            {
                //this.RedLgtCrosngTimeAdded = true;
                this.RedLgtCrosngSecondsValue = TrafficCrossList.Where(a => a.TrafficCrossId == _businessRulesDTO.TrafficCrossTimesId && a.TrafficCrossValue == _businessRulesDTO.TrafficCrossTimesValue).FirstOrDefault();

                //this.RedLgtCrosngSecondsValue = new TrafficCrossDTO();
                //this.RedLgtCrosngSecondsValue.TrafficCrossValue = _businessRulesDTO.TrafficCrossTimesValue;
                //this.RedLgtCrosngSecondsValue.TrafficCrossId = (int)_businessRulesDTO.TrafficCrossTimesId;

            }

            this.RedLgtCrosngNumOfViolnsValue = _businessRulesDTO.TrafficCrossQty;

            this.RuleIntTimeValue = _businessRulesDTO.RuleInterval;
            this.IsRuleDisabled = _businessRulesDTO.IsDeleted;

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

            foreach (var item in list)
            {
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
                //IsDeleted = IsRuleEnabled,
            };

            if (RulePriority != null)
                businessRule.PriorityId = RulePriority.PriorityID;
            if (SpeedInsideCityAdded && SpeedThshldInCity != null)
                businessRule.InsideCityOverSpeedId = SpeedThshldInCity.OverSpeedId;
            if (SpeedOutsideCityAdded && SpeedThshldOutCity != null)
                businessRule.OutsideCityOverSpeedId = SpeedThshldOutCity.OverSpeedId;
            if (RedLgtCrosdAdded && RedLgtCrosngSecondsValue != null)
                businessRule.TrafficCrossTimesId = RedLgtCrosngSecondsValue.TrafficCrossId;
            if (VehicleType != null)
                businessRule.VehicleTypeId = VehicleType.VehicleTypeID;


            var res = _client.SaveBusinessRuleAsync(businessRule);
            res.ContinueWith(x => AddBusinessRuleResult(x.Result));
        }

        public void AddBusinessRuleResult(bool result)
        {
            BusinessRuleName = "";
            RulePriority = null;
            VehicleType = null;
            SpeedInsideCityAdded = false;
            SpeedThshldInCity = null;
            NumOfViolnsInCity = 0;
            SpeedOutsideCityAdded = false;
            SpeedThshldOutCity = null;
            NumOfViolnsOutCity = 0;
            RedLgtCrosdAdded = false;
            RedLgtCrosdValue = 0;
            //RedLgtCrosngTimeAdded = false;
            RedLgtCrosngSecondsValue = null;
            RedLgtCrosngNumOfViolnsValue = 0;
            RuleIntTimeValue = 0;
            /// inform the user that the rule has been added
            var handler = RoleUpdated;
            if (handler != null)
                handler(this, new EventArgs());

        }

        public void UpdateBusinessRuleResult(bool result)
        {
            //BusinessRuleName = "";
            //RulePriority = null;
            //VehicleType = null;
            //SpeedInsideCityAdded = false;
            //SpeedThshldInCity = null;
            //NumOfViolnsInCity = 0;
            //SpeedOutsideCityAdded = false;
            //SpeedThshldOutCity = null;
            //NumOfViolnsOutCity = 0;
            //RedLgtCrosdAdded = false;
            //RedLgtCrosdValue = 0;
            //RedLgtCrosngTimeAdded = false;
            //RedLgtCrosngSecondsValue = null;
            //RedLgtCrosngNumOfViolnsValue = 0;
            //RuleIntTimeValue = 0;
            /// inform the user that the rule has been added
            var handler = RoleUpdated;
            if (handler != null)
                handler(this, new EventArgs());
            IsUpdateGridEnabled = true;
        }

        private void UpdateRuleExecute()
        {
            if (IsUpdateGridEnabled)
            {
                IsUpdateGridEnabled = false;
                _businessRulesDTO.IsDeleted = this.IsRuleDisabled;

                var res = _client.SaveBusinessRuleAsync(_businessRulesDTO);
                res.ContinueWith(x => UpdateBusinessRuleResult(x.Result));
            }
        }

        private void EditForUpdateExecute()
        {

        }

        private void GoBackExecute()
        {
            string closeMsg = Utility.GetLang() == "en" ? "Are you sure you want to cancel any modifications?" : "سيتم الغاء أي تعديلات, هل انت متأكد؟";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            //closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                UpdateBusinessRuleResult(true);
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

        public Boolean SpeedInsideCityAdded
        {
            get { return _speedInsideCityAdded; }
            set { _speedInsideCityAdded = value; this.RaiseNotifyPropertyChanged(); }
        }


        public OverSpeedDTO SpeedThshldInCity
        {
            get { return _speedThshldInCity; }
            set { _speedThshldInCity = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 NumOfViolnsInCity
        {
            get { return _numOfViolnsInCity; }
            set { _numOfViolnsInCity = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Boolean SpeedOutsideCityAdded
        {
            get { return _speedOutsideCityAdded; }
            set { _speedOutsideCityAdded = value; this.RaiseNotifyPropertyChanged(); }
        }



        public OverSpeedDTO SpeedThshldOutCity
        {
            get { return _speedThshldOutCity; }
            set { _speedThshldOutCity = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 NumOfViolnsOutCity
        {
            get { return _numOfViolnsOutCity; }
            set { _numOfViolnsOutCity = value; this.RaiseNotifyPropertyChanged(); }
        }




        public Boolean RedLgtCrosdAdded
        {
            get { return _redLgtCrosdAdded; }
            set { _redLgtCrosdAdded = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 RedLgtCrosdValue
        {
            get { return _redLgtCrosdValue; }
            set { _redLgtCrosdValue = value; this.RaiseNotifyPropertyChanged(); }
        }



        public Boolean RedLgtCrosngTimeAdded
        {
            get { return _redLgtCrosngTimeAdded; }
            set { _redLgtCrosngTimeAdded = value; this.RaiseNotifyPropertyChanged(); }
        }

        public TrafficCrossDTO RedLgtCrosngSecondsValue
        {
            get { return _redLgtCrosngSecondsValue; }
            set { _redLgtCrosngSecondsValue = value; this.RaiseNotifyPropertyChanged(); }
        }


        public Int32 RedLgtCrosngNumOfViolnsValue
        {
            get { return _redLgtCrosngNumOfViolnsValue; }
            set { _redLgtCrosngNumOfViolnsValue = value; this.RaiseNotifyPropertyChanged(); }
        }

        public String RuleIntTimeCategory
        {
            get { return _ruleIntTimeCategory; }
            set { _ruleIntTimeCategory = value; this.RaiseNotifyPropertyChanged(); }
        }

        public int RuleIntTimeValue
        {
            get { return _ruleIntTimeValue; }
            set { _ruleIntTimeValue = value; this.RaiseNotifyPropertyChanged(); }
        }

        public Boolean IsRuleDisabled
        {
            get { return _isRuleDisabled; }
            set { _isRuleDisabled = value; this.RaiseNotifyPropertyChanged(); }
        }



        private bool _isUpdateGridEnabled;

        public bool IsUpdateGridEnabled
        {
            get { return _isUpdateGridEnabled; }
            set
            {
                _isUpdateGridEnabled = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        public ObservableCollection<VehicleTypeDTO> VehicleTypesList { get; set; }
        public ObservableCollection<OverSpeedDTO> OverSpeedList { get; set; }
        public ObservableCollection<TrafficCrossDTO> TrafficCrossList { get; set; }
        public ObservableCollection<BusinessRulePriorityDTO> PriorityTypesList { get; set; }

        #endregion
        public Command AddBusinessRuleCommand { get; set; }

        public Command UpdateRuleCommand { get { return new Command((UpdateRuleExecute)); } }

        public Command ClickedCellCommand { get { return new Command((EditForUpdateExecute)); } }
        public Command GoBackCommand { get { return new Command((GoBackExecute)); } }

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

