using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IConstructionPermitLayer" in both code and config file together.
    [ServiceContract]
    public interface IConstructionPermitLayer
    {
        [OperationContract]
        [WebGet]
        List<ConstructionPermit.CONSTRUCTION_PERMIT_SEARCHRESULT> SearchConstructionPermit(long permitNumber);
        [OperationContract]
        [WebGet]
        List<ConstructionPermit.CONSTRUCTION_PERMIT_SEARCHRESULT> ListOfConstructionAreas(double x, double y);
        [OperationContract]
        [WebGet]
        List<byte> GetConstructionFile(long permitNumber);
    }
}
