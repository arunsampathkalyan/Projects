using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Entities;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.DAL
{
    public class BusinessRuleDependencyDAL 
    {
        private DTO.Interfaces.IDependencySignalR<BusinessRulesDTO> _notificationBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<CorrelationBusinessRule> _notification;
        public BusinessRuleDependencyDAL(DTO.Interfaces.IDependencySignalR<BusinessRulesDTO> notificationBL)
        {
            _notificationBL = notificationBL;
        }
       
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.CorrelationBusinessRules.Select(x=> new {ruleId = x.BusinessRuleId,ruleInterval=x.RuleInterval,name=x.BusinessName,isDeleted=x.IsDeleted});
                
                _notification = new ImmediateNotificationRegister<CorrelationBusinessRule>(_operationDB, query);
                _notification.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }
       
        public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    if (_notificationBL != null)
                    {
                        _notificationBL.Notify(new List<BusinessRulesDTO>());
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }
    }
}
