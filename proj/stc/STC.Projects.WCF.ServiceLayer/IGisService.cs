using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Layers;
using Infrastructure.Entities;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGisService" in both code and config file together.
    [ServiceContract]
    public interface IGisService
    {
        [OperationContract]
        [WebGet]
        List<AssetsDetailsViewDTO> GetAssets(List<MapPointDTO> mapPoints);
        [OperationContract]
        [WebGet]
        List<GraphicDTO> GetDriveTimePolygons(MapPointDTO point);
        [OperationContract]
        [WebGet]
        void UpdatePolygonTimeAndColor(List<PolygonTimeColorDTO> timeList);
        [OperationContract]
        [WebGet]
        List<PolygonTimeColorOutputDTO> GetPolygonTimeAndColor();
    }

    [DataContract]
    public class GraphicDTO
    {
        [DataMember]
        public string polygonObject { get; set; }

        [DataMember]
        public string symbolObject { get; set; }
    }
}
