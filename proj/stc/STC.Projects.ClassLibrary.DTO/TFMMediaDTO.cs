using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class TFMMediaDTO
    {
        [DataMember]
        public List<string> ImagesURLs { get; set; }
        [DataMember]
        public List<string> VideosURLs { get; set; }
        [DataMember]
        public List<string> VoicesURLs { get; set; }
    }
}
