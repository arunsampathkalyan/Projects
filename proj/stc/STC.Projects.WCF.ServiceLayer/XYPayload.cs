using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    [Serializable]
    public class XYPayload
    {
        public XYPayload()
        {
        }


        /// <summary>
        /// Gets or sets the y coordinate of the payload.
        /// </summary>
        public string TransporterPlateNumber { get; set; }
        public long AssetId { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        /// <summary>
        /// Gets or sets the x coordinate of the payload.
        /// </summary>
        public long ViolationTypeId { get; set; }
        public int OverSpeedKM { get; set; }
        public int TrafficLightSecs { get; set; }
        public bool IsInsideCity { get; set; }
        public string Message { get; set; }

        public DateTime EventTime { get; set; } 

    }
}