using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetTypeDimDTO
    {

        [DataMember]
        public int AssetTypeId { get; set; }

        [DataMember]
        public string AssetType { get; set; }
    }
}
