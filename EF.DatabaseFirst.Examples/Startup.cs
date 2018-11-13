using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EF.DatabaseFirst.Examples.Startup))]
namespace EF.DatabaseFirst.Examples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
