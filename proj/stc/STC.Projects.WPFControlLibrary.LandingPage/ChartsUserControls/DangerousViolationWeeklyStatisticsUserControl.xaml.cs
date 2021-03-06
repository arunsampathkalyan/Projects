﻿using System;
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
    /// Interaction logic for DangerousViolationWeeklyStatisticsUserControl.xaml
    /// </summary>
    public partial class DangerousViolationWeeklyStatisticsUserControl : UserControl
    {
        public DangerousViolationWeeklyStatisticsUserControl()
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
