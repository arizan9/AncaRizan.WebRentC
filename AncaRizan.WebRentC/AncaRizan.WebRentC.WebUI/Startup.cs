using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AncaRizan.WebRentC.WebUI.Startup))]
namespace AncaRizan.WebRentC.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
