
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class ViolationsCountChartByViolationTypePerDayOfWeekControlMessage { public string DayOfWeek { set; get; } }
    public class ViolationsCountChartByViolationTypePerDayOfWeekAndHourControlMessage { public string DayOfWeek { set; get; } public int hour { set; get; } }
    public class ViolationsCountChartByLocationCodePerDayOfWeekControlMessage { public string DayOfWeek { set; get; } }
    public class ViolationsCountChartByLocationCodePerDayOfWeekAndHourControlMessage { public string DayOfWeek { set; get; } public int hour { set; get; } }
    public class ViolationsCountChartResetControlMessage { }
}
