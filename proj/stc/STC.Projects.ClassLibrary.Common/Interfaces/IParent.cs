using STC.Projects.ClassLibrary.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
 

namespace STC.Projects.ClassLibrary.Common.Interfaces
{
    public interface IParent
    {
        void InlistSubscriber(UserControl UserControlItem, List<Type> TypesOfInterests);
        void InlistPublisher(UserControl UserControlItem, List<PublisherMessages> TypesOfPublish);
        void Publish(UserControl UserControlItem, Object Item);
        void LoadPage();
        void NavigateToPage(SystemPages page);
        string GetCurrentUsername();
        int GetCurrentUserId(); 
        object GetLastUnHandledNotification();
        void SetLastUnHandledNotification(object obj);
        void Logout();
        int GetPageId();
        Dictionary<string, int> GetUserFeatures();

    }
}
