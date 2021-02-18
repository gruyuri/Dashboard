
using DashboardWpf.Core.Models;
using System.Collections.Generic;

namespace DashboardWpf.Services.Interfaces
{
    public interface IModuleService
    {
        IList<ModuleUI> GetModules();
    }
}
