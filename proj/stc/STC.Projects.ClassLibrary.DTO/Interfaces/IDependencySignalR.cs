using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO.Interfaces
{
    public interface IDependencySignalR<T>
    {
        void RegisterDependency();
        void Notify(List<T> changedObjects);
    }
}
