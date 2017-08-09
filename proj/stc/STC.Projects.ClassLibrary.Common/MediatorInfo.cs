using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace STC.Projects.ClassLibrary.Common
{
    public class MediatorInfo
    {
        public UserControl RegisteredControl { get; set; }
        public List<Type> RegisteredTypes { get; set; }
        public MediatorInfo()
        {
            RegisteredTypes = new List<Type>();
        }
        public MediatorInfo(UserControl RegisteredControl, List<Type> RegisteredTypes)
        {
            this.RegisteredTypes = RegisteredTypes;
            this.RegisteredControl = RegisteredControl;
        }
    }

    public class PublisherMediatorInfo
    {
        public UserControl RegisteredControl { get; set; }
        public List<PublisherMessages> RegisteredTypes { get; set; }
        public PublisherMediatorInfo()
        {
            RegisteredTypes = new List<PublisherMessages>();
        }
        public PublisherMediatorInfo(UserControl RegisteredControl, List<PublisherMessages> RegisteredTypes)
        {
            this.RegisteredTypes = RegisteredTypes;
            this.RegisteredControl = RegisteredControl;
        }
    }
}
