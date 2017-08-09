using System.Collections.Generic;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetViolationDetailsDTO
    {
        [DataMember]
        public string AssetCode { get; set; }
        [DataMember]
        public string AssetLocation { get; set; }
        [DataMember]
        public string AssetVendor { get; set; }
        [DataMember]
        public int AssetViolationCount { get; set; }
        [DataMember]
        public int AssetViolationCountMonth { get; set; }
        [DataMember]
        public int AssetViolationCountYearly { get; set; }
        [DataMember]
        public CubeDTO AssetPieKPIs { get; set; }
        [DataMember]
        public List<CubeDTO> AssetLinerKPIs { get; set; }
    }
}
