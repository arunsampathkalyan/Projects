using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class AutomaticReportDAL
    {
        private STCOperationalDataContext _operationalDataContext = new STCOperationalDataContext();
        public List<TemplateParameterDTO> GetAllParameters()
        {
            try
            {
                var list = _operationalDataContext.TemplateParameters.Where(x => !x.IsDeleted)
                    .Select(y => new TemplateParameterDTO
                    {
                        isDeleted = y.IsDeleted,
                        ParameterId = y.ParameterID,
                        ParameterName = y.ParameterName,
                        ParameterText = y.ParameterText,
                        ParameterIconPath = y.ParameterIconPath
                    }).ToList();
                
                return list;
                //if(list != null && list.Any())
                //{
                //    foreach (var item in list)
                //    {
                //        try
                //        {
                //            item.ParameterIcon = File.ReadAllBytes(item.ParameterIconPath);
                //        }
                //        catch(Exception ex)
                //        {

                //        }
                //    }
                //}
            }
            catch(Exception ex)
            {

            }
            return new List<TemplateParameterDTO>();
        }

        public bool AddTemplate(ReportTemplateDTO template,string templatePath)
        {
            try
            {
                var isNameExist = _operationalDataContext.ReportsTemplate.Any(x => x.TemplateName.ToLower().Trim() == template.TemplateName.ToLower().Trim());
               
                if (isNameExist)
                    return false;

                var entity = new ReportsTemplate
                    {
                        CreatedDate = DateTime.Now,
                        CreatedUserId = template.CreatedUserId,
                        EmailSubjectArabic = template.EmailSubjectArabic,
                        EmailSubjectEnglish = template.EmailSubjectEnglish,
                        IsDeleted = template.IsDeleted,
                        IsEmail = template.IsEmail,
                        IsSMS = template.IsSMS,
                        LastModifiedDate = template.LastModifiedDate,
                        LastModifiedUserId = template.LastModifiedUserId,
                        TemplateName = template.TemplateName
                    };

                foreach (var item in template.TemplateParameters)
                {
                    entity.ReportTemplateParameter.Add(new ReportTemplateParameter
                        {
                            IsDeleted = item.isDeleted,
                            ParameterId = item.ParameterId,
                        });
                }
                _operationalDataContext.ReportsTemplate.Add(entity);
                var isSaved = _operationalDataContext.SaveChanges() > 0;
                if (isSaved)
                {
                    return HandleTemplateFiles(templatePath, entity.TemplateId, template);
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        private bool HandleTemplateFiles(string templatePath,int templateId,ReportTemplateDTO template)
        {
            try
            {
                var arEmailTemplatePath = GetNotificationTemplatePath(templatePath, "Email", "AR");
                var enEmailTemplatePath = GetNotificationTemplatePath(templatePath, "Email", "EN");
                var arSMSTemplatePath = GetNotificationTemplatePath(templatePath, "SMS", "AR");
                var enSMSTemplatePath = GetNotificationTemplatePath(templatePath, "SMS", "EN");

                if (!Directory.Exists(arEmailTemplatePath))
                {
                    Directory.CreateDirectory(arEmailTemplatePath);
                }
                if (!Directory.Exists(enEmailTemplatePath))
                {
                    Directory.CreateDirectory(enEmailTemplatePath);
                }
                if (!Directory.Exists(arSMSTemplatePath))
                {
                    Directory.CreateDirectory(arSMSTemplatePath);
                }
                if (!Directory.Exists(enSMSTemplatePath))
                {
                    Directory.CreateDirectory(enSMSTemplatePath);
                }

                if (template.EmailTextArabic != "")
                    File.WriteAllText(string.Format(@"{0}\{1}.txt", arEmailTemplatePath, templateId), template.EmailTextArabic, Encoding.Unicode);
                if (template.EmailTextEnglish != "")
                    File.WriteAllText(string.Format(@"{0}\{1}.txt", enEmailTemplatePath, templateId), template.EmailTextEnglish);
                if (template.SMSTextArabic != "")
                    File.WriteAllText(string.Format(@"{0}\{1}.txt", arSMSTemplatePath, templateId), template.SMSTextArabic, Encoding.Unicode);
                if (template.SMSTextEnglish != "")
                    File.WriteAllText(string.Format(@"{0}\{1}.txt", enSMSTemplatePath, templateId), template.SMSTextEnglish);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public bool EditTemplate(ReportTemplateDTO template,string templatePath)
        {
            var res = false;
            try
            {
                var isNameExist = _operationalDataContext.ReportsTemplate.Any(x => x.TemplateName.ToLower().Trim() == template.TemplateName.ToLower().Trim() && x.TemplateId != template.TemplateId);

                if (isNameExist)
                    return false;

                var entity = _operationalDataContext.ReportsTemplate.FirstOrDefault(x => x.TemplateId == template.TemplateId);
                if(entity != null)
                {
                    entity.EmailSubjectArabic = template.EmailSubjectArabic;
                    entity.EmailSubjectEnglish = template.EmailSubjectEnglish;
                    entity.TemplateName = template.TemplateName;
                    entity.LastModifiedDate = DateTime.Now;
                    entity.LastModifiedUserId = template.LastModifiedUserId;
                    var addedParametersIds = template.TemplateParameters.Where(x => !x.isDeleted && !entity.ReportTemplateParameter.Any(y => y.ParameterId == x.ParameterId)).Select(x=> x.ParameterId).ToList();
                    var removedParametersIds = entity.ReportTemplateParameter.Where(x => !x.IsDeleted && !template.TemplateParameters.Any(y => y.ParameterId == x.ParameterId)).Select(x=> x.ParameterId).ToList();
                    
                    if(addedParametersIds != null && addedParametersIds.Any())
                    {
                        foreach (var item in addedParametersIds)
                        {
                            if (entity.ReportTemplateParameter.Any(x => x.IsDeleted && x.ParameterId == item))
                            {
                                entity.ReportTemplateParameter.FirstOrDefault(x => x.IsDeleted && x.ParameterId == item).IsDeleted = false;
                            }
                            else
                            {
                                entity.ReportTemplateParameter.Add(new ReportTemplateParameter
                                    {
                                        IsDeleted = false,
                                        ParameterId = item,
                                    });
                            }
                        }
                    }
                    
                    if(removedParametersIds != null && removedParametersIds.Any())
                    {
                        foreach (var item in removedParametersIds)
                        {
                            if (entity.ReportTemplateParameter.Any(x => !x.IsDeleted && x.ParameterId == item))
                            {
                                entity.ReportTemplateParameter.FirstOrDefault(x => !x.IsDeleted && x.ParameterId == item).IsDeleted = true;
                            }
                        }
                    }
                    var isSaved = _operationalDataContext.SaveChanges() > 0;
                    if (isSaved)
                    {
                        return HandleTemplateFiles(templatePath, entity.TemplateId, template);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return res;
        }
        public List<ReportTemplateDTO> GetAllTemplates(string templatePath)
        {
            var res = new List<ReportTemplateDTO>();
            try
            {
                res = _operationalDataContext.ReportsTemplate.Select(x=> new ReportTemplateDTO
                    {
                        CreatedUserId = x.CreatedUserId,
                        DateCreated = x.CreatedDate,
                        IsDeleted = x.IsDeleted,
                        IsEmail = x.IsEmail,
                        IsSMS = x.IsSMS,
                        EmailSubjectArabic = x.EmailSubjectArabic,
                        EmailSubjectEnglish = x.EmailSubjectEnglish,
                        LastModifiedDate = x.LastModifiedDate,
                        LastModifiedUserId = x.LastModifiedUserId,
                        TemplateId = x.TemplateId,
                        TemplateName = x.TemplateName,
                        TemplateParameters = _operationalDataContext.ReportTemplateParameter.Where(y => x.TemplateId == y.TemplateId && y.IsDeleted == false).Select(z => new TemplateParameterDTO
                        {
                            ParameterIconPath = z.TemplateParameters.ParameterIconPath,
                            ParameterId = z.ParameterId,
                            ParameterName = z.TemplateParameters.ParameterName,
                            ParameterText = z.TemplateParameters.ParameterText
                        }).ToList()
                    
                    }).ToList();

                if (res != null && res.Any())
                {
                    foreach (var item in res)
                    {
                        PrepareNotificationText(item,templatePath);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public bool ActivateDeactivateTemplate(int templateId,int userId)
        {
            var res = false;
            try
            {
                var template = _operationalDataContext.ReportsTemplate.FirstOrDefault(x=> x.TemplateId == templateId);
                if(template != null)
                {
                    template.IsDeleted = !template.IsDeleted;
                    template.LastModifiedUserId = userId;
                    template.LastModifiedDate = DateTime.Now;

                    var assignedReports = _operationalDataContext.AutomaticReports.Where(x => x.TemplateId == templateId);
                    if(assignedReports != null && assignedReports.Any())
                    {
                        foreach (var item in assignedReports)
                        {
                            item.IsDeleted = !item.IsDeleted;
                            item.LastModifiedBy = userId;
                            item.LastModifiedDate = DateTime.Now;
                        }
                    }
                    res = _operationalDataContext.SaveChanges() > 0;
                }
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public List<UserGroupsDTO> GetAllUserGroups(bool includeDeleted = false)
        {
            var res = new List<UserGroupsDTO>();
            try
            {
                res = _operationalDataContext.Groups.Where(x => (includeDeleted || x.IsDeleted == false))
                    .Select(y => new UserGroupsDTO
                    {
                        AssignedUsers = y.GroupUsers.Select(user => new UsersDTO()
                        {
                            UserId = user.UserId,
                            UserName = user.User.UserName,
                            RoleId = user.User.RoleId ?? 0,
                            RankId = user.User.RankId ?? 0,
                            FullNameAr = user.User.FullNameAr,
                            FullNameEn = user.User.FullNameEn,
                            Email = user.User.Email,
                            IsActive = user.User.IsActive ?? true,
                            PhoneNumber = user.User.PhoneNumber,
                            RankNameAR = user.User.Rank != null ? user.User.Rank.RankNameAr : "",
                            RankNameEN = user.User.Rank != null ? user.User.Rank.RankNameEn : ""
                        }).ToList(),
                        CreateDate = y.CreatedDate,
                        GroupId = y.GroupId,
                        GroupName = y.GroupName,
                        IsDeleted = y.IsDeleted
                    }).ToList();
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public UserGroupsDTO GetGroupById(int groupId)
        {
            var res = new UserGroupsDTO();
            try
            {
                res = _operationalDataContext.Groups.Where(x => x.GroupId == groupId)
                    .Select(y => new UserGroupsDTO
                    {
                        AssignedUsers = y.GroupUsers.Select(user => new UsersDTO()
                        {
                            UserId = user.UserId,
                            UserName = user.User.UserName,
                            RoleId = user.User.RoleId ?? 0,
                            RankId = user.User.RankId ?? 0,
                            FullNameAr = user.User.FullNameAr,
                            FullNameEn = user.User.FullNameEn,
                            Email = user.User.Email,
                            IsActive = user.User.IsActive ?? true,
                            PhoneNumber = user.User.PhoneNumber,
                            RankNameAR = user.User.Rank != null ? user.User.Rank.RankNameAr : "",
                            RankNameEN = user.User.Rank != null ? user.User.Rank.RankNameEn : ""
                        }).ToList(),
                        CreateDate = y.CreatedDate,
                        GroupId = y.GroupId,
                        GroupName = y.GroupName,
                        IsDeleted = y.IsDeleted
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public bool CreateUserGroup(UserGroupsDTO groupModel)
        {
            try
            {
                if (groupModel.AssignedUsers == null || !groupModel.AssignedUsers.Any())
                    return false;

                var isNameExist = _operationalDataContext.Groups.Any(x => x.GroupName.ToLower().Trim() == groupModel.GroupName.ToLower().Trim());

                if (isNameExist)
                    return false;

                var group = new Group
                    {
                        CreatedBy = groupModel.CurrentUserId,
                        CreatedDate = DateTime.Now,
                        GroupName = groupModel.GroupName,
                        IsDeleted = false,
                    };

                foreach (var item in groupModel.AssignedUsers)
                {
                    group.GroupUsers.Add(new GroupUser
                        {
                            UserId = item.UserId
                        });
                }

                _operationalDataContext.Groups.Add(group);
                
                return _operationalDataContext.SaveChanges() > 0;
            }
            catch(Exception ex)
            { 
            }
            return false;
        }

        public bool EditUserGroup(UserGroupsDTO groupModel)
        {
            try
            {
                if (groupModel.AssignedUsers == null || !groupModel.AssignedUsers.Any())
                    return false;

                var isNameExist = _operationalDataContext.Groups.Any(x => x.GroupName.ToLower().Trim() == groupModel.GroupName.ToLower().Trim() && x.GroupId != groupModel.GroupId);

                if (isNameExist)
                    return false;

                var group = _operationalDataContext.Groups.FirstOrDefault(x => x.GroupId == groupModel.GroupId);

                if (group == null)
                    return false;

                group.GroupName = groupModel.GroupName;
                group.IsDeleted = groupModel.IsDeleted;
                group.LastModified = DateTime.Now;
                group.LastModifiedBy = groupModel.CurrentUserId;

                //group.GroupUsers.Clear();
                var addedUsersIds = groupModel.AssignedUsers.Where(x =>!group.GroupUsers.Any(y => y.UserId == x.UserId)).Select(x => x.UserId).ToList();
                var removedUsersIds = group.GroupUsers.Where(x => !groupModel.AssignedUsers.Any(y => y.UserId == x.UserId)).Select(x => x.UserId).ToList();

                if (addedUsersIds != null && addedUsersIds.Any())
                {
                    foreach (var item in addedUsersIds)
                    {
                        group.GroupUsers.Add(new GroupUser
                            {
                              UserId = item
                            });
                    }
                }


                if (removedUsersIds != null && removedUsersIds.Any())
                {
                    foreach (var item in removedUsersIds)
                    {
                        _operationalDataContext.GroupUsers.Remove(group.GroupUsers.FirstOrDefault(x=> x.UserId == item));
                    }
                }
                //foreach (var item in group.GroupUsers)
                //{
                //    group.GroupUsers.Remove(item);
                //}

                //foreach (var item in groupModel.AssignedUsers)
                //{
                //    group.GroupUsers.Add(new GroupUser
                //    {
                //        UserId = item.UserId,
                //    });
                //}

                return _operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return false;
        }

        public bool ActivateDeActivateGroup(int groupId, int userId)
        {
            try
            {
                var group = _operationalDataContext.Groups.FirstOrDefault(x => x.GroupId == groupId);

                if (group == null)
                    return false;

                group.IsDeleted = !group.IsDeleted;
                group.LastModified = DateTime.Now;
                group.LastModifiedBy = userId;

                return _operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool ActivateDeActivateReport(int reportId, int userId)
        {
            try
            {
                var report = _operationalDataContext.AutomaticReports.FirstOrDefault(x => x.ReportId == reportId);

                if (report == null)
                    return false;

                report.IsDeleted = !report.IsDeleted;
                report.LastModifiedDate = DateTime.Now;
                report.LastModifiedBy = userId;

                return _operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public List<DiminsionRelativeTypeDTO> GetAllRelativeTypes()
        {
            var res = new List<DiminsionRelativeTypeDTO>();
            try
            {
                res = _operationalDataContext.DiminsionRelativeTypeIds.Select(x=> new DiminsionRelativeTypeDTO
                    {
                        ArabicName = x.RelativeTypeNameAR,
                        EnglishName = x.RelativeTypeNameEN,
                       RelativeTypeId = x.RelativeTypeId 
                    }).ToList();
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public List<AutomaticReportLogDTO> GetTodayLog()
        {
            var res = new List<AutomaticReportLogDTO>();
            try
            {
                var todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                res = _operationalDataContext.AutomaticReportsLog.Where(x => x.SendDate >= todayDate).Select(x=> new AutomaticReportLogDTO
                    {
                        EmailAddress = x.EmailAddress,
                        EmailSubject = x.EmailSubject,
                        EmailText = x.EmailText,
                        EnglishEmailText = x.EnglishEmailText,
                        EnglishSMSText = x.EnglishSMSText,
                        LogId = x.LogId,
                        MobileNumber = x.MobileNumber,
                        ReportGroupId = x.ReportGroupId,
                        SendDate = x.SendDate,
                        SMSText = x.SMSText,
                        UserId = x.UserId
                    }).ToList();
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public List<AutomaticReportDTO> GetAllReports(string templatePath)
        {
            var res = new List<AutomaticReportDTO>();
            try
            {
                res = _operationalDataContext.AutomaticReports.Where(x => x.IsDeleted == false).ToList().Select(y =>
                    new AutomaticReportDTO
                    {
                        Diminsion = y.AutomaticReportDiminsion.Where(x => x.IsDeleted == false).Select(x => new AutomaticReportDiminsionDTO
                        {
                            ExactValue = x.ExactValue,
                            IsDeleted = false,
                            IsStaticValue = x.IsStaticValue,
                            RelativeTypeId = x.RelativeTypeId,
                            RelativeValue = (int?)x.RelativeValue,
                            FieldName = x.ParameterDiminsions.FieldName
                        }).FirstOrDefault(),
                        Groups = y.AutomaticReportGroups.Where(x => x.IsDeleted == false).Select(x => new UserGroupsDTO
                        {
                            AssignedUsers = x.Group.GroupUsers.Where(z=> z.GroupId == x.GroupId).Select(z => new UsersDTO
                            {
                                Email = z.User.Email,
                                FullNameAr = z.User.FullNameAr,
                                FullNameEn = z.User.FullNameEn,
                                IsActive = z.User.IsActive ?? true,
                                PhoneNumber = z.User.PhoneNumber,
                                UserId = z.UserId
                            }).ToList(),
                            
                            GroupId = x.GroupId,
                            GroupName = x.Group.GroupName,
                            IsDeleted = false,
                            ReportGroupId = x.ReportGroupId

                        }).ToList(),
                        IsArabic = y.IsArabic,
                        IsDeleted = y.IsDeleted,
                        IsEmail = y.IsEmail,
                         IsEnglish = y.IsEnglish,
                         IsSMS = y.IsSMS,
                         ReportId = y.ReportId,
                         ReportName = y.ReportName,
                        Schedule = GetScheduleFromEntity(y.ReportSchedule),
                        Template = new ReportTemplateDTO
                        {
                            EmailSubjectArabic = y.ReportsTemplate.EmailSubjectArabic,
                            EmailSubjectEnglish = y.ReportsTemplate.EmailSubjectEnglish,
                            EmailTextArabic = GetTemplateText(templatePath,false,true,y.ReportsTemplate.TemplateId),
                            EmailTextEnglish = GetTemplateText(templatePath, false, false, y.ReportsTemplate.TemplateId),
                            IsDeleted = y.ReportsTemplate.IsDeleted,
                            IsEmail = y.ReportsTemplate.IsEmail,
                            IsSMS = y.ReportsTemplate.IsSMS,
                            SMSTextArabic = GetTemplateText(templatePath, true, true, y.ReportsTemplate.TemplateId),
                            SMSTextEnglish = GetTemplateText(templatePath, true, false, y.ReportsTemplate.TemplateId),
                            TemplateId = y.ReportsTemplate.TemplateId,
                            TemplateName = y.ReportsTemplate.TemplateName,
                            TemplateParameters = y.ReportsTemplate.ReportTemplateParameter.Where(x => x.IsDeleted == false).Select(z => new TemplateParameterDTO
                            {
                                isDeleted = false,
                                ParameterId = z.ParameterId,
                                ParameterName = z.TemplateParameters.ParameterName,
                                ParameterText = z.TemplateParameters.ParameterText,
                                CubeName = z.TemplateParameters.CubeName,
                                FieldName = z.TemplateParameters.FieldName,
                                ConnKeyName = z.TemplateParameters.ConStringKey,
                                IsCube = z.TemplateParameters.IsCube ?? true,
                                QueryPath = z.TemplateParameters.QueryFilePath
                            }).ToList()
                        },
                        TemplateId = y.TemplateId,
                        UserGroupIds = y.AutomaticReportGroups.Where(x => x.IsDeleted == false).Select(x=> x.GroupId).ToList()
                    }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return res;
        }

         public bool RemoveAssignedList(int reportId, List<int> userGroupsIds)
        {
             try
             {
                 var reportGroups = _operationalDataContext.AutomaticReportGroups.Where(x => x.ReportId == reportId && x.IsDeleted == false).ToList();
                 if(reportGroups != null && reportGroups.Any())
                 {
                     var groupsToDelete = reportGroups.Where(x => userGroupsIds.Contains(x.ReportGroupId)).ToList();
                     if(groupsToDelete != null && groupsToDelete.Any())
                     {
                         foreach (var item in groupsToDelete)
                         {
                             item.IsDeleted = true;
                         }
                     }
                 }
                 return _operationalDataContext.SaveChanges() > 0;
             }
             catch(Exception ex)
             {

             }
             return false;
        }
        private string GetTemplateText(string templatePath,bool isSMS,bool IsArabic,int templateId)
        {
            var path = string.Format(@"{0}\{1}\{2}\{3}.txt", templatePath, isSMS ? "SMS" : "Email", IsArabic ? "AR" : "EN", templateId);
            if (File.Exists(path))
                return File.ReadAllText(path);
            return "";
        }

        public bool EditAutomaticReport(AutomaticReportDTO report)
        {
             var res = false;
             try
             {
                 var entity = _operationalDataContext.AutomaticReports.FirstOrDefault(x => x.ReportId == report.ReportId);
                 
                 if(entity != null)
                 {
                     entity.IsArabic = report.IsArabic;
                     entity.IsDeleted = report.IsDeleted;
                     entity.IsEmail = report.IsEmail;
                     entity.IsEnglish = report.IsEnglish;
                     entity.IsSMS = report.IsSMS;
                     entity.ReportName = report.ReportName;
                     entity.TemplateId = report.TemplateId;
                     entity.LastModifiedBy = report.CurrentUserId;
                     entity.LastModifiedDate = DateTime.Now;

                     if (entity.AutomaticReportDiminsion.Any())
                     {
                         foreach (var item in entity.AutomaticReportDiminsion)
                         {
                             item.IsDeleted = true;
                         }
                     }

                     if (report.Diminsion != null)
                     {
                         entity.AutomaticReportDiminsion.Add(new AutomaticReportDiminsion
                         {
                             DiminsionId = 1,
                             RelativeTypeId = report.Diminsion.RelativeTypeId,
                             ExactValue = report.Diminsion.ExactValue,
                             IsDeleted = false,
                             IsStaticValue = report.Diminsion.IsStaticValue,
                             RelativeValue = report.Diminsion.RelativeValue,
                         });
                     }

                     if (report.Schedule != null)
                     {
                         entity.ReportSchedule.ScheduleTypeId = report.Schedule.ScheduleTypeId;
                         entity.ReportSchedule.ScheduleValues = PrepareScheduleValues(report.Schedule);
                         entity.ReportSchedule.SendHour = report.Schedule.SendHour;  
                     }

                     if (report.UserGroupIds != null && report.UserGroupIds.Any())
                     {
                         var toBeDeleted = entity.AutomaticReportGroups.Where(x => !report.UserGroupIds.Any(y => x.GroupId == y));
                         var toBeAdded = report.UserGroupIds.Where(x => !entity.AutomaticReportGroups.Any(y => y.GroupId == x));

                         if(toBeDeleted.Any())
                         {
                             foreach (var item in toBeDeleted)
                             {
                                 item.IsDeleted = true;
                             }
                         }

                         if (toBeAdded.Any())
                         {
                             foreach (var item in toBeAdded)
                             {
                                 entity.AutomaticReportGroups.Add(new AutomaticReportGroups
                                 {
                                     GroupId = item,
                                     IsDeleted = false,
                                 });
                             }
                         }

                     }
                     res = _operationalDataContext.SaveChanges() > 0;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return res;
        }
        public bool CreateAutomaticReport(AutomaticReportDTO report)
        {
            var res = false;
            try
            {
                var entity = new AutomaticReports()
                {
                    CreatedBy = report.CurrentUserId,
                    DateCreated = DateTime.Now,
                    IsArabic = report.IsArabic,
                    IsDeleted = false,
                    IsEmail = report.IsEmail,
                    IsEnglish = report.IsEnglish,
                    IsSMS = report.IsSMS,
                    ReportName = report.ReportName,
                    TemplateId = report.TemplateId
                };
               
                if(report.Diminsion != null)
                {
                    entity.AutomaticReportDiminsion.Add(new AutomaticReportDiminsion
                        {
                           DiminsionId = 1,
                           RelativeTypeId = report.Diminsion.RelativeTypeId,
                           ExactValue = report.Diminsion.ExactValue,
                           IsDeleted = false,
                           IsStaticValue = report.Diminsion.IsStaticValue,
                           RelativeValue = report.Diminsion.RelativeValue,
                        });
                }

                if(report.Schedule != null)
                {
                    entity.ReportSchedule = new ReportSchedule
                    {
                        ScheduleTypeId = report.Schedule.ScheduleTypeId,
                        ScheduleValues = PrepareScheduleValues(report.Schedule),
                        SendHour = report.Schedule.SendHour
                    };
                }
                
                if(report.UserGroupIds != null && report.UserGroupIds.Any())
                {
                    foreach (var item in report.UserGroupIds)
                    {
                        entity.AutomaticReportGroups.Add(new AutomaticReportGroups
                        {
                            GroupId = item,
                            IsDeleted = false,
                        }); 
                    }
                }

                _operationalDataContext.AutomaticReports.Add(entity);

                res = _operationalDataContext.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return res;
        }

        private string PrepareScheduleValues(AutomaticScheduleDTO schedule)
        {
            var res = "";
            if (schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Monthly)
            {
                if(schedule.IsLastDayInMonth.HasValue && schedule.IsLastDayInMonth.Value)
                {
                    res += "32,";
                }
                if(schedule.IsFirstDayInMonth.HasValue && schedule.IsFirstDayInMonth.Value)
                {
                    res += "1,";
                }
                if(schedule.DayNumberInMonth.HasValue)
                {
                    res += schedule.DayNumberInMonth.Value + ",";
                }
            }

            else if(schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Weekly)
            {
                foreach (var item in schedule.WeeksDay)
                {
                    res += item + ",";
                }
            }

            return res.TrimEnd(',');
        }

        public AutomaticScheduleDTO GetScheduleFromEntity(ReportSchedule schedule)
        {
            var res = new AutomaticScheduleDTO();
            res.ScheduleTypeId = schedule.ScheduleTypeId;
            if(schedule.ScheduleTypeId == (int)ScheduleTypesEnum.Monthly)
            {
                var tempList = schedule.ScheduleValues.Split(',');
                if(tempList.Any())
                {
                    foreach (var item in tempList)
                    {
                        if(item == "32")
                        {
                            res.IsLastDayInMonth = true;
                        }
                        else if(item == "1")
                        {
                            res.IsFirstDayInMonth = true;
                        }
                        else
                        {
                            int dayNum = 0;
                            if(int.TryParse(item,out dayNum))
                            {
                                res.DayNumberInMonth = dayNum;
                            }
                        }
                    }
                }
            }

            else if(schedule.ScheduleTypeId == (int) ScheduleTypesEnum.Weekly)
            {
                 var tempList = schedule.ScheduleValues.Split(',');
                 if (tempList.Any())
                 {
                     foreach (var item in tempList)
                     {
                         int dayNum = 0;
                         if (int.TryParse(item, out dayNum))
                         {
                             res.WeeksDay.Add(dayNum); // 0 Sunday
                         }
                     }
                 }
            }
            res.SendHour = schedule.SendHour;
            return res;
        }
        private string GetNotificationTemplatePath(string templatePath,string type,string lang)
        {
            return string.Format(@"{0}\{1}\{2}", templatePath,type,lang);
        }

        public bool RecordLogs(List<AutomaticReportLogDTO> logs)
        {
            var res = true;
            try
            {
                foreach (var item in logs)
                {
                    _operationalDataContext.AutomaticReportsLog.Add(new AutomaticReportsLog
                        {
                            EmailAddress = item.EmailAddress,
                            EmailSubject = item.EmailSubject,
                            EmailText = item.EmailText,
                            EnglishEmailText = item.EnglishEmailText,
                            EnglishSMSText = item.EnglishSMSText,
                            MobileNumber = item.MobileNumber,
                            ReportGroupId = item.ReportGroupId,
                            SendDate = item.SendDate,
                            SMSText = item.SMSText,
                            UserId = item.UserId
                        });
                }
                res = _operationalDataContext.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return res;
        }
        private void PrepareNotificationText(ReportTemplateDTO item,string templatePath)
        {
            var arEmailTemplatePath = string.Format(@"{0}\{1}.txt",GetNotificationTemplatePath(templatePath,"Email","AR"),item.TemplateId);
            var enEmailTemplatePath = string.Format(@"{0}\{1}.txt",GetNotificationTemplatePath(templatePath,"Email","EN"),item.TemplateId);
            var arSMSTemplatePath = string.Format(@"{0}\{1}.txt",GetNotificationTemplatePath(templatePath,"SMS","AR"),item.TemplateId);
            var enSMSTemplatePath = string.Format(@"{0}\{1}.txt",GetNotificationTemplatePath(templatePath,"SMS","EN"),item.TemplateId);

            if(File.Exists(arEmailTemplatePath))
            {
                item.EmailTextArabic = File.ReadAllText(arEmailTemplatePath);
            }

            if (File.Exists(enEmailTemplatePath))
            {
                item.EmailTextEnglish = File.ReadAllText(enEmailTemplatePath);
            }

            if (File.Exists(arSMSTemplatePath))
            {
                item.SMSTextArabic = File.ReadAllText(arSMSTemplatePath);
            }

            if (File.Exists(enSMSTemplatePath))
            {
                item.SMSTextEnglish = File.ReadAllText(enSMSTemplatePath);
            }
        }
    }
}
