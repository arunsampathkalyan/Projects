using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.SOPBox.Model
{

    public class MessageTypeSOPParentModel : INotifyPropertyChanged
    {
        private bool isChecked;
        private string strImgCheckedSource;
        private string strBorderBackground;
        private string strRankForeground;

        private Canvas popupCanvas;

        public MessageTypeSOPParentModel()
        {
            SOPItems = new List<MessageTypeSOPModel>();
        }

        public int MessageTypeId { get; set; }


        public string MessageTypeName { get; set; }


        public int Rank { get; set; }


        public int SOPId { get; set; }


        public string SOPContent { get; set; }

        public Canvas PopupCanvas
        {
            get { return popupCanvas; }
            set
            {
                popupCanvas = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PopupCanvas"));
            }
        }

        public string ImgCheckedSource
        {
            get { return strImgCheckedSource; }
            set
            {
                strImgCheckedSource = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImgCheckedSource"));
            }
        }

        public string BorderBackground
        {
            get { return strBorderBackground; }
            set
            {
                strBorderBackground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BorderBackground"));
            }
        }

        public string RankForeground
        {
            get { return strRankForeground; }
            set
            {
                strRankForeground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RankForeground"));
            }
        }

        private bool _isSopItemCompleted;

        public bool IsSopItemCompleted
        {
            get { return _isSopItemCompleted; }
            set { _isSopItemCompleted = value; OnPropertyChanged(new PropertyChangedEventArgs("IsSopItemCompleted")); }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
            }
        }

        public bool IsParent { get; set; }

        public List<MessageTypeSOPModel> SOPItems { get; set; }


        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsExpanded"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    public class MessageTypeSOPModel : INotifyPropertyChanged
    {
        private string strBorderBackground;

        public MessageTypeSOPModel()
        {
            //UserControlModel = new List<object>();
            // UserControlSOPModel = new List<object>();
        }

        public string BorderBackground
        {
            get { return strBorderBackground; }
            set
            {
                strBorderBackground = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BorderBackground"));
            }
        }

        public List<object> UserControlModel { get; set; }
        public string UserControlDetailsControl { get; set; }
        public string DetailsUserControlMessageType { get; set; }
        public string DetailsMessageXSLT { get; set; }
        public string ListUserControlMessageType { get; set; }
        public string ListMessageXSLT { get; set; }

        public bool IsParent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}