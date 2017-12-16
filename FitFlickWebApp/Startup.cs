using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitFlickWebApp.Startup))]
namespace FitFlickWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
