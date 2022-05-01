using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFramework.Abstrations.Theme;

namespace UIFramework
{
    public static class UiMsdiExtensions
    {
        public static IServiceCollection ConfigureUI(this IServiceCollection services)
        {
            services.AddSingleton<IThemesController, ThemesController>();
            return services;
        }
    }
}
