using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.ControlMessages
{
  public  class RepositionMessage
    {
      public ICollection<PatrolLastLocationDTO> Patrols { get; set; }
      public bool IsAddToMap { get; set; }
      
    }
}
