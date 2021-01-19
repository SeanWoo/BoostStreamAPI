using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoostStreamServer
{
    public class Program
    {
        private static Exception _unhandledException;

        public static void Main(string[] args)
        {
            var logger = new LoggerFactory().CreateLogger<Program>();

            int retries = 10;
            do
            {
                try
                {
                    CreateHostBuilder(args).Build().Run();
                }
                catch (Exception ex)
                {
                    logger.LogError($"Retries: {retries}/10 {ex.Message}");

                    if (ex.InnerException != null && ex.InnerException.Message == "Unable to connect to any of the specified MySQL hosts.")
                    {
                        Task.Delay(30000).Wait();
                    }
                    retries--;
                    _unhandledException = ex;
                }
            } while (retries > 0);

            logger.LogCritical(_unhandledException.ToString());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5080); //HTTP port
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
