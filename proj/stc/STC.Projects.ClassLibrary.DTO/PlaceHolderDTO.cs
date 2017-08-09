using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PlaceHolderDTO
    {
        [DataMember]
        public int PlaceHolderId { get; set; }
        [DataMember]
        public string PlaceHolderName { get; set; }
        [DataMember]
        public UserControlDTO UserControl { get; set; }
    }
}
