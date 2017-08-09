
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
    class AccidentsKPITableChartViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

        private KpiDTO[] _accidentsKPICollection;
        public KpiDTO[] AccidentsKPICollection
        {
            get { return _accidentsKPICollection; }
            set
            {
                _accidentsKPICollection = value;
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
                foreach (var item in AccidentsKPICollection)
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

        public AccidentsKPITableChartViewModel()
        {
            //IncidentsCollection = GetSampleData();
            //LoadBasicData();

            GetAccidentsData();
        }


        public void GetAccidentsData()
        {
            var callTask = client.GetAccidentKPIsAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_AccidentsKPIDetails(x));
        }
        private void Add_AccidentsKPIDetails(KpiDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { AccidentsKPICollection = data; });
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
