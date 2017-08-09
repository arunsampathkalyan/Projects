using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL.Utilities
{
    public enum IncidentStatusEnum
    {
        Acknoledge = 1,
        Arrived = 2,
        SecondArrive = 3,
        Closed = 4,
        ManualAddIncident = 5,
        AddAndAssigned = 6,
        OnTheWay = 8,
        LateArrive = 9
    }
    public enum ImageCategory
    {
        Photo = 1,
        Video = 2
    }
}
