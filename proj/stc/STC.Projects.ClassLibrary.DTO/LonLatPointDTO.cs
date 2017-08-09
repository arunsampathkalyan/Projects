using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class LonLatPointDTO
    {
        [DataMember]
        public double Lon { set; get; }
        [DataMember]
        public double Lat { set; get; }
    }
}
