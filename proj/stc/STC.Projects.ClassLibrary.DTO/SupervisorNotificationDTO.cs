using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class SupervisorNotificationDTO
    {
        [DataMember]
        public long SupervisorNoticationId { get; set; }
        [DataMember]
        public DateTime NotificationTime { get; set; }
        [DataMember]
        public int SenderId { get; set; }
        [DataMember]
        public int ReceiverId { get; set; }
        [DataMember]
        public bool IsNoticed { get; set; }
        [DataMember]
        public long ResponseToId { get; set; }
        [DataMember]
        public SupervisorNotificationReportDangerousDTO DangerousViolatorDetails { get; set; }
        [DataMember]
        public SupervisorNotificationStatus Status { get; set; }
    }
    [DataContract]
    public class SupervisorNotificationReportDangerousDTO
    {
        [DataMember]
        public long ReportDangerousId { get; set; }
        [DataMember]
        public long SupervisorNotificationId { get; set; }
        [DataMember]
        public string NotificationText { get; set; }
        [DataMember]
        public string PlateNumber { get; set; }
        [DataMember]
        public string PlateKind { get; set; }
        [DataMember]
        public string PlateColor { get; set; }
        [DataMember]
        public string PlateAuthority { get; set; }
        [DataMember]
        public long? BusinessRuleId { get; set; }
        [DataMember]
        public string BusinessRuleName { get; set; }
        [DataMember]
        public double? Lat { get; set; }
        [DataMember]
        public double? Lon { get; set; }
        [DataMember]
        public string MediaURL { get; set; }
        [DataMember]
        public MediaTypes MediaType { get; set; }
        [DataMember]
        public string MediaFileFormat { get; set; }
    }
}
