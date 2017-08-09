using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference
{
    public partial class AssetsViewDTO : INotifyPropertyChanged
    {
        public TowerActionsDTO SelectedAction { get; set; }
        private string strImgCheckedSource;
        private string strCurrentVMSMessage;

        public double? SelectedSpeed { get; set; }

        public string ImgCheckedSource
        {
            get { return strImgCheckedSource; }
            set
            {
                strImgCheckedSource = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImgCheckedSource"));
            }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public string CurrentVMSMessage
        {
            get { return strCurrentVMSMessage; }
            set
            {
                strCurrentVMSMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentVMSMessage"));
            }
        }
    }

    
}
