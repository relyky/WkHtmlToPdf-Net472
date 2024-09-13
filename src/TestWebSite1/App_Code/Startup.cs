using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestWebSite1.Startup))]
namespace TestWebSite1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
