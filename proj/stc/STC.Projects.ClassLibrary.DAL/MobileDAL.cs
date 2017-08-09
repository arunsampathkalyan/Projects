using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class MobileDAL : IMobileDAL
    {
        private readonly STCOperationalDataContext _dBContext;
        public MobileDAL()
        {
            _dBContext = new STCOperationalDataContext();
        }
        public IEnumerable<FAQDTO> GetAllFAQs()
        {
            return _dBContext.FAQs.ToList().Where(f => f.UserId == null).ToFAQDtos();
        }

        public bool AddQuestion(string question, int userId)
        {
            _dBContext.FAQs.Add(new FAQ()
            {
                Question = question,
                UserId = userId,
                IssueDate = DateTime.Now
            });
            return _dBContext.SaveChanges() > 0;
        }

        public IEnumerable<FAQDTO> GetAskedQuestions(int userId)
        {
            return _dBContext.FAQs.ToList().Where(f => f.UserId== userId).ToFAQDtos();
        }

        public IEnumerable<FeedbackDTO> GetFeedbaks()
        {
            return _dBContext.Feedbacks.ToList().ToFeedbackDtos();
        }

        public bool AddFeedback(string feedback, string feedbackType, int userId)
        {
            _dBContext.Feedbacks.Add(new Feedback()
            {
                Feedback1 = feedback,
                UserId = userId,
                FeedbackType = feedbackType,
                IssueDate = DateTime.Now
            });
            return _dBContext.SaveChanges() > 0;
        }

        public IEnumerable<RoadSafetyTipDTO> GetRoadSafetyTips()
        {
            return _dBContext.RoadSafetyTips.ToList().ToRoadSafetyTipDTO();
        }

        public IEnumerable<EmergencyContactDTO> GetEmergencyContacts()
        {
            return _dBContext.EmergencyContacts.ToList().ToEmergencyContactDTOs();
        }


        public List<AssetsViewDTO> GetSmartTowers()
        {
            try
            {



                var lstAssets = (from av in _dBContext.AssetsViews
                                 join at in _dBContext.AssetTypeDIMViews on av.AssetTypeId equals at.AssetTypeId
                                 where at.AssetTypeCode == "SmartTowersCode"
                                 select new AssetsViewDTO()
                                 {
                                     Latitude = av.Latitude,
                                     Longitude = av.Longitude,
                                     ItemId = av.Id,
                                     ItemCategoryId = av.AssetTypeId,
                                     ItemCategoryName = av.AssetType,
                                     ItemImage = "",
                                     ItemName = av.Name,
                                     ItemStatusId = av.AssetStatusId,
                                     ItemStatusName = av.AssetStatus,
                                     SerialNo = av.SerialNo,
                                     LocationCode = av.LocationCode,
                                     CurrentValue = av.CurrentValue
                                 }).ToList();

                return lstAssets;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public string SendNotification(List<string> deviceRegIds, object message)
        {
            try
            {
                var regIds = string.Join("\",\"", deviceRegIds);
                const string apiKey = "AIzaSyCchfgohvV10iAJh0A4SJ3Ze0n5VTnmiUw";
                const string appId = "stcapp-1218";
                var value =  JsonConvert.SerializeObject(message, Formatting.Indented);
                WebRequest wRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                wRequest.Method = "post";
                wRequest.ContentType = " application/json;charset=UTF-8";
                wRequest.Headers.Add(string.Format("Authorization: key={0}", apiKey));
                wRequest.Headers.Add(string.Format("Sender: id={0}", appId));
                var postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"message\" : " + "\"" + value + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

                var bytes = Encoding.UTF8.GetBytes(postData);
                wRequest.ContentLength = bytes.Length;

                var stream = wRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                var wResponse = wRequest.GetResponse();

                stream = wResponse.GetResponseStream();

                var reader = new StreamReader(stream);

                var response = reader.ReadToEnd();

                var httpResponse = (HttpWebResponse)wResponse;
                string status = httpResponse.StatusCode.ToString();

                reader.Close();
                stream.Close();
                wResponse.Close();

                if (status == "")
                {
                    return response;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public bool UpdateQuestion(FAQDTO faq)
        {
            _dBContext.FAQs.Attach(faq.ToFaq());
            _dBContext.Entry(faq).State = EntityState.Modified;
            return _dBContext.SaveChanges() > 0;
        }





        public IEnumerable<WeatherDTO> GetWeatherNotifications()
        {
            return _dBContext.Weathers.ToList().ToWeatherDtos();
           
        }

        public bool AddWeatherNotification(WeatherDTO weatherNotificaition)
        {
            _dBContext.Weathers.Add(weatherNotificaition.ToWeather());
            return _dBContext.SaveChanges() > 0;

        }


        public bool AddRoadSafetyNotification(RoadSafetyTipDTO roadSafetyTipNotification)
        {
            
              _dBContext.RoadSafetyTips.Add(roadSafetyTipNotification.ToRoadSafetyTip());
            return _dBContext.SaveChanges() > 0;
        }


        public bool AddNewUser(string password,PublicUserDTO userDto)
        {
            try
            {
                
                var userentity = userDto.ToPublicUser();// userDto.ToUser();
                var salt = new Byte[32];
                using (var provider = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    provider.GetBytes(salt); // Generated salt
                }
                var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt);
                pbkdf2.IterationCount = 1000;
                byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
                userentity.Salt = salt;
                userentity.Password = hash;
                userentity.IssueDate = DateTime.Now;
                _dBContext.PublicUsers.Add(userentity);

                return _dBContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PublicUserDTO Authenticate(string username, string password)
        {
            var user = _dBContext.PublicUsers.FirstOrDefault(x => x.Username == username);

            if (user == null || !user.ToPublicUserDto().IsAuthentic(password))
                return null;
            return user.ToPublicUserDto();

        }

        public bool UpdateUser(PublicUserDTO userDto)
        {
            //var user = userDto.ToPublicUser();
            var oldUser = _dBContext.PublicUsers.FirstOrDefault(u => u.UserId == userDto.Id);
            if (oldUser==null)
            {
                oldUser = _dBContext.PublicUsers.FirstOrDefault(u => u.Username == userDto.Username);
                if (oldUser==null)
                {
                    return false;
                }
            }
            oldUser.Birthdate = userDto.Birthdate;
            oldUser.FullName = userDto.FullName;
            oldUser.IdentityNumber = userDto.IdentityNumber;
            oldUser.ImageURL= userDto.ImageUrl;
            oldUser.IsActive = userDto.IsActive;
            oldUser.Mail = userDto.Mail;
            oldUser.NationalityId = userDto.NationalityId;
            oldUser.Phone = userDto.Phone;
            oldUser.NotificationToken = userDto.NotificationToken;



           
            //_dBContext.PublicUsers.Attach(user);
            _dBContext.Entry(oldUser).State = EntityState.Modified;
           return _dBContext.SaveChanges() > 0;
        }

        public bool ActivateUser(bool isActive, string username)
        {
            _dBContext.PublicUsers.FirstOrDefault(u => u.Username == username).IsActive = isActive;
           return _dBContext.SaveChanges() > 0;
        }


        public bool UpdatePassword(int id, string oldPassword, string newPassword)
        {
            var user = _dBContext.PublicUsers.Find(id);

            if (user.ToPublicUserDto().IsAuthentic(oldPassword))
            {
                // User-entered password

                var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(newPassword, user.Salt);
                pbkdf2.IterationCount = 1000;
                byte[] hash = pbkdf2.GetBytes(32); // Hashed and salted password
                user.Password = hash;
                _dBContext.PublicUsers.Attach(user);
                _dBContext.Entry(user).State = EntityState.Modified;
                _dBContext.SaveChanges();
                return true;

            }
            else
            {
                throw new Exception("Password doesn't match");
            }
        }

        public IEnumerable<NationalityDTO> GetNationalities()
        {
            return _dBContext.Nationalities.ToList().ToNationalityDtos();
        }


        public PublicUserDTO GetUserById(int id)
        {
            return _dBContext.PublicUsers.Find(id).ToPublicUserDto();
        }

        public bool CheckPublicUserIfExisits(string username)
        {
            return _dBContext.PublicUsers.ToList().Any(u => u.Username.ToLower() == username.ToLower());
        }


        public PublicUserDTO GetUserByName(string username)
        {
            var user= _dBContext.PublicUsers.FirstOrDefault(u => u.Username.ToLower() == username.ToLower()).ToPublicUserDto();
            return user;
        }

        public MobileLookup GetVision()
        {
            var vision = _dBContext.MobileLookups.ToList().FirstOrDefault(l => l.Code == "v");
            return vision;
        }

        public MobileLookup GetMission()
        {
            var mission = _dBContext.MobileLookups.ToList().FirstOrDefault(l => l.Code == "m");
            return mission;
        }

        public IEnumerable<MobileLookup> GetSocialLinks()
        {
            var socailLinks = _dBContext.MobileLookups.ToList().Where(l => l.Code == "sl");
            return socailLinks;
        }
    }
}
