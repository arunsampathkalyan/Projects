using System;
using System.Collections.Generic;
using System.ServiceModel;
using STC.Projects.ClassLibrary.DTO;
using System.ServiceModel.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceLayer
    {
        [OperationContract]
        [WebGet]
        string GetAssetLocation(int assetId, bool isArabic);
        [OperationContract]
        [WebGet]
        string GetAssetSerialNumber(long assetId);
        [OperationContract]
        [WebGet]
        List<KpiDTO> GetAllTargetsList();

        [OperationContract]
        [WebGet]
        List<KPITargetDTO> GetAllTargets();

        [OperationContract]
        [WebGet]
        bool UpdateKPITarget(string keyname, double newValue, int userId);

        [OperationContract]
        [WebGet]
        void AddToViolationQueue(string ViolationTypeName, int ViolationTypeId, string PlateNumber, string PlateKind, string PlateColor, string PlateSource, double Latitude, double Longitude, int VehicleType, bool IsInsideCity, int SpeedLimit, int SpeedTolerance, int CurrentSpeed, int TrafficCrossElapsedTimeSecs, string SerialNumber);

        [OperationContract]
        [WebGet]
        AssetViolationDetailsDTO GetAssetViolations(string originalIdent, string lang);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetAllAssets();

        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetDangerousVehicleViolations(DateTime? StartDateTime, DateTime? EndDateTime, string PlateNumber);

        [OperationContract]
        [WebGet]
        DangerousVehicleDetailsDTO GetDangerousVehicleDetailsByPlateNumber(string plateNumber, string lang);
        [OperationContract]
        [WebGet]
        bool AddNewEvent(string XmlToSend);
        [OperationContract]
        [WebGet]
        BusinessRulesDTO GetMessageBusinessRule(long messageId);
        [OperationContract]
        [WebGet]
        void AddWantedCarEventManualy(string plateNumber, string plateCategory, string plateSource, string plateColor, DateTime violationDate, string lang, string ruleName, string ruleId, double lat, double lon);
        [OperationContract]
        [WebGet]
        void AddWantedCarEvent(string plateNumber, DateTime violationDate, string lang, string ruleName, string ruleId);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationCountMonthAndCity();
        [OperationContract]
        [WebGet]
        decimal GetWantedCarRadius();
        [OperationContract]
        [WebGet]
        List<ViolationsGroupedByLocationsDTO> GetViolationsListGroupedByLocation(DateTime? StartDateTime = null, int? ViolationTypeId = null);
        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetUpdatedViolationsList(bool? IsNoticed = null);

        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetViolationListByVehicle(string plateNumber);

        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetViolationsListByDate(DateTime? StartDateTime, DateTime? EndDateTime);

        [OperationContract]
        [WebGet]
        List<IncidentsDTO> GetIncidentsListByDate(DateTime? StartDateTime, DateTime? EndDateTime);

        [OperationContract]
        [WebGet]
        List<ViolationsDetailsByLocationDTO> GetViolationsDetailsForLocation(string LocationCode, DateTime? StartDateTime = null, int? ViolationTypeId = null);

        [OperationContract]
        [WebGet]
        List<IncidentsDTO> GetActiveIncidentsList();

        [OperationContract]
        [WebGet]
        IncidentsDTO GetIncidentDetails(int IncidentId);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetAssetsList(int? AssetStatusId = null, int? AssetTypeId = null);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetAssetsListBySource(int? AssetStatusId = null, int? AssetTypeId = null, int? sourceId = null);

        [OperationContract]
        [WebGet]
        List<AssetLastStatusDTO> GetAssetsLastStatusList(bool? IsNoticed = null);

        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetPatrolsList(int? PatrolStatusId = null);

        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetAllPatrolsList();
        // TODO: Implement Events DOT and DB View
        [OperationContract]
        [WebGet]
        void GetEventsList();

        [OperationContract]
        [WebGet]
        List<PatrolStatusDimDTO> GetPatrolStatusList();

        [OperationContract]
        [WebGet]
        List<AssetTypeDimDTO> GetAssetTypesList();

        [OperationContract]
        [WebGet]
        List<AssetStatusDimDTO> GetAssetStatusList();

        [OperationContract]
        [WebGet]
        List<ViolationTypeDimDTO> GetViolationTypesList();

        [OperationContract]
        [WebGet]
        List<ViolationsCountPerDayOfWeekDTO> GetViolationsCountPerDayOfWeek();

        [OperationContract]
        [WebGet]
        List<ViolationsCountPerDayOfWeekAndHourDTO> GetViolationsCountPerDayOfWeekAndHour();

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByType();

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByTypePerDayOfWeek(string dayofWeek);

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByTypeDTO> GetViolationsCountGroupedByTypePerDayOfWeekAndHour(string dayofWeek, int hour);

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocation();

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocationPerDayOfWeek(string dayofWeek);

        [OperationContract]
        [WebGet]
        List<ViolationsCountGroupedByLocationDTO> GetViolationsCountGroupedByLocationPerDayOfWeekAndHour(string dayofWeek, int hour);

        [OperationContract]
        [WebGet]
        int GetMaxNotificationsCount();

        [OperationContract]
        [WebGet]
        IncidentDetailsDTO GetIncidentFullDetails(int IncidentId);

        [OperationContract]
        [WebGet]
        ViolationDetailsDTO GetViolationDetailsByAsset(string LocationCode, DateTime? StartDateTime = null, int? ViolationTypeId = null);

        [OperationContract]
        [WebGet]
        PatrolOfficersDetailsDTO GetPatrolDetails(int PatrolId);

        [OperationContract]
        [WebGet]
        bool SaveSendControlToUsers(string xmlToSend, List<string> Usernames);

        [OperationContract]
        [WebGet]
        List<MessageTypeSOPDTO> GetMessageTypeSOP(int MessageType);
        [OperationContract]
        [WebGet]
        List<MessageTypeSOPDTO> GetMessageTypeSOPs(int MessageType, int MessageId);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetNearByTowersByLatLon(double longitude, double latitude);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetNearByRadarsByLatLon(double longitude, double latitude);

        [OperationContract]
        [WebGet]
        List<AssetsViewDTO> GetNearByCamerasByLatLon(double longitude, double latitude);

        [OperationContract]
        [WebGet]
        List<TowerActionsDTO> GetAllTowerActions();

        [OperationContract]
        [WebGet]
        double GetRadiusForNearByAssets();

        [OperationContract]
        [WebGet]
        MessageTypeConvertOutput ConvertXML(string xml, int generalTypeId);

        [OperationContract]
        [WebGet]
        List<AssetsDetailsViewDTO> GetAllTowerCameras(long TowerId);

        [OperationContract]
        [WebGet]
        List<OfficersLastLocationViewDTO> GetOfficersList();

        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetNearByPatrolsByLatLon(double longitude, double latitude, int patrolsCount);

        [OperationContract]
        [WebGet]
        List<ViolationsHistoricalDTO> GetViolationHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType);

        [OperationContract]
        [WebGet]
        List<IncidentHistoricalDTO> GetIncidentHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType);

        [OperationContract]
        [WebGet]
        List<IncidentsDTO> GetIncidentsHistoricalListByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType);

        [OperationContract]
        [WebGet]
        IncidentsDTO GetIncidentHistoricalDetails(int IncidentId);

        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetViolationsHistorySearchByDate(DateTime? StartDateTime, DateTime? EndDateTime, string PlateNumber);
        [OperationContract]
        [WebGet]
        VehicleLiveTrackingDTO GetVehicleDetailsByPlateNumber(string plateNumer);
        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetViolationsDetailsByAsset(string serialNumber);
        [OperationContract]
        [WebGet]
        int GetViolationsCountByAsset(DateTime? StartDateTime, DateTime? EndDateTime, string serialNumber);
        [OperationContract]
        [WebGet]
        List<ViolationsGroupedByLocationsDTO> GetHistoricalViolationsListGroupedByLocation(DateTime? StartDateTime, DateTime? EndDateTime, int? ViolationTypeId);
        [OperationContract]
        [WebGet]
        List<string> GetViolationImageURLsById(long ViolationNotificationId);
        [OperationContract]
        [WebGet]
        List<Byte[]> GetViolationImagesById(long ViolationNotificationId);
        [OperationContract]
        [WebGet]
        string GetViolationVideoURLById(long ViolationNotificationId);
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationCountPerSpeedAndTime();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationPerMonthAndViolationType();
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetViolationPerVehicleTypeAndDayOfWeek();
        [OperationContract]
        [WebGet]
        bool ChangeNotificationStatus(long NotificationId, int NewStatus, int UserId);
        [OperationContract]
        [WebGet]
        List<ViolationNotificationDTO> GetLatestViolationNotification();
        [OperationContract]
        [WebGet]
        List<IncidentsDTO> GetLatestIncidents();
        [OperationContract]
        [WebGet]
        List<UserUserControlDTO> GetLatestEvents(int UserId);
        [OperationContract]
        [WebGet]
        bool UpdateAssetValue(long AssetId, string NewValue);
        [OperationContract]
        [WebGet]
        bool SaveSOPNotificationLog(int sopStepId, long notificationId, int? sopCommandId, int userId, string previousValue, string currentValue);

        [OperationContract]
        [WebGet]
        void AddTruckViolationEvent(string plateNumber, DateTime violationDate, double lat, double lon, string lang, string assetSerial);
        [OperationContract]
        [WebGet]
        List<BusinessRulesDTO> GetAllBusinessRules(bool IsAcivatedOnly);
        [OperationContract]
        [WebGet]
        bool SaveBusinessRule(BusinessRulesDTO businessRule);
        [OperationContract]
        [WebGet]
        BusinessRulesDTO GetBusinessRuleByID(int businessRuleId);
        [OperationContract]
        [WebGet]
        List<BusinessRulePriorityDTO> GetAllPriorities();
        [OperationContract]
        [WebGet]
        List<OverSpeedDTO> GetAllOverSpeed();
        [OperationContract]
        [WebGet]
        List<TrafficCrossDTO> GetAllTrafficCrossTimes();
        [OperationContract]
        [WebGet]
        List<VehicleTypeDTO> GetAllVehicleTypes();
        [OperationContract]
        [WebGet]
        List<KpiDTO> GetViolationKPIs();
        [OperationContract]
        [WebGet]
        List<KpiDTO> GetAccidentKPIs();
        [OperationContract]
        [WebGet]
        List<PatrolDTO> GetAssignedPatrols(long notificationId);
        [OperationContract]
        [WebGet]
        List<VehicleLiveTrackingDTO> GetAllWantedCarLiveTrack(string plateNumber, int maxHours);
        [OperationContract]
        [WebGet]
        bool ChangePatrolActivation(int patrolId, bool isDeleted);

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
        List<CubeDetailsDTO> GetMainDashboard(string language);
        #endregion

        #region Fatality Target 2030
        [OperationContract]
        [WebGet]
        List<CubeDTO> GetFatalitiyTarget();
        #endregion

        #region ViolationsCount
        [OperationContract]
        [WebGet]
        List<ViolationsCountForMapDTO> GetViolationsCountPerAsset(PeriodType type);

        #endregion

        #region Available Years
        [OperationContract]
        [WebGet]
        List<int> GetViolationsAvailableYears();
        [OperationContract]
        [WebGet]
        List<int> GetIncidentsAvailableYears();
        [OperationContract]
        [WebGet]
        DateTime GetIncidentFirstDate();
        #endregion

        #region SocialMedia
        [OperationContract]
        [WebGet]
        bool PublishToFacebook(string post, byte[] imageBytes);

        [OperationContract]
        [WebGet]
        bool PublishToTwitter(string post, byte[] imageBytes);

        #endregion

        #region Users and Roles

        [OperationContract]
        [WebGet]
        List<UsersDTO> GetAllUsersList();
        [OperationContract]
        [WebGet]
        bool AddUser(string userPassword, UsersDTO user);
        [OperationContract]
        [WebGet]
        bool UpdateUser(UsersDTO user);
        [OperationContract]
        [WebGet]
        bool DeactivateUser(int userId, bool isActive);
        [OperationContract]
        [WebGet]
        bool DeleteUser(UsersDTO user);
        [OperationContract]
        [WebGet]
        bool AddRank(RanksDTO Rank);
        [OperationContract]
        [WebGet]
        bool UpdateRank(RanksDTO Rank);
        [OperationContract]
        [WebGet]
        bool DeleteRank(RanksDTO Rank);
        [OperationContract]
        [WebGet]
        bool AddFeature(FeaturesDTO Feature);
        [OperationContract]
        [WebGet]
        bool UpdateFeature(FeaturesDTO Feature);
        [OperationContract]
        [WebGet]
        bool DeleteFeature(FeaturesDTO Feature);
        [OperationContract]
        [WebGet]
        List<RanksDTO> GetRanksList();
        [OperationContract]
        [WebGet]
        List<FeaturesDTO> GetFeaturesList();
        [OperationContract]
        [WebGet]
        bool AddUserRole(UserRolesDTO role);
        [OperationContract]
        [WebGet]
        bool UpdateUserRole(UserRolesDTO role);
        [OperationContract]
        [WebGet]
        bool DeleteUserRole(UserRolesDTO role);
        [OperationContract]
        [WebGet]
        List<UserRolesDTO> GetUserRolesList();
        [OperationContract]
        [WebGet]
        UsersDTO GetUserById(int userId);
        [OperationContract]
        [WebGet]
        UsersDTO Login(string Username, string Password);
        [OperationContract]
        [WebGet]
        int GetSupervisorId();
        [OperationContract]
        [WebGet]
        string SendNotification(List<string> deviceRegIds, string message);
        [OperationContract]
        [WebGet]
        List<PublicUserDTO> GetActivePublicUsers();
        #endregion

        #region Supervisor Notification

        [OperationContract]
        [WebGet]
        bool SaveSupervisorNotification(SupervisorNotificationDTO supervisorNotification);
        [OperationContract]
        [WebGet]
        SupervisorNotificationDTO GetSupervisorNotificationById(int supervisorNotificationId);
        [OperationContract]
        [WebGet]
        List<SupervisorNotificationDTO> GetSupervisorNotificationsByUserId(int userId);

        [OperationContract]
        [WebGet]
        bool SetSupervisorNotificationNoticed(long notificationId, bool isNoticed = true);

        #endregion

        #region DangerousVehicleSearch
        [OperationContract]
        [WebGet]
        List<CorrelationMessagesLogDTO> GetCorrelationLogByVehicleDetails(string plateNumber, string plateColor, string plateSource, string plateKind);
        [OperationContract]
        [WebGet]
        List<CorrelationMessagesLogDTO> GetCorelationsLogByBusinessRule(int businessRuleId);
        [OperationContract]
        [WebGet]
        bool IsDangerousVehicleActive(string plateNumber, string plateColor, string plateSource, string plateKind);
        [OperationContract]
        [WebGet]
        void DeactivateDangeriousVehicle(string plateNumber, string plateColor, string plateSource, string plateKind);
        [OperationContract]
        [WebGet]
        void DeactivateDangerousViolatorList(List<PlateDetailsDTO> vehicles);
        #endregion
    }
}