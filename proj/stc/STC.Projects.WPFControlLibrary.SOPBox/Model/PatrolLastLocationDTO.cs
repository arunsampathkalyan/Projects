using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference
{
    public partial class PatrolLastLocationDTO : INotifyPropertyChanged
    {
        private string strImgCheckedSource;

        public string ImgCheckedSource
        {
            get { return strImgCheckedSource; }
            set
            {
                strImgCheckedSource = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImgCheckedSource"));
            }
        }

        private bool _isAssigned;

        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { _isAssigned = value; RaisePropertyChanged("IsAssigned"); }
        }


        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    
}
