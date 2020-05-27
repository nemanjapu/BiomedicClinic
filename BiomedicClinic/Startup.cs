using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BiomedicClinic.Startup))]
namespace BiomedicClinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
