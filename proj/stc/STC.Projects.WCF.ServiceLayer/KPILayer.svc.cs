using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    public class KPILayer : IKPILayer
    {
        private string _analysisViolationConnectionString = ConfigurationManager.AppSettings["AnalysisViolationConnectionString"];
        private string _analysisTruckViolationConnectionString = ConfigurationManager.AppSettings["AnalysisTruckViolationConnectionString"];
        private string _analysisIncidentConnectionString = ConfigurationManager.AppSettings["AnalysisIncidentConnectionString"];
        private string _analysisDangerousViolationConnectionString = ConfigurationManager.AppSettings["AnalysisDangerousViolationConnectionString"];
        #region KPI Region

        [AspNetCacheProfile("CacheForTargets")]
        [WebGet()]
        public List<KPITargetDTO> GetAllTargets()
        {
            var res = new List<KPITargetDTO>();
            res = new KPITargetsDAL().GetAllTargets();
            return res;
        }



        #region Violation KPI
        public List<KpiDTO> GetViolationKPIs()
        {
            var res = new List<KpiDTO>();
            res.AddRange(GetViolationKPIPerViolationType());
            res.AddRange(GetViolationKPIPerVehicleType());
            return res;
        }

        public List<KpiDTO> GetViolationKPIPerVehicleType()
        {
            var res = new List<KpiDTO>();
            res.Add(GetCarViolationKPI());
            res.Add(GetTruckViolationKPI());
            return res;
        }

        private KpiDTO GetCarViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVEHICLETYPE_CAR",
                CurrentValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year, "Car"),
                PreviousValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year - 1, "Car"),
                LabelValueArabic = "مخالفات السيارات",
                LabelValueEnglish = "Car Violation",
                Percentage = GetTargetPercentage("TARGET_PERVEHICLETYPE_CAR")
            };
            return res;
        }

        private KpiDTO GetTruckViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVEHICLETYPE_TRUCK",
                CurrentValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year, "Truck"),
                PreviousValue = GetViolationCountPerYearPerVehicleType(DateTime.Now.Year - 1, "Truck"),
                LabelValueArabic = "مخالفات الشاحنات",
                LabelValueEnglish = "Truck Violation",
                Percentage = GetTargetPercentage("TARGET_PERVEHICLETYPE_TRUCK")
            };
            return res;
        }


        public List<KpiDTO> GetViolationKPIPerViolationType()
        {
            var res = new List<KpiDTO>();
            res.Add(GetSpeedViolationKPI());
            res.Add(GetRedLightViolationKPI());
            return res;
        }

        public bool UpdateKPITarget(string keyname, double newValue, int userId)
        {
            return new KPITargetsDAL().UpdateTarget(keyname, newValue, userId);
        }

        private double GetTargetPercentage(string keyname)
        {
            double res = 0.00;
            var allTarget = GetAllTargets();
            if (allTarget != null)
            {
                var target = allTarget.FirstOrDefault(x => x.TargetName == keyname);
                if (target != null)
                {
                    res = target.TargetValue;
                }
            }
            return res;
        }

        public List<KpiDTO> GetAllTargetsList()
        {
            var list = new List<KpiDTO>();
            list.AddRange(GetViolationKPIs());
            list.AddRange(GetAccidentKPIs());
            return list;
        }

        public KpiDTO GetSpeedViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVIOLATIONTYPE_SPEED",
                CurrentValue = GetViolationCountPerYearPerType(DateTime.Now.Year, "SPEED"),
                PreviousValue = GetViolationCountPerYearPerType(DateTime.Now.Year - 1, "SPEED"),
                LabelValueArabic = "أعداد مخالفات السرعة",
                LabelValueEnglish = "Speed Violation",
                Percentage = GetTargetPercentage("TARGET_PERVIOLATIONTYPE_SPEED")
            };
            return res;
        }

        public KpiDTO GetRedLightViolationKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_PERVIOLATIONTYPE_RED",
                CurrentValue = GetViolationCountPerYearPerType(DateTime.Now.Year, "RED"),
                PreviousValue = GetViolationCountPerYearPerType(DateTime.Now.Year - 1, "RED"),
                LabelValueArabic = "أعداد مخالفات الاشارة الحمراء",
                LabelValueEnglish = "Red Light Violation",
                Percentage = GetTargetPercentage("TARGET_PERVIOLATIONTYPE_RED")
            };
            return res;
        }

        public double GetViolationCountPerYearPerType(int year, string type)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationCountPerYearPerType(year, type);
        }

        public double GetViolationCountPerYearPerVehicleType(int year, string vehicleType)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationCountPerYearAndVehicleType(year, vehicleType);
        }
        # endregion

        # region Accident KPI
        public List<KpiDTO> GetAccidentKPIs()
        {
            var res = new List<KpiDTO>();
            res.Add(GetFatalityKPI());
            res.AddRange(GetAccidentKPIPerAccidentTypes());
            res.Add(GetVehicleProblemAccidentsKPI());
            //res.AddRange(GetAccidentKPIPerRegions());
            res.Add(GetFatalInjuriesKPI());
            res.Add(GetRedLightAccidentsKPI());
            res.Add(GetRedLightFatalityKPI());
            return res;
        }

        public KpiDTO GetVehicleProblemAccidentsKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREASON_VEHICLEPROBLEM",
                CurrentValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year, "VehicleProblem"),
                PreviousValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year - 1, "VehicleProblem"),
                LabelValueArabic = "حوادث بسبب اعطال المركبة",
                LabelValueEnglish = "Accidents because of vehicles problems",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREASON_VEHICLEPROBLEM")
            };
            return res;
        }

        public KpiDTO GetRedLightAccidentsKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREASON_REDLIGHT",
                CurrentValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year, "RedLight"),
                PreviousValue = GetAccidentCountPerYearAndAccidentReason(DateTime.Now.Year - 1, "RedLight"),
                LabelValueArabic = "حوادث بسبب تخطي الاشارة الحمراء",
                LabelValueEnglish = "Accidents because of violating red light",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREASON_REDLIGHT")
            };
            return res;
        }

        public List<KpiDTO> GetAccidentKPIPerRegions()
        {
            var res = new List<KpiDTO>();
            res.Add(GetFatalityKPIInAbuDhabi());
            res.Add(GetFatalityKPIInWestren());
            res.Add(GetFatalityKPIInAlAin());
            return res;
        }

        public KpiDTO GetFatalityKPIInAbuDhabi()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_ABUDHABI",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "Abu Dhabi Island"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "Abu Dhabi Island"),
                LabelValueArabic = "الوفيات في جزيرة ابو ظبي",
                LabelValueEnglish = "Fatalities on Abu Dhabi island",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_ABUDHABI")
            };
            return res;
        }

        public KpiDTO GetFatalityKPIInWestren()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_WESTERN",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "Western"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "Western"),
                LabelValueArabic = "الوفيات في القطاع الغربي",
                LabelValueEnglish = "Fatalities on Western Region",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_WESTERN")
            };
            return res;
        }

        public KpiDTO GetFatalityKPIInAlAin()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERREGION_ALAIN",
                CurrentValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year, "AlAin"),
                PreviousValue = GetFatalityCountPerYearAndRegion(DateTime.Now.Year - 1, "AlAin"),
                LabelValueArabic = "الوفيات في العين",
                LabelValueEnglish = "Fatalities on Al-Ain",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERREGION_ALAIN")
            };
            return res;
        }

        public List<KpiDTO> GetAccidentKPIPerAccidentTypes()
        {
            var res = new List<KpiDTO>();
            res.Add(GetInjuriesAccidentKPI());
            res.Add(GetFatalAccidentKPI());
            res.Add(GetNormalAccidentKPI());
            return res;
        }

        public KpiDTO GetInjuriesAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_INJURIES",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "InjAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "InjAcc"),
                LabelValueArabic = "حوادث اصابات",
                LabelValueEnglish = "Injuries accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_INJURIES")
            };
            return res;

        }

        public KpiDTO GetFatalAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_FATAL",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "FatAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "FatAcc"),
                LabelValueArabic = " نوع حوادث الوفيات",
                LabelValueEnglish = "Fatal accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_FATAL")
            };
            return res;

        }

        public KpiDTO GetNormalAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_NORMAL",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "NorAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "NorAcc"),
                LabelValueArabic = "حوادث سير بسيطة",
                LabelValueEnglish = "Normal accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_NORMAL")
            };
            return res;

        }

        public KpiDTO GetFatalityKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYACCIDENTS",
                CurrentValue = GetFatalityCountPerYear(DateTime.Now.Year),
                PreviousValue = GetFatalityCountPerYear(DateTime.Now.Year - 1),
                LabelValueArabic = "حوادث الوفيات",
                LabelValueEnglish = "Fatality accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYACCIDENTS")
            };
            return res;
        }

        public KpiDTO GetFatalInjuriesKPI()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalInjuriesKPI();
        }

        public KpiDTO GetRedLightFatalityKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYREDLIGHTACCIDENTS",
                CurrentValue = GetFatalityCountPerYearAndReason(DateTime.Now.Year, "RedLight"),
                PreviousValue = GetFatalityCountPerYearAndReason(DateTime.Now.Year - 1, "RedLight"),
                LabelValueArabic = "وفيات حوادث تخطي الاشارة الحمراء",
                LabelValueEnglish = "Fatalities of red light violation",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYREDLIGHTACCIDENTS")
            };
            return res;
        }

        private double GetFatalityCountPerYear(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYear(year);
        }

        private double GetFatalInjuiresCountPerYear(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalInjuriesCountPerYear(year);
        }

        private double GetAccidentCountPerYearAndAccidentType(int year, string accidentType)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetAccidentCountPerYearAndAccidentType(year, accidentType);
        }

        private double GetAccidentCountPerYearAndAccidentReason(int year, string accidentReason)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetAccidentCountPerYearAndAccidentReason(year, accidentReason);
        }

        private double GetFatalityCountPerYearAndRegion(int year, string region)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYearAndRegion(year, region);
        }

        private double GetFatalityCountPerYearAndReason(int year, string reason)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalityCountPerYearAndReason(year, reason);
        }

        # endregion
        #endregion

        #region Trend Analysis

        public List<CubeDTO> GetViolationTypeTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetViolationTypeTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationTypeTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentTrendQuarterOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendDaysOfWeek(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendDaysOfWeek(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendWeekOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendWeekOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendMonthOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendMonthOfYear(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationTrendQuarterOfYear(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTrendQuarterOfYear(startYear, endYear);
        }

        #endregion

        #region Region Wise Normal

        public List<CubeDTO> GetViolationsRegionWiseYearly()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseYearly();
        }

        public List<CubeDTO> GetViolationsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetViolationsRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetViolationsRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetTruckViolationTarget()
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationTarget();
        }

        public List<CubeDTO> GetTruckViolationRegionYearly()
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseYearly();
        }

        public List<CubeDTO> GetTruckViolationRegionMonthly(int year)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionWeekly(int year)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);

            return cubeDAL.GetTruckViolationRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetTruckViolationRegionDaily(DateTime WeekStartDate)
        {
            CubeDAL cubeDAL = new CubeDAL(_analysisTruckViolationConnectionString);
            DateTime endDate = WeekStartDate.AddDays(7);
            return cubeDAL.GetTruckViolationRegionWiseDaily(WeekStartDate, endDate);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseYearly()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseYearly();
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationRegionWiseWeekly(year);
        }

        public List<CubeDTO> GetIncidentsRegionWiseYearly()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseYearly();
        }

        public List<CubeDTO> GetIncidentsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseDaily(WeekStartDate, WeekEndDate);
        }

        public List<CubeDTO> GetIncidentsRegionWiseMonthly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseMonthly(year);
        }

        public List<CubeDTO> GetIncidentsRegionWiseWeekly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionWiseWeekly(year);
        }

        #endregion

        #region Region Wise Comparison


        public List<CubeDTO> GetTruckViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetIncidentsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsRegionComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsRegionComparisonDaily(firstDay, secondDay);
        }

        #endregion

        #region Statistical Normal

        public List<CubeDTO> GetTruckViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetTruckViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisTruckViolationConnectionString).GetTruckViolationsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetIncidentsStatisticalYearly()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStatisticalYearly();
        }

        public List<CubeDTO> GetIncidentsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetIncidentsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetIncidentsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsStatisticalDaily(weekStartDate, weekEndDate);

        }

        public List<CubeDTO> GetDangerousViolationsStatisticalYearly()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStatisticalYearly();
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalMonthly(int year)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStaticsticalMonthly(year);
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStaticsticalWeekly(year, month);
        }

        public List<CubeDTO> GetDangerousViolationsStatisticalDaily(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(7);
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsStatisticalDaily(weekStartDate, weekEndDate);
        }

        #endregion

        #region Statistical Comparison

        public List<CubeDTO> GetTruckViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetTruckViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetTruckViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetTruckViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetTruckViolationsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetIncidentsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetIncidentsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetIncidentsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetIncidentsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsComparisonDaily(firstDay, secondDay);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonYearly(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonYearly(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonMonthly(year, startMonth, endMonth);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonWeekly(firstWeek, secondWeek);
        }

        public List<CubeDTO> GetDangerousViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationsComparisonDaily(firstDay, secondDay);
        }

        #endregion

        #region Target Analysis

        public List<CubeDTO> GetDangerousViolationTarget()
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolationTarget();
        }
        public List<CubeDTO> GetIncidentsTarget()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetIncidentsTarget();
        }
        public List<CubeDTO> GetViolationsTarget()
        {
            return new CubeDAL(_analysisViolationConnectionString).GetViolationsTarget();
        }

        #endregion

        #region Main Dashboard

        public List<CubeDTO> GetTotalInjuriesComparisonForDashboard(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetTotalInjuriesComparisonForDashboard(startYear, endYear);
        }

        public List<CubeDTO> GetSevereAccidentsComparisonForDashboard(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetSevereAccidentsComparisonForDashboard(startYear, endYear);
        }

        public List<CubeDTO> GetFatalitiesComparisonForDashboard(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalitiesComparisonForDashboard(startYear, endYear);
        }

        public List<CubeDTO> GetDangerousViolatorsComparisonForDashboard(int startYear, int endYear)
        {
            return new CubeDAL(_analysisDangerousViolationConnectionString).GetDangerousViolatorsComparisonForDashboard(startYear, endYear);
        }

        public List<CubeDTO> GetAccidentsWithFatalitiesCountComparisonForDashboard(int startYear, int endYear)
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetAccidentsWithFatalitiesCountComparisonForDashboard(startYear, endYear);
        }

        public List<CubeDetailsDTO> GetMainDashboard(string language)
        {
            List<CubeDetailsDTO> list = new List<CubeDetailsDTO>();

            list.Add(new CubeDAL(_analysisViolationConnectionString).GetTotalViolationsForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalFatalitiesForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalSlightInjuriesForMainDashboard(language));
            list.Add(new CubeDAL(_analysisIncidentConnectionString).GetTotalSevereInjuriesForMainDashboard(language));

            return list;
        }

        #endregion

        #region Fatality Target 2030

        public List<CubeDTO> GetFatalitiyTarget()
        {
            return new CubeDAL(_analysisIncidentConnectionString).GetFatalitiyTarget();
        }

        #endregion
    }
}
