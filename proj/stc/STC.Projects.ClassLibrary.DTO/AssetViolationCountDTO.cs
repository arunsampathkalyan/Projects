using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetViolationCountDTO
    {
        [DataMember]
        public int DayCount { get; set; }
        [DataMember]
        public int WeekCount { get; set; }
        [DataMember]
        public int MonthCount { get; set; }
        [DataMember]
        public int YearCount { get; set; }
    }
}
