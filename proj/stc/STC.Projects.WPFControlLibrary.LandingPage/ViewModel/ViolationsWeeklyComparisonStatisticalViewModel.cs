
using STC.Projects.WPFControlLibrary.LandingPage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Reactive.Threading.Tasks;
using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using System.Windows;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{

    public class ViolationsWeeklyComparisonStatisticalViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private Dictionary<DateTime, string> weekDayDateTimeDictObj = null;

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

                GetNumberOfSpecificDaysinMonth();
                if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 2)
                {
                    FromWeekStartValue = 0;
                    ToWeekStartValue = 1;
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

                if (_toWeekNum != -1 && _toWeekNum != _fromWeekNum)
                    GetViolationData();

                this.RaiseNotifyPropertyChanged();
            }
        }

        private int _toWeekNum;
        public Int32 ToWeekStartValue
        {
            get { return _toWeekNum; }
            set
            {
                if (_toWeekNum != value)
                {
                    _toWeekNum = value;
                    if (_toWeekNum != -1 && _toWeekNum != _fromWeekNum)
                        GetViolationData();

                    this.RaiseNotifyPropertyChanged();
                }

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

            GetNumberOfSpecificDaysinMonth();
            if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 2)
            {
                FromWeekStartValue = 0;
                ToWeekStartValue = 1;
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
        public ViolationsWeeklyComparisonStatisticalViewModel()
        {
            //IncidentsCollection = GetSampleData();
            LoadBasicData();
            GetViolationData();
        }




        public void GetViolationData()
        {
            //var callTask = client.GetViolationTypeTrendWeekOfYearAsync(FromYearValue, ToYearValue);

            var callTask = client.GetViolationsComparisonWeeklyAsync(weekDayDateTimeDictObj.Keys.ElementAt(FromWeekStartValue), weekDayDateTimeDictObj.Keys.ElementAt(ToWeekStartValue));
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
