using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.KPIDashboard.VM
{
    public class KPIDashboardVM
    {
        public ObservableCollection<chartmodel> Traffic1 { set; get; }
        public ObservableCollection<chartmodel> Traffic2 { set; get; }

        public ObservableCollection<chartmodel> Accident { set; get; }

        public ObservableCollection<chartmodel> AssetsNotWorking { set; get; }
        public ObservableCollection<chartmodel> AssetsWorking { set; get; }

        public ObservableCollection<chartmodel> ResponseTime { set; get; }

        public ObservableCollection<chartmodel> Violation { set; get; }

        public KPIDashboardVM()
        {
            //Traffic Denstiy Chart
            #region
            Traffic1 = new ObservableCollection<chartmodel>();
            Traffic1.Add(null);
            Traffic1.Add(new chartmodel() { Text = "السبت", Value = 14 });
            Traffic1.Add(new chartmodel() { Text = "الأحد", Value = 7 });
            Traffic1.Add(new chartmodel() { Text = "الأثنين", Value = 9 });
            Traffic1.Add(new chartmodel() { Text = "الثلاثاء", Value = 14 });
            Traffic1.Add(new chartmodel() { Text = "الأربعاء", Value = 11 });
            Traffic1.Add(new chartmodel() { Text = "الخميس", Value = 13 });
            Traffic1.Add(new chartmodel() { Text = "الجمعة", Value = 6 });

            Traffic2 = new ObservableCollection<chartmodel>();
            Traffic2.Add(null);
            Traffic2.Add(new chartmodel() { Text = "السبت", Value = 19 });
            Traffic2.Add(new chartmodel() { Text = "الأحد", Value = 20 });
            Traffic2.Add(new chartmodel() { Text = "الأثنين", Value = 30 });
            Traffic2.Add(new chartmodel() { Text = "الثلاثاء", Value = 25 });
            Traffic2.Add(new chartmodel() { Text = "الأربعاء", Value = 15 });
            Traffic2.Add(new chartmodel() { Text = "الخميس", Value = 18 });
            Traffic2.Add(new chartmodel() { Text = "الجمعة", Value = 16 });
            #endregion

            //Accident Chart
            #region
            Accident = new ObservableCollection<chartmodel>();
            Accident.Add(new chartmodel() { Text = "السرعة الزائدة عن الحد", Value = 150 });
            Accident.Add(new chartmodel() { Text = "السير عكس الاتجاه ", Value = 45 });
            Accident.Add(new chartmodel() { Text = "كسر إشارة المرور", Value = 250 });
            Accident.Add(new chartmodel() { Text = "عبور مشاة مخالف", Value = 21 });
            Accident.Add(new chartmodel() { Text = "االتقارب بين سيارة واخري ", Value = 9 });
            Accident.Add(new chartmodel() { Text = "عدم إرتداء حزام الأمان", Value = 25 });
            Accident.Add(new chartmodel() { Text = "إستخدام الهاتف المتحرك أثناء القيادة", Value = 18 });

            #endregion

            //Assets Chart
            #region
            AssetsNotWorking = new ObservableCollection<chartmodel>();
            AssetsNotWorking.Add(new chartmodel() { Text = "الردارات العامة", Value = 4 });
            AssetsNotWorking.Add(new chartmodel() { Text = "الأبراج الذكية", Value = 7 });
            AssetsNotWorking.Add(new chartmodel() { Text = "الكاميرات الذكية", Value = 9 });
            AssetsNotWorking.Add(new chartmodel() { Text = "موبيل ردارات", Value = 14 });


            AssetsWorking = new ObservableCollection<chartmodel>();
            AssetsWorking.Add(new chartmodel() { Text = "الردارات العامة", Value = 14 });
            AssetsWorking.Add(new chartmodel() { Text = "الأبراج الذكية", Value = 17 });
            AssetsWorking.Add(new chartmodel() { Text = "الكاميرات الذكية", Value = 19 });
            AssetsWorking.Add(new chartmodel() { Text = "موبيل ردارات", Value = 14 });

            #endregion

            //Respons Time Chart
            #region
            ResponseTime = new ObservableCollection<chartmodel>();
            ResponseTime.Add(new chartmodel() { Text = "سرعة الإستجابة", Value = 85 });
            ResponseTime.Add(new chartmodel() { Text = "تأخر الإستجابة", Value = 15 });
            #endregion

            //Violation Chart
            #region
            Violation = new ObservableCollection<chartmodel>();
            Violation.Add(new chartmodel() { Text = "المخالفات", Value = 50 });
            Violation.Add(new chartmodel() { Text = "حوادث المركبات", Value = 20 });
            Violation.Add(new chartmodel() { Text = "حوادث مميتة", Value = 5 });
            Violation.Add(new chartmodel() { Text = "مخالفات السرعة", Value = 25 });
            #endregion
        }

    }
}
