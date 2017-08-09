using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using STC.Projects.WPFControlLibrary.SOPBox.UserControls;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using System.Windows.Media.Imaging;
using STC.Projects.WPFControlLibrary.SOPBox.Converters;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    public class DangerousVehicleViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        ServiceLayerClient _client = new ServiceLayerClient();

        public Command ShowVideoCommand { get; set; }
        public Command ShowImageCommand { get; set; }

        private ImagePopupViewModel _imagePoupVM;
        private string _currentPlateNumber { get; set; }
        public ImagePopupViewModel ImagePoupVM
        {
            get { return _imagePoupVM; }
            set { _imagePoupVM = value; this.RaiseNotifyPropertyChanged(); }
        }

        private DangerousVehicleDetailsDTO _dangerousVehicleDetails;

        public DangerousVehicleDetailsDTO DangerousVehicleDetails
        {
            get { return _dangerousVehicleDetails; }
            set { _dangerousVehicleDetails = value; this.RaiseNotifyPropertyChanged(); }
        }

        public ObservableCollection<CubeDTO> LineChart
        { get; set; }

        //public ObservableCollection<LineChartdata> LineChartCollection { get; set; }

        //public ObservableCollection<LineChartModel> PieChartCollection { set; get; }

        public DangerousVehicleViewModel()
        {

            LineChart = new ObservableCollection<CubeDTO>();
            ShowVideoCommand = new Command(ShowVideo);
            ShowImageCommand = new Command(ShowImage);

            //ImagePoupVM = new ImagePopupViewModel();
            //DangerousVehicleDetails.VehicleViolations = new ViolationNotificationDTO[1] { new ViolationNotificationDTO { ViolationTypeId = 1 } };
            //LineChartCollection = GetSampleData();

            //PieChartCollection = new ObservableCollection<LineChartModel>();
            //PieChartCollection.Add(new LineChartModel() { Text = "السرعة الزائدة", Value = 25 });
            //PieChartCollection.Add(new LineChartModel() { Text = "تجاوز الإشارة الضوئية الحمراء", Value = 15 });
            //PieChartCollection.Add(new LineChartModel() { Text = "عدم ترك مسافة كافية", Value = 25 });
            //PieChartCollection.Add(new LineChartModel() { Text = "أستخدام الجوال اثناء القيادة", Value = 35 });
        }

        private void ShowVideo(object violationNotificationDTO)
        {
            var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;
            string videopath = _client.GetViolationVideoURLById(violationNotification.ViolationNotificationId);

            ImagePoupVM.SourceURL = "Video";
            //(imagePopup.DataContext as ImagePopupViewModel).VideoURL = "http://hubblesource.stsci.edu/sources/video/clips/details/images/hst_1.mpg";
            if (videopath != null)
            {
                ImagePoupVM.VideoURL = videopath;
                ImagePoupVM.ShowStream();
                //imagePopup.Show();
            }
        }
        private void ShowImage(object violationNotificationDTO)
        {
            var violationNotification = (ViolationNotificationDTO)violationNotificationDTO;

            ImagePoupVM.SourceURL = "Image";
            //(imagePopup.DataContext as ImagePopupViewModel).ImageURLList = new List<string>() { "https://upload.wikimedia.org/wikipedia/commons/c/c0/Salik's_Al_Garhoud_Bridge_Toll_Gate.jpg", "https://upload.wikimedia.org/wikipedia/commons/c/c0/Salik's_Al_Garhoud_Bridge_Toll_Gate.jpg" };
            List<string> imagepathList = _client.GetViolationImageURLsById(violationNotification.ViolationNotificationId).ToList();
            //List<Byte[]> imagesBytesList = _client.GetViolationImagesById(violationNotification.ViolationNotificationId).ToList();

            //if (imagesBytesList != null && imagesBytesList.Count > 0)
            //{
            //    (imagePopup.DataContext as ImagePopupViewModel).ImagesBytesList = imagesBytesList;
            //    (imagePopup.DataContext as ImagePopupViewModel).ShowStream();
            //    imagePopup.Show();
            //}

            if (imagepathList != null && imagepathList.Count > 0)
            {
            //    List<BitmapImage> imageBitmapList = new List<BitmapImage>();
            //    Base64ImageConverter base64Image = new Base64ImageConverter();
            //    BitmapImage btm;

            //    foreach (string base64Item in imagepathList)
            //    {
            //        btm = (BitmapImage)base64Image.Convert(base64Item, null, null, null);
            //        imageBitmapList.Add(btm);
            //    }

            //    ImagePoupVM.ImageURLBitmap = imageBitmapList;
            //    ImagePoupVM.ShowStream();  
            //    
            //    //imagePopup.Show();


                ImagePoupVM.ImageURLList = imagepathList;
                ImagePoupVM.ShowStream();
            }

           
        }

        public void SetCurrentPlateNumber(string plateNumber)
        {
            _currentPlateNumber = plateNumber;
        }

        public void GetDangerousVehicleDetails()
        {
            var callTask = _client.GetDangerousVehicleDetailsByPlateNumberAsync(_currentPlateNumber, Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_DangerousVehicleDetails(x));
        }

        private void Add_DangerousVehicleDetails(DangerousVehicleDetailsDTO data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DangerousVehicleDetails = data;
                if (DangerousVehicleDetails.VehicleLinerKPIs != null && DangerousVehicleDetails.VehicleLinerKPIs.Length > 0)
                    LineChart.Add(DangerousVehicleDetails.VehicleLinerKPIs[0]);
            });
        }

        private ObservableCollection<LineChartdata> GetSampleData()
        {
            var result = new ObservableCollection<LineChartdata>();

            result.Add(new LineChartdata()
            {
                LineColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52d726")),

                LegendName = "محمد بن احمد ",

                Violations = new ObservableCollection<LineChartModel>
                {
                                new LineChartModel() { Text = "يناير", Value = 5},
                                new LineChartModel() { Text = "فبراير", Value = 1},
                                new LineChartModel() { Text = "مارس", Value = 3},
                                new LineChartModel() { Text = "أبريل", Value = 8},
                                new LineChartModel() { Text = "مايو", Value = 12},
                                new LineChartModel() { Text = "يونيو", Value = 17},
                                new LineChartModel() { Text = "يوليو", Value = 19},
                                new LineChartModel() { Text = "أغسطس", Value = 18},
                                new LineChartModel() { Text = "سبتمبر", Value = 14},
                                new LineChartModel() { Text = "أكتوبر", Value = 19},
                                new LineChartModel() { Text = "نوفمبر", Value = 18},
                                new LineChartModel() { Text = "ديسمبر", Value = 14}
                }
            });

            return result;
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

    public class LineChartdata
    {
        public System.Windows.Media.Brush LineColor { get; set; }
        public string LegendName { get; set; }
        public ObservableCollection<LineChartModel> Violations { set; get; }
    }
}
