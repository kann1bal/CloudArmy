using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MAP.Presentation.Startup))]
namespace MAP.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
           
            ConfigureAuth(app);
        }
    }
}
