using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetStatusDimDTO
    {
        [DataMember]
        public int AssetStatusId { get; set; }

        [DataMember]
        public string AssetStatus { get; set; }
    }
}
