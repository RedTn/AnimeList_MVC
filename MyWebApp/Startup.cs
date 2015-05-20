using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebApp.Startup))]
namespace MyWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
