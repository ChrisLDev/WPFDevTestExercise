using Microsoft.Extensions.DependencyInjection;

namespace Modularization
{
    public interface IModule
    {
        void RegisterServices(IServiceCollection service);
    }
}