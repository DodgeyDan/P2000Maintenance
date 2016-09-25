using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(P2000Maintenance.Startup))]
namespace P2000Maintenance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
