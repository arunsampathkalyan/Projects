using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common
{
    public class EventDetailModel 
    {
        string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;}
        }
    }
}
