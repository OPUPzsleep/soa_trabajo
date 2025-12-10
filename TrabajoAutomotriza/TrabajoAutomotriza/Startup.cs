using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrabajoAutomotriza.Startup))]
namespace TrabajoAutomotriza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
