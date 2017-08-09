using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class LayoutDTO
    {
        [DataMember]
        public string LayoutXaml { get; set; }
        [DataMember]
        public int LayoutId { get; set; }
        [DataMember]
        public string LayoutName { get; set; }
        [DataMember]
        public List<PlaceHolderDTO> PlaceHolders { get; set; }
    }
}
