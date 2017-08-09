using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class KpiDTO
    {
        public string TargetName { get; set; } // The unique key of percentage On DB. not to be shown to the user
        public double CurrentValue { get; set; } // Absolute current value of this year
        public double PreviousValue { get; set; } // Absolute previous value of this year
        public double TargetValue { get { return Math.Round((Percentage) * PreviousValue); } set { TargetValue = value; } }  // Absolute target value of this year. Calculated (1-Percentage) * PreviousValue
        public double Percentage { get; set; }  //The target degree percentage of previous year which is saved in DB (ya3ny 2d eh 3awz a2l 3an el sana elly fatet)
        public string LabelValueEnglish { get; set; }  //KPI English Name
        public string LabelValueArabic { get; set; } //KPI Arabic Name
        public double ActualPercentage { get { if (PreviousValue == 0) return 0; else return ((PreviousValue - CurrentValue) / PreviousValue) * 100; } set { ActualPercentage = value; } } //Percentage of improvment from last year
      
    }
}
