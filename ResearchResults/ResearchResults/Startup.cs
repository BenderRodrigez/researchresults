using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResearchResults.Startup))]
namespace ResearchResults
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
