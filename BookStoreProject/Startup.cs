using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookStoreProject.Startup))]
namespace BookStoreProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
