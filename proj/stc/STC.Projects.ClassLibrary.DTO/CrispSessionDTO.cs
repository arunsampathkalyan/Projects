using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class CrispSessionDTO
    {
        public long SessionId { get; set; }
        public string SessionName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public string NodeXmlPath { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }

        public int? MaxAllowedTimeMin { get; set; }
    }
}
