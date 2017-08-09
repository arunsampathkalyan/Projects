using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.WPFControlLibrary.AdminPage.ViewModel;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace STC.Projects.WPFControlLibrary.AdminPage
{
    /// <summary>
    /// Interaction logic for AdminPageUserControl.xaml
    /// </summary>
    public partial class UpdateRulesPageUserControl : UserControl, IUserControl
    {
        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate RoleUpdated;
        AdminPageControlViewModel vm = null;

        public UpdateRulesPageUserControl()
        {
            //vm = new AdminPageControlViewModel();
            //vm.RoleAdded += vm_RoleUpdated;
            //this.DataContext = vm;
            InitializeComponent();

        }

        public void vm_RoleUpdated(object sender, EventArgs e)
        {

            if (RoleUpdated != null)
                RoleUpdated(sender, e);


            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    var window = Window.GetWindow(this);
            //    if (window != null)
            //        window.Close();
            //});

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            string closeMsg = "Are you sure, want to close the Update Business Rule Screen?";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                Window.GetWindow(this).Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            string closeMsg = Utility.GetLang() == "en" ? "Are you sure you want to cancel any modifications?" : "سيتم الغاء أي تعديلات, هل انت متأكد؟";


            //if (Utility.GetLang() == "ar")
            //    closeMsg = "هل أنت متأكد من الغاء التعديل";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {

                //Window.GetWindow(this).Close();
            }
        }

        private void btnAddRule_Click(object sender, RoutedEventArgs e)
        {
            string closeMsg = "Are you sure, want to Add this Business Rule?";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {


                //Window.GetWindow(this).Close();
            }
        }


    }
}
