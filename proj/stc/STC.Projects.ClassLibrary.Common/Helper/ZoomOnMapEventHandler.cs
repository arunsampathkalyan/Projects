using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common.Helper
{
    public delegate void ZoomOnMapEventHandler(double? lat, double? lon, string plateNumber);
    public delegate void ZoomOnMapLiveTrackEventHandler(double orgLat, double lat, double orgLon, double lon, string plateNumber);
    public delegate void OpenPopups();
}
