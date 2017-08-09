using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Media;

using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.LandingPage.Model;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class ViolationsDailyComparisonStatisticalViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        private Dictionary<DateTime, string> weekDayDateTimeDictObj = null;
        private Dictionary<string, List<DateTime>> daysDateTimeDictObj = null;

        private int _fromMonthValue;
        private int _toMonthValue;
        private string _highestValue;

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

                _toMonthValue = value;
                if (value != -1)
                    GetNumberOfWeekAndDays();
                if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 0)
                {
                    ToWeekStartValue = 0;
                }

                this.RaiseNotifyPropertyChanged();

            }
        }
        private int _fromWeekNum;
        public Int32 FromWeekStartValue
        {
            get { return _fromWeekNum; }
            set
            {
                _fromWeekNum = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private int _toWeekNum;
        public Int32 ToWeekStartValue
        {
            get { return _toWeekNum; }
            set
            {

                _toWeekNum = value;
                if (value != -1)
                    GetNumberOfDaysInweek();
                if (DaysColl != null && DaysColl.Count > 1)
                {
                    FromDateValue = 0;
                    ToDateValue = 1;
                }

                this.RaiseNotifyPropertyChanged();

            }
        }


        private int _fromDayNum;
        public Int32 FromDateValue
        {
            get { return _fromDayNum; }
            set
            {
                _fromDayNum = value;

                if (_toDayNum != -1 && _toDayNum != _fromDayNum)
                    GetViolationData();

                this.RaiseNotifyPropertyChanged();
            }
        }

        private int _toDayNum;
        public Int32 ToDateValue
        {
            get { return _toDayNum; }
            set
            {

                _toDayNum = value;
                if (_toDayNum != -1 && _toDayNum != _fromDayNum)
                    GetViolationData();

                this.RaiseNotifyPropertyChanged();

            }
        }

        private ObservableCollection<string> _weekOfMonthColl;

        public ObservableCollection<String> WeekOfMonthColl
        {
            get
            {
                return _weekOfMonthColl;
            }

            set
            {
                _weekOfMonthColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _daysColl;

        public ObservableCollection<String> DaysColl
        {
            get
            {
                return _daysColl;
            }

            set
            {
                _daysColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<DateTime> dateTimeColl;

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

        private int _yearValue;

        public int YearValue
        {
            get
            {
                if (_yearValue == 0)
                    _yearValue = YearValueColl[0];
                return _yearValue;
            }
            set
            {
                if (_yearValue != value)
                {
                    _yearValue = value;
                    this.RaiseNotifyPropertyChanged();
                }
            }
        }
        public String HighestValue
        {
            get { return _highestValue; }
            set { _highestValue = value; this.RaiseNotifyPropertyChanged(); }
        }


        private ObservableCollection<string> _fromMonthValueColl;
        private ObservableCollection<string> _toMonthValueColl;


        public ObservableCollection<String> FromMonthValueColl
        { get { return _fromMonthValueColl; } set { _fromMonthValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        public ObservableCollection<String> ToMonthValueColl
        { get { return _toMonthValueColl; } set { _toMonthValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }
        private void LoadBasicData()
        {

            if (YearValueColl == null)
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(DateTime.Now.Year - 2010));

            if (FromMonthValueColl == null)
                FromMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            GetNumberOfWeekAndDays();

            if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 0)
            {
                ToWeekStartValue = 0;
            }

            if (DaysColl != null && DaysColl.Count > 2)
            {
                FromDateValue = 0;
                ToDateValue = 1;
            }
        }

        private void GetNumberOfSpecificDaysinMonth()
        {
            weekDayDateTimeDictObj = new Dictionary<DateTime, string>();
            WeekOfMonthColl = new ObservableCollection<string>();
            List<DateTime> weekDatesCurrent = Utility.GetNumberOfSpecificDaysInMonth(YearValue, ToMonthValue + 1, DayOfWeek.Sunday);
            foreach (DateTime date in weekDatesCurrent)
            {
                weekDayDateTimeDictObj.Add(date, date.Day + " - " + date.Date.ToString("dd MMM"));
                WeekOfMonthColl.Add(date.DayOfWeek + " - " + date.Date.ToString("dd MMM"));
            }
        }

        //private void GetNumberOfDaysinMonth()
        //{
        //    daysDateTimeDictObj = new Dictionary<DateTime, string>();
        //    DaysColl = new ObservableCollection<string>();
        //    List<DateTime> daysMonthCurrent = Utility.GetNumberOfDaysInMonth(YearValue, ToMonthValue + 1);
        //    foreach (DateTime date in daysMonthCurrent)
        //    {
        //        daysDateTimeDictObj.Add(date, date.Day + " - " + date.Date.ToString("dd MMM"));
        //        DaysColl.Add(date.DayOfWeek + " - " + date.Date.ToString("dd MMM"));
        //    }
        //}

        private void GetNumberOfWeekAndDays()
        {
            DaysColl = new ObservableCollection<string>();
            daysDateTimeDictObj = Utility.GetNumberOfWeeksAndDays(YearValue, ToMonthValue + 1, DayOfWeek.Sunday);
            WeekOfMonthColl = new ObservableCollection<string>(daysDateTimeDictObj.Keys.ToList());
        }

        private void GetNumberOfDaysInweek()
        {
            DaysColl = new ObservableCollection<string>();
            dateTimeColl = daysDateTimeDictObj[daysDateTimeDictObj.Keys.ElementAt(ToWeekStartValue)];
            foreach (DateTime date in dateTimeColl)
            {
                DaysColl.Add(date.DayOfWeek + " - " + date.Date.ToString("dd MMM"));
            }
        }


        public ViolationsDailyComparisonStatisticalViewModel()
        {
            //IncidentsCollection = GetSampleData();
            LoadBasicData();
            GetViolationData();
        }
        public void GetViolationData()
        {
            var callTask = client.GetViolationsComparisonDailyAsync(dateTimeColl[FromDateValue], dateTimeColl[ToDateValue]);            
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }



        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
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
