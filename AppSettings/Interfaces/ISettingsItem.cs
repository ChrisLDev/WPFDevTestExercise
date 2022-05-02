using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettings.Interfaces
{
    public interface ISettingsItem
    {
        string MenuCategory { get; }

        string IconResource { get; }

        string MenuItemName { get; }

        Type ContentViewModelType { get; }
    }
}
