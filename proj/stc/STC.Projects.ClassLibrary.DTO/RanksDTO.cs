using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class RanksDTO
    {
        [DataMember]
        public int RankId { get; set; }
        [DataMember]
        public string RankNameAr { get; set; }
        [DataMember]
        public string RankNameEn { get; set; }
    }
}
