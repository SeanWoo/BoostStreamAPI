using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BoostStreamServer.Areas.Identity.IdentityHostingStartup))]
namespace BoostStreamServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}