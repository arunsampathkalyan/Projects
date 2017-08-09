using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class MessageTypeSOPDTO
    {
        [DataMember]
        public int MessageTypeId { get; set; }

        [DataMember]
        public string MessageTypeName { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public int SOPId { get; set; }

        [DataMember]
        public string SOPContent { get; set; }

        [DataMember]
        public string SOPControlName { get; set; }

        [DataMember]
        public string SOPDetailsControlName { get; set; }

        [DataMember]
        public string SOPDetailsDataMessageType { get; set; }


        [DataMember]
        public string SOPDetailsXSLT { get; set; }

        [DataMember]
        public string SOPListDataMessageType { get; set; }

        [DataMember]
        public int? SOPViewDetailsMessageId { get; set; }
        [DataMember]
        public int? SOPViewListMessageId { get; set; }

        [DataMember]
        public string SOPListXSLT { get; set; }
        [DataMember]
        public int? PriorityId { get; set; }
        [DataMember]
        public bool IsDone { get; set; }
    }
}