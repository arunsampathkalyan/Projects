﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Reactive.Threading.Tasks;
using STC.Projects.WPFControlLibrary.LandingPage.Model;
using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using System.Windows;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class DangerousViolationMonthlyStatisticsViewModel : System.ComponentModel.INotifyPropertyChanged
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
                        GetViolationData();
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
                YearValueColl = new ObservableCollection<int>(Utility.GetRecentYearsList(DateTime.Now.Year - 2010));

            if (FromMonthValueColl == null)
                FromMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (ToMonthValueColl == null)
                ToMonthValueColl = new ObservableCollection<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


        }
        public DangerousViolationMonthlyStatisticsViewModel()
        {
            //IncidentsCollection = GetSampleData();
            LoadBasicData();
            GetViolationData();
        }

        ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }
        public void GetViolationData()
        {
            //var callTask = client.GetViolationTypeTrendMonthOfYearAsync(FromYearValue, ToYearValue);
            var callTask = client.GetDangerousViolationsComparisonMonthlyAsync(YearValue, (ServiceLayerReference.MonthOfYear)FromMonthValue + 1, (ServiceLayerReference.MonthOfYear)ToMonthValue + 1);
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