using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.LandingPage.KPILayerServiceReference;
using STC.Projects.WPFControlLibrary.LandingPage.Model;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    public class BarChartViewModel : INotifyPropertyChanged
    {
        #region Properties

        KPILayerClient client = new KPILayerClient();

        private CubeDTO[] _violationsCollection;

        private ObservableCollection<BarChartModel> _noOfFatalityViewModeldataList;

        public ObservableCollection<BarChartModel> NoOfFatalityViewModelDataList
        {
            get { return _noOfFatalityViewModeldataList; }
            set
            {
                _noOfFatalityViewModeldataList = value;
                RaiseNotifyPropertyChanged();
            }
        }

         private ObservableCollection<BarChartModel> _noOfDangerousVilationViewModeldataList;

        public ObservableCollection<BarChartModel> NoOfDangerousVilationViewModelDataList
        {
            get { return _noOfDangerousVilationViewModeldataList; }
            set
            {
                _noOfDangerousVilationViewModeldataList = value;
                RaiseNotifyPropertyChanged();
            }
        }

         private ObservableCollection<BarChartModel> _noOfServiceAccidentsViewmodeldataList;

        public ObservableCollection<BarChartModel> NoOfServiceAccidentsViewmodelDataList
        {
            get { return _noOfServiceAccidentsViewmodeldataList; }
            set
            {
                _noOfServiceAccidentsViewmodeldataList = value;
                RaiseNotifyPropertyChanged();
            }
        }

         private ObservableCollection<BarChartModel> _noOfAccidentWithFatalityViewModeldataList;

        public ObservableCollection<BarChartModel> NoOfAccidentWithFatalityViewModelDataList
        {
            get { return _noOfAccidentWithFatalityViewModeldataList; }
            set
            {
                _noOfAccidentWithFatalityViewModeldataList = value;
                RaiseNotifyPropertyChanged();
            }
        }

         private ObservableCollection<BarChartModel> _noOfInjuiriesViewModeldataList;

        public ObservableCollection<BarChartModel> NoOfInjuiriesViewModelDataList
        {
            get { return _noOfInjuiriesViewModeldataList; }
            set
            {
                _noOfInjuiriesViewModeldataList = value;
                RaiseNotifyPropertyChanged();
            }
        }
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

        private string _borderColor;

        public string BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                RaiseNotifyPropertyChanged();
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
        public BarChartViewModel()
        {
            this.FromYearValue = DateTime.Now.Year - 1;
            this.ToYearValue = DateTime.Now.Year;
            GetNooFatalities();
            GetNoofDangerousViolations();
            GetNoSevereAccident();
            GetNoofaccidentwithfatality();
            GetNooInjuries();
        }

        #endregion

        #region Methods

        private void GetNooFatalities()
        {
            var callTask = client.GetFatalitiesComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_GetNooFatalities(x));
        }

        private void GetNoofDangerousViolations()
        {
            var callTask = client.GetDangerousViolatorsComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_GetNoofDangerousViolations(x));
        }

        private void GetNoSevereAccident ()
        {
            var callTask = client.GetSevereAccidentsComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_GetNoSevereAccident(x));
        }

        private void GetNoofaccidentwithfatality ()
        {
            var callTask = client.GetAccidentsWithFatalitiesCountComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_GetNoofaccidentwithfatality(x));
        }

        private void GetNooInjuries()
        {
            var callTask = client.GetTotalInjuriesComparisonForDashboardAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_GetNooInjuries(x));
        }

        private void Add_GetNooFatalities(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
              NoOfFatalityViewModelDataList=  UpdateDataList(data);
                
            });
        }

       

        private void Add_GetNoofDangerousViolations(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
               NoOfDangerousVilationViewModelDataList= UpdateDataList(data);
            });
        }

        private void Add_GetNoSevereAccident(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
               NoOfServiceAccidentsViewmodelDataList= UpdateDataList(data);
            });
        }

        private void Add_GetNoofaccidentwithfatality(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
               NoOfAccidentWithFatalityViewModelDataList= UpdateDataList(data);
            });
        }
        private void Add_GetNooInjuries(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
               NoOfInjuiriesViewModelDataList= UpdateDataList(data);
            });
        }

        private ObservableCollection<BarChartModel> UpdateDataList(CubeDTO[] data)
        {
            ViolationsCollection = data;
           var dataList = new ObservableCollection<BarChartModel>();

            var targetvalue =
                ViolationsCollection.FirstOrDefault(item => item.LegendName.ToLower().Contains("target"));
            foreach (var cubeDto in ViolationsCollection)
            {
                var barChartModel = new BarChartModel();
                barChartModel.Key = cubeDto.LegendName;
                barChartModel.Value = cubeDto.Details[0].Value;

                if (cubeDto.LegendName.Trim() == this.ToYearValue.ToString())
                {
                    barChartModel.Color = targetvalue.Details[0].Value < barChartModel.Value ? "Red" : "#2E9D01";
                    BorderColor = targetvalue.Details[0].Value < barChartModel.Value ? "Red" : "#00ffcc";
                    dataList.Add(barChartModel); ;

                }
                else if (cubeDto.LegendName.Trim() == FromYearValue.ToString())
                {
                    barChartModel.Color = "#0090FF";
                    dataList.Insert(0, barChartModel);
                }
                else
                {
                    barChartModel.Color = "#5E2A05";
                    dataList.Insert(1, barChartModel);
                }
            }

            return dataList;
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
