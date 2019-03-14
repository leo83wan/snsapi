using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.leo83.apis.sns.Data.v1;
using com.leo83.apis.sns.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace com.leo83.apis.sns
{
    /// <summary>
    /// 启类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// API版本提供者
        /// </summary>
        private IApiVersionDescriptionProvider _provider;

        private const string _projectName = "SNS API";

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });
            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            _provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            // 注册Swagger生成器, 定义一个和多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new Info
                    {
                        Version = description.ApiVersion.ToString(),
                        Title = $"{_projectName} API v{description.ApiVersion}",
                        Description = $"A web and miniProgram and app of {_projectName} API",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Leo.Wan",
                            Email = "1830802211@qq.com",
                            Url = "http://www.leo83.com/"
                        },
                        // License = new License { 
                        //     Name = "许可证名字", 
                        //     Url = "http://www.cnblogs.com/yilezhu/" 
                        // }
                    });
                }

                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Roken}",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { 
                            "Bearer",
                            Enumerable.Empty<string>()
                        },
                    });
                // 为 Swagger JSON and UI设置xml文档注释路径
                c.IncludeXmlComments(this.GetType().Assembly.Location.Replace(".dll", ".xml"), true);
                c.DocumentFilter<ApplyTagDescriptions>();
            });

            //添加jwt验证：
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "leo83.com",//Audience
                        ValidIssuer = "api.leo83.com",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                    };
                });
            var connection = @"Data Source=" + Configuration["DBFile"];
            services.AddDbContext<SnsContext>
                (options => options.UseSqlite(connection));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点    
            app.UseSwaggerUI(c =>
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{_projectName} API {description.GroupName}");
                    c.RoutePrefix = string.Empty;
                }
            });

            // 启用JWT
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
