using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common.ExtensionClasses
{
    public static class ObservableCollectionExtensionClass
    {
        public static ObservableCollection<T> TrimFirst<T>(this ObservableCollection<T> List, int MaxCount)
        {
            try
            {
                if (List == null)
                    throw new ArgumentNullException("List");

                if (List.Count <= MaxCount)
                    return List;

                do
                {
                    T first = List.First<T>();
                    List.Remove(first);
                }
                while (List.Count > MaxCount);

                return List;
            }
            catch (Exception ex)
            {
                // add error logging here
                throw;
            }

        }

        public static ObservableCollection<T> TrimLast<T>(this ObservableCollection<T> List, int MaxCount)
        {
            try
            {
                if (List == null)
                    throw new ArgumentNullException("List");

                if (List.Count <= MaxCount)
                    return List;

                do
                {
                    T last = List.Last<T>();
                    List.Remove(last);
                }
                while (List.Count > MaxCount);

                return List;
            }
            catch (Exception ex)
            {
                // add error logging here
                throw;
            }
        }
    }
}
