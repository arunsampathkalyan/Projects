using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDynamicRolesService" in both code and config file together.
    [ServiceContract]
    public interface IDynamicRolesService
    {
        [OperationContract]
        [WebGet]
        bool SaveRule(DynamicRulesDTO rule);

        [OperationContract]
        [WebGet]
        List<BusinessRuleRegionDTO> GetAllRegions();
        [OperationContract]
        [WebGet]
        List<DynamicRulesDTO> GetAllRules();
        [OperationContract]
        [WebGet]
        bool ActivateDeActivateRule(long ruleId, bool IsDeleted);
        [OperationContract]
        [WebGet]
        DynamicRulesDTO ValidateBusinessRuleName(string ruleName);
    }
}
