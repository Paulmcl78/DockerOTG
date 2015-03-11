using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dockertest.Startup))]
namespace dockertest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        { 

            ConfigureAuth(app);
        }
    }
}
