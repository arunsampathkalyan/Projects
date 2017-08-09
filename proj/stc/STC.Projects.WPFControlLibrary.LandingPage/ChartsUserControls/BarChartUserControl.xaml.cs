using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STC.Projects.WPFControlLibrary.LandingPage.Model;

namespace STC.Projects.WPFControlLibrary.LandingPage.ChartsUserControls
{
    /// <summary>
    /// Interaction logic for BarChartUserControl.xaml
    /// </summary>
    public partial class BarChartUserControl : UserControl
    {
        public BarChartUserControl()
        {
            InitializeComponent();
        }


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(BarChartUserControl), new PropertyMetadata(string.Empty, OnCaptionPropertyChanged));
        private static void OnCaptionPropertyChanged(DependencyObject dependencyObject,
               DependencyPropertyChangedEventArgs e)
        {
            var myUserControl = dependencyObject as BarChartUserControl;
            if (myUserControl != null && e.NewValue != null) 
                myUserControl.HeaderText.Text = e.NewValue.ToString();
        }





        public ObservableCollection<BarChartModel> DataItemSource
        {
            get { return (ObservableCollection<BarChartModel>)GetValue(DataItemSourceProperty); }
            set { SetValue(DataItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataItemSourceProperty =
            DependencyProperty.Register("DataItemSource", typeof(ObservableCollection<BarChartModel>), typeof(BarChartUserControl), new PropertyMetadata(null,OnItemSourceChanged));

        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myUserControl = d as BarChartUserControl;
            if (myUserControl != null && e.NewValue != null)
                myUserControl.BarSeries.ItemsSource = (ObservableCollection<BarChartModel>)e.NewValue;
        }

        

        
    }
}
