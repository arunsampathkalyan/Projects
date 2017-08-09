using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class PatrolTrackDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<PatrolLastLocationDTO> _patrolLocationsBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<PatrolLastLocation> _notification;
        public PatrolTrackDependencyDAL(DTO.Interfaces.IDependencySignalR<PatrolLastLocationDTO> patrolLocationsBL)
        {
            _patrolLocationsBL = patrolLocationsBL;
        }
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.PatrolLastLocations.Where(x => !x.IsNoticed);

                _notification = new ImmediateNotificationRegister<PatrolLastLocation>(_operationDB, query);
                _notification.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {
                string lines = "Query Registeration Error exc";

                // Write the string to a file.
              //  System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");
               // file.WriteLine(lines);

               // file.Close();
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
                    if (_patrolLocationsBL != null && changed != null && changed.Any())
                    {
                        _patrolLocationsBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }

        public void UpdateChanged(List<PatrolLastLocationDTO> changed)
        {
            _operationDB = new STCOperationalDataContext();

            foreach (var item in changed)
            {
                var entity = _operationDB.PatrolLastLocations.FirstOrDefault(x => x.PatrolLatLocationId == item.PatrolLatLocationId);
                entity.IsNoticed = true;
            }
            _operationDB.SaveChanges();
        }

        private List<PatrolLastLocationDTO> GetUpdated()
        {
            try {
                PatrolsDAL patrolsDAL = new PatrolsDAL();

                return patrolsDAL.GetUpdatedPatrolsList(false);
            }
            catch (Exception ex)
            {
                string lines = "Getting Data Exc:" + ex.Message ;

                // Write the string to a file.
               // System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");
                //file.WriteLine(lines);

                //file.Close();
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
