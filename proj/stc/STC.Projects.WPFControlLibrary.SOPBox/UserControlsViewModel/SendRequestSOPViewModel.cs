using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.MessageBoxControl;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class SendRequestSOPViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        public SendRequestSOPViewModel()
        {

            _trackID = "TR-" + DateTime.Now.ToString("MMddHHmmss");
        }

        private string _trackID;


        public string TrackID
        {
            get { return _trackID; }            
        }



        private string _requestMessage;


        public string RequestMessage
        {
            get { return _requestMessage; }
            set
            {
                _requestMessage = value;
                
                RaiseNotifyPropertyChanged();
            }
        }
       

       

        #region IDataErrorInfo

        public string Error
        {
            get { return string.Empty; }
        }

        public String this[string columnName]
        {
            get
            {
                string result = string.Empty;


                return result;
            }
        }

        public Command SubmitCommand { get { return new Command((SubmitExecute)); } }

        private void SubmitExecute()
        {
            string closeMsg = Utility.GetLang() == "en" ? "Are you sure, want to close the send request to central operation room?" : "؟";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            //closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                //AddBusinessRuleResult(true);
                
            }
        }

        #endregion

        #region INotifyPropertyChanged interface

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaiseNotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}