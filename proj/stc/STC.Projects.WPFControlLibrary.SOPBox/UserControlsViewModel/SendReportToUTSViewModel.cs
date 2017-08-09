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
    public class SendReportToUTSViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        public SendReportToUTSViewModel()
        {

            
        }

        


        private string _reportMessage;


        public string ReportMessage
        {
            get { return _reportMessage; }
            set
            {
                _reportMessage = value;
                
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
            string closeMsg = Utility.GetLang() == "en" ? "Are you sure, want to close the send Report to UTS?" : "؟";
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