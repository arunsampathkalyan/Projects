using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.MapControl.ViewModel
{
    public class ImagePopupViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public ImagePopupViewModel()
        {
            
        }

        private string _videoURL;
        public string VideoURL
        {
            get { return _videoURL; }
            set
            {
                _videoURL = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<string> _imageURLList;
        public List<string> ImageURLList
        {
            get { return _imageURLList; }
            set
            {
                _imageURLList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<Byte[]> _imagesBytesList;
        public List<Byte[]> ImagesBytesList
        {
            get { return _imagesBytesList; }
            set
            {
                _imagesBytesList = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        private string _sourceURL;
        public string SourceURL
        {
            get { return _sourceURL; }
            set
            {
                _sourceURL = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _isVideoStreamingVisible;
        public bool IsVideoStreamingVisible
        {
            get { return _isVideoStreamingVisible; }
            set
            {
                _isVideoStreamingVisible = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        public void ShowStream()
        {
            if (SourceURL == "Image")
            {
                IsVideoStreamingVisible = false;
            }
            else if (SourceURL == "Video")
            {
                IsVideoStreamingVisible = true;
            }
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