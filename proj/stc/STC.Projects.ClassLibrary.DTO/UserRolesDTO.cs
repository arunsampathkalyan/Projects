using System.Collections.Generic;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class UserRolesDTO
    {
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string RoleDescriptionAr { get; set; }
        [DataMember]
        public string RoleDescriptionEn { get; set; }
        [DataMember]
        public List<FeaturesDTO> RoleFeatures { get; set; }
    }
}
