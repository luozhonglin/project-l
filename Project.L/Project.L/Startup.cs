using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Project.DAO.Impl;
using Project.DAO.Impl.DataBase;
using Project.L.Common;
using Project.Service.Impl;
using System.Reflection;
using System.Text;

namespace Project.L
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 添加基础设施服务
            //services.AddSingleton<DapperContext>();
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();

            //替换控制器激活器的配置用Autofac属性注入必须配置这个 因为Controller 默认是由 Mvc 模块管理的，需要把控制器放到IOC容器中
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });


            // 添加控制器和Swagger
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Luo.API", Version = "v1" });
            });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            //全局异常处理
            services.AddScoped<GlobalExceptionMiddleware>();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<DapperContext>().SingleInstance();
            builder.RegisterModule<DAOModuleRegister>();
            builder.RegisterModule<ServiceModuleRegister>();
            //Controller
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<ControllerBase>().PropertiesAutowired();




        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            app.UseForwardedHeaders();

            app.UseMiddleware<RequestMiddleware>();

            // 配置中间件管道
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors("AllowAll");
        }
    }

}
