using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
   public class ViewModelLocator
    {
       private AssignedPatrolsViewModel _assignedPatrols;
       public AssignedPatrolsViewModel AssignedPatrols
        {
            get
            {
                {
                    return _assignedPatrols ?? (_assignedPatrols = new AssignedPatrolsViewModel());
                }
            }
        }
    }
}
