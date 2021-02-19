using DashboardWpf.Modules.ModuleName;
using DashboardWpf.Services;
using DashboardWpf.Services.Interfaces;
using DashboardWpf.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace DashboardWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IModuleService, ModuleService>();
            containerRegistry.RegisterSingleton<IDepoService, DepoService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<TKBModule>();
        }
    }
}
