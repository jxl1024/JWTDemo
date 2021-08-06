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
using System;
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
                        //ValidIssuer = config.Issuer,
                        //ValidAudience = config.RefreshTokenAudience,
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.IssuerSigningKey))

                        ValidateIssuerSigningKey = true,//是否验证签名,不验证的画可以篡改数据，不安全
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.IssuerSigningKey)),//解密的密钥
                        ValidateIssuer = true,//是否验证发行人，就是验证载荷中的Iss是否对应ValidIssuer参数
                        ValidIssuer = config.Issuer,//发行人
                        ValidateAudience = true,//是否验证订阅人，就是验证载荷中的Aud是否对应ValidAudience参数
                        ValidAudience = config.Audience,//订阅人
                        ValidateLifetime = true,//是否验证过期时间，过期了就拒绝访问
                        ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        RequireExpirationTime = true,
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
