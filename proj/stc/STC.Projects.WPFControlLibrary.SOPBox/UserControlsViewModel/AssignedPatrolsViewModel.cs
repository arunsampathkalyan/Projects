using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.SOPBox.CrispService;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.TFMServiceReference;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.UserControls;

using STC.Projects.WPFControlLibrary.SOPBox.Converters;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class AssignedPatrolsViewModel : INotifyPropertyChanged
    {

        ServiceLayerClient client;
        TFMIntegrationServiceClient tfmClient;

        private ImagePopupViewModel _imagePoupVM;

        public ImagePopupViewModel ImagePoupVM
        {
            get { return _imagePoupVM; }
            set { _imagePoupVM = value; this.RaiseNotifyPropertyChanged(); }
        }

        public AssignedPatrolsViewModel()
        {
            AssignedPatrols = new ObservableCollection<PatrolDTO>();

            client = new ServiceLayerClient();
            tfmClient = new TFMIntegrationServiceClient();

            ShowVideoCommand = new Command(ShowVideo);
            ShowImageCommand = new Command(ShowImage);

        }
        private ObservableCollection<PatrolDTO> _assignedPatrols;

        public ObservableCollection<PatrolDTO> AssignedPatrols
        {
            get { return _assignedPatrols; }
            set { _assignedPatrols = value; RaiseNotifyPropertyChanged(); }
        }

        private ObservableCollection<PatrolDtoUI> _assignedPatrolsUI;
        
        public ObservableCollection<PatrolDtoUI> AssignedPatrolsUI
        {
            get { return _assignedPatrolsUI; }
            set { _assignedPatrolsUI = value; RaiseNotifyPropertyChanged(); }
        }



        public long NotificationId { get; set; }

        public void GetAssignedPatrols()
        {
            var _client = new ServiceLayerClient();
            AssignedPatrols.Clear();
            var patrols = _client.GetAssignedPatrols(NotificationId);
            foreach (var assignedPatrol in patrols)
            {
                AssignedPatrols.Add(assignedPatrol);
            }

            GetImagesAndVideosForAssignedPatrols();

        }


        private void GetImagesAndVideosForAssignedPatrols()
        {
            if (AssignedPatrols != null && AssignedPatrols.Count > 0)
            {
                AssignedPatrolsUI = new ObservableCollection<PatrolDtoUI>();
                foreach (var patrol in AssignedPatrols)
                {
                    if (patrol != null)
                    {

                        PatrolDtoUI patrolUI = new PatrolDtoUI() { PatrolDtoObj = patrol };

                        if (patrolUI.PatrolDtoObj.CurrentTaskId != 0)
                        {
                            try
                            {
                                //var imagePathListRes = tfmClient.GetTaskImagesURLsAsync(patrolUI.PatrolDtoObj.CurrentTaskId);
                                var imagePathListRes = tfmClient.GetTaskImagesURLsAsync(patrolUI.PatrolDtoObj.CurrentTaskId);
                                imagePathListRes.ContinueWith(x => AssignImagePathList(patrolUI, imagePathListRes.Result));

                                //var videoPathListRes = tfmClient.GetTaskVideosURLsAsync(patrolUI.PatrolDtoObj.CurrentTaskId);
                                var videoPathListRes = tfmClient.GetTaskVideosURLsTestAsync(patrolUI.PatrolDtoObj.CurrentTaskId);
                                videoPathListRes.ContinueWith(x => AssignVideoPathList(patrolUI, videoPathListRes.Result));
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        AssignedPatrolsUI.Add(patrolUI);
                    }
                }
            }
        }

        private void AssignImagePathList(PatrolDtoUI patrolDtoUiObj, string[] imagePathList)
        {
            if (patrolDtoUiObj != null && imagePathList != null)
                patrolDtoUiObj.ImagePathList = new List<string>(imagePathList);
        }

        private void AssignVideoPathList(PatrolDtoUI patrolDtoUiObj, string[] videoPathList)
        {
            if (patrolDtoUiObj != null && videoPathList != null)
            {
                patrolDtoUiObj.VideoPathList = new List<string>(videoPathList);
            }
        }


        public Command ShowVideoCommand { get; set; }
        public Command ShowImageCommand { get; set; }

        private void ShowVideo(object assignedPatrol)
        {
            try
            {
                var assignedPatrolDetails = (PatrolDtoUI)assignedPatrol;

                if (assignedPatrolDetails == null) return;
                //List<string> videopathList = tfmClient.GetTaskVideosURLs(assignedPatrolDetails.PatrolDtoObj.CurrentTaskId).ToList();

                ImagePoupVM.SourceURL = "Video";
                ImagePoupVM.ShowStream();
                if (assignedPatrolDetails.VideoPathList != null && assignedPatrolDetails.VideoPathList.Count > 0)
                {
                    //videopath = @"D:\split.mp4";
                    //ImagePoupVM.VideoURL = videopath;
                    
                   // var videoStream = new MemoryStream(Convert.FromBase64String(str));
                    foreach (var vidPath in assignedPatrolDetails.VideoPathList)
                    {
                        //MediaPlayerWindow mplayer = new MediaPlayerWindow(@"http://13.82.52.255/WCFService/Videos/112/10.wmv");
                        //MediaPlayerWindow mplayer = new MediaPlayerWindow(vidPath);
                       
                        //mplayer.Show();
                        //mplayer.Activate();
                        //mplayer.Focus();
                        //mplayer.Topmost = true;

                        ImagePoupVM.VideoURL = vidPath;
                    }

                }
            }
            catch (Exception ex)
            {

                //throw ex;
            }
        }
        private void ShowImage(object assignedPatrol)
        {
            try
            {
                var assignedPatrolDetails = (PatrolDtoUI)assignedPatrol;
                                                                     
                ImagePoupVM.SourceURL = "Image";
                ImagePoupVM.ShowStream(); 
                if (assignedPatrolDetails.PatrolDtoObj.CurrentTaskId != 0)
                {
                    string[] imagesURL = tfmClient.GetTaskImagesURLs(assignedPatrolDetails.PatrolDtoObj.CurrentTaskId);

                    assignedPatrolDetails.ImagePathList = imagesURL.ToList();
                    if (assignedPatrolDetails.ImagePathList != null && assignedPatrolDetails.ImagePathList.Count > 0)
                    {
                        List<string> imagepathList = assignedPatrolDetails.ImagePathList;
                        Base64ImageConverter base64Image = new Base64ImageConverter();
                        if (imagepathList != null && imagepathList.Count > 0)
                        {
                            List<BitmapImage> imageBitmapList = new List<BitmapImage>();
                            BitmapImage btm;
                         
  
                            foreach (string base64Item in imagepathList)
                            {
                                btm = (BitmapImage)base64Image.Convert(base64Item, null, null, null);
                                imageBitmapList.Add(btm);
                            }

                            ImagePoupVM.ImageURLBitmap = imageBitmapList;
                               
                            
                        }
                    }
                }                          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //public void ProcessMessage(WantedCarModel message)
        //{

        //    AssignedPatrols = new ObservableCollection<PatrolDTO>(GetAssignedPatrols(message.NotificationId));

        //}
        //public void ProcessMessage(WantedCarModel message)
        //{
        //    AssignedPatrols = new ObservableCollection<PatrolDTO>(GetAssignedPatrols(long.Parse(message.MessageId)));
        //}
        public void SetNotificationID(long notificationId)
        {
            NotificationId = notificationId;

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

    public class PatrolDtoUI : INotifyPropertyChanged
    {
        private bool _IsImageAvailable;

        public bool IsImageAvailable
        {
            get { return _IsImageAvailable; }
            set { _IsImageAvailable = value; this.RaiseNotifyPropertyChanged(); }
        }


        private bool _IsVideoAvailable;

        public bool IsVideoAvailable
        {
            get { return _IsVideoAvailable; }
            set { _IsVideoAvailable = value; this.RaiseNotifyPropertyChanged(); }
        }

        private List<string> _ImagePathList;

        public List<string> ImagePathList
        {
            get { return _ImagePathList; }
            set
            {
                _ImagePathList = value;
                if (value != null && value.Count > 0)
                    IsImageAvailable = true;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private List<string> _VideoPathList;

        public List<string> VideoPathList
        {
            get { return _VideoPathList; }
            set
            {
                _VideoPathList = value;
                if (value != null && value.Count > 0)
                    IsVideoAvailable = true;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private PatrolDTO _patrolDtoObj;

        public PatrolDTO PatrolDtoObj
        {
            get { return _patrolDtoObj; }
            set { _patrolDtoObj = value; this.RaiseNotifyPropertyChanged(); }
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
