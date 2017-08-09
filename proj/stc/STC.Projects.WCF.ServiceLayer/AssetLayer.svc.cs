using STC.Projects.ClassLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AssetLayer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AssetLayer.svc or AssetLayer.svc.cs at the Solution Explorer and start debugging.
    public class AssetLayer : IAssetLayer
    {
        public bool UpdateVitronicStatus()
        {
            return new AssetStatusUpdateDAL().UpdateVitronicStatus();
        }

        public bool UpdateEkinStatus()
        {
            return new AssetStatusUpdateDAL().UpdateEkinStatus();
        }

        public bool UpdateThreshold(int threshold)
        {
            return new AssetStatusUpdateDAL().UpdateThreshold(threshold);
        }
    }
}
