using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common
{
    public class EsriMapViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        //MapView are not contained in the Page's control hierarchy. If you set a data context for the Page (or Window) it won't be available on things like Map and Layer. So use as static resour to bind anything in maps
        private string _mapLayerServiceUrl;

        public string MapLayerServiceUrl
        {
            get { return _mapLayerServiceUrl; }
            set
            {
                _mapLayerServiceUrl = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        public EsriMapViewModel()
        {
            MapLayerServiceUrl = System.Configuration.ConfigurationSettings.AppSettings["MapLayerServiceUrl"];
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
