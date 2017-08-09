using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace STC.Projects.ClassLibrary.Common.PaggingControl
{
    /// <summary>
    /// Interaction logic for Pager.xaml
    /// </summary>
    public partial class Pager : UserControl
    {
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new PropertyMetadata(CurrentPagePropertyChanged));

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pager), new PropertyMetadata(CurrentPagePropertyChanged));

        public static void CurrentPagePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register("TotalPages", typeof(int), typeof(Pager), new PropertyMetadata(CurrentPagePropertyChanged));

        public static readonly RoutedEvent PageChangedEvent =
      EventManager.RegisterRoutedEvent("PageChangedEvent", RoutingStrategy.Direct,
      typeof(RoutedEventHandler), typeof(Pager));

        public event RoutedEventHandler PageChanged
        {
            add { AddHandler(PageChangedEvent, value); }
            remove { RemoveHandler(PageChangedEvent, value); }
        }

        public Pager()
        {
            InitializeComponent();
        }

        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }
        public int CurrentPage
        {
            get
            {
                return (int)GetValue(CurrentPageProperty);
            }
            set
            {
                SetValue(CurrentPageProperty, value);
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)GetValue(TotalPagesProperty);
            }
            set
            {
                SetValue(TotalPagesProperty, value);
            }
        }

        private ObservableCollection<PageVM> _pageList;
        public ObservableCollection<PageVM> PageList
        {
            get { return _pageList; }
            set { _pageList = value; }
        }

        public void SetSource()
        {
            int start = 0, end = 0;

            if (TotalPages <= PageSize)
            {
                btnPreviousPages.Visibility = Visibility.Collapsed;
                btnNextPages.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnPreviousPages.Visibility = Visibility.Visible;
                btnNextPages.Visibility = Visibility.Visible;
            }



            //int currentViewTotalPages = (TotalPages / PageSize) + ((TotalPages % PageSize == 0) ? 0 : 1);

            if (CurrentPage == 1)
            {
                start = 1;
                if (TotalPages >= PageSize)
                    end = PageSize;
                else
                    end = TotalPages;
            }
            else if (CurrentPage == TotalPages)
            {
                end = TotalPages;
                if (TotalPages > 0 && TotalPages >= PageSize)
                    start = TotalPages - (TotalPages % PageSize) + 1;
                else
                    start = 1;
            }
            else
            {

                start = (CurrentPage - (CurrentPage % PageSize)) + 1;

                if (start > CurrentPage)
                    start = CurrentPage - (PageSize - 1);
                if (start + (PageSize - 1) > TotalPages)
                    end = TotalPages;
                else
                    end = start + (PageSize - 1);

                //if (CurrentPage - 4 <= 1)
                //    start = 1;
                //else
                //    start = CurrentPage - 4;
                //if (CurrentPage + 4 >= TotalPages)
                //    end = TotalPages;
                //else
                //    end = CurrentPage + 4;



            }

            if (CurrentPage <= PageSize)
                btnPreviousPages.IsEnabled = false;
            else
                btnPreviousPages.IsEnabled = true;

            if (end < TotalPages)
                btnNextPages.IsEnabled = true;
            else
                btnNextPages.IsEnabled = false;

            PageList = new ObservableCollection<PageVM>();
            for (int i = start; i <= end; i++)
            {
                PageList.Add(new PageVM { Page = i, IsSelected = false });
            }
            if (CurrentPage > 0 && PageList.Count > 0 && CurrentPage % PageSize == 1)
                PageList[0].IsSelected = true;
            pagerList.ItemsSource = PageList;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            CurrentPage = int.Parse((((Border)sender).Child as TextBlock).Text);
            RaiseEvent(new RoutedEventArgs(Pager.PageChangedEvent));
            SetSource();
            PageList.Where(a => a.Page == int.Parse((((Border)sender).Child as TextBlock).Text)).FirstOrDefault().IsSelected = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSource();
        }

        private void btnPreviousPages_Click(object sender, RoutedEventArgs e)
        {

            int changeCurrentPage = ((CurrentPage % PageSize) != 0) ? (((CurrentPage - (CurrentPage % PageSize)) - PageSize) + 1) : (CurrentPage - PageSize) + 1;

            CurrentPage = (changeCurrentPage <= TotalPages) ? changeCurrentPage : CurrentPage;
            RaiseEvent(new RoutedEventArgs(Pager.PageChangedEvent));
            SetSource();
            //PageList.Where(a => a.Page == int.Parse((((Border)sender).Child as TextBlock).Text)).FirstOrDefault().IsSelected = true;
        }

        private void btnNextPages_Click(object sender, RoutedEventArgs e)
        {
            int changeCurrentPage = ((CurrentPage % PageSize) != 0) ? ((CurrentPage - (CurrentPage % PageSize)) + PageSize + 1) : (CurrentPage + 1);
            CurrentPage = (changeCurrentPage <= TotalPages) ? changeCurrentPage : CurrentPage;
            RaiseEvent(new RoutedEventArgs(Pager.PageChangedEvent));
            SetSource();
            //PageList.Where(a => a.Page == int.Parse((((Border)sender).Child as TextBlock).Text)).FirstOrDefault().IsSelected = true;
        }
    }

    public class PageVM : INotifyPropertyChanged
    {
        private int _page;
        public int Page
        {
            get { return _page; }
            set { _page = value; this.RaiseNotifyPropertyChanged(); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; this.RaiseNotifyPropertyChanged(); }
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
