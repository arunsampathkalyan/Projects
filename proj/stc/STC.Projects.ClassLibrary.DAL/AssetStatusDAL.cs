using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class AssetStatusDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<AssetStatusDimDTO> GetAssetStatusList()
        {
            try
            {
                var lstPatrols = operationalDataContext.AssetStatusDIMViews
                    .Select(AssetStatus => new AssetStatusDimDTO
                    {
                        AssetStatus = AssetStatus.AssetStatus,
                        AssetStatusId = AssetStatus.AssetStatusId
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
