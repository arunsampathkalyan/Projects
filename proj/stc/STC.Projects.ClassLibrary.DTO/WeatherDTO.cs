using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
   public class WeatherDTO
    {
       public int Id { get; set; }
       public double Lat { get; set; }
       public double Lon { get; set; }
       public string Alert { get; set; }
       public string Type { get; set; }
       public DateTime IssueDate { get; set; }
    }
}
