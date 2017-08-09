using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class ZoomOnMap { public double Longitude { set; get; } public double Latitude { set; get; } public int Scale { set; get; } }
    public class ShowMapNotification { };
    public class ClearMapNotificationData { };
    public class LoadMapNotificationData
    {
        public MapNotificationPopUpDTO MapPopupContent { get; set; }
        public LoadMapNotificationData()
        {
            MapPopupContent = new MapNotificationPopUpDTO();
        }
    }
    public class ViewViolationLayer{ }
    public class ViewIncidentLayer { }
    public class ViewNotificationLayer { }
    public class ViewPatrolLayer { }
    public class HideAllLayers { }

    public class ViolationToDraw
    {
        public ViolationToDraw()
        {
            ViolationObj = new ViolationNotificationDTO();
        }

        public double Longitude { set; get; } 
        
        public double Latitude { set; get; } 
        
        public string Id { set; get; }

        public int ViolationTypeId { get; set; }

        public ViolationNotificationDTO ViolationObj { get; set; }
    } 
    public class IncidentToDraw {

        public IncidentToDraw()
        {
            IncidentObj = new IncidentsDTO();
        }

        public double Longitude { set; get; } 
        
        public double Latitude { set; get; } 
        
        public long Id { set; get; } 
        
        public string IncidentTypeName { set; get; } 
        
        public string IncidentAddress { set; get; }

        public int IncidentTypeId { get; set; }

        public IncidentsDTO IncidentObj { get; set; }

        public bool IsSOPPublish { get; set; }

    }

    public class ShowNotHandledEventNotification
    {
        public List<UserUserControlDTO> Events { get; set; }
    }

    public class ShowNotHandlingEventNotification
    {
        public List<UserUserControlDTO> Events { get; set; }
    }
    
}
