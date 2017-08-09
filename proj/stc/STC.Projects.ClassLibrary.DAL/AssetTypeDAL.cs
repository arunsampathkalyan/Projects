using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class AssetTypeDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<AssetTypeDimDTO> GetAssetTypesList()
        {
            try
            {
                var lstPatrols = operationalDataContext.AssetTypeDIMViews
                    .Select(AssetType => new AssetTypeDimDTO
                    {
                        AssetType = AssetType.AssetType,
                        AssetTypeId = AssetType.AssetTypeId
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
