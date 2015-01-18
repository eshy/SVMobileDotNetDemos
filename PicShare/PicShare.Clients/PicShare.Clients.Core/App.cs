using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using EShyMedia.ClientApi.SimpleRestClient;
using PicShare.Clients.Core.Services;

namespace PicShare.Clients.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            Mvx.LazyConstructAndRegisterSingleton<ISimpleRestClient, SimpleRestClient>();
            Mvx.LazyConstructAndRegisterSingleton<IPicShareApiClient, PicShareApiClient>();

            RegisterAppStart<ViewModels.PicturesViewModel>();
        }
    }
}