using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
   public class RoadSafetyTipDTO
    {
       public int Id { get; set; }
       public string Title { get; set; }
       public string Description { get; set; }
       public DateTime IssueDate { get; set; }

    }
}
