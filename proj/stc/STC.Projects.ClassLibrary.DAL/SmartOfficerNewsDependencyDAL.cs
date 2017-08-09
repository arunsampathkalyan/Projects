using STC.Projects.ClassLibrary.Common;
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
    public class SmartOfficerNewsDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<SmartOfficerNewsDTO> _officerNewsBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<SmartOfficerNew> _officerNews;
        public SmartOfficerNewsDependencyDAL(DTO.Interfaces.IDependencySignalR<SmartOfficerNewsDTO> officerNewsBL)
        {
            _officerNewsBL = officerNewsBL;
        }

        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.SmartOfficerNews.Where(x => x.IsNoticed.HasValue && x.IsNoticed.Value == false);

                _officerNews = new ImmediateNotificationRegister<SmartOfficerNew>(_operationDB, query);
                _officerNews.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }
        }

        public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    if (_officerNewsBL != null)
                    {
                        var changed = GetUpdated();

                        _officerNewsBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }
        }

        public void UpdateChanged(List<SmartOfficerNewsDTO> changed)
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                foreach (var item in changed)
                {
                    var entity = _operationDB.SmartOfficerNews.FirstOrDefault(x => x.OfficerNewsId == item.OfficerNewsId);

                    if (entity != null)
                        entity.IsNoticed = true;
                }

                _operationDB.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private List<SmartOfficerNewsDTO> GetUpdated()
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                var lstChanged = _operationDB.SmartOfficerNews.Where(x => x.IsNoticed == false).Select(x => new SmartOfficerNewsDTO()
                {
                    OfficerNewsId = x.OfficerNewsId,
                    OfficerNewsText = x.OfficerNewsText,
                    OfficerNewsImage = x.OfficerNewsImage,
                    OfficerNewsDate = x.OfficerNewsDate ?? DateTime.Now,
                    OfficerNewsCreatedBy = x.OfficerNewsCreatedBy ?? 0,
                    IsNoticed = x.IsNoticed ?? false

                }).ToList();

                return lstChanged;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }
    }
}
