using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAssetLayer" in both code and config file together.
    [ServiceContract]
    public interface IAssetLayer
    {
        [OperationContract]
        bool UpdateVitronicStatus();

        [OperationContract]
        bool UpdateEkinStatus();
        [OperationContract]
        bool UpdateThreshold(int threshold);
    }
}
