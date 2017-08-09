using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class EventsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public object GetEventsList()
        {
            throw new System.NotImplementedException();
        }
        public UserUserControlDTO GetEventById(int messageId)
        {
            var res = new UserUserControlDTO();
            try
            {
                res = operationalDataContext.UsersUserControls.Where(x => x.UserUserControlsID == messageId)
                    .Select(y => new UserUserControlDTO()
                    {
                        IsNoticed = y.IsNoticed,
                        NotificationId = y.NotificationId.HasValue ? y.NotificationId.Value : 0,
                        XML = y.XML,
                        PriorityId = y.PriorityId
                    }).FirstOrDefault();
            }
            catch(Exception ex)
            {

            }
            return res;
        }
        public bool AddNewEvent(string XmlToSend,int? priorityId,out long MessageId)
        {
            try
            {
                UsersDAL usersDAL = new UsersDAL();
                var lstUsers = usersDAL.GetUsersList();
                var item = new UsersUserControl
                {
                    XML = XmlToSend,
                    IsNoticed = false,
                    PriorityId = priorityId,
                    UserUserControlUsers = new List<UserUserControlUser>(),
                    Notification = new Notification
                    {
                        DateCreated = DateTime.Now,
                        LastStatus = 1
                    }
                };

                foreach (var user in lstUsers)
                {
                    item.UserUserControlUsers.Add(new UserUserControlUser
                    {
                        UserId = user.UserId,
                    });
                }

                operationalDataContext.UsersUserControls.Add(item);

                var flag= operationalDataContext.SaveChanges() > 0;
                MessageId = item.UserUserControlsID;
                return flag;
                //UsersDAL usersDAL = new UsersDAL();

                //var lstUsers = usersDAL.GetUsersList();
                //var item = new UsersUserControl
                //{
                //    XML = XmlToSend,
                //    IsNoticed = false,
                //    UserUserControlUsers = new List<UserUserControlUser>()
                //};

                //foreach (var user in lstUsers)
                //{
                //    item.UserUserControlUsers.Add(new UserUserControlUser
                //        {
                //            UserId = user.UserId,
                //        });
                //}

                //operationalDataContext.UsersUserControls.Add(item);

               // return operationalDataContext.SaveChanges() > 0;
            }

            catch (Exception e)
            {
                MessageId = 0;
                return false;
            }
        }
    }
}
