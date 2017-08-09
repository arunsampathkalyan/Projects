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
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for RadarsListUserControl.xaml
    /// </summary>
    public partial class RadarsListUserControl : UserControl, IUserControl
    {
        public RadarsListUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();
        }

        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            var handler = CloseCanvas;
            if (handler != null)
                handler(this, E);
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

        private void RadarsList_OnSelectionChanged(object Sender, SelectionChangedEventArgs E)
        {
            try
            {
                var selectedItem = RadarsList.SelectedItem as AssetsViewDTO;
                if (selectedItem == null)
                    return;

                RadarsListViewModel vm = DataContext as RadarsListViewModel;
                if (vm == null)
                    return;

                //vm.PopupCanvas.Children.Clear();

                //vm.PopupCanvas.Children.Add(new RadarDetailsUserControl(selectedItem, vm.PopupCanvas));

                //Canvas.SetRight(vm.PopupCanvas, 750);

                //vm.PopupCanvas.Visibility = Visibility.Visible;

                var detailsCtrl = new RadarDetailsUserControl(selectedItem);
                detailsCtrl.CloseCanvas += detailsCtrl_CloseCanvas;

                CanvasEventArgs canvasEventArgs = new CanvasEventArgs
                {
                    ChildControl = detailsCtrl,
                    Width = 750
                };

                OnAddChildContent(canvasEventArgs);


                PublishMessages(selectedItem);
                //RadarsList.SelectedItem = null;
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

        private void PublishMessages(AssetsViewDTO selectedRadar)
        {
            if (selectedRadar.Longitude.HasValue && selectedRadar.Latitude.HasValue)
            {
                var clearNotificationLayer = new SOPMapClearObjects();
                var drawMessage = new SOPMapDraw() { Lat = selectedRadar.Latitude.Value, Lon = selectedRadar.Longitude.Value, ObjectTypeToDraw = (int)MarkerType.Assets, ObjectToDraw = selectedRadar };
                var zoomMessage = new SOPMapZoom() { Lat = selectedRadar.Latitude.Value, Lon = selectedRadar.Longitude.Value };
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
