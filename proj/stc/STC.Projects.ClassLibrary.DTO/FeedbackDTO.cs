using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
   public class FeedbackDTO
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public int UserId { get; set; }
        public string Feedback { get; set; }
        public string FeedbackType { get; set; }

    }
}
