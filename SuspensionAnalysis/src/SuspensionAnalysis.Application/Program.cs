using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SuspensionAnalysis
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method of application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// The main method of application to create the host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
