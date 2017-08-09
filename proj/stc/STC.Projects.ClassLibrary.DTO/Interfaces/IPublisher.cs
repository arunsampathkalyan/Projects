using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO.Interfaces
{
    public interface IPublisher<T>
    {
        void Publish(List<T> changedObjects);
    }
}
