using System;
using System.Collections.Generic;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.Legend;

namespace STC.Projects.WPFControlLibrary.LandingPage.ChartsUserControls
{
    /// <summary>
    /// Interaction logic for AccidentsDailyStatisticsUserControl.xaml
    /// </summary>
    public partial class AccidentsDailyStatisticsUserControl : UserControl
    {
        public AccidentsDailyStatisticsUserControl()
        {
            InitializeComponent();
        }

        private void Legend_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Border brder = (Border)sender;
            //brder.BorderThickness = new Thickness(0, 0, 0, 1);
            LegendItem item = brder.DataContext as LegendItem;
            BarSeries series = item.Presenter as BarSeries;
            RadCartesianChart chart = series.Chart as RadCartesianChart;

            foreach (BarSeries s in chart.Series)
            {
                s.Visibility = Visibility.Collapsed;
            }

            series.Visibility = Visibility.Visible;

            foreach (Border br in FindVisualChildren<Border>(chartLegend))
            {
                if (br.Name == "brdr")
                {
                    br.BorderThickness = new Thickness(0, 0, 0, 0);
                }

            }

            brder.BorderThickness = new Thickness(0, 0, 0, 1.5);


            /*
             
             In xaml you can pass the legend as a CommandParameter in the following way:
<telerik:RadLegend x:Name="legend">
<telerik:RadLegend.ItemTemplate>
  <DataTemplate>
      <telerik:RadButton Content="{Binding Title}"
            Command="{x:Static local:MyCommands.HideShowCommand}"
            CommandParameter="{Binding ElementName=legend}"
            Background="{Binding MarkerFill}"
            Tag="LegendItemButton"/>
    </DataTemplate>
</telerik:RadLegend.ItemTemplate>
</telerik:RadLegend>

In the CommandExecuteHandler handler in the code behind you can introduce these changes:
private static void CommandExecuteHandler(object sender, ExecutedRoutedEventArgs e)
{
	var legend = e.Parameter as RadLegend;
 var buttons = legend.ChildrenOfType<RadButton>().Where(x => x.Tag.ToString() == "LegendItemButton");
 
 RadButton button = (RadButton)sender;
 LegendItem item = button.DataContext as LegendItem;
 BarSeries series = item.Presenter as BarSeries;
 RadCartesianChart chart = series.Chart as RadCartesianChart;
 var items = chart.LegendItems;
 
 foreach (BarSeries s in chart.Series)
 {
   s.Visibility = Visibility.Collapsed;
 }
 foreach (var btn in buttons)
 {
  btn.Opacity= 0.5;
 }
 
 series.Visibility = Visibility.Visible;
 button.Opacity = 1;
}
This way when the user clicks on a legend button, the relevant series item will be highlighted and all the other legend buttons will fade.
             
             */
        }

        private void Chart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RadCartesianChart chart = sender as RadCartesianChart;

            foreach (BarSeries s in chart.Series)
            {
                s.Visibility = Visibility.Visible;
            }

            foreach (Border br in FindVisualChildren<Border>(chartLegend))
            {
                if (br.Name == "brdr")
                {
                    br.BorderThickness = new Thickness(0, 0, 0, 0);
                }
                // do something with tb here
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
