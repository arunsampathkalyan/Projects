using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using System.Windows.Media.Animation;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ServiceLayerReference;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel
{
    public class AlertUserControlViewModel : System.ComponentModel.INotifyPropertyChanged, IDataErrorInfo
    {
        ServiceLayerClient client = null;

        public AlertUserControlViewModel(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            client = new ServiceLayerClient();
            _supervisorNotificationDTO = supervisorNotificationDTO;

            if (_supervisorNotificationDTO != null && _supervisorNotificationDTO.ResponseToId == null)
            {
                GetUserName(_supervisorNotificationDTO.SenderId);
            }
        }

        private SupervisorNotificationDTO _supervisorNotificationDTO;

        public SupervisorNotificationDTO SupervisorNotificationDTO
        {
            get { return _supervisorNotificationDTO; }
            set { _supervisorNotificationDTO = value; RaiseNotifyPropertyChanged(); }
        }



        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; RaiseNotifyPropertyChanged(); }
        }

        private string _reportedUserName;

        public string ReportedUserName
        {
            get { return _reportedUserName; }
            set { _reportedUserName = value; RaiseNotifyPropertyChanged(); }
        }

        private bool _showApproveRejectButtons;

        public bool ShowApproveRejectButtons
        {
            get { return _showApproveRejectButtons; }
            set { _showApproveRejectButtons = value; RaiseNotifyPropertyChanged(); }
        }


        private void GetUserName(int userId)
        {
            var userDetails = client.GetUserByIdAsync(userId);
            userDetails.ContinueWith(x => Application.Current.Dispatcher.Invoke(() =>
            {
                ReportedUserName = (Utility.GetLang() == "ar" ? x.Result.FullNameAr : x.Result.FullNameEn);
            }
            ));
        }
        public void SetSupervisorNotificationNoticed(SupervisorNotificationDTO notification, bool isNoticed)
        {
            client.SetSupervisorNotificationNoticedAsync(notification.SupervisorNoticationId, isNoticed);
        }

        //public Command ViewDetailsCommand { get { return new Command((ViewDetailsDangerousViolatorExecute)); } }
        //public Command ViolationHistoryCommand { get { return new Command((ViolationHistoryOfDangerousViolatorExecute)); } }
        //public Command ReportDangerousViolatorCommand { get { return new Command((ReportDangerousViolatorExecute)); } }

        public Command CloseDetailsPopupCommand { get { return new Command((CloseDangerousViolatorPopupExecute)); } }


        private void CloseDangerousViolatorPopupExecute()
        {
            //Storyboard CloseDetailsPopup = (Storyboard)TryFindResource("CloseViolatorDetails");
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

                switch (columnName)
                {


                }

                return result;
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