using System.Web.Http;
using Microsoft.Owin;
using Owin;
using PicShare.Api.Middleware;

[assembly:OwinStartup(typeof(PicShare.Api.Startup))]
namespace PicShare.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof (BasicAuthenticationMiddleware));

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            app.MapSignalR();
        }
    }
}
