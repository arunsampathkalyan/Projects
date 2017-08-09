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
    class TruckViolationWeeklyStatisticalByTypeViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

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
                    _yearValue = _yearValueColl[0];
                return _yearValue;
            }
            set
            {
                if (_yearValue != value)
                {
                    _yearValue = value;

                    GetTruckViolationsData();

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

        private int _monthValue;

        public int MonthValue
        {
            get { return _monthValue; }
            set
            {
                if (_monthValue != value)
                {
                    _monthValue = value;

                    GetTruckViolationsData();

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _monthValueColl;

        public ObservableCollection<string> MonthValueColl
        {
            get
            {
                return _monthValueColl;
            }

            set
            {
                _monthValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        #region Constractors
        public TruckViolationWeeklyStatisticalByTypeViewModel()
        {
            LoadBasicData();

            GetTruckViolationsData();
        }

        #endregion

        #region Methods

        private void GetTruckViolationsData()
        {
            var callTask = client.GetTruckViolationsStaticsticalWeeklyAsync(YearValue, (ServiceLayerReference.MonthOfYear)(MonthValue + 1));
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

            if (MonthValueColl == null)
                MonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
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
