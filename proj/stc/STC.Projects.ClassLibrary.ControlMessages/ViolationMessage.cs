using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class ViolationMessage
    {
        public long Id { set; get; }
        
        public string AssetId { get; set; }
        
        public string AssetCode { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public double Altitude { get; set; }
        
        public int LocationCode { get; set; }
        
        public int ViolationTypeId { get; set; }
        
        public string ViolationTypeName { get; set; }
        
        public DateTime DateTaken { get; set; }
        
        public int SpeedLimit { get; set; }
        
        public int MesuredSpeed { get; set; }
        
        public int CapturedSpeed { get; set; }
        
        public string PlateNumber { get; set; }
        
        public int VehicleTypeId { get; set; }
        
        public string VehicleTypeName { get; set; }
        
        public string PlateColorName { get; set; }
        
        public string PlateSourceName { get; set; }
        
        public string PlateTypeName { get; set; }
        
        public string PlateKindName { get; set; }
        
        public int? Count { get; set; }
    }
}
