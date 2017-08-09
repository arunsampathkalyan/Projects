using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.NavigationBar.ViewModel
{
    public class NavigationBarViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string checkBtn;

        public string CheckBtn
        {
            get { return checkBtn; }
            set
            {
                checkBtn = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        #endregion

        #region Constractors
        public NavigationBarViewModel()
        {
            CheckBtn = "Live";
        }
        #endregion

        #region Methods 

        public void ChangeCheckedButton(string str)
        {
            CheckBtn = str;
        }

        #endregion

        #region Notify Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
