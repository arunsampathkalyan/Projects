using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Common.ExtensionClasses
{
    public static class ListExtensionClass
    {
        public static List<T> TrimFirst<T>(this List<T> List, int MaxCount)
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

        public static List<T> TrimLast<T>(this List<T> List, int MaxCount)
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

        /// <summary>
        /// Shuffle any List based on the Fisher-Yates shuffle
        /// http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        public static void Shuffle<T>(this List<T> List)
        {
            Random rng = new Random();
            int n = List.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = List[k];
                List[k] = List[n];
                List[n] = value;
            }
        }

    }
}
