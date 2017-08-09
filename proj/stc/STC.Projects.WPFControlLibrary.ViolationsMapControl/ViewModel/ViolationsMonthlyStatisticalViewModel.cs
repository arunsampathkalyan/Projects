using STC.Projects.WPFControlLibrary.ViolationsMapControl.ServiceLayerReference;
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

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.ViewModel
{
    class ViolationsMonthlyStatisticalViewModel : System.ComponentModel.INotifyPropertyChanged
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
                    _yearValue = DateTime.Now.Year - 4;
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

        #endregion

        #region Constractors
        public ViolationsMonthlyStatisticalViewModel()
        {
            LoadBasicData();

            GetViolationData();
        }

        public ViolationsMonthlyStatisticalViewModel(int year, int month = 0)
        {
            _yearValue = year;
            LoadBasicData();

            GetViolationData();

            if (ViolationsCollection == null || ViolationsCollection.Count() == 0)
                LoadSampleData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetViolationsStaticsticalMonthlyAsync(YearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
        }


        private void LoadSampleData()
        {
            ViolationsCollection = new CubeDTO[4];
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
                ViolationsCollection[i - 1] = cdto;
            }
        }
        private void LoadBasicData()
        {
            if (YearValueColl == null)
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(5));
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
