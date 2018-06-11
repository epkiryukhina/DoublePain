using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoublePain.Startup))]
namespace DoublePain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
