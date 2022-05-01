using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFramework.Abstrations.Theme
{
    public interface IThemesController
    {
        IReadOnlyList<ThemeResourceDictionary> AvailableThemes { get; }
        ThemeResourceDictionary? CurrentTheme { get; set; }
    }
}
