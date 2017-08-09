using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PolygonTimeColorDTO
    {
        public PolygonTimeColorDTO()
        {
            Opacity = 50;
        }

        [DataMember]
        public int Time { get; set; }
        [DataMember]
        public int Opacity { get; set; }
        [DataMember]
        public int Red { get; set; }
        [DataMember]
        public int Green { get; set; }
        [DataMember]
        public int Blue { get; set; }
    }

    [DataContract]
    public class PolygonTimeColorOutputDTO
    {
        [DataMember]
        public int Time { get; set; }
        [DataMember]
        public string RGB { get; set; }
    }
}
