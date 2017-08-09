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
    class ViolationsYearlyComparisonStatisticalViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        ServiceLayerClient client = new ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }

        private int _fromYearValue;

        public int FromYearValue
        {
            get { return _fromYearValue; }
            set
            {
                if (_fromYearValue != value)
                {
                    _fromYearValue = value;

                    GetViolationData();

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<int> _fromYearValueColl;

        public ObservableCollection<int> FromYearValueColl
        {
            get
            {
                return _fromYearValueColl;
            }

            set
            {
                _fromYearValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private int _toYearValue;

        public int ToYearValue
        {
            get { return _toYearValue; }
            set
            {
                if (_toYearValue != value)
                {
                    _toYearValue = value;

                    GetViolationData();

                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<int> _toYearValueColl;

        public ObservableCollection<int> ToYearValueColl
        {
            get
            {
                return _toYearValueColl;
            }

            set
            {
                _toYearValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        #region Constractors
        public ViolationsYearlyComparisonStatisticalViewModel()
        {
            LoadBasicData();

            GetViolationData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetViolationsComparisonYearlyAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
        }

        private void LoadBasicData()
        {
            if (FromYearValueColl == null)
                FromYearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(DateTime.Now.Year - 2010).OrderBy(x => x).ToList());

            if (ToYearValueColl == null)
                ToYearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(DateTime.Now.Year - 2010).OrderBy(x => x).ToList());
            if (FromYearValue == 0)
                FromYearValue = FromYearValueColl[0];
            if (ToYearValue == 0)
                ToYearValue = ToYearValueColl[ToYearValueColl.Count - 1];
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
