using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Workflow.Web.Startup))]
namespace Workflow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
