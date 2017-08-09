using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common
{
    public class NameValueModel : INotifyPropertyChanged
    {
        int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; this.RaiseNotifyPropertyChanged(); }
        }
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; this.RaiseNotifyPropertyChanged(); }
        }

        #region INotifyPropertyChanged interface
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
