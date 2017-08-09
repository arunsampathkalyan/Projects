using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutomaticReports" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AutomaticReports.svc or AutomaticReports.svc.cs at the Solution Explorer and start debugging.
    public class AutomaticReportsLayer : IAutomaticReportsLayer
    {
        public List<TemplateParameterDTO> GetAllParameters()
        {
            return new AutomaticReportDAL().GetAllParameters();
        }

        public bool AddTemplate(ReportTemplateDTO template)
        {
            return new AutomaticReportDAL().AddTemplate(template, Utility.GetTemplatePath());
        }

        public bool EditTemplate(ReportTemplateDTO template)
        {
            return new AutomaticReportDAL().EditTemplate(template, Utility.GetTemplatePath());
        }

        public List<ReportTemplateDTO> GetAllTemplates()
        {
            return new AutomaticReportDAL().GetAllTemplates(Utility.GetTemplatePath());
        }

        public bool ActivateDeactivateTemplate(int templateId, int userId)
        {
            return new AutomaticReportDAL().ActivateDeactivateTemplate(templateId, userId);
        }

        public List<UserGroupsDTO> GetAllUserGroups(bool includeDeleted)
        {
            return new AutomaticReportDAL().GetAllUserGroups(includeDeleted);
        }

        public UserGroupsDTO GetGroupById(int groupId)
        {
            return new AutomaticReportDAL().GetGroupById(groupId);
        }
        public bool CreateUserGroup(UserGroupsDTO groupModel)
        {
            return new AutomaticReportDAL().CreateUserGroup(groupModel);
        }

        public bool EditUserGroup(UserGroupsDTO groupModel)
        {
            try
            {
                return new AutomaticReportDAL().EditUserGroup(groupModel);
            }
            catch (DbEntityValidationException exDb)
            {
                Utility.WriteErrorLog(exDb);
                foreach (var item in exDb.EntityValidationErrors)
                {
                    Utility.WriteLog(item.ValidationErrors.FirstOrDefault().ErrorMessage);
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);

                return false;
            }
        }

        public bool ActivateDeActivateGroup(int groupId, int userId)
        {
            return new AutomaticReportDAL().ActivateDeActivateGroup(groupId, userId);
        }

        public bool ActivateDeActivateReport(int report, int userId)
        {
            return new AutomaticReportDAL().ActivateDeActivateReport(report, userId);
        }
        public bool CreateAutomaticReport(AutomaticReportDTO report)
        {
            try
            {
                return new AutomaticReportDAL().CreateAutomaticReport(report);
            }

             catch(DbEntityValidationException exDb )
            {
                Utility.WriteErrorLog(exDb);
                 foreach (var item in exDb.EntityValidationErrors)
                 {
                     Utility.WriteLog(item.ValidationErrors.FirstOrDefault().ErrorMessage);
                 }
                 return false;
            }
            catch(Exception ex)
            {
                Utility.WriteErrorLog(ex);
                
                return false;
            }
        }

        public bool EditAutomaticReport(AutomaticReportDTO report)
        {
            try
            {
                return new AutomaticReportDAL().EditAutomaticReport(report);
            }

            catch (DbEntityValidationException exDb)
            {
                Utility.WriteErrorLog(exDb);
                foreach (var item in exDb.EntityValidationErrors)
                {
                    Utility.WriteLog(item.ValidationErrors.FirstOrDefault().ErrorMessage);
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);

                return false;
            }
        }

        public List<DiminsionRelativeTypeDTO> GetAllRelativeTypes()
        {
            return new AutomaticReportDAL().GetAllRelativeTypes();
        }

        public void HandleReports()
        {
            var reports = PrepareReports();
            
            if(reports == null || !reports.Any())
                return;

           // Utility.WriteLog("Handle Reports : After Prepare Reports");

            var reportLogs = new List<AutomaticReportLogDTO>();
            foreach (var item in reports)
            {
                foreach (var group in item.Groups)
                {
                    foreach (var user in group.AssignedUsers)
                    {
                        var log = new AutomaticReportLogDTO();
                        log.UserId = user.UserId;
                        log.ReportGroupId = group.ReportGroupId;
                        if (item.IsSMS && item.Template != null && user.PhoneNumber != "")
                        {
                            var sms = new SMSDTO
                            {
                                RecipientNumber = user.PhoneNumber
                            };
                            var smsEn = new SMSDTO
                            {
                                RecipientNumber = user.PhoneNumber
                            };
                            log.MobileNumber = user.PhoneNumber;
                            
                            if (item.IsArabic && item.Template.SMSTextArabic != "")
                            {
                                sms.Language = SMSLanguage.Arabic;
                                sms.MessageBody = item.Template.SMSTextArabic;
                                log.SMSText = sms.MessageBody;
                                new CommunicationService().SendSMS(sms);
                            }
                            if (item.IsEnglish && item.Template.SMSTextEnglish != "")
                            {
                                smsEn.Language = SMSLanguage.English;
                                smsEn.MessageBody = item.Template.SMSTextEnglish;
                                log.EnglishSMSText = smsEn.MessageBody;
                                Utility.WriteLog("Send SMS with text = " + smsEn.MessageBody);
                                new CommunicationService().SendSMS(smsEn);
                            }

                           
                        }
                        log.SendDate = DateTime.Now;
                        reportLogs.Add(log);
                    }
                }
            }
            RecordLogs(reportLogs);
        }

        [AspNetCacheProfile("CacheForReports")]
        [WebGet()]
        public List<AutomaticReportDTO> GetAllReports()
        {
            try
            {
                return new AutomaticReportDAL().GetAllReports(Utility.GetTemplatePath());
            }
            catch(Exception ex)
            {
                Utility.WriteErrorLog(ex);
            }
            return new List<AutomaticReportDTO>();
        }
        
        public bool RemoveAssignedList(int reportId, List<int> userGroupsIds)
        {
            return new AutomaticReportDAL().RemoveAssignedList(reportId,userGroupsIds);
        }
        public List<AutomaticReportDTO> PrepareReports()
        {
            var res = new List<AutomaticReportDTO>();
            var allReports = GetAllReports();
            if(allReports != null && allReports.Any())
            {
                foreach (var item in allReports)
                {
                    if(IsOnTime(item.Schedule))
                    {
                        if (item.Groups != null)
                        {
                            var todaysLog = new AutomaticReportDAL().GetTodayLog();
                            Utility.WriteLog("Today Log is :" + todaysLog.Count);
                            if (todaysLog != null && todaysLog.Any(x => item.Groups.Any(y => y.ReportGroupId == x.ReportGroupId)))
                                continue;
                            
                            foreach (var grp in item.Groups)
                            {
                                grp.AssignedUsers = grp.AssignedUsers.Where(x => x.IsActive).ToList();
                            }
                            
                            PrepareTemplate(item.Template, item.Diminsion);
                            res.Add(item);
                        }
                        
                    }
                }
            }
            return res;
        }

        private void PrepareTemplate(ReportTemplateDTO template, AutomaticReportDiminsionDTO diminsion)
        {
            try
            {
                DateTime startDate = DateTime.Now;
                if (diminsion.IsStaticValue && diminsion.ExactValue != "")
                {
                    startDate = new DateTime(int.Parse(diminsion.ExactValue.Substring(0, 4)), int.Parse(diminsion.ExactValue.Substring(4, 2)), int.Parse(diminsion.ExactValue.Substring(6, 2)));
                }
                else if (diminsion.RelativeValue.HasValue && diminsion.RelativeTypeId.HasValue)
                {
                    if (diminsion.RelativeTypeId.Value == (int)RelativeTypesEnum.Hours)
                    {
                        startDate = startDate.AddHours(-1 * diminsion.RelativeValue.Value);
                    }
                    else if (diminsion.RelativeTypeId.Value == (int)RelativeTypesEnum.Days)
                    {
                        startDate = startDate.AddDays(-1 * diminsion.RelativeValue.Value);
                    }
                    else if (diminsion.RelativeTypeId.Value == (int)RelativeTypesEnum.Months)
                    {
                        startDate = startDate.AddMonths(-1 * diminsion.RelativeValue.Value);
                    }
                }
               
                foreach (var item in template.TemplateParameters)
                {
                    var connString = "";
                    connString = item.IsCube ? ConfigurationManager.AppSettings[item.ConnKeyName] : ConfigurationManager.ConnectionStrings[item.ConnKeyName].ConnectionString;
                    if(connString != "")
                    {
                        var engPeriodState = "";
                        var arPeriodState = "";
                        var endDate = DateTime.Now;
                        var count = 0;
                        if (item.IsCube)
                        {
                            string mdx = "";
                            try
                            {
                                count = new CubeDAL(connString).GetAutomaticReportCounts(startDate, endDate, item.FieldName, item.CubeName, diminsion.FieldName, out mdx);

                            }
                            catch (Exception ex)
                            {
                                Utility.WriteErrorLog(ex);
                            }
                            Utility.WriteLog("Mdx is :" + mdx);
                             engPeriodState = string.Format(" during the period between {0} and {1} ", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                             arPeriodState = " في الفترة بين " + startDate.ToString("yyyy-MM-dd") + " إلى " + endDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            var queryTxt = "";
                            if(File.Exists(item.QueryPath))
                            {
                                try
                                {
                                    queryTxt = File.ReadAllText(item.QueryPath);
                                    count = new CubeDAL(connString).GetAutomaticReportCounts(queryTxt);
                                }
                                catch (Exception ex)
                                {
                                    Utility.WriteErrorLog(ex);
                                }
                                Utility.WriteLog("Query is :" + queryTxt);
                            }
                        }
                        if(count >= 0)
                        {
                            if(template.EmailTextArabic != "")
                                template.EmailTextArabic = template.EmailTextArabic.Replace(item.ParameterText, count.ToString()) + arPeriodState;
                            if(template.EmailTextEnglish != "")
                                template.EmailTextEnglish = template.EmailTextEnglish.Replace(item.ParameterText, count.ToString()) + engPeriodState;
                            if(template.SMSTextArabic != "")
                                template.SMSTextArabic = template.SMSTextArabic.Replace(item.ParameterText, count.ToString()) + arPeriodState;
                            if(template.SMSTextEnglish != "")
                                template.SMSTextEnglish = template.SMSTextEnglish.Replace(item.ParameterText, count.ToString()) + engPeriodState;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.WriteErrorLog(ex);
            }
        }

        private bool IsOnTime(AutomaticScheduleDTO schedule)
        {
            bool isTime = false;
            if (schedule != null && schedule.SendHour <= DateTime.Now.Hour)
            {
                if (schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Daily)
                {
                    isTime = true;
                }
                else if (schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Weekly)
                {
                    isTime = schedule.WeeksDay.Any(x => x == (int)DateTime.Now.DayOfWeek);
                }
                else if (schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Monthly)
                {
                    if (schedule.IsLastDayInMonth.HasValue && schedule.IsLastDayInMonth.Value)
                    {
                        if (DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                        {
                            isTime = true;
                        }
                    }
                    if (schedule.IsFirstDayInMonth.HasValue && schedule.IsFirstDayInMonth.Value)
                    {
                        if (DateTime.Now.Day == 1)
                        {
                            isTime = true;
                        }
                    }
                    if (schedule.DayNumberInMonth.HasValue)
                    {
                        if (DateTime.Now.Day == schedule.DayNumberInMonth.Value)
                            isTime = true;
                    }
                }
            }
            return isTime;
        }
        public bool RecordLogs(List<AutomaticReportLogDTO> logs)
        {
            try
            {
                return new AutomaticReportDAL().RecordLogs(logs);
            }
            catch(Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return false;
            }
        }
    }
}
