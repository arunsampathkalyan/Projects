using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IKPILayer" in both code and config file together.
    [ServiceContract]
    public interface IKPILayer
    {
        #region Trend Analysis

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationTypeTrendDaysOfWeek(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationTypeTrendWeekOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationTypeTrendMonthOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationTypeTrendQuarterOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentTrendDaysOfWeek(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentTrendWeekOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentTrendMonthOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentTrendQuarterOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsTrendDaysOfWeek(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsTrendWeekOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsTrendMonthOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsTrendQuarterOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationTrendDaysOfWeek(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationTrendWeekOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationTrendMonthOfYear(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationTrendQuarterOfYear(int startYear, int endYear);

        #endregion

        #region Region Wise Normal

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationRegionYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationRegionMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationRegionWeekly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationRegionDaily(DateTime WeekStartDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationRegionWiseYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationRegionWiseMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationRegionWiseWeekly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionWiseYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionWiseMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionWiseWeekly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionWiseYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionWiseMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionWiseWeekly(int year);

        #endregion

        #region Region Wise Comparison

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsRegionComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsRegionComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsRegionComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay);


        #endregion

        #region Statistical Normal

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsStatisticalYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsStaticsticalMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsStaticsticalWeekly(int year, MonthOfYear month);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsStatisticalDaily(DateTime weekStartDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsStatisticalYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsStaticsticalMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsStaticsticalWeekly(int year, MonthOfYear month);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsStatisticalDaily(DateTime weekStartDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsStatisticalYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsStaticsticalMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsStaticsticalWeekly(int year, MonthOfYear month);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsStatisticalDaily(DateTime weekStartDate);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsStatisticalYearly();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsStaticsticalMonthly(int year);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsStaticsticalWeekly(int year, MonthOfYear month);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsStatisticalDaily(DateTime weekStartDate);

        #endregion

        #region Statistical Comparison

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationsComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsComparisonDaily(DateTime firstDay, DateTime secondDay);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsComparisonYearly(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationsComparisonDaily(DateTime firstDay, DateTime secondDay);

        #endregion

        #region Target Analysis

        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationsTarget();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTruckViolationTarget();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetIncidentsTarget();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolationTarget();

        #endregion

        #region Main Dashboard
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetTotalInjuriesComparisonForDashboard(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetSevereAccidentsComparisonForDashboard(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetFatalitiesComparisonForDashboard(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetDangerousViolatorsComparisonForDashboard(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetAccidentsWithFatalitiesCountComparisonForDashboard(int startYear, int endYear);
        [OperationContract]
        [WebGet]
        List<CubeDetailsDTO> GetMainDashboard(string language);
        #endregion

        #region Fatality Target 2030
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetFatalitiyTarget();
        #endregion
    }
}
