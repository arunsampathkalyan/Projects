using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class ViolationTypeDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<ViolationTypeDimDTO> GetViolationTypesList()
        {
            try
            {
                var lstPatrols = operationalDataContext.ViolationTypesDIMViews
                    .Select(ViolationType => new ViolationTypeDimDTO
                    {
                        ViolationType = ViolationType.ViolationType,
                        ViolationTypeId = ViolationType.ViolationTypeId
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
