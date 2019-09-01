using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using ZFS.Core.Interfaces;
using ZFS.EFCore.DbFile;
using ZFS.EFCore.Repositories;
using AutoMapper;
using ZFS.EFCore.Resources.ViewModel;
using FluentValidation;
using ZFS.EFCore.Resources.Validator;
using ZFS.EFCore.Services.OrderBys;
using ZFS.EFCore.Resources.OrderByMapping;
using ZFS.EFCore.Services.FilterVerification;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ZFS.Api
{
    /// <summary>
    /// 自定义行为配置: 组件、服务注册、功能、中间件管道
    /// </summary>
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                //支持XML 媒体格式数据
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddJsonOptions(options =>
            {
                //序列化首字母小写
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddXmlSerializerFormatters()
             .AddFluentValidation(); //添加Fluent验证

            services.AddDbContext<ZfsDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5001;
            //});
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            //添加实体映射
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //添加业务接口
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ILoginLogRepository, LoginlogRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupFuncRepository, GroupFuncRepository>();
            services.AddScoped<IGroupUserRepository, GroupUserRepository>();
            services.AddScoped<IDictionaryTypeRepository, DictionaryTypeRepository>();
            services.AddScoped<IDictionaryRepository, DictionaryRepository>();
            services.AddScoped<IAuthorithitemRepository, AuthorithitemRepository>();
            services.AddScoped<IUnitDbWork, UnitDbWork>();

            //添加验证器
            services.AddTransient<IValidator<UserViewModel>, UserViewModelValidator>();
            services.AddTransient<IValidator<UserAddViewModel>, UserAddOrUpdateResourceValidator<UserAddViewModel>>();
            services.AddTransient<IValidator<UserUpdateViewModel>, UserAddOrUpdateResourceValidator<UserAddOrUpdateViewModel>>();
            services.AddTransient<IValidator<DictionariesViewModel>, DictionariesValidator>();
            
            //MapContainer
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<UserPropertyMapping>();
            propertyMappingContainer.Register<DictPropertyMapping>();
            propertyMappingContainer.Register<GroupPropertyMappting>();
            propertyMappingContainer.Register<MenuPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);

            services.AddTransient<ITypeHelperService, TypeHelperService>();

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments("../docs/zfsApi.xml");
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "ZFS Service API",
                    Version = "v1",
                    Description = "Sample Service for Professional C#7",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "trace gg",
                        Url = "http://www.cnblogs.com/zh7791"
                    },
                });

            });
        }

        /// <summary>
        /// 配置中间件管道服务请求
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())//开发运行时
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //if (env.IsProduction()) { }//开发环境
            //if (env.IsStaging()) { }//运行环境
            //if (env.IsEnvironment("")) { } //自定义环境变量
            //app.UseHttpsRedirection();
            app.UseHsts();
            app.UseCors("any");
            app.UseMvc();
            //app.UseStaticFiles();//show img
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ZFS接口文档");
            });
        }
    }
}
