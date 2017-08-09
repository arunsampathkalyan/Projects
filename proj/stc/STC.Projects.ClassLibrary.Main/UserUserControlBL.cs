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
    public class UserUserControlBL : IDependencySignalR<UserUserControlDTO>
    {
        IPublisher<UserUserControlDTO> _usercontrolsHub = null;
        UserControlDependencyDAL _usercontrolsDAL = null;
        public UserUserControlBL(IPublisher<UserUserControlDTO> usercontrolsHub)
        {
            _usercontrolsHub = usercontrolsHub;
        }
        public void RegisterDependency()
        {
            _usercontrolsDAL = new UserControlDependencyDAL(this);
            _usercontrolsDAL.RegisterDependency();
        }
        public void Notify(List<UserUserControlDTO> changedControls)
        {
            if (_usercontrolsHub != null)
            {
                _usercontrolsHub.Publish(changedControls);
            }
        }

        public bool Save(string xmlToSend, List<string> usernames)
        {
            return new UserControlDependencyDAL(null).SaveUserControl(xmlToSend, usernames);
        }
    }
}
