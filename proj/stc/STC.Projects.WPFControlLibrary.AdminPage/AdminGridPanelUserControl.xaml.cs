using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.Common.PaggingControl;
using STC.Projects.WPFControlLibrary.AdminPage.ViewModel;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
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
    /// Interaction logic for AdminGridPanelUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "AdminBusinessRulePage")]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    public partial class AdminGridPanelUserControl : UserControl, IUserControl
    {
        AdminPageGridViewModel _vm;
        //public delegate void PublishDelegate(object sender, EventArgs e);
        //public event PublishDelegate KpiTargetUpdated;
        public AdminGridPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());
            InitializeComponent();
            _vm = new AdminPageGridViewModel();
            this.DataContext = _vm;
            this._vm.currentUserId = this.GetCurrentUserId();
            adCreateRole.RoleAdded += adCreateRole_RoleAdded;
            ucUpdateRole.RoleUpdated += ucUpdateRole_RoleUpdated;
            //_vm.KpiTargetUpdated += Kpi_TargetUpdated;
        }

        void ucUpdateRole_RoleUpdated(object sender, EventArgs e)
        {
            _vm.LoadData();

            _vm.HideUpdateBusinessRulePage();
            //make visible for add business rule
        }

        void adCreateRole_RoleAdded(object sender, EventArgs e)
        {
            _vm.LoadData();

            _vm.HideAddBusinessRulePage();

            Application.Current.Dispatcher.Invoke(() => AssignSourceForAdMinGrid());
            //make visible for add business rule
        }

        void Kpi_TargetUpdated(object sender, EventArgs e)
        {
            //_vm.LoadData();

            //_vm.HideUpdateBusinessRulePage();
            ////make visible for add business rule
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string closeMsg = "Are you sure to update the Target Percentage?";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                _vm.UpdateKPIexecute();
                //Window.GetWindow(this).Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new AdminWindow();
            p.ShowDialog();
            _vm.LoadData();
        }

        private void pageControl_PreviewPageChange(object sender, PageChangedEventArgs args)
        {
            _vm.currentGridIndicator = "BusinessRules";
            //List<Object> items = pageControl.ItemsSource.ToList();
            //int count = items.Count;
        }

        private void pageControl_PageChanged(object sender, PageChangedEventArgs args)
        {
            //List<Object> items = pageControl.ItemsSource.ToList();
            //int count = items.Count;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
                _vm.currentSelectedRuleName = (sender as TextBlock).Text;
            else if (sender is Border)
                _vm.currentSelectedRuleName = ((sender as Border).Child as TextBlock).Text;

            if (_vm.SelectedBusinessRule != null && _vm.SelectedBusinessRule.BusinessName == _vm.currentSelectedRuleName)
            {
                _vm.GetUpdatePageVm();

                _vm.UpdatePageVM.RoleUpdated += ucUpdateRole.vm_RoleUpdated;
                ucUpdateRole.DataContext = _vm.UpdatePageVM;
                _vm.ShowUpdateBusinessRulePage = true;
                _vm.currentSelectedRuleName = string.Empty;
            }
        }



        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //HandleMouseDown(sender, e);
        }

        //void HandleMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //AdminPageGridViewModel mainVM = this.DataContext as AdminPageGridViewModel;
        //    _vm.canUpdateSelectedRow = true;
        //    //mainVM.ShowUpdateBusinessRulePage = true;
        //    //UpdateAdminPageControlViewModel vm = new UpdateAdminPageControlViewModel(mainVM.BusinessRules[this.dgrAdmin.SelectedIndex]);
        //    //vm.RoleUpdated += ucUpdateRole.vm_RoleUpdated;
        //    _vm.UpdatePageVM.RoleUpdated += ucUpdateRole.vm_RoleUpdated;
        //    ucUpdateRole.DataContext = _vm.UpdatePageVM;
        //    //ucUpdateRole.DataContext = vm;
        //}

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            string closeMsg = "Are you sure, want to close the Add Business Rule Screen?";
            MessageBoxControl.MessageBoxUserControl closeMsgBox = new MessageBoxUserControl(closeMsg, true);
            closeMsgBox.Owner = Window.GetWindow(this);


            closeMsgBox.ShowDialog();

            var res = closeMsgBox.GetResult();

            if (res == true)
            {
                //popupAddBusinessRule.IsOpen = false;
            }
        }

        private void dgrAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgrAdmin.SelectedIndex != -1 && _vm != null
                && !string.IsNullOrEmpty(_vm.currentSelectedRuleName)
                && _vm.currentSelectedRuleName == _vm.SelectedBusinessRule.BusinessName
                && _vm.UpdatePageVM != null)
            {
                _vm.UpdatePageVM.RoleUpdated += ucUpdateRole.vm_RoleUpdated;
                ucUpdateRole.DataContext = _vm.UpdatePageVM;
                _vm.ShowUpdateBusinessRulePage = true;
                _vm.currentSelectedRuleName = string.Empty;
            }
        }

        private void dgrdKPItableViolation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtBlkKPITargetColVal_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
                _vm.currentSelectedKPIName = (sender as TextBlock).Text;
            else if (sender is Border)
                _vm.currentSelectedKPIName = ((sender as Border).Child as TextBlock).Text;
        }

        private void pageControlKPItarget_PreviewPageChange(object sender, PageChangedEventArgs args)
        {
            _vm.currentGridIndicator = "KPITarget";

            //List<Object> items = pageControlKPItarget.ItemsSource.ToList();
            //int count = items.Count;
        }

        private void pageControlKPItarget_PageChanged(object sender, PageChangedEventArgs args)
        {
            //List<Object> items = pageControlKPItarget.ItemsSource.ToList();
            //int count = items.Count;
        }


        private void AdMinGridPageChanged(object sender, RoutedEventArgs e)
        {


            if (_vm != null && _vm.BusinessRulesColl != null && _vm.BusinessRulesColl.Count > 0)
            {
                // _vm.BusinessRules = new System.Collections.ObjectModel.ObservableCollection<ServiceReference1.BusinessRulesDTO>();

                //pager.CurrentPage = 1;

                pager.PageSize = 10;
                AssignSourceForAdMinGrid();
                //pager.SetSource();

                //pager.SetSource();
            }
            //Navigate(PageChanges.PageNum);
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null && _vm.BusinessRulesColl != null && _vm.BusinessRulesColl.Count > 0)
            {
                // _vm.BusinessRules = new System.Collections.ObjectModel.ObservableCollection<ServiceReference1.BusinessRulesDTO>();

                pager.CurrentPage = 1;

                pager.PageSize = 10;
                AssignSourceForAdMinGrid();
                pager.SetSource();

                //pager.SetSource();
            }
            //pager.CurrentPage = 1;
            //pager.TotalPages = 10;
        }

        private void AssignSourceForAdMinGrid()
        {
            int totalRecords;
            int newPageSize;

            if (pager == null)
            {
                return;
            }

            //RaisePreviewPageChange(Page, 1);//Raise to set current itemsource indicator to fetch data.

            totalRecords = (int)_vm.GetTotalCount();
            newPageSize = pager.PageSize;

            if (totalRecords == 0)
            {
                _vm.BusinessRules.Clear();

                pager.TotalPages = 0;

            }
            else
            {
                pager.TotalPages = (totalRecords / newPageSize) + ((totalRecords % newPageSize == 0) ? 0 : 1);
            }



            int StartingIndex = (pager.CurrentPage - 1) * newPageSize;

            //uint oldPage = Page;
            //RaisePreviewPageChange(Page, newPage);

            //Page = newPage;
            _vm.BusinessRules.Clear();

            ICollection<object> fetchData = _vm.GetAdminGridRecordsBy((uint)StartingIndex, (uint)newPageSize, null);
            foreach (object row in fetchData)
            {
                _vm.BusinessRules.Add((ServiceReference1.BusinessRulesDTO)row);
            }

            //RaisePageChanged(oldPage, Page);
        }


        private void AssignSourceForKpiTargetsGrid()
        {
            int totalRecords;
            int newPageSize;

            if (pagerKpiTargets == null)
            {
                return;
            }

            //RaisePreviewPageChange(Page, 1);//Raise to set current itemsource indicator to fetch data.

            totalRecords = (int)_vm.GetKPiTargetsTotalCount();
            newPageSize = pagerKpiTargets.PageSize;

            if (totalRecords == 0)
            {
                _vm.KPItargets.Clear();

                pagerKpiTargets.TotalPages = 0;

            }
            else
            {
                pagerKpiTargets.TotalPages = (totalRecords / newPageSize) + ((totalRecords % newPageSize == 0) ? 0 : 1);
            }


            int StartingIndex = (pagerKpiTargets.CurrentPage - 1) * newPageSize;

            //uint oldPage = Page;
            //RaisePreviewPageChange(Page, newPage);

            //Page = newPage;
            _vm.KPItargets.Clear();

            _vm.kpiCollPagingStartIndex = StartingIndex;
            _vm.kpiCollectionPagingSize = newPageSize;

            ICollection<object> fetchData = _vm.GetKpiTargetsGridRecordsBy((uint)StartingIndex, (uint)newPageSize, null);
            foreach (object row in fetchData)
            {
                _vm.KPItargets.Add((ServiceReference1.KpiDTO)row);
            }

            //RaisePageChanged(oldPage, Page);
        }
        private void pagerKpiTargets_PageChanged(object sender, RoutedEventArgs e)
        {
            if (_vm != null && _vm.KPItargets != null && _vm.KPItargets.Count > 0)
            {
                // _vm.BusinessRules = new System.Collections.ObjectModel.ObservableCollection<ServiceReference1.BusinessRulesDTO>();

                //pager.CurrentPage = 1;

                pagerKpiTargets.PageSize = 10;
                AssignSourceForKpiTargetsGrid();
                //pager.SetSource();

                //pager.SetSource();
            }
            //Navigate(PageChanges.PageNum);
        }

        private void dgrdKPItableViolation_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null && _vm.KPItargetCollection != null && _vm.KPItargetCollection.Count() > 0)
            {
                _vm.KPItargets = new System.Collections.ObjectModel.ObservableCollection<ServiceReference1.KpiDTO>();
                // _vm.BusinessRules = new System.Collections.ObjectModel.ObservableCollection<ServiceReference1.BusinessRulesDTO>();

                pagerKpiTargets.CurrentPage = 1;

                pagerKpiTargets.PageSize = 10;
                AssignSourceForKpiTargetsGrid();
                pagerKpiTargets.SetSource();

                //pager.SetSource();
            }
            //pager.CurrentPage = 1;
            //pager.TotalPages = 10;
        }
    }
}
