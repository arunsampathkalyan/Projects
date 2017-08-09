using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Entities;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using System.Collections.Generic;
using System.Linq;

namespace STC.Projects.ClassLibrary.DAL
{
    public class CrispSessionDAL
    {
        STCOperationalDataContext _operation = new STCOperationalDataContext();
        public List<CrispSessionDTO> GetAvailableSessions()
        {
            var res = new List<CrispSessionDTO>();
            res = _operation.CrispSessions.Where(x => !x.IsDeleted).Select(y => 
                new CrispSessionDTO() { 
                    SessionName = y.SessionName,
                    CreatedBy = y.CreatedBy, 
                    DateCreated = y.DateCreated, 
                    DateModified = y.DateModified, 
                    EndDate = y.EndDate, 
                    EndHour = y.EndHour, 
                    IsDeleted = y.IsDeleted, 
                    ModifiedBy = y.ModifiedBy, 
                    NodeXmlPath = y.NodeXmlPath, 
                    SessionId = y.SessionId, 
                    StartDate = y.StartDate, 
                    StartHour = y.StartHour,
                    MaxAllowedTimeMin = y.MaxAllowedTimeMin
                }).ToList();
            return res;
        }

        public bool UpdateMaxAllowedTimePerSession(long sessionId, int maxAllowedTimeMins)
        {
            var session = _operation.CrispSessions.FirstOrDefault(x => x.SessionId == sessionId);
            if (session != null)
                session.MaxAllowedTimeMin = maxAllowedTimeMins;
            return _operation.SaveChanges() > 0;
        }
    }
}
