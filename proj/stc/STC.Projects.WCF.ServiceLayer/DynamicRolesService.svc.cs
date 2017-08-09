using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DynamicRolesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DynamicRolesService.svc or DynamicRolesService.svc.cs at the Solution Explorer and start debugging.
    public class DynamicRolesService : IDynamicRolesService
    {
        public bool SaveRule(DynamicRulesDTO rule)
        {
            return new DynamicRulesDAL().SaveRule(rule);
        }

        public List<BusinessRuleRegionDTO> GetAllRegions()
        {
            return new DynamicRulesDAL().GetAllRegions();
        }

        public List<DynamicRulesDTO> GetAllRules()
        {
            return new DynamicRulesDAL().GetAllRules(false);
        }

        public bool ActivateDeActivateRule(long ruleId, bool IsDeleted)
        {
            return new DynamicRulesDAL().ActivateDeactivateRule(ruleId, IsDeleted);
        }

        public DynamicRulesDTO ValidateBusinessRuleName(string ruleName)
        {
            return new DynamicRulesDAL().ValidateBusinessRuleName(ruleName);
        }
    }
}
