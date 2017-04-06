using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tipsters.Web.Startup))]
namespace Tipsters.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
