using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JWTDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            // 设置url地址
            .UseUrls("http://localhost:3000")
                .UseStartup<Startup>();
    }
}
