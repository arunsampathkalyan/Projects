using STC.Projects.WPFControlLibrary.LandingPage.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reactive.Threading.Tasks;
using System.Windows;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class MainIndicatorsViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        ServiceLayerClient client = new ServiceLayerClient();

        private CubeDetailsDTO[] indicatorsColl;
        public CubeDetailsDTO[] IndicatorsColl
        {
            get { return indicatorsColl; }
            set { indicatorsColl = value; this.RaiseNotifyPropertyChanged(); }
        }

        #endregion

        #region Constractors
        public MainIndicatorsViewModel()
        {
            GetViolationData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetMainDashboardAsync(Utility.GetLang());
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_Details(x));
        }

        private void Add_Details(CubeDetailsDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() => { IndicatorsColl = data; });
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
