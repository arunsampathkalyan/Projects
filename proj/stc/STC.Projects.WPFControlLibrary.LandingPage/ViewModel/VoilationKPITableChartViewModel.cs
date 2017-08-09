
using STC.Projects.WPFControlLibrary.LandingPage.Model;
using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
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
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class VoilationKPITableChartViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

        private KpiDTO[] _violationsKPICollection;
        public KpiDTO[] ViolationsKPICollection
        {
            get { return _violationsKPICollection; }
            set
            {
                _violationsKPICollection = value;

                if (value != null)
                {
                    SetDoughnutValuesCollection();
                }
                //ModifyCollectionData();
                this.RaiseNotifyPropertyChanged();
            }
        }


        private ObservableCollection<DoughnutSeriesColl> _doughnutSeriesValueColl;

        public ObservableCollection<DoughnutSeriesColl> DoughnutSeriesValueColl
        {
            get { return _doughnutSeriesValueColl; }
            set
            {
                this._doughnutSeriesValueColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        void SetDoughnutValuesCollection()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ObservableCollection<DoughnutSeriesColl> doughnutValuesCollTemp = new ObservableCollection<DoughnutSeriesColl>();
                DoughnutSeriesColl doughnutSeries;
                foreach (var item in ViolationsKPICollection)
                {
                    doughnutSeries = new DoughnutSeriesColl();
                    doughnutSeries.DoughnutItemColl = new ObservableCollection<DoughnutItem>();
                    doughnutSeries.DoughnutItemColl.Add(new DoughnutItem()
                    {
                        ChartPercentValue = item.ActualPercentage,
                        //ChartPercentValue = ((item.Percentage * 100) * (item.TargetValue / 100)),
                        //? ((item.Percentage * 100) * (item.TargetValue / 100)) : 25,
                        //Percentage = 15,
                        //Color = ((item.ActualPercentage * 100) * (item.TargetValue / 100) >= item.TargetValue ? (Color)ColorConverter.ConvertFromString("#00ffcc") : (Color)ColorConverter.ConvertFromString("#181818"))
                        Color = (item.ActualPercentage >= 100) ? (Color)ColorConverter.ConvertFromString("#00ffcc") : (Color)ColorConverter.ConvertFromString("#181818")
                    });

                    doughnutSeries.DoughnutItemColl.Add(new DoughnutItem()
                    {
                        ChartPercentValue = 100 - (item.ActualPercentage),
                        //Percentage = (item.TargetValue > 0 ? item.TargetValue : 20),
                        Color = (Color)ColorConverter.ConvertFromString("#0a1114")
                    });

                    //doughnutSeries.KpiCategory
                    doughnutSeries.ActPercentage = item.ActualPercentage;
                    doughnutSeries.TargetValue = item.TargetValue;

                    doughnutSeries.KPIName = Utility.GetLang() == "ar" ? item.LabelValueArabic : item.LabelValueEnglish;
                    doughnutSeries.ColorActualPercent = new SolidColorBrush((item.ActualPercentage >= 100) ? (Color)ColorConverter.ConvertFromString("#00ffcc") : (Color)ColorConverter.ConvertFromString("#181818"));
                    //doughnutSeries.ColorActualPercent = new SolidColorBrush(((item.ActualPercentage * 100) * (item.TargetValue / 100) >= item.TargetValue ? (Color)ColorConverter.ConvertFromString("#00ffcc") : (Color)ColorConverter.ConvertFromString("#181818")));
                    //doughnutSeries.ColorActualPercent = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0a1114"));
                    doughnutValuesCollTemp.Add(doughnutSeries);
                }
                DoughnutSeriesValueColl = doughnutValuesCollTemp;
            });
        }

        public VoilationKPITableChartViewModel()
        {
            //IncidentsCollection = GetSampleData();
            //LoadBasicData();
            GetVioalationData();
        }


        public void GetVioalationData()
        {
            var callTask = client.GetViolationKPIsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsKPIDetails(x));
        }
        private void Add_ViolationsKPIDetails(KpiDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() => { ViolationsKPICollection = data; });
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

    public class DoughnutSeriesColl : System.ComponentModel.INotifyPropertyChanged
    {

        private string _kpiName;
        public String KPIName
        {
            get
            {
                return _kpiName;
            }
            set
            {
                this._kpiName = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _kpiCategory;
        public String KpiCategory
        {
            get
            {
                return _kpiCategory;
            }
            set
            {
                this._kpiCategory = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private double _actPercentage;
        public Double ActPercentage
        {
            get
            {
                return _actPercentage;
            }
            set
            {
                this._actPercentage = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private double _targetValue;
        public Double TargetValue
        {
            get
            {
                return _targetValue;
            }
            set
            {
                this._targetValue = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private System.Windows.Media.Brush _colorActualPercent;

        public System.Windows.Media.Brush ColorActualPercent
        {
            get { return _colorActualPercent; }
            set
            {
                this._colorActualPercent = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<DoughnutItem> _doughnutItemColl;

        public ObservableCollection<DoughnutItem> DoughnutItemColl
        {
            get { return _doughnutItemColl; }
            set
            {
                this._doughnutItemColl = value;
                this.RaiseNotifyPropertyChanged();
            }
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

    public class DoughnutItem : System.ComponentModel.INotifyPropertyChanged
    {



        private double _chartPercentValue;
        public Double ChartPercentValue
        {
            get { return _chartPercentValue; }
            set
            {
                this._chartPercentValue = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private Color _color;

        public Color Color
        {
            get { return _color; }
            set
            {
                this._color = value;
                this.RaiseNotifyPropertyChanged();
            }
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
