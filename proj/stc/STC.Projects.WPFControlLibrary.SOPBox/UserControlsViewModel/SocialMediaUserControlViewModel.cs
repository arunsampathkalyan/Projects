using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class SocialMediaUserControlViewModel : System.ComponentModel.INotifyPropertyChanged, IDataErrorInfo
    {

        public SocialMediaUserControlViewModel()
        {
            _isFaceBookChecked = true;
            _isTwitterChecked = true;
            //_isPublishButtonEnabled = true;
            _msgRemainingLength = _twitterMaximumChar = 140;
        }

        private int _twitterMaximumChar;

        public int TwitterMaximumChar
        {
            get { return _twitterMaximumChar; }
            set
            {
                _twitterMaximumChar = value;
                MsgRemainingLength = (!string.IsNullOrEmpty(MsgToPost)) ? TwitterMaximumChar - MsgToPost.Length : TwitterMaximumChar;
                RaiseNotifyPropertyChanged();

                //RaiseNotifyPropertyChanged("MsgRemainingLength");
            }
        }


        private string _msgToPost;


        public string MsgToPost
        {
            get { return _msgToPost; }
            set
            {
                _msgToPost = value;
                MsgRemainingLength = (!string.IsNullOrEmpty(_msgToPost)) ? TwitterMaximumChar - _msgToPost.Length : TwitterMaximumChar;
                RaiseNotifyPropertyChanged();
            }
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                if (!string.IsNullOrEmpty(_filePath))
                {
                    TwitterMaximumChar = 116;
                }
                else
                {
                    TwitterMaximumChar = 140;
                }
                //IsMaxCharsCrossed = IsTwitterChecked && !string.IsNullOrEmpty(MsgToPost) && MsgToPost.Length > TwitterMaximumChar;
                RaiseNotifyPropertyChanged();
                RaiseNotifyPropertyChanged("MsgToPost");
            }
        }

        private int _msgRemainingLength;

        public int MsgRemainingLength
        {
            get
            {
                return _msgRemainingLength;
            }
            set
            {
                _msgRemainingLength = value;
                if (value < 0 && IsTwitterChecked)
                    IsMaxCharsCrossed = true;
                RaiseNotifyPropertyChanged();
            }
        }

        private bool _isMaxCharsCrossed;

        public bool IsMaxCharsCrossed
        {
            get { return _isMaxCharsCrossed; }
            set
            {
                _isMaxCharsCrossed = value;
                IsPublishButtonEnabled = !_isMaxCharsCrossed && MsgRemainingLength < 140;
                RaiseNotifyPropertyChanged();
            }
        }

        private bool _isFaceBookChecked;

        public bool IsFaceBookChecked
        {
            get { return _isFaceBookChecked; }
            set
            {
                _isFaceBookChecked = value;
                RaiseNotifyPropertyChanged();
            }
        }


        private bool _isTwitterChecked;

        public bool IsTwitterChecked
        {
            get { return _isTwitterChecked; }
            set
            {
                _isTwitterChecked = value;
                RaiseNotifyPropertyChanged();
                RaiseNotifyPropertyChanged("MsgToPost");
            }
        }

        private bool _isPublishButtonEnabled;

        public bool IsPublishButtonEnabled
        {
            get { return _isPublishButtonEnabled; }
            set
            {
                _isPublishButtonEnabled = value;
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

                switch (columnName)
                {

                    case "MsgToPost":
                        {
                            if (IsTwitterChecked && !string.IsNullOrEmpty(MsgToPost) && MsgToPost.Length > TwitterMaximumChar)
                            {
                                result = Utility.GetLang() == "ar"
                                    ? " تم تجاوز  عدد الحروف المسموحة"
                                    : "You exceeded the no of allowed characters";

                                IsMaxCharsCrossed = true;
                            }
                            else
                            {
                                IsMaxCharsCrossed = false;
                            }
                            break;
                        }

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