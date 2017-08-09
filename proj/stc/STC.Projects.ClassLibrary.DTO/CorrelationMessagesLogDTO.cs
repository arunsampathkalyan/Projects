using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class CorrelationMessagesLogDTO
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long MessageId { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public DateTime CorrelationDate { get; set; }
        [DataMember]
        public string PlateNumber { get; set; }
        [DataMember]
        public string PlateKind { get; set; }
        [DataMember]
        public string PlateSource { get; set; }
        [DataMember]
        public string PlateColor { get; set; }
        [DataMember]
        public int BusinessRuleId { get; set; }
        [DataMember]
        public string BusinessRuleName { get; set; }
    }
}
