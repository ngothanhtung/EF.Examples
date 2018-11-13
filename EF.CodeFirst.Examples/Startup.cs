using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EF.CodeFirst.Examples.Startup))]
namespace EF.CodeFirst.Examples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
