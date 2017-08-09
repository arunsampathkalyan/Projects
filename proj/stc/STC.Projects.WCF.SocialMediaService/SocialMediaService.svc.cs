using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Facebook;
using System.Dynamic;
using System.Net;
using System.Drawing;
using TweetSharp;

namespace STC.Projects.WCF.SocialMediaService
{
    public class SocialMediaService : ISocialMediaService
    {
        public bool PublishToFacebook(string post, byte[] imageBytes)
        {
            var accessToken = Properties.Settings.Default.FBAccessToken;
            var AppId = Properties.Settings.Default.FBAppId;

            FacebookClient fbClient = new FacebookClient(accessToken);
            var args = new Dictionary<string, object>();

            try
            {
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    Image postImage = Image.FromStream(new MemoryStream(imageBytes));
                    string fileName = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"\" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";

                    postImage.Save(fileName);

                    args["caption"] = post;
                    args["source"] = new FacebookMediaObject
                    {
                        ContentType = "image/gif",
                        FileName = Path.GetFileName(fileName)
                    }.SetValue(File.ReadAllBytes(fileName));

                    var result = fbClient.Post("/" + AppId + "/photos", args);
                }
                else
                {

                    args["message"] = post;

                    var result = fbClient.Post("/" + AppId + "/feed", args);
                }

                return true;
            }
            catch (FacebookOAuthException ex)
            {
                throw ex;
                return false;
            }
            catch (FacebookApiException ex)
            {
                throw ex;
                return false;
            }
        }

        public bool PublishToTwitter(string post, byte[] imageBytes)
        {
            try
            {
                var service = new TwitterService(Properties.Settings.Default.TwitterConsumerId, Properties.Settings.Default.TwitterConsumerSecret);
                service.AuthenticateWith(Properties.Settings.Default.TwitterToken, Properties.Settings.Default.TwitterTokenSecret);

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    Image postImage = Image.FromStream(new MemoryStream(imageBytes));
                    string fileName = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"\" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpg";

                    postImage.Save(fileName);

                    Dictionary<string, Stream> dic = new Dictionary<string, Stream>();

                    FileStream ms = new FileStream(fileName, FileMode.Open);

                    dic.Add("", ms);
                    service.BeginSendTweetWithMedia(new SendTweetWithMediaOptions() { Status = post, Images = dic });
                }
                else
                {
                    service.BeginSendTweet(new SendTweetOptions() { Status = post });
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
