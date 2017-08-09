using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAutomaticReports" in both code and config file together.
    [ServiceContract]
    public interface IAutomaticReportsLayer
    {
        [OperationContract]
        List<TemplateParameterDTO> GetAllParameters();

        [OperationContract]
        bool AddTemplate(ReportTemplateDTO template);

        [OperationContract]
        bool EditTemplate(ReportTemplateDTO template);

        [OperationContract]
        List<ReportTemplateDTO> GetAllTemplates();

        [OperationContract]
        bool ActivateDeactivateTemplate(int templateId, int userId);

        [OperationContract]
        List<UserGroupsDTO> GetAllUserGroups(bool includeDeleted);
        [OperationContract]
        UserGroupsDTO GetGroupById(int groupId);
        [OperationContract]
        bool CreateUserGroup(UserGroupsDTO groupModel);
        [OperationContract]
        bool EditUserGroup(UserGroupsDTO groupModel);
        [OperationContract]
        bool ActivateDeActivateGroup(int groupId, int userId);
        [OperationContract]
        bool CreateAutomaticReport(AutomaticReportDTO report);
        [OperationContract]
        void HandleReports();
        [OperationContract]
        List<DiminsionRelativeTypeDTO> GetAllRelativeTypes();
        [OperationContract]
        List<AutomaticReportDTO> GetAllReports();
        [OperationContract]
        List<AutomaticReportDTO> PrepareReports();
        [OperationContract]
        bool RemoveAssignedList(int reportId, List<int> userGroupsIds);
        [OperationContract]
        bool EditAutomaticReport(AutomaticReportDTO report);
        [OperationContract]
        bool ActivateDeActivateReport(int report, int userId);
       
    }
}
