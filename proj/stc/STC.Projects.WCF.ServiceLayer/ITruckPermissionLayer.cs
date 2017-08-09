using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITruckPermissionLayer" in both code and config file together.
    [ServiceContract]
    public interface ITruckPermissionLayer
    {
        [OperationContract]
        [WebGet]
        List<HeavyLoadRef.LOAD_PERMIT_DETAIL> GetPermitDetails(long tcfNumber);
    }
}
