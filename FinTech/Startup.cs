using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinTech.Startup))]
namespace FinTech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
