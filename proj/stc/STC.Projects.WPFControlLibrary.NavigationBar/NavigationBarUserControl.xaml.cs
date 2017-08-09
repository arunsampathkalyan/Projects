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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.ComponentModel.Composition;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using System.Globalization;
using System.Windows.Media.Animation;
using System.Runtime.CompilerServices;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System.Threading;
using STC.Projects.WPFControlLibrary.NavigationBar.ViewModel;


namespace STC.Projects.WPFControlLibrary.NavigationBar
{
    /// <summary>
    /// Interaction logic for NavigationBarUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "NavigationBarUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class NavigationBarUserControl : UserControl, IUserControl
    {

        #region Properties

        int sleepInterval = 150;

        NavigationBarViewModel VM = new NavigationBarViewModel();

        #endregion

        #region Constarctors
        public NavigationBarUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            this.DataContext = VM;
            this.Loaded += NavigationBarUserControl_Loaded;

        }

        void NavigationBarUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var key in Utility.GetCurrentUserFeatuers(this).Values)
            {
                switch (key)
                {
                    case 2:
                        btnViolation.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        btnPatrol.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        btnTraffic.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        btnAccident.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        btnTruckPermissions.Visibility = Visibility.Visible;
                        break;
                    case 7:
                        btnworkZone.Visibility = Visibility.Visible;
                        break;
                    case 8:
                        btnDashboard.Visibility = Visibility.Visible;
                        break;
                    case 13:
                        btnDangerousViolator.Visibility = Visibility.Visible;
                        break;
                    case 12:
                        btnAdminPatrol.Visibility = Visibility.Visible;

                        btn_admin.Visibility = Visibility.Visible;
                        break;
                    case 11:
                        btnAdminDangerousViolator.Visibility = Visibility.Visible;
                        btn_admin.Visibility = Visibility.Visible;
                        break;
                    case 14:
                        btnAdminKPI.Visibility = Visibility.Visible;

                        btn_admin.Visibility = Visibility.Visible;
                        break;

                    case 15:
                        btnAdminMessage.Visibility = Visibility.Visible;

                        btn_admin.Visibility = Visibility.Visible;
                        break;

                    default:
                        btnLive.Visibility = Visibility.Visible;
                        break;
                }



            }

            OperatorRadialPanel.Refresh();
            AdminRadialPanel.Refresh();
        }
        #endregion

        #region Event Handelers

        private async void btnLive_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //if (btnTraffic.Visibility == Visibility.Collapsed)
                //{
                //    await HideKPIsIcons();

                //    ShowPagesIcons();

                //}

                VM.ChangeCheckedButton("Live");

                Utility.NavigateToPage(this, SystemPages.OperationTest);

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (btnTraffic.Visibility == Visibility.Visible)
                {
                    //await HidePagesIcons();

                    //ShowKPIsIcons();
                }

                VM.ChangeCheckedButton("Dashboard");

                Utility.NavigateToPage(this, SystemPages.LandingPage);

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnAccident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Accident");

                Utility.NavigateToPage(this, SystemPages.IncidentPage);

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnPatrol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Patrol");

                Utility.NavigateToPage(this, SystemPages.PatrolPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnAssets_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Assets");

                Utility.NavigateToPage(this, SystemPages.AssetsPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnViolation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Violation");

                Utility.NavigateToPage(this, SystemPages.ViolationPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnTraffic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Traffic");

                Utility.NavigateToPage(this, SystemPages.TrraficPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnworkZone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("WorkZone");

                Utility.NavigateToPage(this, SystemPages.WorkZonesPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnTruckPermissions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("TruckPermissions");

                Utility.NavigateToPage(this, SystemPages.TruckPermissionsPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("Logout");

                var lang = Utility.GetLang();

                var cap = lang == "ar" ? "تأكيد الخروج" : "Confirm";
                var msg = lang == "ar" ? "سيتم غلق التطبيق، هل أنت متأكد" : "The application will be closed, are you sure";

                var msgBox = new MessageBoxUserControl(msg, true);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();

                var res = msgBox.GetResult();

                if (res == true)
                {
                    Utility.LogOut();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                Utility.LogOut();
            }
        }

        private void btnViolationKPI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.ViolationsKPIPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnSearchKPI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.SelectChartCriteria);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnIncidentsKPI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.IncidentsKPIPage);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        #endregion

        #region Methods

        public void ChangeButtonsEnableStatus(bool isEnabled)
        {
            btnAccident.IsEnabled = isEnabled;
            btnViolation.IsEnabled = isEnabled;
            btnTruckPermissions.IsEnabled = isEnabled;
            btnDashboard.IsEnabled = isEnabled;
            btnLive.IsEnabled = isEnabled;
            btnPatrol.IsEnabled = isEnabled;
            btnTraffic.IsEnabled = isEnabled;
            btnworkZone.IsEnabled = isEnabled;
            btnDangerousViolator.IsEnabled = isEnabled;
        }

        private async Task HidePagesIcons()
        {
            await Task.Delay(sleepInterval);

            btnTraffic.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnViolation.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnPatrol.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnAccident.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnTruckPermissions.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnworkZone.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnDangerousViolator.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);
        }

        private async void ShowPagesIcons()
        {
            await Task.Delay(sleepInterval);

            btnTraffic.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnViolation.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnPatrol.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnAccident.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnTruckPermissions.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnworkZone.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnDangerousViolator.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);
        }

        private async Task HideKPIsIcons()
        {
            await Task.Delay(sleepInterval);

            btnSearchKPI.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnViolationKPI.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);

            btnIncidentsKPI.Visibility = Visibility.Collapsed;

            await Task.Delay(sleepInterval);
        }

        private async void ShowKPIsIcons()
        {
            await Task.Delay(sleepInterval);

            btnSearchKPI.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnViolationKPI.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);

            btnIncidentsKPI.Visibility = Visibility.Visible;

            await Task.Delay(sleepInterval);
        }

        public void setChecktedPage(string pgeName)
        {
            VM.ChangeCheckedButton(pgeName);
        }

        private void btnDangerousViolator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ChangeCheckedButton("DangerousViolator");
                Utility.NavigateToPage(this, SystemPages.DangerousViolator);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
        //private void UnselectedIcons()
        //{
        //    imgLive.Source = new BitmapImage(new Uri("Images/live_page.png", UriKind.Relative));
        //    imgTraffic.Source = new BitmapImage(new Uri("Images/traffic_page.png", UriKind.Relative));
        //    imgViolation.Source = new BitmapImage(new Uri("Images/violation_page.png", UriKind.Relative));
        //    //imgAssets.Source = new BitmapImage(new Uri("Images/assets_page.png", UriKind.Relative));
        //    imgPatrol.Source = new BitmapImage(new Uri("Images/patrol_page.png", UriKind.Relative));
        //    imgAccident.Source = new BitmapImage(new Uri("Images/Incident_page.png", UriKind.Relative));
        //    imgTruckPermission.Source = new BitmapImage(new Uri("Images/TruckPermission_Page.png", UriKind.Relative));
        //    imgWorkZone.Source = new BitmapImage(new Uri("Images/WorkZone_page.png", UriKind.Relative));
        //    imgDashboard.Source = new BitmapImage(new Uri("Images/dashborad_page.png", UriKind.Relative));
        //    imgSearchKPI.Source = new BitmapImage(new Uri("Images/search_icon.png", UriKind.Relative));
        //    imgViolationKPI.Source = new BitmapImage(new Uri("Images/ViolationKPI_page.png", UriKind.Relative));
        //    imgIncidentKPI.Source = new BitmapImage(new Uri("Images/ViolationKPI_page.png", UriKind.Relative));
        //    imgLogout.Source = new BitmapImage(new Uri("Images/signout.png", UriKind.Relative));
        //}

        #endregion

        // private bool _isAdmin;
        private void TogAdmin_OnClick(object sender, RoutedEventArgs e)
        {

            //if (!_isAdmin)
            //{

            //    //DeactivateAdminButtons();
            //}
            //else
            //{
            //  //  ActivateAdminButtons();

            //}
            //_isAdmin =! _isAdmin;

            Utility.NavigateToPage(this, SystemPages.AdminDangerousViolatorUserControl);
            VM.CheckBtn = "AdminDangerousViolator";
        }

        private void DeactivateAdminButtons()
        {
            btnAccident.Visibility = Visibility.Collapsed;
            btnDashboard.Visibility = Visibility.Collapsed;
            //btnLive.Visibility = Visibility.Collapsed;
            btnTraffic.Visibility = Visibility.Collapsed;
            btnDangerousViolator.Visibility = Visibility.Collapsed;
            btnViolation.Visibility = Visibility.Collapsed;
            btnPatrol.Visibility = Visibility.Collapsed;
            btnTruckPermissions.Visibility = Visibility.Collapsed;
            btnworkZone.Visibility = Visibility.Collapsed;

            //////////////////////////////////////////

            btnAdminDangerousViolator.Visibility = Visibility.Visible;
            btnAdminPatrol.Visibility = Visibility.Visible;
            btnAdminKPI.Visibility = Visibility.Visible;
            btnAdminMessage.Visibility = Visibility.Visible;
            btnAdminTruckPermissions.Visibility = Visibility.Visible;
            btnAdminworkZone.Visibility = Visibility.Visible;

        }

        private void ActivateAdminButtons()
        {
            btnAccident.Visibility = Visibility.Visible;
            btnDashboard.Visibility = Visibility.Visible;
            //btnLive.Visibility = Visibility.Visible;
            btnTraffic.Visibility = Visibility.Visible;
            btnDangerousViolator.Visibility = Visibility.Visible;

            btnViolation.Visibility = Visibility.Visible;
            btnPatrol.Visibility = Visibility.Visible;
            btnTruckPermissions.Visibility = Visibility.Visible;
            btnworkZone.Visibility = Visibility.Visible;

            ////////////////////////////////////


            btnAdminDangerousViolator.Visibility = Visibility.Collapsed;
            btnAdminPatrol.Visibility = Visibility.Collapsed;
            btnAdminKPI.Visibility = Visibility.Collapsed;
            btnAdminMessage.Visibility = Visibility.Collapsed;

            btnAdminTruckPermissions.Visibility = Visibility.Collapsed;
            btnAdminworkZone.Visibility = Visibility.Collapsed;
        }

        private void BtnAdminPatrol_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.AdminPatrolPage);
                VM.CheckBtn = "AdminPatrol";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }



        private void BtnAdminTruckPermissions_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.AdminBusinessRule);
                VM.CheckBtn = "AdminTruckPermissions";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }



        private void BtnAdminworkZone_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAdminDangerousViolator_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.AdminDangerousViolatorUserControl);
                VM.CheckBtn = "AdminDangerousViolator";

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnAdminKPI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.AdminKPIAdministrationPage);
                VM.CheckBtn = "AdminKPI";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnAdminMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utility.NavigateToPage(this, SystemPages.AdminMessageTemplatePage);
                VM.CheckBtn = "AdminMessage";
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
    }
}
