
using STC.Projects.WPFControlLibrary.LandingPage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Reactive.Threading.Tasks;
using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using System.Windows;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class TruckPermissionWeeklyTrendViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public ObservableCollection<LineChartdata> IncidentsCollection { get; set; }

        private int _fromYearValue;
        private int _toYearValue;
        private string _highestValue;
        private bool yearValueChanged;
        public Int32 FromYearValue
        {
            get { return _fromYearValue; }
            set
            {
                if (_fromYearValue != value)
                {
                    _fromYearValue = value;
                    yearValueChanged = true;
                    this.RaiseNotifyPropertyChanged();
                }
            }
        }
        public Int32 ToYearValue
        {
            get { return _toYearValue; }
            set
            {
                if (_toYearValue != value)
                {
                    _toYearValue = value;

                    if (!(_toYearValue < FromYearValue) && yearValueChanged)
                    {
                        GetViolationTypeTrendWeekOfYearAsync();
                    }


                    this.RaiseNotifyPropertyChanged();
                }
            }
        }
        public String HighestValue
        {
            get { return _highestValue; }
            set { _highestValue = value; this.RaiseNotifyPropertyChanged(); }
        }

        private ObservableCollection<int> _fromYearValueColl;
        private ObservableCollection<int> _toYearValueColl;


        public ObservableCollection<Int32> FromYearValueColl
        { get { return _fromYearValueColl; } set { _fromYearValueColl = value; this.RaiseNotifyPropertyChanged(); } }

        public ObservableCollection<Int32> ToYearValueColl
        { get { return _toYearValueColl; } set { _toYearValueColl = value; this.RaiseNotifyPropertyChanged(); } }


        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set
            {
                _violationsCollection = value;

                CalculateHighestValue();
                this.RaiseNotifyPropertyChanged();
            }
        }
        private void CalculateHighestValue()
        {

            if (ViolationsCollection != null && ViolationsCollection.Length > 0)
            {
                HighestValue = string.Empty;
                Dictionary<string, double> categroryDetails = new Dictionary<string, double>();

                foreach (var violation in ViolationsCollection)
                {
                    foreach (var details in violation.Details)
                    {
                        if (!categroryDetails.ContainsKey(details.Key))
                            categroryDetails.Add(details.Key, details.Value);
                        else
                            categroryDetails[details.Key] += details.Value;

                    }
                }

                HighestValue = categroryDetails.OrderByDescending(x => x.Value).First().Key;
            }

        }

        public TruckPermissionWeeklyTrendViewModel()
        {
            //IncidentsCollection = GetSampleData();
            LoadBasicData();
            GetViolationTypeTrendWeekOfYearAsync();
        }


        private void ModifyCollectionData()
        {
            try
            {
                foreach (var violation in ViolationsCollection)
                {
                    foreach (var details in violation.Details)
                    {
                        if (details.Value < 10000)
                            details.Value = details.Value + 55000;
                    }

                }
            }
            catch (Exception ex)
            {


            }

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

        public void GetViolationTypeTrendWeekOfYearAsync()
        {
            var callTask = client.GetTruckViolationsTrendWeekOfYearAsync(FromYearValue, ToYearValue);
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }
        private void Add_ViolationsDetails(CubeDTO[] data)
        {

            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
        }

        private ObservableCollection<LineChartdata> GetSampleData()
        {
            var result = new ObservableCollection<LineChartdata>();

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f2f2f2")),

                LegendName = "صدم متتالي",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 5},
                                new LineChartModel() { Text = "فبراير", Value = 10},
                                new LineChartModel() { Text = "مارس", Value = 6},
                                new LineChartModel() { Text = "إبريل", Value = 8},
                                new LineChartModel() { Text = "مايو", Value = 3},
                                new LineChartModel() { Text = "يونيو", Value = 9},
                                new LineChartModel() { Text = "يوليو", Value = 15},
                                new LineChartModel() { Text = "أغسطس", Value = 20},
                                new LineChartModel() { Text = "سبتمبر", Value = 18},
                                new LineChartModel() { Text = "أكتوبر", Value = 12},
                                new LineChartModel() { Text = "نوفمبر", Value = 2},
                                new LineChartModel() { Text = "ديسمبر", Value = 5}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8fec7c")),

                LegendName = "صدم أثناء الدوران",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 22},
                                new LineChartModel() { Text = "فبراير", Value = 27},
                                new LineChartModel() { Text = "مارس", Value = 23},
                                new LineChartModel() { Text = "إبريل", Value = 30},
                                new LineChartModel() { Text = "مايو", Value = 35},
                                new LineChartModel() { Text = "يونيو", Value = 39},
                                new LineChartModel() { Text = "يوليو", Value = 25},
                                new LineChartModel() { Text = "أغسطس", Value = 20},
                                new LineChartModel() { Text = "سبتمبر", Value = 23},
                                new LineChartModel() { Text = "أكتوبر", Value = 15},
                                new LineChartModel() { Text = "نوفمبر", Value = 18},
                                new LineChartModel() { Text = "ديسمبر", Value = 14}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f6a25b")),

                LegendName = "صدم جسم غير ثابت في الطريق",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 40},
                                new LineChartModel() { Text = "فبراير", Value = 44},
                                new LineChartModel() { Text = "مارس", Value = 48},
                                new LineChartModel() { Text = "إبريل", Value = 52},
                                new LineChartModel() { Text = "مايو", Value = 56},
                                new LineChartModel() { Text = "يونيو", Value = 60},
                                new LineChartModel() { Text = "يوليو", Value = 57},
                                new LineChartModel() { Text = "أغسطس", Value = 54},
                                new LineChartModel() { Text = "سبتمبر", Value = 51},
                                new LineChartModel() { Text = "أكتوبر", Value = 48},
                                new LineChartModel() { Text = "نوفمبر", Value = 45},
                                new LineChartModel() { Text = "ديسمبر", Value = 43}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7bb4eb")),

                LegendName = "إحتراق",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 63},
                                new LineChartModel() { Text = "فبراير", Value = 66},
                                new LineChartModel() { Text = "مارس", Value = 70},
                                new LineChartModel() { Text = "إبريل", Value = 75},
                                new LineChartModel() { Text = "مايو", Value = 77},
                                new LineChartModel() { Text = "يونيو", Value = 73},
                                new LineChartModel() { Text = "يوليو", Value = 80},
                                new LineChartModel() { Text = "أغسطس", Value = 86},
                                new LineChartModel() { Text = "سبتمبر", Value = 88},
                                new LineChartModel() { Text = "أكتوبر", Value = 80},
                                new LineChartModel() { Text = "نوفمبر", Value = 73},
                                new LineChartModel() { Text = "ديسمبر", Value = 75}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e74c3c")),

                LegendName = "صدم متقابل",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 10},
                                new LineChartModel() { Text = "فبراير", Value = 5},
                                new LineChartModel() { Text = "مارس", Value = 0},
                                new LineChartModel() { Text = "إبريل", Value = 5},
                                new LineChartModel() { Text = "مايو", Value = 20},
                                new LineChartModel() { Text = "يونيو", Value = 70},
                                new LineChartModel() { Text = "يوليو", Value = 25},
                                new LineChartModel() { Text = "أغسطس", Value = 80},
                                new LineChartModel() { Text = "سبتمبر", Value = 3},
                                new LineChartModel() { Text = "أكتوبر", Value = 50},
                                new LineChartModel() { Text = "نوفمبر", Value = 15},
                                new LineChartModel() { Text = "ديسمبر", Value = 100}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff7300")),

                LegendName = "صدم خلفي",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 7},
                                new LineChartModel() { Text = "فبراير", Value = 0},
                                new LineChartModel() { Text = "مارس", Value = 10},
                                new LineChartModel() { Text = "إبريل", Value = 10},
                                new LineChartModel() { Text = "مايو", Value = 0},
                                new LineChartModel() { Text = "يونيو", Value = 22},
                                new LineChartModel() { Text = "يوليو", Value = 0},
                                new LineChartModel() { Text = "أغسطس", Value = 15},
                                new LineChartModel() { Text = "سبتمبر", Value = 20},
                                new LineChartModel() { Text = "أكتوبر", Value = 0},
                                new LineChartModel() { Text = "نوفمبر", Value = 18},
                                new LineChartModel() { Text = "ديسمبر", Value = 5}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52d726")),

                LegendName = "دهس إنسان",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 0},
                                new LineChartModel() { Text = "فبراير", Value = 0},
                                new LineChartModel() { Text = "مارس", Value = 0},
                                new LineChartModel() { Text = "إبريل", Value = 0},
                                new LineChartModel() { Text = "مايو", Value = 18},
                                new LineChartModel() { Text = "يونيو", Value = 22},
                                new LineChartModel() { Text = "يوليو", Value = 10},
                                new LineChartModel() { Text = "أغسطس", Value = 3},
                                new LineChartModel() { Text = "سبتمبر", Value = 15},
                                new LineChartModel() { Text = "أكتوبر", Value = 0},
                                new LineChartModel() { Text = "نوفمبر", Value = 33},
                                new LineChartModel() { Text = "ديسمبر", Value = 45}
                }
            });

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffec00")),

                LegendName = "أخرى",

                Incidents = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 7},
                                new LineChartModel() { Text = "فبراير", Value = 6},
                                new LineChartModel() { Text = "مارس", Value = 9},
                                new LineChartModel() { Text = "إبريل", Value = 14},
                                new LineChartModel() { Text = "مايو", Value = 18},
                                new LineChartModel() { Text = "يونيو", Value = 22},
                                new LineChartModel() { Text = "يوليو", Value = 26},
                                new LineChartModel() { Text = "أغسطس", Value = 29},
                                new LineChartModel() { Text = "سبتمبر", Value = 23},
                                new LineChartModel() { Text = "أكتوبر", Value = 19},
                                new LineChartModel() { Text = "نوفمبر", Value = 18},
                                new LineChartModel() { Text = "ديسمبر", Value = 14}
                }
            });

            return result;
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
