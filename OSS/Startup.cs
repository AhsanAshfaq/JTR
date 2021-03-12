using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSS.Startup))]
namespace OSS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
