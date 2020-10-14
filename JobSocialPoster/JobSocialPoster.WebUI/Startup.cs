using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobSocialPoster.WebUI.Startup))]
namespace JobSocialPoster.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
