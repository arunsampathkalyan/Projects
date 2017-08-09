using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PatrolOfficersDetailsDTO
    {
        public PatrolOfficersDetailsDTO()
        {
            Officers = new List<StaffPatrolModel>();
        }
        [DataMember]
        public string PatrolAllocation { get; set; }
        [DataMember]
        public string PatrolCode { get; set; }
        [DataMember]
        public string PatrolPlateNumber { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
        [DataMember]
        public string StatusArabic { get { return IsAvailable ? "متاحة" : "عليها بلاغ"; } set { } }
        [DataMember]
        public string StatusEnglish { get { return IsAvailable ? "Available" : "Busy"; } set { } }
        [DataMember]
        public List<StaffPatrolModel> Officers { get; set; }
    }

    [DataContract]
    public class StaffPatrolModel
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } set { } }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string MilitaryNumber { get; set; }
    }
}