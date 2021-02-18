using DashboardWpf.Core;
using DashboardWpf.Modules.TKB.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DashboardWpf.Modules.ModuleName
{
    public class TKBModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public TKBModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // TODO: Navigate from Menu
            //_regionManager.RequestNavigate(RegionNames.CONTENT_REGION, "TKBStammdatenView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TKBStammdatenView>();
            containerRegistry.RegisterForNavigation<TKBDispositionView>();
        }
    }
}