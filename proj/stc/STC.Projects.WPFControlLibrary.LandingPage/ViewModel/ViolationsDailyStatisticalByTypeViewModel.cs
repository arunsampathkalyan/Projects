using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reactive.Threading.Tasks;
using System.Windows;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class ViolationsDailyStatisticalByTypeViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        private Dictionary<DateTime, string> weekDayDateTimeDictObj = null;
        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

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

        private int _toMonthValue;
        public Int32 ToMonthValue
        {
            get { return _toMonthValue; }
            set
            {

                _toMonthValue = value;

                GetNumberOfSpecificDaysinMonth();
                if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 2)
                {
                    ToWeekStartValue = 0;

                }

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
                if (_toWeekNum != -1)
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

        private ObservableCollection<string> _toMonthValueColl;
        public ObservableCollection<String> ToMonthValueColl
        { get { return _toMonthValueColl; } set { _toMonthValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }

        #endregion



        #region Constractors
        public ViolationsDailyStatisticalByTypeViewModel()
        {
            LoadBasicData();
            GetViolationData();
        }


        #endregion

        #region Methods
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
        private void GetViolationData()
        {
            var callTask = client.GetViolationsStatisticalDailyAsync(weekDayDateTimeDictObj.Keys.ElementAt(ToWeekStartValue));
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
        }

        private void LoadBasicData()
        {
            if (YearValueColl == null)
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(DateTime.Now.Year - 2010));

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            GetNumberOfSpecificDaysinMonth();
            if (WeekOfMonthColl != null && WeekOfMonthColl.Count > 2)
            {
                ToWeekStartValue = 0;

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
