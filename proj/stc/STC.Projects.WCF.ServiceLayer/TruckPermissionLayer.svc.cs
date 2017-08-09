using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TruckPermissionLayer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TruckPermissionLayer.svc or TruckPermissionLayer.svc.cs at the Solution Explorer and start debugging.
    public class TruckPermissionLayer : ITruckPermissionLayer
    {
        public List<HeavyLoadRef.LOAD_PERMIT_DETAIL> GetPermitDetails(long tcfNumber)
        {
            var client = new HeavyLoadRef.HeavyLoadPermits();
            return client.SearchTPermitDetail(null, 0, "", null, tcfNumber, null, null, null, null, null, "", "", null, "", null, null, 0).ToList();
        }
    }
}
