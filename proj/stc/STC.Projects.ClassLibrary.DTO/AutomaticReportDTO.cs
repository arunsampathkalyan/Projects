using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
   

    [DataContract]
    public class TemplateParameterDTO
    {
         [DataMember]
        public int ParameterId { get; set; }
         [DataMember]
        public string ParameterName { get; set; }
         [DataMember]
        public string ParameterText { get; set; }
         [DataMember]
        public byte[] ParameterIcon { get; set; }
         [DataMember]
        public string ParameterIconPath { get; set; }
         [DataMember]
        public bool isDeleted { get; set; }
         [DataMember]
         public string CubeName { get; set; }
         [DataMember]
         public string FieldName { get; set; }
         [DataMember]
         public string ConnKeyName { get; set; }
         [DataMember]
         public bool IsCube { get; set; }
         [DataMember]
         public string QueryPath { get; set; }
    }

    public enum RelativeTypesEnum
    {
        Hours = 1,
        Days = 2,
        Months = 3
    }
    [DataContract]
    public class ReportTemplateDTO
    {
        public ReportTemplateDTO()
        {
            TemplateParameters = new List<TemplateParameterDTO>();
        }
         [DataMember]
        public int TemplateId { get; set; }
         [DataMember]
        public string TemplateName { get; set; }
         [DataMember]
        public bool IsEmail { get; set; }
         [DataMember]
        public bool IsSMS { get; set; }
         [DataMember]
        public bool IsDeleted { get; set; }
         [DataMember]
        public int CreatedUserId { get; set; }
         [DataMember]
        public DateTime DateCreated { get; set; }
         [DataMember]
        public int? LastModifiedUserId { get; set; }
         [DataMember]
        public DateTime? LastModifiedDate { get; set; }
         [DataMember]
        public string SMSTextArabic { get; set; }
         [DataMember]
        public string SMSTextEnglish { get; set; }
         [DataMember]
        public string EmailTextArabic { get; set; }
         [DataMember]
        public string EmailTextEnglish { get; set; }
         [DataMember]
        public string EmailSubjectArabic { get; set; }
         [DataMember]
        public string EmailSubjectEnglish { get; set; }
         [DataMember]
        public List<TemplateParameterDTO> TemplateParameters { get; set; }

    }

    [DataContract]
    public class UserGroupsDTO
    {
        public UserGroupsDTO()
        {
            AssignedUsers = new List<UsersDTO>();
        }
        [DataMember]
        public int GroupId { get; set; }
         [DataMember]
        public string GroupName { get; set; }
         [DataMember]
        public DateTime CreateDate { get; set; }
         [DataMember]
        public int CurrentUserId { get; set; }
         [DataMember]
        public bool IsDeleted { get; set; }
         [DataMember]
        public List<UsersDTO> AssignedUsers { get; set; }
         [DataMember]
         public int ReportGroupId { get; set; }
    }

     [DataContract]
    public class AutomaticReportDTO
    {
         public AutomaticReportDTO()
         {
             Diminsion = new AutomaticReportDiminsionDTO();
             Schedule = new AutomaticScheduleDTO();
             Groups = new List<UserGroupsDTO>();
             Template = new ReportTemplateDTO();
         }
          [DataMember]
        public int ReportId { get; set; }
          [DataMember]
        public string ReportName { get; set; }
          [DataMember]
        public int TemplateId { get; set; }
          [DataMember]
        public List<int> UserGroupIds { get; set; }
          [DataMember]
        public bool IsEmail { get; set; }
          [DataMember]
        public bool IsSMS { get; set; }
          [DataMember]
        public bool IsArabic { get; set; }
          [DataMember]
        public bool IsEnglish { get; set; }
          [DataMember]
        public int CurrentUserId { get; set; }
          [DataMember]
        public bool IsDeleted { get; set; }
          [DataMember]
        public AutomaticReportDiminsionDTO Diminsion { get; set; }
          [DataMember]
        public AutomaticScheduleDTO Schedule { get; set; }
         [DataMember]
          public List<UserGroupsDTO> Groups { get; set; }
         [DataMember]
          public ReportTemplateDTO Template { get; set; }
    }

    [DataContract]
    public class AutomaticReportLogDTO
    {
        public long LogId { get; set; }
        public int ReportGroupId { get; set; }
        public int UserId { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SMSText { get; set; }
        public string EnglishSMSText { get; set; }
        public string EmailText { get; set; }
        public string EnglishEmailText { get; set; }
        public DateTime SendDate { get; set; }
        public string EmailSubject { get; set; }
    }

     [DataContract]
    public class AutomaticScheduleDTO 
    {
         public AutomaticScheduleDTO()
         {
             WeeksDay = new List<int>();
         }
          [DataMember]
        public int ScheduleTypeId { get; set; }
          [DataMember]
        public bool? IsLastDayInMonth { get; set; }
          [DataMember]
        public bool? IsFirstDayInMonth { get; set; }
          [DataMember]
        public int? DayNumberInMonth { get; set; }
          [DataMember]
        public List<int> WeeksDay { get; set; }
          [DataMember]
          public int SendHour { get; set; }
    }

     [DataContract]
    public enum ScheduleTypesEnum
    {
        Daily = 1,
        Weekly = 2,
        Monthly =3
    }

     [DataContract]
    public class AutomaticReportDiminsionDTO
    {
          [DataMember]
        public bool IsStaticValue { get; set; }
          [DataMember]
        public string ExactValue { get; set; } // in format yyyyMMdd
          [DataMember]
        public int? RelativeValue { get; set; }
          [DataMember]
        public int? RelativeTypeId { get; set; }
          [DataMember]
        public bool IsDeleted { get; set; }
          public string FieldName { get; set; }
    }

     [DataContract]
    public class DiminsionRelativeTypeDTO
    {
          [DataMember]
        public int RelativeTypeId { get; set; }
          [DataMember]
        public string ArabicName { get; set; }
          [DataMember]
        public string EnglishName { get; set; }
    }
}
