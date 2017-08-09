using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class FeaturesDTO
    {
        [DataMember]
        public int FeatureId { get; set; }
        [DataMember]
        public long PageId { get; set; }
        [DataMember]
        public string FeatureNameAr { get; set; }
        [DataMember]
        public string FeatureNameEn { get; set; }
    }
}
