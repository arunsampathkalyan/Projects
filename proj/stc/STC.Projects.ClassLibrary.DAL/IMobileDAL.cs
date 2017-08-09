using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public interface IMobileDAL
    {
        IEnumerable<FAQDTO> GetAllFAQs();
        bool AddQuestion(string question, int userId);
        bool UpdateQuestion(FAQDTO faq);
        IEnumerable<FAQDTO> GetAskedQuestions(int userId);
        IEnumerable<FeedbackDTO> GetFeedbaks();
        bool AddFeedback(string feedback, string feedbackType, int userId);
        IEnumerable<RoadSafetyTipDTO> GetRoadSafetyTips();
        IEnumerable<EmergencyContactDTO> GetEmergencyContacts();
        List<AssetsViewDTO> GetSmartTowers();
        string SendNotification(List<string> deviceRegIds, object message);
        IEnumerable<WeatherDTO> GetWeatherNotifications();
        bool AddWeatherNotification(WeatherDTO weatherNotificaition);
        bool AddRoadSafetyNotification(RoadSafetyTipDTO roadSafetyTipNotification);
        bool AddNewUser(string password,PublicUserDTO userDto);
        PublicUserDTO Authenticate(string username, string password);
        bool UpdateUser(PublicUserDTO userDto);

        bool ActivateUser(bool isActive, string username);
        bool UpdatePassword(int id, string oldPassword, string newPassword);
        IEnumerable<NationalityDTO> GetNationalities();
        PublicUserDTO GetUserById(int id);
        bool CheckPublicUserIfExisits(string username);
        PublicUserDTO GetUserByName(string username);

        MobileLookup GetVision();
        MobileLookup GetMission();
        IEnumerable<MobileLookup> GetSocialLinks();
    }
}
