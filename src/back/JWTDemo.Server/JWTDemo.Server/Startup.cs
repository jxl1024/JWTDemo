using JWTDemo.Server.DatabaseContext;
using JWTDemo.Server.TokenInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTDemo.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置数据库连接字符串
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // 从appsettings.json中读取配置文件
                options.UseSqlServer(Configuration["DbConnection"].ToString());
            });


            // 注入
            services.AddScoped<ITokenHelper, TokenHelper>();


            // 读取appsettings.json文件
            services.Configure<JWTConfig>(Configuration.GetSection("JWT"));

            JWTConfig config = new JWTConfig();
            Configuration.GetSection("JWT").Bind(config);

            // 启用JWT认证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = config.Issuer,
                        ValidAudience = config.RefreshTokenAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.IssuerSigningKey))
                    };
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 添加跨域设置
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();//AllowAnyOrigin表示允许所有来源可以跨域
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            }); 
            #endregion
            // 启用认证中间件
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
