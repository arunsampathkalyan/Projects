using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for MediaPlayerWindow.xaml
    /// </summary>
    public partial class MediaPlayerWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private bool suppressMediaPositionUpdate = false;

        public MediaPlayerWindow(string vidSource)
        {
            InitializeComponent();
            this.Loaded += MediaPlayerWindow_Loaded;
            mePlayer.MediaFailed += mePlayer_MediaFailed;
            this.MouseLeftButtonDown += MediaPlayerWindow_MouseLeftButtonDown;
            // this.Owner = Application.Current.MainWindow;
            if (!string.IsNullOrEmpty(vidSource)) mePlayer.Source = new Uri(vidSource);
           
            btnPause.Visibility = Visibility.Collapsed;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void mePlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }

        void MediaPlayerWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        void MediaPlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mePlayer.Source != null)
            {
                mePlayer.Play(); mediaPlayerIsPlaying = true;
                ControlButtonsVisiblity(mediaPlayerIsPlaying);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                suppressMediaPositionUpdate = true;
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.mpg;*.mpeg)|*.mp3;*.mp4;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
            ControlButtonsVisiblity(mediaPlayerIsPlaying);
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
            mediaPlayerIsPlaying = false;
            ControlButtonsVisiblity(mediaPlayerIsPlaying);
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
            ControlButtonsVisiblity(mediaPlayerIsPlaying);
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (suppressMediaPositionUpdate)
            {
                suppressMediaPositionUpdate = false;
            }
            else
            {
                mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
            }
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void ControlButtonsVisiblity(bool isMediaPlayerPlaying)
        {
            if (mediaPlayerIsPlaying)
            {
                btnPlay.Visibility = Visibility.Collapsed;
                btnPause.Visibility = Visibility.Visible;
            }
            else
            {
                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Collapsed;
            }
        }


    }
}
