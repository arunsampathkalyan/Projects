namespace STC.Projects.ClassLibrary.Common.Enums
{
    public enum MarkerType
    {
        Incident = 1,
        Violation = 2,
        Patrols = 3,
        Assets = 4,
        ViolationsNotifications = 5,
        IncidentNotification = 6,
        Fog = 7,
        DetectedAccident = 8,
        Officers = 9,
        WantedCar = 10,
        WantedCarTracking = 11,
        TruckViolation = 12,
        CarPlateNumber = 13,
        Cluster = 14,
        RecommendedPatrol = 15,
        SuggestedPatrol = 16,
        LastWantedCarPlace = 17

    };

    public enum SystemPages
    {
        OperationTest = 1,
        Dashboard = 6,
        AssetsPage = 7,
        KPIDashboardPage = 8,
        PatrolPage = 9,
        TrraficPage = 10,
        ViolationPage = 11,
        IncidentPage = 12,
        ViolationsKPIPage = 13,
        SelectChartCriteria = 15,
        IncidentsKPIPage = 10013,
        TruckPermissionsPage = 20013,
        WorkZonesPage = 20014,
        LandingPage = 20016,
        DangerousViolator = 20020,
        AdminPatrolPage = 20022,
        AdminDangerousViolatorUserControl = 20023,
        AdminBusinessRule = 20017,
        AdminKPIAdministrationPage = 20024,
        AdminMessageTemplatePage = 20025
    }

    public enum LayerTypeEnum
    {
        Violations, Incidents, Patrols, Assets, AssetsSmartTowers, AssetsRedLights, AssetsSpeed, Notifications, Officers, Traffic, HistoricalViolations, Sugestions
    }

    public enum PatrolStatusEnum
    {
        Available = 1,
        Dispatched = 2,
        Acknowledged = 3,
        OnTheWay = 4,
        ArrivedToLocation = 5,
        AssignedToEvent = 6,
        UnderMaintenance = 7
    };

    public enum AssetStatusEnum
    {
        Working = 1,
        NotWorking = 2,
        UnderMaintenance = 4
    };


    public enum AssetTypesEnum
    {
        EkinRedLightCamera = 2,
        VitronicRadars = 3,
        VitronicMobileRadars = 4,
        SpeedGuns = 5,
        DOTcounters = 6,
        SmartTowers = 7
    };

    public enum IncidentTypeEnum
    {
        HighPriorityIncident = 1,
        Medium = 2,
        Low = 3
    };

    public enum ViolationTypesEnum
    {
        Speed = 1,
        RedLight = 2,
        Tailgating = 3,
        RouteDeviation = 4,
        ShoulderSpeeding = 5,
        DirectionReversing = 6,
        P2P = 7
    };

    public enum ChartFeltrationType
    {
        Legend, Category, Non
    }
}