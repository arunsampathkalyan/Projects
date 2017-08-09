using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class VehicleModelDTO
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
