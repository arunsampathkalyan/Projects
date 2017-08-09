using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetLastStatusDTO
    {
        [DataMember]
        public int AssetLastStatusId { get; set; }

        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public int AssetStatusId { get; set; }

        [DataMember]
        public string AssetStatusName { get; set; }

        [DataMember]
        public DateTime DateChanged { get; set; }

        [DataMember]
        public bool IsNoticed { get; set; }

        [DataMember]
        public int? AssetTypeId { get; set; }

        [DataMember]
        public string AssetTypeName { get; set; }
    }
}
