using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class ShowNotificationBox { };

    public class FinishEvent { public string EventId { get; set; } };

    public class OpenNotification { public string MessageId { get; set; } }

    public class ChangeNotificationStatus { public string MessageId { get; set; } }
}
