
using STC.Projects.WPFControlLibrary.ViolationsMapControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Reactive.Threading.Tasks;
using System.Windows;
using STC.Projects.WPFControlLibrary.ViolationsMapControl.ServiceLayerReference;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.ViewModel
{
    class ViolationsCountViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private int _fromMonthValue;
        private int _toMonthValue;
        private string _highestValue;
        private bool yearValueChanged;
        public Int32 FromMonthValue
        {
            get { return _fromMonthValue; }
            set
            {
                if (_fromMonthValue != value)
                {
                    _fromMonthValue = value;
                    yearValueChanged = true;
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

                    if (_toMonthValue != FromMonthValue)
                    {
                        GetAccidentsRegionComparisionStatisticsData();
                    }


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



        private void LoadBasicData()
        {
            if (YearValueColl == null)
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(5));

            if (FromMonthValueColl == null)
                FromMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


        }
        public ViolationsCountViewModel()
        {
            //IncidentsCollection = GetSampleData();
            LoadBasicData();
            GetAccidentsRegionComparisionStatisticsData();
        }

        STC.Projects.WPFControlLibrary.ViolationsMapControl.ServiceLayerReference.ServiceLayerClient client = new STC.Projects.WPFControlLibrary.ViolationsMapControl.ServiceLayerReference.ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }

        public void GetAccidentsRegionComparisionStatisticsData()
        {
            //var callTask = client.GetViolationCountMonthAndCity();
            //var obs = callTask.ToObservable();
            //obs.Subscribe((x) => Add_ViolationsDetails(x));
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

    //public class LineChartdata
    //{
    //    public Brush LineColor { get; set; }
    //    public string LegendName { get; set; }
    //    public ObservableCollection<LineChartModel> Incidents { set; get; }
    //}
}
