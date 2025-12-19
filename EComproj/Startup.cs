using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EComproj.Startup))]
namespace EComproj
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
