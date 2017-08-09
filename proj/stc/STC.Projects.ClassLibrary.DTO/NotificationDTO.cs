using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STC.Projects.ClassLibrary.DTO
{
    public class NotificationDTO
    {
        public long NotificationId { get; set; }
        public DateTime DateCreated { get; set; }
        public int LastStatus { get; set; }
        public DateTime? LastModified { get; set; }
        public int? OwnerId { get; set; }
        public int? LastModifiedBy { get; set; }
        public bool IsNoticed { get; set; }
    }
}
