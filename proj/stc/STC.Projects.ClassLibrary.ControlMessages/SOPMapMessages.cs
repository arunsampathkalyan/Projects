using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class SOPMapClearObjects
    {
    }

    public class SOPMapZoom
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class SOPMapDraw
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int ObjectTypeToDraw { get; set; }
        public object ObjectToDraw { get; set; }
    }

    public class ShowAllLayers { }
    public class ClearAllNotificationLayer 
    {
        public string MessageId { get; set; }
        public bool IsCancle { get; set; }
    }

    public class UnregisterLiveTrackingDependency { }

    public class AccessNotificationFromMapClick { }

    public class ZoomToExtend 
    {
        public ZoomToExtend()
        {
            points = new List<CustomPoints>();
        }
        public List<CustomPoints> points { get; set; } 
    }
}
