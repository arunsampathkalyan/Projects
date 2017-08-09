using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class KPITargetDTO
    {
        public int TargetID { get; set; }
        public string TargetName { get; set; }
        public string TaregtDescriptionEn { get; set; }
        public string TargetDescriptionAr { get; set; }
        public double TargetValue { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
