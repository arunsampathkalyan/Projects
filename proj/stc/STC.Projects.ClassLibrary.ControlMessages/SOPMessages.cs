using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class SOPGeneralMessage
    {
        public object OriginalObject { get; set; }

        public string MessageId { get; set; }

        public SOPSources GeneralType { get; set; }

        public long NotificationId { get; set; }
    }

    public class ScaleMessage
    {
        public double Width { get; set; }
    }

}
