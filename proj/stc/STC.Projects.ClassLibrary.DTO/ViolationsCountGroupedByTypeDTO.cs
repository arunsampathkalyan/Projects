using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountGroupedByTypeDTO
    {
        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}