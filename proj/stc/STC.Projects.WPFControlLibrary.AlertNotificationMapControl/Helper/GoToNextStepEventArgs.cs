using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper
{
    public class GoToNextStepEventArgs : EventArgs
    {
        public bool Confirmation { get; set; }

        public SupervisorNotificationDTO Notification { get; set; }
    }

    public class ProcessNextItemEventArgs : EventArgs
    {
        public bool CanProcessNextItem { get; set; }

        //public SupervisorNotificationStatus Status { get; set; }
    }
}
