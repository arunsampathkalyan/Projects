using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using DependencyObject = System.Windows.DependencyObject;

namespace STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors
{
    public class PatrolsListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CrispTemplate { get; set; }
        public DataTemplate PatrolTempalte { get; set; }
        public DataTemplate PatrolOfficerTempalte { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var patrol = item as PatrolLastLocationDTO;
            return patrol.IsRecommended ? CrispTemplate : (patrol.isPatrol ? PatrolTempalte : PatrolOfficerTempalte);
            //var viewModel = ((FrameworkElement)container).DataContext as PatrolsListViewModel;
            //if (viewModel == null) return PatrolTempalte;
        
            //var index = viewModel.PatrolsList.IndexOf(patrol);
            //return index == 0 ? CrispTemplate : PatrolTempalte;
        }
    }
}
