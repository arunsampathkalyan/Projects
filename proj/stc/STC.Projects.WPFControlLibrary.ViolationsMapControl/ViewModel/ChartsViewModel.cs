using STC.Projects.WPFControlLibrary.ViolationsMapControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.ViewModel
{
    public class ChartsViewModel
    {
        public ObservableCollection<LineChartdata> LineChartCollection { get; set; }

        public ObservableCollection<LineChartModel> PieChartCollection { set; get; }
        public ChartsViewModel()
        {
            LineChartCollection = GetSampleData();

            PieChartCollection = new ObservableCollection<LineChartModel>();
            PieChartCollection.Add(new LineChartModel() { Text = "السرعة الزائدة", Value = 25 });
            PieChartCollection.Add(new LineChartModel() { Text = "تجاوز الإشارة الضوئية الحمراء", Value = 15 });
            PieChartCollection.Add(new LineChartModel() { Text = "عدم ترك مسافة كافية", Value = 25 });
            PieChartCollection.Add(new LineChartModel() { Text = "أستخدام الجوال اثناء القيادة", Value = 35 });
        }

        private ObservableCollection<LineChartdata> GetSampleData()
        {
            var result = new ObservableCollection<LineChartdata>();

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52d726")),

                LegendName = "محمد بن احمد ",

                Violations = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 5},
                                new LineChartModel() { Text = "فبراير", Value = 1},
                                new LineChartModel() { Text = "مارس", Value = 3},
                                new LineChartModel() { Text = "أبريل", Value = 8},
                                new LineChartModel() { Text = "مايو", Value = 12},
                                new LineChartModel() { Text = "يونيو", Value = 17},
                                new LineChartModel() { Text = "يوليو", Value = 19},
                                new LineChartModel() { Text = "أغسطس", Value = 18},
                                new LineChartModel() { Text = "سبتمبر", Value = 14},
                                new LineChartModel() { Text = "أكتوبر", Value = 19},
                                new LineChartModel() { Text = "نوفمبر", Value = 18},
                                new LineChartModel() { Text = "ديسمبر", Value = 14}
                }
            });

            return result;
        }
    }

    public class LineChartdata
    {
        public Brush LineColor { get; set; }
        public string LegendName { get; set; }
        public ObservableCollection<LineChartModel> Violations { set; get; }
    }
}