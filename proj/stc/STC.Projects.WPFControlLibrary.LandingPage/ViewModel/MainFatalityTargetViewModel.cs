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

namespace STC.Projects.WPFControlLibrary.LandingPage.ViewModel
{
    class MainFatalityTargetViewModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region Properties

        ServiceLayerClient client = new ServiceLayerClient();

        private CubeDTO[] _violationsCollection;
        public CubeDTO[] ViolationsCollection
        {
            get { return _violationsCollection; }
            set { _violationsCollection = value; this.RaiseNotifyPropertyChanged(); }
        }

        #endregion

        #region Constractors
        public MainFatalityTargetViewModel()
        {
            GetViolationData();
        }

        #endregion

        #region Methods

        private void GetViolationData()
        {
            var callTask = client.GetFatalitiyTargetAsync();
            var obs = callTask.ToObservable();
            obs.Subscribe((x) => Add_ViolationsDetails(x));
        }

        private void Add_ViolationsDetails(CubeDTO[] data)
        {
            Application.Current.Dispatcher.Invoke(() => { ViolationsCollection = data; });
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
