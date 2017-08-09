using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    [ServiceContract]
    public interface ICommunicationService
    {
        [OperationContract]
        bool SendSMS(SMSDTO message);
    }
}
