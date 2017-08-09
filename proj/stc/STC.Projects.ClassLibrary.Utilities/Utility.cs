using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Utilities
{
    public static class Utility
    {
        public static string ConnectionString
        {
            get
            { return new System.Configuration.AppSettingsReader().GetValue("connectionString", typeof(string)).ToString(); }
        }
    }
}
