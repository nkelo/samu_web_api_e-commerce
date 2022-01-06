using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SamuEcommerce_Web_App.Startup))]
namespace SamuEcommerce_Web_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
