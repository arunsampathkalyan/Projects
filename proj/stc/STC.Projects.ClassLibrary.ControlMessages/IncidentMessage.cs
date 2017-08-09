using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class IncidentMessage
    {
        public long Id { set; get; }
        
        public string IncidentNumber { get; set; }

        
        public int IncidentTypeId { get; set; }

        
        public string IncidentTypeName { get; set; }

        
        public int PriorityId { get; set; }

        
        public string PriorityName { get; set; }

        
        public double Latitude { get; set; }

        
        public double Longitude { get; set; }

        
        public string IncidentAddress { get; set; }

        
        public string CallerName { get; set; }

        
        public string CallerNumber { get; set; }

        
        public string CallerAddress { get; set; }

        
        public string CallerLanguage { get; set; }

        
        public int? CallTakerId { get; set; }

        
        public string CallTakerName { get; set; }

        
        public int StatusId { get; set; }

        
        public string StatusName { get; set; }

        
        public int? DispatcherId { get; set; }

        
        public string DispatcherName { get; set; }

        
        public DateTime? CreatedTime { get; set; }

        
        public DateTime? DispatcheTime { get; set; }

        
        public DateTime? ArrivedTime { get; set; }

        
        public DateTime? EndTime { get; set; }
    }
}
