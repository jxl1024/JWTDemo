using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JWTDemo.Server
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
            CreateWebHostBuilder(args,config).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>();
    }
}
