using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class ViolationDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<ViolationNotificationDTO> _violationBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<ViolationNotification> _notification;
        public ViolationDependencyDAL(DTO.Interfaces.IDependencySignalR<ViolationNotificationDTO> violationBL)
        {
            _violationBL = violationBL;
        }
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.ViolationNotifications.Where(x=> !x.IsNoticed);

                _notification = new ImmediateNotificationRegister<ViolationNotification>(_operationDB, query);
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
                    var changed = GetUpdated();
                    if (_violationBL != null && changed != null && changed.Any())
                    {
                        _violationBL.Notify(changed);

                        UpdateNoticed(changed);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }
        public void UpdateNoticed(List<ViolationNotificationDTO> changed)
        {
            _operationDB = new STCOperationalDataContext();
            foreach (var item in changed)
            {
                var entity = _operationDB.ViolationNotifications.FirstOrDefault(x => x.ViolationNotificationId == item.ViolationNotificationId);
                entity.IsNoticed = true;
            }
            _operationDB.SaveChanges();
        }
        private List<ViolationNotificationDTO> GetUpdated()
        {
            try {
                ViolationNotificationDAL violationNotificationDAL = new ViolationNotificationDAL();

                return violationNotificationDAL.GetUpdatedViolations();
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
