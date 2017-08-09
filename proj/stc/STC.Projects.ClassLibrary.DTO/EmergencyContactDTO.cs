using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class EmergencyContactDTO
    {
        public int Id { get; set; }
        public string AuthorityName { get; set; }
        public virtual ICollection<ContactItemDTO> Contacts { get; set; }
    }
}
