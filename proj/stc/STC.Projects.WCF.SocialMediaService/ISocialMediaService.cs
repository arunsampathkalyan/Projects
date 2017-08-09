using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace STC.Projects.WCF.SocialMediaService
{
    [ServiceContract]
    public interface ISocialMediaService
    {
        [OperationContract]
        bool PublishToFacebook(string post, byte[] imageBytes);
        [OperationContract]
        bool PublishToTwitter(string post, byte[] imageBytes);
    }
}
