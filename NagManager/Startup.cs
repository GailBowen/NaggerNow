using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NagManager.Startup))]
namespace NagManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
