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
    public class AssetsChangeDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<AssetLastStatusDTO> _assetChangeBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<AssetLastStatu> _notification;
        public AssetsChangeDependencyDAL(DTO.Interfaces.IDependencySignalR<AssetLastStatusDTO> assetChangeBL)
        {
            _assetChangeBL = assetChangeBL;
        }
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.AssetLastStatus.Where(x => !x.IsNoticed);

                _notification = new ImmediateNotificationRegister<AssetLastStatu>(_operationDB, query);
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
                    if (_assetChangeBL != null && changed != null && changed.Any())
                    {
                        _assetChangeBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }

        public void UpdateChanged(List<AssetLastStatusDTO> changed)
        {
            _operationDB = new STCOperationalDataContext();

            foreach (var item in changed)
            {
                var entity = _operationDB.AssetLastStatus.FirstOrDefault(x => x.AssetLastStatusId == item.AssetLastStatusId);
                entity.IsNoticed = true;
            }
            _operationDB.SaveChanges();
        }

        private List<AssetLastStatusDTO> GetUpdated()
        {
            try
            {              
                AssetsDAL assetsDAL = new AssetsDAL();

                return assetsDAL.GetAssetsLastStatusList(false);
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}