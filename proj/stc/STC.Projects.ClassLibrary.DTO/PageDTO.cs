using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PageDTO
    {
        public PageDTO()
        {
            UsersId = new List<int>();
        }
        [DataMember]
        public long PageId { get; set; }
        [DataMember]
        public string PageName { get; set; }
        [DataMember]
        public LayoutDTO Layout { get; set; }
        [DataMember]
        public List<int> UsersId { get; set; }
    }
}
