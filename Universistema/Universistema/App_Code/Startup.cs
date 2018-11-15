using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Universistema.Startup))]
namespace Universistema
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
