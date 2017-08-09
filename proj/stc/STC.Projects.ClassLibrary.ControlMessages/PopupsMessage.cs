using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class PatrolOfficersDetailsClientDTO
    {
        public PatrolOfficersDetailsClientDTO()
        {
            Officers = new List<StaffPatrolModelClient>();
        }

        public string PatrolAllocation { get; set; }

        public string PatrolCode { get; set; }

        public string PatrolPlateNumber { get; set; }

        public bool IsAvailable { get; set; }

        public string StatusArabic { get { return IsAvailable ? "متاحة" : "عليها بلاغ"; } set { } }

        public string StatusEnglish { get { return IsAvailable ? "Available" : "Busy"; } set { } }

        public List<StaffPatrolModelClient> Officers { get; set; }

        public bool IsPatrol { get; set; }
    }

    public class StaffPatrolModelClient
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } set { } }

        public string ImagePath { get; set; }

        public byte[] ImageArray { get; set; }

        public string MilitaryNumber { get; set; }
    }

    public class IncidentsClientDTO
    {
        public IncidentsClientDTO()
        {

        }

        public long IncidentId { get; set; }


        public string IncidentNumber { get; set; }


        public int IncidentTypeId { get; set; }


        public string IncidentTypeName { get; set; }


        public int PriorityId { get; set; }


        public string PriorityName { get; set; }


        public double? Latitude { get; set; }


        public double? Longitude { get; set; }
        //public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }


        public string IncidentAddress { get; set; }


        public string CallerName { get; set; }


        public string CallerNumber { get; set; }


        public string CallerAddress { get; set; }


        public string CallerLanguage { get; set; }


        public int? CallTakerId { get; set; }


        public string CallTakerName { get; set; }


        public int StatusId { get; set; }


        public string StatusName { get; set; }


        public int? DispatcherId { get; set; }


        public string DispatcherName { get; set; }


        public DateTime? CreatedTime { get; set; }


        public DateTime? DispatcheTime { get; set; }


        public DateTime? ArrivedTime { get; set; }


        public DateTime? EndTime { get; set; }


        public bool IsNoticed { get; set; }


        public string MessageText { get; set; }


        public string BackgroundColor { get; set; }


        public string MessageId { get; set; }


        public bool IsCritical { get; set; }


        public long NotificationId { get; set; }


        public int AreaId { get; set; }

        public int CauseId { get; set; }

        public int CityId { get; set; }

        public int CrashSeverityId { get; set; }

        public int CrashTypeId { get; set; }

        public int EmirateId { get; set; }

        public int IntersectionId { get; set; }

        public int LightingId { get; set; }

        public int LocationId { get; set; }

        public int LocationTypeId { get; set; }

        public int PConditionId { get; set; }

        public int RoadTypeId { get; set; }

        public int WeatherId { get; set; }

        public int Speed { get; set; }

        public int LanesCount { get; set; }

        public int SlightInjuries { get; set; }

        public int MediumInjuries { get; set; }

        public int SevereInjuries { get; set; }

        public int Fatalities { get; set; }

        public int TotalInjuriesFatalities { get; set; }

        public string IncidentDescription { get; set; }

        public string LocationDescription { get; set; }

        public string ZoneDescription { get; set; }

        public string PoliceStation { get; set; }


        public string AreaName { get; set; }

        public string CauseName { get; set; }

        public string CityName { get; set; }

        public string CrashSeverityName { get; set; }

        public string CrashTypeName { get; set; }

        public string IntersectionName { get; set; }

        public string EmirateName { get; set; }

        public string LocationName { get; set; }

        public string LightingName { get; set; }

        public string RoadTypeName { get; set; }

        public string LocationTypeName { get; set; }

        public string WeatherName { get; set; }

        public string PConditionName { get; set; }

    }

    public class AssetViolationDetailsClientDTO
    {
        public string AssetCode { get; set; }

        public string AssetLocation { get; set; }

        public string AssetVendor { get; set; }

        public int AssetViolationCount { get; set; }

        public int AssetViolationCountMonth { get; set; }

        public int AssetViolationCountYearly { get; set; }

        public string AssetStatus { get; set; }

    }

    public class OpenPatrolTip
    {
        public PatrolOfficersDetailsClientDTO OriginalItem { get; set; }
        public string Tag { get; set; }
    }

    public class OpenIncidentTip
    {
        public IncidentsClientDTO OriginalItem { get; set; }
    }

    public class OpenAssetTip
    {
        public AssetViolationDetailsClientDTO OriginalItem { get; set; }
    }
    public class OpenSmartAssetTip
    {
        public AssetViolationDetailsClientDTO OriginalItem { get; set; }
    }
    public class AssignPatrol
    {
        public string Tag { get; set; }
        public string Address { get; set; }
        public string TaskMessage { get; set; }
    }
    public class PopUpAddress
    {
        public string Address { get; set; }
    }
    public class ClosedPopUpMessage
    {

    }

    public class CloseAllPopups
    {

    }
    public class ClosePatrolPopups
    {

    }
    public class CloseIncidentPopups
    {

    }
    public class CloseAssetPopups
    {

    }
}
