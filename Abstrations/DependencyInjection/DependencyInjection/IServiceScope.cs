using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public interface IServiceScope<T> : IDisposable where T : class
    {
        /// <summary>
        /// Get service of type T from the system.IServiceProvider
        /// </summary>
        /// <returns></returns>
        T GetRequiredServices();
    }
}
