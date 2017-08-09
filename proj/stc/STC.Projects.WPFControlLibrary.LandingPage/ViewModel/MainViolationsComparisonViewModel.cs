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
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.LandingPage.KPILayerServiceReference;
using STC.Projects.WPFControlLibrary.LandingPage.Model;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class MainViolationsComparisonViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        KPILayerClient client = new KPILayerClient();

        private ObservableCollection<BarChartModel> _dataList;

        public ObservableCollection<BarChartModel> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                RaiseNotifyPropertyChanged();
            }
        }
        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }

        private int _fromYearValue;
        private int _toYearValue;

        public Int32 FromYearValue
        {
            get { return _fromYearValue; }
            set
            {
                _fromYearValue = value;

                this.RaiseNotifyPropertyChanged();
            }
        }
        public Int32 ToYearValue
        {
            get { return _toYearValue; }
            set
            {
                _toYearValue = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        #region Constractors
        public MainViolationsComparisonViewModel()
        {
            this.FromYearValue = DateTime.Now.Year - 1;
            this.ToYearValue = DateTime.Now.Year;
            GetViolationData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetAccidentsWithFatalitiesCountComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                ViolationsCollection = data;
                DataList = new ObservableCollection<BarChartModel>();

                var targetvalue =
                    ViolationsCollection.FirstOrDefault(item => item.LegendName.ToLower().Contains("target"));
                foreach (var cubeDto in ViolationsCollection)
                {
                    var barChartModel = new BarChartModel();
                    barChartModel.Key = cubeDto.LegendName;
                    barChartModel.Value = cubeDto.Details[0].Value;

                    if (cubeDto.LegendName.Trim() == this.ToYearValue.ToString())
                    {
                        if (targetvalue.Details[0].Value < barChartModel.Value)
                        {
                            barChartModel.Color = "Red";
                        }
                        else
                        {
                            barChartModel.Color = "#2E9D01";
                        }
                        DataList.Add(barChartModel); ;

                    }
                    else if (cubeDto.LegendName.Trim() == FromYearValue.ToString())
                    {
                        barChartModel.Color = "#0090FF";
                        DataList.Insert(0, barChartModel);
                    }
                    else
                    {
                        barChartModel.Color = "#5E2A05";
                        DataList.Insert(1, barChartModel);
                    }
                }
            });
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
