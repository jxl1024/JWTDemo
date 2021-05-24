using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace JWTDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                 .AddJsonFile("hosting.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile("appsettings.Development.json", true, true)
                .Build();
            CreateWebHostBuilder(args, config).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
