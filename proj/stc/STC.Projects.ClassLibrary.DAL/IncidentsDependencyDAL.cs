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
    public class IncidentsDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<IncidentsDTO> _incidentsBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<Incident> _notification;
        public IncidentsDependencyDAL(DTO.Interfaces.IDependencySignalR<IncidentsDTO> incidentsBL)
        {
            _incidentsBL = incidentsBL;
        }

        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.Incident.Where(x => !x.IsNoticed.Value);

                _notification = new ImmediateNotificationRegister<Incident>(_operationDB, query);
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
                    if (_incidentsBL != null && changed != null && changed.Any())
                    {
                        _incidentsBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }

        public void UpdateChanged(List<IncidentsDTO> changed)
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                foreach (var item in changed)
                {
                    var entity = _operationDB.Incident.FirstOrDefault(x => x.IncidentId == item.IncidentId);
                    entity.IsNoticed = true;
                }
                _operationDB.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private List<IncidentsDTO> GetUpdated()
        {
            try
            {
                IncidentsDAL incidentsDAL = new IncidentsDAL();

                return incidentsDAL.GetUpdatedIncidents();
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
