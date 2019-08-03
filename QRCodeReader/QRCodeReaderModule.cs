using QRCodeReader.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace QRCodeReader
{
    public class QRCodeReaderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}