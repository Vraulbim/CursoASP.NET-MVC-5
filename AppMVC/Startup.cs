using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppMVC.Startup))]
namespace AppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
