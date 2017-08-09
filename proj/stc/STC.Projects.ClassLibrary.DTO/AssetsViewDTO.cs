using System.Collections.Generic;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AssetsViewDTO
    {
        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string CurrentValue { get; set; }

        [DataMember]
        public long ItemId { get; set; }

        [DataMember]
        public string OriginalIdent { get; set; }

        [DataMember]
        public string ItemName { get; set; }

        //[DataMember]
        //public long SourceId { get; set; }

        //[DataMember]
        //public string SourceName { get; set; }

        [DataMember]
        public int? ItemCategoryId { get; set; }

        [DataMember]
        public string ItemCategoryName { get; set; }

        //[DataMember]
        //public int? ModelYear { get; set; }

        [DataMember]
        public int? ItemStatusId { get; set; }

        [DataMember]
        public string ItemStatusName { get; set; }

        //[DataMember]
        //public int? ItemMakeModelId { get; set; }

        //[DataMember]
        //public string ItemMakeName { get; set; }

        //[DataMember]
        //public string ItemModelName { get; set; }

        //[DataMember]
        //public string LocationInvolvementName { get; set; }

        [DataMember]
        public string LocationCode { get; set; }

        //[DataMember]
        //public double? Altitude { get; set; }

        [DataMember]
        public double? Latitude { get; set; }

        [DataMember]
        public double? Longitude { get; set; }

        //[DataMember]
        //public string ModelCode { get; set; }

        //[DataMember]
        //public string MakeCode { get; set; }

        //[DataMember]
        //public string ItemUsageName { get; set; }

        //[DataMember]
        //public int? ItemUsageId { get; set; }

        [DataMember]
        public List<AssetsDetailsViewDTO> AssetsDetails { get; set; }

        [DataMember]
        public string SerialNo { get; set; }

        [DataMember]
        public string ItemImage { get; set; }

        [DataMember]
        public int AreaId { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public string LinkId { get; set; }

        [DataMember]
        public string FromNodeId { get; set; }
    }
}