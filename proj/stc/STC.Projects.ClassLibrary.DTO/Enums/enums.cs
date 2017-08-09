public enum NotificationMessageType
{
    Violation = 100,
    Incident = 200,
    Events = 300,
    CurrentlyHandling = 400,
    UserControl = 500,
    Handled = 600,
    NotHandled = 700
};

public enum SOPSources
{
    Violation = 1,
    Incident = 2,
    Events = 3,
    Fog = 4,
    Sand = 5,
    Rain = 6,
    LandSlide = 7,
    WantedCar = 8,
    DetectedAccident = 9,
    TruckViolation = 13
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
    HighPriorityIncident = 3,
    Medium = 2,
    Low = 1,
    Fatality = 4
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

public enum MonthOfYear
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public enum PeriodType
{
    Daily = 1,
    Weekly = 2,
    Monthly = 3
}

public enum SupervisorNotificationStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}

public enum MediaTypes
{
    None = 0,
    Image = 1,
    Video = 2
}

public enum SMSLanguage
{
    Arabic = 1,
    English = 2
}