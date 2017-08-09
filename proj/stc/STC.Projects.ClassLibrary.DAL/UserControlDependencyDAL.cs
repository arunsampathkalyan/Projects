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
    public class UserControlDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<UserUserControlDTO> _userControlBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<UsersUserControl> _notification;
        public UserControlDependencyDAL(DTO.Interfaces.IDependencySignalR<UserUserControlDTO> userControlBL)
        {
            _userControlBL = userControlBL;
        }
        public bool SaveUserControl(string xmlToSend, List<string> usernames)
        {
            var db = new STCOperationalDataContext();
            var userEvent = new UsersUserControl
            {
                XML = xmlToSend,
                IsNoticed = false,
                UserUserControlUsers = new List<UserUserControlUser>()
            };
            foreach (var item in usernames)
            {
                var user = db.Users.Where(x => x.UserName == item).FirstOrDefault();
                if (user != null)
                    userEvent.UserUserControlUsers.Add(new UserUserControlUser() { UserId = user.UserId });
            }
            db.UsersUserControls.Add(userEvent);
            return db.SaveChanges() > 0;
        }
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.UsersUserControls.Where(x => !x.IsNoticed);

                _notification = new ImmediateNotificationRegister<UsersUserControl>(_operationDB, query);
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
                    Task.Delay(1000);
                    var changed = GetUpdated();
                    if (_userControlBL != null && changed != null && changed.Any())
                    {
                        _userControlBL.Notify(changed);

                        UpdateNoticed(changed);

                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }
        public void UpdateNoticed(List<UserUserControlDTO> changed)
        {
            _operationDB = new STCOperationalDataContext();
            foreach (var item in changed)
            {
                var entity = _operationDB.UsersUserControls.FirstOrDefault(x => x.UserUserControlsID == item.UserUserControlsID);
                entity.IsNoticed = true;
            }
            _operationDB.SaveChanges();
        }
        private List<UserUserControlDTO> GetUpdated()
        {
            try
            {
                var list = _operationDB.UserUserControlViews.Where(x => !x.IsNoticed && x.NotificationId.HasValue).Select(x => new UserUserControlDTO
                    {
                        UserName = x.UserName,
                        XML = x.XML,
                        UserUserControlsID = x.UserUserControlsID,
                        IsNoticed = x.IsNoticed,
                        NotificationId = x.NotificationId.HasValue ? x.NotificationId.Value : 0,
                        Notification = new NotificationDTO
                        {
                            NotificationId = x.NotificationId.HasValue? x.NotificationId.Value : 0,
                            DateCreated = DateTime.Now,
                            LastStatus= 1
                        }

                    }).ToList();
                if (list != null && list.Any())
                {
                    list.ForEach(x =>
                    {
                        x.PopupContent = STC.Projects.ClassLibrary.DTO.Helper.DesrializeXml(x.XML);
                    }
                        );
                }
                return list;
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
