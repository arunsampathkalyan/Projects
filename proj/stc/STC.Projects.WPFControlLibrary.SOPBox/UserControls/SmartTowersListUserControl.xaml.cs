using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Messaging;
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
using STC.Projects.ClassLibrary.Common.ExtensionClasses;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using Telerik.Windows.Controls;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for SmartTowersListUserControl.xaml
    /// </summary>
    public partial class SmartTowersListUserControl : UserControl, IUserControl
    {
        public SmartTowersListUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();
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
        void detailsCtrl_CloseCanvas(object sender, CanvasEventArgs e)
        {
            try
            {
                OnCloseCanvas(e);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }


        public event CanvasEventHandler AddChildContent;
        protected virtual void OnAddChildContent(CanvasEventArgs E)
        {
            try
            {
                var handler = AddChildContent;
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

        private void TowersList_OnSelectionChanged(object Sender, SelectionChangedEventArgs E)
        {
            try
            {
                var selectedItem = TowersList.SelectedItem as AssetsViewDTO;
                if (selectedItem == null)
                    return;

                TowersListViewModel vm = DataContext as TowersListViewModel;
                if (vm == null)
                    return;

                //vm.PopupCanvas.Children.Clear();

                //vm.PopupCanvas.Children.Add(new SmartTowerDetailsUserControl(selectedItem, vm.PopupCanvas));

                //Canvas.SetRight(vm.PopupCanvas, 820);

                //vm.PopupCanvas.Visibility = Visibility.Visible;

                var detailsCtrl = new SmartTowerDetailsUserControl(selectedItem);
                detailsCtrl.CloseCanvas += detailsCtrl_CloseCanvas;

                CanvasEventArgs canvasEventArgs = new CanvasEventArgs
                {
                    ChildControl = detailsCtrl,
                    Width = 820
                };

                OnAddChildContent(canvasEventArgs);


                PublishMessages(selectedItem);
                TowersList.SelectedItem = null;
                //selectedItem.ImgCheckedSource = "../images/true.png";
                //if (vm.SetCheckedList(selectedItem))
                //{
                //    var parent = GetParent() as SOPList;
                //    if (parent == null)
                //        return;
                //    var parentVM = parent.DataContext as SOPViewModel;
                //    if (parentVM == null)
                //        return;
                //    parentVM.SetStepChecked(vm);
                //    //parent.
                //}
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private UserControl GetParent()
        {
            var parent = this.VisualParent.ParentOfType<UserControl>() as UserControl;
            return parent;
        }

        private void PublishMessages(AssetsViewDTO selectedTower)
        {
            if (selectedTower.Longitude.HasValue && selectedTower.Latitude.HasValue)
            {
                var clearNotificationLayer = new SOPMapClearObjects();
                var drawMessage = new SOPMapDraw() { Lat = selectedTower.Latitude.Value, Lon = selectedTower.Longitude.Value, ObjectTypeToDraw = (int)MarkerType.Assets, ObjectToDraw = selectedTower };
                var zoomMessage = new SOPMapZoom() { Lat = selectedTower.Latitude.Value, Lon = selectedTower.Longitude.Value };
                var parent = GetParent();
                if (parent == null)
                    return;
                //parent.Publish(clearNotificationLayer);
                parent.Publish(drawMessage);
                parent.Publish(zoomMessage);
            }
        }
    }
}
