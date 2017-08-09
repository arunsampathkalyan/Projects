using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ConstructionPermitLayer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ConstructionPermitLayer.svc or ConstructionPermitLayer.svc.cs at the Solution Explorer and start debugging.
    public class ConstructionPermitLayer : IConstructionPermitLayer
    {
        public List<ConstructionPermit.CONSTRUCTION_PERMIT_SEARCHRESULT> SearchConstructionPermit(long permitNumber)
        {
            var client = new ConstructionPermit.ConstructionPermits();
            return client.SearchConstructionPermits(null,permitNumber,null,null,null,"","",0,0,0,0,0,0,null,null,null,null).ToList();
        }

        public List<ConstructionPermit.CONSTRUCTION_PERMIT_SEARCHRESULT> ListOfConstructionAreas(double x,double y)
        {
            var client = new ConstructionPermit.ConstructionPermits();
            return client.SearchConstructionPermits(null, null, null, null, null, "", "", 0, x, y, 0, 0, 0, null, null, null, null).ToList();
        }

        public List<byte> GetConstructionFile(long permitNumber)
        {
            var client = new ConstructionPermit.ConstructionPermits();
            return client.GetConstructionFile(permitNumber,"pdf").ToList();
        }
    }
}
