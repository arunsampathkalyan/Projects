using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Main
{
    public class AssetsChangeBL: IDependencySignalR<AssetLastStatusDTO>
    {
        IPublisher<AssetLastStatusDTO> _assetHub=null;
        AssetsChangeDependencyDAL _assetsDAL = null;
        public AssetsChangeBL(IPublisher<AssetLastStatusDTO> assetHub)
        {
            _assetHub = assetHub;
        }
        public void RegisterDependency()
        {
          _assetsDAL = new AssetsChangeDependencyDAL(this);
            _assetsDAL.RegisterDependency();
        }
        public void Notify(List<AssetLastStatusDTO> changedAssets)
        {
            if (_assetHub != null)
            {
                _assetHub.Publish(changedAssets);
               
            }
        }
    }
}
