using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Common;
using System.Runtime.CompilerServices;
using STC.Projects.WPFControlLibrary.LandingPage.Properties;


namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    public class LandingPageUserControlViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private string _activeMainBtnCategory;

        private string _activeSubBtnCategory;
        public LandingPageUserControlViewModel()
        {
            ActiveMainButtonCategory = Resources.strGeneral;
            ActiveSubButtonCategory = Resources.strOverview;


            ManualKPIurl = System.Configuration.ConfigurationSettings.AppSettings["ManualKPIurl"];

            HeatMapKPIurl = System.Configuration.ConfigurationSettings.AppSettings["HeatMapKPIurl"];
        }


        public bool isUserHaveAccessToManualKPI;

        private bool _showMainDashBrdChartArea;

        public Boolean ShowMainDashBrdChartArea
        {
            get { return _showMainDashBrdChartArea; }
            set
            {
                _showMainDashBrdChartArea = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _manualKPIurl;

        public string ManualKPIurl
        {
            get { return _manualKPIurl; }
            set
            {
                _manualKPIurl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private string _heatMapKPIurl;

        public string HeatMapKPIurl
        {
            get { return _heatMapKPIurl; }
            set
            {
                _heatMapKPIurl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }



        private bool _showChartArea;

        public Boolean ShowChartArea
        {
            get { return _showChartArea; }
            set
            {
                _showChartArea = value;
                this.RaiseNotifyPropertyChanged();
            }
        }



        private bool _showChartAreaViolationTrend;

        public Boolean ShowChartAreaViolationTrend
        {
            get { return _showChartAreaViolationTrend; }
            set
            {
                _showChartAreaViolationTrend = value;
                if (value)
                {
                    ShowChartAreaAccidentsTrend = false;
                    ShowChartAreaDangerViolnTrend = false;
                    ShowChartAreaTruckPermTrend = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaViolationStatistics;

        public Boolean ShowChartAreaViolationStatistics
        {
            get { return _showChartAreaViolationStatistics; }
            set
            {
                _showChartAreaViolationStatistics = value;

                //ShowChartAreaViolationByRegionStatistics = value;
                ShowChartAreaViolationByTypeStatistics = value;
                if (value)
                {

                    //ShowChartAreaViolationByTypeStatistics = false;
                    ShowChartAreaViolationByCompStatistics = false;

                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaViolationByRegionStatistics;

        public Boolean ShowChartAreaViolationByRegionStatistics
        {
            get { return _showChartAreaViolationByRegionStatistics; }
            set
            {
                _showChartAreaViolationByRegionStatistics = value;
                if (value)
                {
                    ShowChartAreaViolationByTypeStatistics = false;
                    ShowChartAreaViolationByCompStatistics = false;

                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaViolationByTypeStatistics;

        public Boolean ShowChartAreaViolationByTypeStatistics
        {
            get { return _showChartAreaViolationByTypeStatistics; }
            set
            {
                _showChartAreaViolationByTypeStatistics = value;
                if (value)
                {
                    ShowChartAreaViolationByRegionStatistics = false;
                    ShowChartAreaViolationByCompStatistics = false;

                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaViolationByCompStatistics;

        public Boolean ShowChartAreaViolationByCompStatistics
        {
            get { return _showChartAreaViolationByCompStatistics; }
            set
            {
                _showChartAreaViolationByCompStatistics = value;
                if (value)
                {
                    ShowChartAreaViolationByRegionStatistics = false;
                    ShowChartAreaViolationByTypeStatistics = false;

                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }



        private bool _showChartAreaAccidentsByTypeStatistics;

        public bool ShowChartAreaAccidentsByTypeStatistics
        {
            get { return _showChartAreaAccidentsByTypeStatistics; }
            set
            {
                _showChartAreaAccidentsByTypeStatistics = value;


                if (value)
                {

                    ShowChartAreaAccidentsByCompStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }
        private bool _showChartAreaAccidentsByCompStatistics;

        public bool ShowChartAreaAccidentsByCompStatistics
        {
            get { return _showChartAreaAccidentsByCompStatistics; }
            set
            {
                _showChartAreaAccidentsByCompStatistics = value;
                if (value)
                {

                    ShowChartAreaAccidentsByTypeStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _showChartAreaDangByTypeStatistics;

        public bool ShowChartAreaDangByTypeStatistics
        {
            get { return _showChartAreaDangByTypeStatistics; }
            set
            {
                _showChartAreaDangByTypeStatistics = value;
                if (value)
                {

                    ShowChartAreaDangByCompStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaDangByCompStatistics;

        public bool ShowChartAreaDangByCompStatistics
        {
            get { return _showChartAreaDangByCompStatistics; }
            set
            {
                _showChartAreaDangByCompStatistics = value;
                if (value)
                {

                    ShowChartAreaDangByTypeStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaTruckByTypeStatistics;

        public bool ShowChartAreaTruckByTypeStatistics
        {
            get { return _showChartAreaTruckByTypeStatistics; }
            set
            {
                _showChartAreaTruckByTypeStatistics = value;
                if (value)
                {

                    ShowChartAreaTruckByCompStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                }

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaTruckByCompStatistics;

        public bool ShowChartAreaTruckByCompStatistics
        {
            get { return _showChartAreaTruckByCompStatistics; }
            set
            {
                _showChartAreaTruckByCompStatistics = value;
                if (value)
                {

                    ShowChartAreaTruckByTypeStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _showChartAreaAccidentsTrend;

        public Boolean ShowChartAreaAccidentsTrend
        {
            get { return _showChartAreaAccidentsTrend; }
            set
            {
                _showChartAreaAccidentsTrend = value;
                if (value)
                {
                    ShowChartAreaViolationTrend = false;
                    ShowChartAreaDangerViolnTrend = false;
                    ShowChartAreaTruckPermTrend = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaAccidentsStatistics;

        public Boolean ShowChartAreaAccidentsStatistics
        {
            get { return _showChartAreaAccidentsStatistics; }
            set
            {
                _showChartAreaAccidentsStatistics = value;

                ShowChartAreaAccidentsByTypeStatistics = value;
                if (value)
                {
                    ShowChartAreaAccidentsByCompStatistics = false;

                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;
                    ShowChartAreaTruckPermStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _showChartAreaTruckPermTrend;

        public Boolean ShowChartAreaTruckPermTrend
        {
            get { return _showChartAreaTruckPermTrend; }
            set
            {
                _showChartAreaTruckPermTrend = value;
                if (value)
                {
                    ShowChartAreaAccidentsTrend = false;
                    ShowChartAreaViolationTrend = false;
                    ShowChartAreaDangerViolnTrend = false;

                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaTruckPermStatistics;

        public Boolean ShowChartAreaTruckPermStatistics
        {
            get { return _showChartAreaTruckPermStatistics; }
            set
            {
                _showChartAreaTruckPermStatistics = value;

                ShowChartAreaTruckByTypeStatistics = value;

                if (value)
                {
                    ShowChartAreaTruckByCompStatistics = false;

                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaViolationStatistics = false;
                    ShowChartAreaDangerViolnStatistics = false;

                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaDangerViolnTrend;

        public Boolean ShowChartAreaDangerViolnTrend
        {
            get { return _showChartAreaDangerViolnTrend; }
            set
            {
                _showChartAreaDangerViolnTrend = value;
                if (value)
                {
                    ShowChartAreaTruckPermTrend = false;
                    ShowChartAreaAccidentsTrend = false;
                    ShowChartAreaViolationTrend = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showChartAreaDangerViolnStatistics;

        public Boolean ShowChartAreaDangerViolnStatistics
        {
            get { return _showChartAreaDangerViolnStatistics; }
            set
            {
                _showChartAreaDangerViolnStatistics = value;

                ShowChartAreaDangByTypeStatistics = value;
                if (value)
                {
                    ShowChartAreaDangByCompStatistics = false;

                    ShowChartAreaTruckPermStatistics = false;
                    ShowChartAreaAccidentsStatistics = false;
                    ShowChartAreaViolationStatistics = false;
                }
                this.RaiseNotifyPropertyChanged();
            }
        }


        private bool _showKPITableChartArea;

        public Boolean ShowKPITableChartArea
        {
            get { return _showKPITableChartArea; }
            set
            {
                _showKPITableChartArea = value;
                this.RaiseNotifyPropertyChanged();


                switch (_activeMainBtnCategory)
                {
                    case "VIOLATIONS":
                    case "مخالفات":
                        {
                            ShowKPITableChartViolationArea = true;
                            break;
                        }
                    case "ACCIDENTS":
                    case "حوادث":
                        {
                            ShowKPITableChartAccidentArea = true;
                            break;
                        }
                    case "DANGEROUS VIOLATOR":
                    case "مخالفات خطرة":
                        {

                            break;
                        }
                    case "TRUCK PERMISSIONS":
                    case "مخالفات شاحنات":
                        {

                            break;
                        }
                    default:
                        { break; }

                }


            }
        }


        private bool _showManualKPIArea;

        public Boolean ShowManualKPIArea
        {
            get { return _showManualKPIArea; }
            set
            {
                _showManualKPIArea = value;
                this.RaiseNotifyPropertyChanged();


            }
        }

        private bool _showIncidentHeatMapKPIArea;

        public Boolean ShowIncidentHeatMapKPIArea
        {
            get { return _showIncidentHeatMapKPIArea; }
            set
            {
                _showIncidentHeatMapKPIArea = value;
                this.RaiseNotifyPropertyChanged();


            }
        }


        private bool _showKPITableChartViolationArea;

        public Boolean ShowKPITableChartViolationArea
        {
            get { return _showKPITableChartViolationArea; }
            set
            {
                _showKPITableChartViolationArea = value;
                if (value)
                    ShowKPITableChartAccidentArea = false;

                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _showKPITableChartAccidentArea;

        public Boolean ShowKPITableChartAccidentArea
        {
            get { return _showKPITableChartAccidentArea; }
            set
            {
                _showKPITableChartAccidentArea = value;

                if (value)
                    ShowKPITableChartViolationArea = false;

                this.RaiseNotifyPropertyChanged();


            }
        }

        private bool _canEnableGeneralOverViewButton;

        public bool CanEnableGeneralOverViewButton
        {
            get { return _canEnableGeneralOverViewButton; }
            set
            {
                _canEnableGeneralOverViewButton = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _canEnableStatisticsButton;

        public bool CanEnableStatisticsButton
        {
            get { return _canEnableStatisticsButton; }
            set
            {
                _canEnableStatisticsButton = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _canEnablePerformanceButton;
        public Boolean CanEnablePerformanceButton
        {
            get { return _canEnablePerformanceButton; }
            set
            {
                this._canEnablePerformanceButton = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _canEnableManualKPIButton;
        public Boolean CanEnableManualKPIButton
        {
            get { return _canEnableManualKPIButton; }
            set
            {
                this._canEnableManualKPIButton = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool _canEnableHeatMapButton;
        public Boolean CanEnableHeatMapButton
        {
            get { return _canEnableHeatMapButton; }
            set
            {
                this._canEnableHeatMapButton = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        private bool CanExecuteHome()
        {
            switch (ActiveMainButtonCategory)
            {
                case "GENERAL":
                    {
                        return true;
                    }
                case "VIOLATIONS":
                case "مخالفات":
                    {
                        return false;
                    }
                case "ACCIDENTS":
                case "حوادث":
                    {
                        return false;
                    }
                case "TRUCK PERMISSIONS":
                case "مخالفات شاحنات":
                    {
                        return false;
                    }
                case "DANGEROUS VIOLATOR":
                case "مخالفات خطرة":
                    {
                        return false;
                    }
                case "WORK ZONES":
                case "مناطق العمل":
                    {
                        return false;
                    }
                case "PATROL CAR":
                case "دوريات المرور":
                    {
                        return false;
                    }
            }
            return false;
        }


        private bool CanExecuteStatistics()
        {
            switch (ActiveMainButtonCategory)
            {
                case "GENERAL":
                    {
                        return false;
                    }
                case "VIOLATIONS":
                case "مخالفات":
                    {
                        return true;
                    }
                case "ACCIDENTS":
                case "حوادث":
                    {
                        return true;
                    }
                case "TRUCK PERMISSIONS":
                case "مخالفات شاحنات":
                    {
                        return true;
                    }
                case "DANGEROUS VIOLATOR":
                case "مخالفات خطرة":
                    {
                        return true;
                    }
                case "WORK ZONES":
                case "مناطق العمل":
                    {
                        return false;
                    }
                case "PATROL CAR":
                case "دوريات المرور":
                    {
                        return false;
                    }
            }
            return false;
        }

        private bool CanExecutePerformance()
        {
            switch (ActiveMainButtonCategory)
            {
                case "GENERAL":
                    {
                        return false;
                    }
                case "VIOLATIONS":
                case "مخالفات":
                    {
                        return true;
                    }
                case "ACCIDENTS":
                case "حوادث":
                    {
                        return true;
                    }
                case "TRUCK PERMISSIONS":
                case "مخالفات شاحنات":
                    {
                        return false;
                    }
                case "DANGEROUS VIOLATOR":
                case "مخالفات خطرة":
                    {
                        return false;
                    }
                case "WORK ZONES":
                case "مناطق العمل":
                    {
                        return false;
                    }
                case "PATROL CAR":
                case "دوريات المرور":
                    {
                        return false;
                    }
            }
            return false;
        }




        /*
         public ICommand FinishCommand
        {
            get { return new RelayCommand(FinishExecute, CanFinishExecute); }
        }
         */
        public Command GeneralButtonCommand { get { return new Command(GeneralExecute); } }

        public Command ViolationsButtonCommand { get { return new Command(ViolationExecute); } }

        public Command AccidentsButtonCommand { get { return new Command(AccidentsExecute); } }

        public Command TruckPermissionsButtonCommand { get { return new Command(TruckPermissionExecute); } }

        public Command DangerousViolatorButtonCommand { get { return new Command(DangerousVoilationsExecute); } }

        public Command WorkZonesButtonCommand { get { return new Command(WorkZonesExecute, false); } }

        public Command PatrolButtonCommand { get { return new Command(PatrolCarExecute, false); } }

        public Command HomeButtonCommand { get { return new Command((HomeExecute), CanExecuteHome()); } }

        public Command AnalysisButtonCommand { get { return new Command(AnalysisExecute); } }

        public Command PerformanceButtonCommand { get { return new Command(PerformanceExecute); } }

        public Command HeatMapButtonCommand { get { return new Command(HeatMapExecute); } }

        public Command ForeCastButtonCommand { get { return new Command(ForeCastExecute, false); } }

        public Command ManualKPIButtonCommand { get { return new Command(ManualKPIExecute); } }

        public Boolean EnableChartArea { get; set; }

        private int _activeAnalsisCategory;
        public Int32 ActiveAnalsisCategory
        {
            get { return _activeAnalsisCategory; }
            set
            {
                _activeAnalsisCategory = value;
                {
                    switch (value)
                    {
                        case 0:
                            {
                                switch (_activeMainBtnCategory)
                                {
                                    case "VIOLATIONS":
                                    case "مخالفات":
                                        {
                                            ShowChartAreaViolationTrend = true;
                                            break;
                                        }
                                    case "ACCIDENTS":
                                    case "حوادث":
                                        {
                                            ShowChartAreaAccidentsTrend = true;
                                            break;
                                        }
                                    case "DANGEROUS VIOLATOR":
                                    case "مخالفات خطرة":
                                        {
                                            ShowChartAreaDangerViolnTrend = true;
                                            break;
                                        }
                                    case "TRUCK PERMISSIONS":
                                    case "مخالفات شاحنات":
                                        {
                                            ShowChartAreaTruckPermTrend = true;
                                            break;
                                        }
                                    default:
                                        { break; }

                                }

                                break;
                            }
                        case 1:
                            {
                                switch (_activeMainBtnCategory)
                                {
                                    case "VIOLATIONS":
                                    case "مخالفات":
                                        {
                                            ShowChartAreaViolationStatistics = true;
                                            //ShowChartAreaViolationByTypeStatistics = true;
                                            break;
                                        }
                                    case "ACCIDENTS":
                                    case "حوادث":
                                        {
                                            ShowChartAreaAccidentsStatistics = true;
                                            break;
                                        }
                                    case "DANGEROUS VIOLATOR":
                                    case "مخالفات خطرة":
                                        {
                                            ShowChartAreaDangerViolnStatistics = true;
                                            break;
                                        }
                                    case "TRUCK PERMISSIONS":
                                    case "مخالفات شاحنات":
                                        {
                                            ShowChartAreaTruckPermStatistics = true;
                                            break;
                                        }
                                    default:
                                        { break; }

                                }
                                break;
                            }
                        case 2:
                            {
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

        public String ActiveMainButtonCategory
        {
            get { return _activeMainBtnCategory; }
            set
            {
                _activeMainBtnCategory = value;

                CanEnableGeneralOverViewButton = false;
                CanEnableManualKPIButton = false;
                CanEnableStatisticsButton = false;
                CanEnablePerformanceButton = false;
                CanEnableHeatMapButton = false;

                if (_activeMainBtnCategory == Resources.strGeneral)
                {
                    CanEnableGeneralOverViewButton = true;
                    CanEnableManualKPIButton = true;

                    //EnableDisableManualKPIbutton();
                }


                if (_activeMainBtnCategory == Resources.strViolations || _activeMainBtnCategory == Resources.strAccidents
                    || _activeMainBtnCategory == Resources.strDangerousViolator || _activeMainBtnCategory == Resources.strTruckPermissions)
                {
                    CanEnableStatisticsButton = true;
                }


                if (_activeMainBtnCategory == Resources.strViolations || _activeMainBtnCategory == Resources.strAccidents)
                {
                    CanEnablePerformanceButton = true;

                    if (_activeMainBtnCategory == Resources.strAccidents)
                        CanEnableHeatMapButton = true;
                }


                this.RaiseNotifyPropertyChanged();
            }
        }

        public String ActiveSubButtonCategory
        {
            get { return _activeSubBtnCategory; }
            set
            {
                _activeSubBtnCategory = value;

                ShowManualKPIArea = false;
                ShowChartArea = false;
                ShowKPITableChartArea = false;
                ShowMainDashBrdChartArea = false;
                ShowIncidentHeatMapKPIArea = false;
                switch (value)
                {
                    case "OVERVIEW":
                    case "نظرة عامة":
                        {
                            if (ActiveMainButtonCategory == Resources.strGeneral)
                                ShowMainDashBrdChartArea = true;
                            else
                                ShowMainDashBrdChartArea = false;

                            break;
                        }
                    case "STATISTICS":
                    case "إحصائيات":
                        {

                            ShowChartArea = true;

                            ActiveAnalsisCategory = 0;
                            break;
                        }
                    case "PERFORMANCE":
                    case "مؤشرات الأداء":
                        {

                            if (ActiveMainButtonCategory == Resources.strViolations || ActiveMainButtonCategory == Resources.strAccidents)
                                ShowKPITableChartArea = true;
                            else
                                ShowKPITableChartArea = false;

                            break;
                        }
                    case "HEAT MAPS":
                    case "خريطة العرض التصويري":
                        {
                            ShowIncidentHeatMapKPIArea = true;
                            break;
                        }
                    case "PREDICTION":
                    case "توقعات":
                        {

                            break;
                        }
                    case "MANUAL KPI":
                        {
                            ShowManualKPIArea = true;
                            break;
                        }


                }

                //if (value != Resources.strStatistics)
                //{
                //    ShowChartArea = false;
                //}

                //if (value != Resources.strOverview)
                //{
                //    ShowMainDashBrdChartArea = false;
                //}

                this.RaiseNotifyPropertyChanged();
            }
        }

        public void EnableDisableManualKPIbutton()
        {

            this.CanEnableManualKPIButton = isUserHaveAccessToManualKPI;
            ManualKPIurl = System.Configuration.ConfigurationSettings.AppSettings["ManualKPIurl"];

        }
        Boolean CanExecuteGeneral()
        {
            return (true);
        }

        private void GeneralExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strGeneral;
                ActiveSubButtonCategory = Resources.strOverview;

                ShowMainDashBrdChartArea = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void ViolationExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strViolations;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void AccidentsExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strAccidents;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void TruckPermissionExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strTruckPermissions;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void DangerousVoilationsExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strDangerousViolator;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void WorkZonesExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strWorkZones;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void PatrolCarExecute()
        {
            try
            {
                ActiveMainButtonCategory = Resources.strPatrolCar;
                //ActiveSubButtonCategory = Resources.strOverview;
                ActiveSubButtonCategory = Resources.strStatistics;
            }
            catch (Exception ex)
            {

            }
        }

        private void HomeExecute()
        {
            try
            {

                ActiveSubButtonCategory = Resources.strOverview;
            }
            catch (Exception ex)
            {

            }
        }

        private void AnalysisExecute()
        {
            try
            {

                ActiveSubButtonCategory = Resources.strStatistics;

                if (_activeMainBtnCategory == Resources.strViolations
                    || _activeMainBtnCategory == Resources.strAccidents
                    || _activeMainBtnCategory == Resources.strDangerousViolator
                    || _activeMainBtnCategory == Resources.strTruckPermissions)
                {
                    ShowChartArea = true;
                    ActiveAnalsisCategory = 0;
                    ShowMainDashBrdChartArea = false;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void PerformanceExecute()
        {
            try
            {
                ActiveSubButtonCategory = Resources.strPerformance;
            }
            catch (Exception ex)
            {

            }
        }

        private void HeatMapExecute()
        {
            try
            {
                ActiveSubButtonCategory = Resources.strHeatMap;
            }
            catch (Exception ex)
            {

            }
        }

        private void ForeCastExecute()
        {
            try
            {
                ActiveSubButtonCategory = Resources.strPrediction;
            }
            catch (Exception ex)
            {

            }
        }

        private void ManualKPIExecute()
        {
            try
            {
                ActiveSubButtonCategory = Resources.strManualKPI;
            }
            catch (Exception ex)
            {

            }
        }

        private void TrendAnalysisExecute()
        {
            try
            {
                ActiveSubButtonCategory = Resources.strTrendAnalysis;
            }
            catch (Exception ex)
            {

            }
        }

        private void StatisticsAnalysisExecute()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void KPIChartTableExecute()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
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
