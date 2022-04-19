using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class ServiceScopeFactory<T> : IServiceScopeFactory<T>
        where T : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceScopeFactory(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

        public IServiceScope<T> CreateScope() => new ServiceScope<T>(_serviceScopeFactory.CreateScope());
    }
}
