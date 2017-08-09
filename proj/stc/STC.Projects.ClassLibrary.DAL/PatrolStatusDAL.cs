using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class PatrolStatusDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<PatrolStatusDimDTO> GetPatrolStatusList()
        {
            try
            {
                var lstPatrols = operationalDataContext.PatrolStatusDIMViews
                    .Select(PatrolStatus => new PatrolStatusDimDTO
                    {
                        PatrolStatusId = PatrolStatus.PatrolStatusId,
                        PatrolStatus = PatrolStatus.PatrolStatus
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
