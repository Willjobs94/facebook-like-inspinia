using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FacebookLikeInspinia.Startup))]
namespace FacebookLikeInspinia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
