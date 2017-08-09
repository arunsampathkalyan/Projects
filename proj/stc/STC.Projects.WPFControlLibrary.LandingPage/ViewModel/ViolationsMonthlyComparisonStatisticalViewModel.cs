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
    class ViolationsMonthlyComparisonStatisticalViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        ServiceLayerClient client = new ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
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

        private int _fromMonthValue;

        public int FromMonthValue
        {
            get { return _fromMonthValue; }
            set
            {
                if (_fromMonthValue != value)
                {
                    _fromMonthValue = value;
                    if (_toMonthValue != FromMonthValue)
                    {
                        GetViolationData();
                    }
                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _fromMonthValueColl;

        public ObservableCollection<string> FromMonthValueColl
        {
            get
            {
                return _fromMonthValueColl;
            }

            set
            {
                _fromMonthValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private int _toMonthValue;

        public int ToMonthValue
        {
            get { return _toMonthValue; }
            set
            {
                if (_toMonthValue != value)
                {
                    _toMonthValue = value;

                    if (_toMonthValue != FromMonthValue)
                    {
                        GetViolationData();
                    }

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _toMonthValueColl;

        public ObservableCollection<string> ToMonthValueColl
        {
            get
            {
                return _toMonthValueColl;
            }

            set
            {
                _toMonthValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        #region Constractors
        public ViolationsMonthlyComparisonStatisticalViewModel()
        {
            LoadBasicData();
            GetViolationData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetViolationsComparisonMonthlyAsync(YearValue, (ServiceLayerReference.MonthOfYear)FromMonthValue + 1, (ServiceLayerReference.MonthOfYear)ToMonthValue + 1);
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

            if (FromMonthValueColl == null)
                FromMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
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
