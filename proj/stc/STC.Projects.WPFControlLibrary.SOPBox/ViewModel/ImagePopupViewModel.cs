using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace STC.Projects.WPFControlLibrary.SOPBox.ViewModel
{
    public class ImagePopupViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate ImageMediaSourceUpdated;
        public int itemsCount { get; set; }
        

        public ImagePopupViewModel()
        {
            _activeIndex = -1;
        }       

        private bool _canEnablePreviousButton;

        public bool CanEnablePreviousButton
        {
            get { return _canEnablePreviousButton; }
            set { _canEnablePreviousButton = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _canEnableNextButton;

        public bool CanEnableNextButton
        {
            get { return _canEnableNextButton; }
            set { _canEnableNextButton = value; this.RaiseNotifyPropertyChanged(); }
        }

        private int _activeIndex;

        public int ActiveIndex
        {
            get { return _activeIndex; }
            set
            {
                CanEnablePreviousButton = true;
                CanEnableNextButton = true;

                if (value == 0) { CanEnablePreviousButton = false; }
                if (itemsCount > 0)
                {
                    if (value <= itemsCount)
                        _activeIndex = value;

                    if (value >= itemsCount - 1)
                        CanEnableNextButton = false;
                }
                else
                {
                    CanEnableNextButton = false;
                    CanEnablePreviousButton = false;
                    _activeIndex = value;
                }



                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _videoURL;
        public string VideoURL
        {
            get { return _videoURL; }
            set
            {
                _videoURL = value;
                if (value != null)
                {                    
                    var handler = ImageMediaSourceUpdated;
                    if (handler != null)
                        ImageMediaSourceUpdated(this, new EventArgs());
                }
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
                if (value != null)
                {
                    itemsCount = value.Count;
                    //if (value.Count > 0)
                    //    ActiveIndex = 0;
                    ImageSrcType = "StringPath";
                    var handler = ImageMediaSourceUpdated;
                    if (handler != null)
                        ImageMediaSourceUpdated(this, new EventArgs());
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<BitmapImage> _imageURLBitmap;

        public List<BitmapImage> ImageURLBitmap
        {
            get { return _imageURLBitmap; }
            set
            {
                _imageURLBitmap = value;
                //if (value != null)
                //{
                itemsCount = value.Count;
                ImageSrcType = "BitmapImage";
                var handler = ImageMediaSourceUpdated;
                if (handler != null)
                    ImageMediaSourceUpdated(this, new EventArgs());
                //}
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

        private string _imageSrcType;
        public string ImageSrcType
        {
            get { return _imageSrcType; }
            set
            {
                _imageSrcType = value;
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