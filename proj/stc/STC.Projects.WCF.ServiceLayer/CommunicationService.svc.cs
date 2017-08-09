using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL;
using System.Configuration;

namespace STC.Projects.WCF.ServiceLayer
{
    public class CommunicationService : ICommunicationService
    {
        private string smsConnectionString = ConfigurationManager.AppSettings["SMSOracleConnection"];
        public bool SendSMS(SMSDTO message)
        {
            try
            {
                return new CommunicationDAL(smsConnectionString).SendSMS(message);
            }
            catch(Exception ex)
            {
                Utility.WriteErrorLog(ex);
            }
            return false;
        }
    }
}
