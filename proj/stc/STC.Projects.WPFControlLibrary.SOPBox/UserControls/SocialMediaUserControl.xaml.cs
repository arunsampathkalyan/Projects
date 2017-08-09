using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Facebook;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using TweetSharp;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for SocialMediaUserControl.xaml
    /// </summary>
    public partial class SocialMediaUserControl : UserControl
    {
        private SocialMediaUserControlViewModel _socialMediaUserControlViewModel;
        public SocialMediaUserControl()
        {
            InitializeComponent();

            _socialMediaUserControlViewModel = new SocialMediaUserControlViewModel();

            this.DataContext = _socialMediaUserControlViewModel;

        }

        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            try
            {
                var handler = CloseCanvas;
                if (handler != null)
                    handler(this, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        public event GoToNextStepEventHandler GoToNextStep;
        protected virtual void OnGoToNextStep(GoToNextStepEventArgs E)
        {
            try
            {
                var handler = GoToNextStep;
                if (handler != null)
                    handler(this, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup(false);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void DoneClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup(true);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }


        private void ClosePopup(bool Confirmation)
        {
            OnCloseCanvas(new CanvasEventArgs());

            OnGoToNextStep(new GoToNextStepEventArgs
            {
                Confirmation = Confirmation
            });

            ResetControls();
        }

        private void ResetControls()
        {
            //TxtBrowseFile.Text = "";

            //MessageText.Text = "";

            _socialMediaUserControlViewModel.FilePath = "";
            _socialMediaUserControlViewModel.MsgToPost = "";


            LayoutRoot.Visibility = Visibility.Visible;
            ConfirmationGrid.Visibility = Visibility.Hidden;
        }

        private void PublishEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                radBusyIndicator.IsBusy = true;
                var task = new Task(PublishEvent);
                task.Start();
                task.ContinueWith((x) => Application.Current.Dispatcher.Invoke(() => radBusyIndicator.IsBusy = false));
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private async void PublishEvent()
        {

            bool tweetSent = true;
            var isTwitter = false;
            var msg = "";
            var filePath = "";
            var isFacebook = false;

            ServiceLayerReference.ServiceLayerClient client = new ServiceLayerReference.ServiceLayerClient();
            //Application.Current.Dispatcher.Invoke(() => isFacebook = FacebookCheckBox.IsChecked.HasValue && FacebookCheckBox.IsChecked.Value);

            //Application.Current.Dispatcher.Invoke(() => msg = MessageText.Text);
            //Application.Current.Dispatcher.Invoke(() => filePath = TxtBrowseFile.Text);

            //Application.Current.Dispatcher.Invoke(() => isTwitter = TwitterCheckBox.IsChecked.HasValue && TwitterCheckBox.IsChecked.Value);




            MemoryStream ms = null;
            try
            {

                System.Drawing.Image img = System.Drawing.Image.FromFile(_socialMediaUserControlViewModel.FilePath);

                ms = new MemoryStream();

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                //ms = null;
            }

            bool facebookSent = true;

            try
            {
                //if (isTwitter)
                if (_socialMediaUserControlViewModel.IsTwitterChecked)
                {
                    var resTwitter = client.PublishToTwitterAsync(_socialMediaUserControlViewModel.MsgToPost, ms == null ? null : ms.ToArray());
                    tweetSent = await resTwitter;
                }

               
                //if (isFacebook)
                if (_socialMediaUserControlViewModel.IsFaceBookChecked)
                {
                    var resFaceBook = client.PublishToFacebookAsync(_socialMediaUserControlViewModel.MsgToPost, ms == null ? null : ms.ToArray());
                    facebookSent = await resFaceBook;
                    //facebookSent = PublishToFacebook(msg, filePath);
                }
            }
            catch (Exception ex)
            {
            }

            

            if (tweetSent && facebookSent)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LayoutRoot.Visibility = Visibility.Hidden;
                    ConfirmationGrid.Visibility = Visibility.Visible;
                });

                Task.Delay(1000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    OnGoToNextStep(new GoToNextStepEventArgs
                    {
                        Confirmation = true
                    });
                    ClosePopup(true);
                });
            }
        }

        private bool PublishToFacebook(string textMsg, string filePath)
        {
            var accessToken = "CAAXLAeGYyqcBABZBVHAIJtS9Fdi9PXbdeIzIQOY0vIEJTjn5pqp9VFz5KbEhEJq574TPPdXGcOzwyzrkGjWty330stkoB9kJrOqOdZCiJp5rE0fV6LSfldDZBSNr7LNzXqy3zeJELa8HNZBRlo13D2E6s1wzSXzSfWF7p7sZBgO8Ifr0m3ZBzJj79egaWSHb4kagQotxroBgZDZD";

            FacebookClient app = new FacebookClient
            {
                AppId = "1630583823846055",
                AppSecret = "1bf1ef58eccdebd1014301d5db7bc523",
                AccessToken = accessToken
            };
            try
            {
                dynamic messagePost = new ExpandoObject();
                messagePost.access_token = accessToken;

                //messagePost.picture = UploadImage();//"https://s3.amazonaws.com/kinlane-productions/microsoft/visual-studio.jpg";
                //messagePost.link = "[SOME_LINK]";
                //messagePost.name = "[SOME_NAME]";
                //messagePost.description = MessageText.Text;

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    //Upload the file 
                    string ftplocation = "ftp://stc-connect.cloudapp.net/Files/";
                    string file = filePath;
                    string user = @"CloudApp\SoftecAdmin";
                    string password = "P@ssw0rd32";
                    var fileName = UploadToFTP(ftplocation, file, user, password);

                    messagePost.url = "http://stc-connect.cloudapp.net/MediaFiles/Files/" + fileName;
                    messagePost.caption = textMsg;

                    var result = app.Post("/632841543517582/photos", messagePost);
                }
                else
                {
                    messagePost.message = textMsg;

                    var result = app.Post("/632841543517582/feed", messagePost);
                }

                return true;

            }
            catch (FacebookOAuthException ex)
            {
                //handle something
                return false;
            }
            catch (FacebookApiException ex)
            {
                //handle something else
                return false;
            }
        }

        static string UploadToFTP(String FtpServerAndPath, String FullPathToLocalFile, String Username, String Password)
        {
            // Get the local file name: C:\Users\Rhyous\Desktop\File1.zip
            // and get just the filename: File1.zip. This is so we can add it
            // to the full URI.
            String filename = System.IO.Path.GetFileName(FullPathToLocalFile);

            // Open a request using the full URI, c/file.ext
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FtpServerAndPath + "/" + filename);

            // Configure the connection request
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(Username, Password);
            request.UsePassive = false;
            request.UseBinary = true;
            request.KeepAlive = false;

            // Create a stream from the file
            FileStream stream = File.OpenRead(FullPathToLocalFile);
            byte[] buffer = new byte[stream.Length];

            // Read the file into the a local stream
            stream.Read(buffer, 0, buffer.Length);

            // Close the local stream
            stream.Close();

            // Create a stream to the FTP server
            Stream reqStream = request.GetRequestStream();

            // Write the local stream to the FTP stream
            // 2 bytes at a time
            int offset = 0;
            int chunk = (buffer.Length > 2048) ? 2048 : buffer.Length;
            while (offset < buffer.Length)
            {
                reqStream.Write(buffer, offset, chunk);
                offset += chunk;
                chunk = (buffer.Length - offset < chunk) ? (buffer.Length - offset) : chunk;
            }
            // Close the stream to the FTP server
            reqStream.Close();

            return filename;
        }

        private bool Tweet(string textMsg, string filePath)
        {
            try
            {
                var _consumerKey = "sKYbEuMWcgHVstzMNI2LOGzPP";// "rbZAEqnJlTuOALvQuJVwTv4Gr";
                var _consumerSecret = "JOGniXtq1ozm4x5Kg5HpaJKw4TCpuKcD7RNFVIGuGfda9sZFWN";//"8aKU3z8WfuZocaBsQx50sw16fgnptX3nGzNnPLluge2fF8sITk";
                var _accessToken = "3244195770-fBcXK8QFz7zQSyNyfq6YkLYrtl8l16Q0bIiA9Sr";//"21481943-ugmojCmL3nd3lX9IM5elidGBpxqREifI5FJSDFXRW";
                var _accessTokenSecret = "J2JQqupHCZOHYdD2TU6bMrh2yiIFYD2YdIaQFcCw48uv4";//"r5neeSY6xM5JZsEQwxJgIn6bBxAvMLepIm73zF15ze6wq";

                var service = new TwitterService(_consumerKey, _consumerSecret);
                service.AuthenticateWith(_accessToken, _accessTokenSecret);

                TwitterStatus twitterStatus;

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    var dicImages = new Dictionary<string, Stream>();

                    dicImages.Add("temp", new FileStream(filePath, FileMode.Open));

                    var twitterTask = service.BeginSendTweetWithMedia(new SendTweetWithMediaOptions
                    {
                        Status = textMsg,
                        Images = dicImages
                    });
                    twitterStatus = service.EndSendTweetWithMedia(twitterTask);
                }
                else
                {
                    var twitterTask = service.BeginSendTweet(new SendTweetOptions
                    {
                        Status = textMsg,
                    });
                    twitterStatus = service.EndSendTweet(twitterTask);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void BtnBrowse_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                bool? result = ofd.ShowDialog();

                if (result == true)
                {
                    //TxtBrowseFile.Text = ofd.FileName;

                    _socialMediaUserControlViewModel.FilePath = ofd.FileName;

                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

    }


}
