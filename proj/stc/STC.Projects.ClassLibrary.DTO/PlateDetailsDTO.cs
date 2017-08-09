using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PlateDetailsDTO
    {
        [DataMember]
        public string plateNumber { get; set; }
        [DataMember]
        public string plateColor { get; set; }
        [DataMember]
        public string plateSource { get; set; }
        [DataMember]
        public string plateKind { get; set; }
    }
}
