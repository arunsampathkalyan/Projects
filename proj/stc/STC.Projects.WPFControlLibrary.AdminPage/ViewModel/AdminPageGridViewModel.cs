using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using STC.Projects.ClassLibrary.Common;
using System.Windows;
using STC.Projects.ClassLibrary.Common.PaggingControl;
using STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1;


namespace STC.Projects.WPFControlLibrary.AdminPage.ViewModel
{
    public class AdminPageGridViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        //public delegate void PublishDelegate(object sender, EventArgs e);
        //public event PublishDelegate KpiTargetUpdated;

        ServiceReference1.ServiceLayerClient client = new ServiceReference1.ServiceLayerClient();

        public int currentUserId { get; set; }

        public string currentSelectedRuleName = string.Empty;

        public string currentSelectedKPIName = string.Empty;

        public string currentGridIndicator;

        public int kpiCollPagingStartIndex;
        public int kpiCollectionPagingSize;
        public bool targetUpdated;

        private UpdateAdminPageControlViewModel _updateVM;

        public UpdateAdminPageControlViewModel UpdatePageVM
        {
            get { return _updateVM; }
            set
            {
                _updateVM = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        ObservableCollection<ServiceReference1.BusinessRulesDTO> _businessRulesColl;
        public ObservableCollection<ServiceReference1.BusinessRulesDTO> BusinessRulesColl
        {
            get { return _businessRulesColl; }
            set
            {
                this._businessRulesColl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }


        ObservableCollection<ServiceReference1.BusinessRulesDTO> _businessRules;
        public ObservableCollection<ServiceReference1.BusinessRulesDTO> BusinessRules
        {
            get { return _businessRules; }
            set
            {
                this._businessRules = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ServiceReference1.BusinessRulesDTO _selectedBusinessRule;

        public ServiceReference1.BusinessRulesDTO SelectedBusinessRule
        {
            get { return _selectedBusinessRule; }
            set
            {
                this._selectedBusinessRule = value;
                if (!string.IsNullOrEmpty(currentSelectedRuleName) && value != null && value.BusinessName == currentSelectedRuleName)
                    GetUpdatePageVm();
                this.RaiseNotifyPropertyChanged();
            }
        }

        public void GetUpdatePageVm()
        {
            //this.ShowUpdateBusinessRulePage = true;
            this.UpdatePageVM = new UpdateAdminPageControlViewModel(this.SelectedBusinessRule);

        }



        //private bool _isKPItargetUpdated;

        //public Boolean IsKPITargetUpdated
        //{
        //    get { return _isKPItargetUpdated; }

        //    set
        //    {
        //        this._isKPItargetUpdated = value;
        //        this.RaiseNotifyPropertyChanged();
        //    }
        //}


        private STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO[] _kPItargetCollection;
        public STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO[] KPItargetCollection
        {
            get { return _kPItargetCollection; }
            set
            {
                _kPItargetCollection = value;

                if (targetUpdated && KPItargets != null && KPItargets.Count > 0 && value != null && value.Count() > 0)
                {
                    //KPItargets.Clear();
                    ICollection<object> fetchData = GetKpiTargetsGridRecordsBy((uint)kpiCollPagingStartIndex, (uint)kpiCollectionPagingSize, null);

                    var kpitargetsTemp = new ObservableCollection<ServiceReference1.KpiDTO>();
                    foreach (object row in fetchData)
                    {
                        kpitargetsTemp.Add((ServiceReference1.KpiDTO)row);
                    }
                    Application.Current.Dispatcher.Invoke(() => { KPItargets = kpitargetsTemp; });
                    targetUpdated = false;
                }
                //ModifyCollectionData();
                this.RaiseNotifyPropertyChanged();
            }
        }

        private ObservableCollection<STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO> _kPItargets;
        public ObservableCollection<STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO> KPItargets
        {
            get { return _kPItargets; }
            set
            {
                _kPItargets = value;
                //ModifyCollectionData();
                this.RaiseNotifyPropertyChanged();
            }
        }

        private STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO _selectedKPI;
        public STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO SelectedKPI
        {
            get { return _selectedKPI; }
            set
            {
                if (value != null && !string.IsNullOrEmpty(this.currentSelectedKPIName)
                    && (this.currentSelectedKPIName == value.LabelValueEnglish || this.currentSelectedKPIName == value.LabelValueArabic))
                {
                    _selectedKPI = value;
                    //ModifyCollectionData();
                    this.RaiseNotifyPropertyChanged();
                    KpiTargetPercentValue = value.Percentage;
                    TaregtDescription = (Utility.GetLang() == "ar") ? value.LabelValueArabic : value.LabelValueEnglish;
                }

            }
        }



        private int selectedKpiIndex;

        public Int32 SelectedKpiIndex
        {
            get { return this.selectedKpiIndex; }
            set
            {
                this.selectedKpiIndex = value;
                if (value != -1 && KPItargetCollection != null && KPItargetCollection.Count() > selectedKpiIndex)
                {
                    KpiTargetPercentValue = KPItargetCollection[selectedKpiIndex].Percentage;
                    TaregtDescription = (Utility.GetLang() == "ar") ? KPItargetCollection[selectedKpiIndex].LabelValueArabic : KPItargetCollection[selectedKpiIndex].LabelValueEnglish;
                    this.RaiseNotifyPropertyChanged();
                }
            }
        }

        //private string _kpiNametoUpdate;

        //public String KPInameToUpdate
        //{
        //    get
        //    {
        //        return this._kpiNametoUpdate;
        //    }
        //    set
        //    {
        //        this._kpiNametoUpdate = value;
        //        this.RaiseNotifyPropertyChanged();
        //    }
        //}

        private double? _kpiTargetPercentValue;

        public Double? KpiTargetPercentValue
        {
            get
            {
                return this._kpiTargetPercentValue;
            }
            set
            {

                this._kpiTargetPercentValue = Convert.ToDouble(value) < 1 ? Convert.ToDouble(value) * 100 : Convert.ToDouble(value);
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _kpiDescription;

        public String TaregtDescription
        {
            get
            {
                return this._kpiDescription;
            }
            set
            {
                this._kpiDescription = value;
                this.RaiseNotifyPropertyChanged();
            }
        }







        private int _activetab;
        public Int32 ActiveTab
        {
            get { return _activetab; }
            set
            {
                _activetab = value;
                {
                    switch (value)
                    {
                        case 0:
                            {
                                ShowAddBusinessRuleTab = true;
                                ShowUpdateBusinessRuleTab = false;
                                break;
                            }
                        case 1:
                            {
                                ShowUpdateBusinessRuleTab = true;
                                ShowAddBusinessRuleTab = false;
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _showAddBusinessRuleTab;

        public Boolean ShowAddBusinessRuleTab
        {
            get { return _showAddBusinessRuleTab; }
            set
            {
                _showAddBusinessRuleTab = value;
                if (value)
                {
                    ShowgridRules = true;
                    ShowUpdateBusinessRuleTab = false;

                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showUpdateBusinessRuleTab;

        public Boolean ShowUpdateBusinessRuleTab
        {
            get { return _showUpdateBusinessRuleTab; }
            set
            {
                _showUpdateBusinessRuleTab = value;
                if (value)
                {
                    ShowAddBusinessRuleTab = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showgridRules;

        public Boolean ShowgridRules
        {
            get { return _showgridRules; }
            set
            {
                _showgridRules = value;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showAddBusinessRulePage;

        public Boolean ShowAddBusinessRulePage
        {
            get { return _showAddBusinessRulePage; }
            set
            {
                _showAddBusinessRulePage = value;
                if (value) ShowgridRules = false; else ShowgridRules = true;

                this.RaiseNotifyPropertyChanged();
            }
        }



        private bool _showUpdateBusinessRulePage;

        public Boolean ShowUpdateBusinessRulePage
        {
            get { return _showUpdateBusinessRulePage; }
            set
            {
                _showUpdateBusinessRulePage = value;
                if (value) ShowgridRules = false; else ShowgridRules = true;
                this.RaiseNotifyPropertyChanged();
            }
        }



        public Command DeleteRuleCommand { get; set; }
        public Command UpdateKPItargetCommand { get { return new Command((UpdateKPIexecute)); } }

        public Command ClickedCellCommand { get { return new Command((UpdateBusinessRuleExecute)); } }

        public Command AddBusinessRuleBtnClickedCommand { get { return new Command((AddBusinessRuleBtnClickedExecute)); } }

        public Command PopupCloseButtonClickedCommand { get { return new Command((HideAddBusinessRulePage)); } }

        private void AddBusinessRuleBtnClickedExecute()
        {
            ShowAddBusinessRulePage = true;
        }

        public void HideAddBusinessRulePage()
        {
            ShowAddBusinessRulePage = false;
        }

        public void HideUpdateBusinessRulePage()
        {
            ShowUpdateBusinessRulePage = false;
        }


        private void UpdateBusinessRuleExecute()
        {
            ShowUpdateBusinessRulePage = true;
        }



        public void UpdateKPIexecute()
        {
            if (SelectedKPI != null &&
                Convert.ToDouble(KpiTargetPercentValue) != SelectedKPI.Percentage)
            {
                UpdateKPITarget();
            }
        }



        public async void UpdateKPITarget()
        {
            bool isKPIupdated = await client.UpdateKPITargetAsync(SelectedKPI.TargetName, Convert.ToDouble(this.KpiTargetPercentValue), this.currentUserId);

            if (isKPIupdated)
            {
                targetUpdated = true;
                GetAllTargetData();
            }

        }

        public void GetAllTargetData()
        {

            var callTask = client.GetAllTargetsListAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_KPItargetDetails(x));
        }

        private void Add_KPItargetDetails(STC.Projects.WPFControlLibrary.AdminPage.ServiceReference1.KpiDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() => { KPItargetCollection = data; });
        }

        public AdminPageGridViewModel()
        {
            BusinessRulesColl = new ObservableCollection<ServiceReference1.BusinessRulesDTO>();
            BusinessRules = new ObservableCollection<ServiceReference1.BusinessRulesDTO>();

            DeleteRuleCommand = new Command(DeleteBusinessRule);
            currentGridIndicator = "BusinessRules";
            LoadData();
            GetAllTargetData();
            if (KPItargetCollection != null && KPItargetCollection.Count() > 0)
            {
                selectedKpiIndex = 0;
                KPItargets = new ObservableCollection<ServiceReference1.KpiDTO>();
            }
            ActiveTab = 0;


        }
        private void DeleteBusinessRule(object parameter)
        {
            var client = new ServiceReference1.ServiceLayerClient();
            var obj = parameter as ServiceReference1.BusinessRulesDTO;
            if (obj != null)
            {
                obj.IsDeleted = true;
                client.SaveBusinessRule(obj);
                LoadData();
            }
        }
        public void LoadData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var client = new ServiceReference1.ServiceLayerClient();
                LoadGrid(client.GetAllBusinessRules(false).ToList());

            });

            // var task = client.GetAllBusinessRulesAsync(false).ToObservable();
            //task.Subscribe(x => LoadGrid(x.ToList()));
        }

        public void LoadGrid(List<ServiceReference1.BusinessRulesDTO> list)
        {
            BusinessRulesColl.Clear();
            BusinessRules=new ObservableCollection<BusinessRulesDTO>();
            foreach (var item in list)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => BusinessRulesColl.Add(item));
                System.Windows.Application.Current.Dispatcher.Invoke(() => BusinessRules.Add(item));

                //for (int i = 0; i < 12; i++)
                //{

                //    System.Windows.Application.Current.Dispatcher.Invoke(() => BusinessRules.Add((ServiceReference1.BusinessRulesDTO)item));
                //}

            }



        }


        #region IPageControlContract Members

        public uint GetTotalCount()
        {
            return (uint)(BusinessRulesColl != null ? BusinessRulesColl.Count : 0);
        }

        public uint GetKPiTargetsTotalCount()
        {
            return (uint)(KPItargetCollection != null ? KPItargetCollection.Count() : 0);
        }
        //Implemented method for paggingcontrol to set the itemSource
        public ICollection<object> GetAdminGridRecordsBy(uint StartingIndex, uint NumberOfRecords, object FilterTag)
        {

            if (BusinessRulesColl != null)
            {
                if (StartingIndex >= BusinessRulesColl.Count)
                {
                    return new List<object>();
                }

                List<ServiceReference1.BusinessRulesDTO> result = new List<ServiceReference1.BusinessRulesDTO>();

                for (int i = (int)StartingIndex; i < BusinessRulesColl.Count && i < StartingIndex + NumberOfRecords; i++)
                {
                    result.Add(BusinessRulesColl[i]);
                }

                return result.ToList<object>();
            }
            return null;
        }

        public ICollection<object> GetKpiTargetsGridRecordsBy(uint StartingIndex, uint NumberOfRecords, object FilterTag)
        {
            if (KPItargetCollection != null)
            {
                if (StartingIndex >= KPItargetCollection.Count())
                {
                    return new List<object>();
                }

                List<ServiceReference1.KpiDTO> result = new List<ServiceReference1.KpiDTO>();

                for (int i = (int)StartingIndex; i < KPItargetCollection.Count() && i < StartingIndex + NumberOfRecords; i++)
                {
                    result.Add(KPItargetCollection[i]);
                }

                return result.ToList<object>();
            }

            return null;
        }

        #endregion

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
