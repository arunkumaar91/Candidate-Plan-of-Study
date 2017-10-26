using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AchieversCPS.Startup))]
namespace AchieversCPS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
