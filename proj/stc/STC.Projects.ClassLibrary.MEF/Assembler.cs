using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using STC.Projects.ClassLibrary.Common.Interfaces;
using System.ComponentModel.Composition.Hosting;
using System.Runtime.InteropServices;

namespace STC.Projects.ClassLibrary.MEF
{
    public class Assembler
    {
        [ImportMany]
        IEnumerable<Lazy<IUserControl, IUserControlData>> UserControls;
        private CompositionContainer _container;
        AggregateCatalog catalog;
        public Assembler()
        {
             catalog= new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            //    catalog.Catalogs.Add(new AssemblyCatalog(typeof(Prog).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(string.Format(@"{0}\", System.Configuration.ConfigurationSettings.AppSettings["MEFFolder"])));
            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            ComposeParts();
        }

        private void ComposeParts()
        {
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        public IUserControl GetUserControl(string UserControlName, bool IsNeedRecompose = false)
        {
            if (IsNeedRecompose)
            {
                _container.Dispose();
 //               GC.Collect();
                _container = new CompositionContainer(catalog);
                ComposeParts();
            }
            var control = UserControls.FirstOrDefault(x => x.Metadata.UserControlName.Equals(UserControlName));
            if (control != null)
                return control.Value;
            return null;
        }
    }
}
