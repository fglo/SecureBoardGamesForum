using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BBDProject.Clients.Db;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Db.Migrations;
using BBDProject.Clients.Models;
using BBDProject.Clients.Repositories;
using BBDProject.Clients.Repositories.Product;
using BBDProject.Clients.Services;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace BBDProject.Clients.WebApp
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }

        private readonly IWebHostEnvironment _environment;

        private static string ENVIRONMENT_VARIABLE { get; set; }

        public static string GetEnvironmentVariable() => Startup.ENVIRONMENT_VARIABLE;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            _environment = env;
            Startup.ENVIRONMENT_VARIABLE = env.EnvironmentName;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = ".BBDProject.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddIdentity<DaoUser, DaoRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<DaoRole>()
                .AddRoleManager<RoleManager<DaoRole>>()
                .AddSignInManager<SignInManager<DaoUser>>()
                .AddEntityFrameworkStores<ClientDbContext>()
                .AddDefaultTokenProviders();


            services.AddSignalR();
            //services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.Name = ".BBDProject";

                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User";
                options.SlidingExpiration = true;
            });

            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DatabaseInitialize(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperProfile))));
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File($"/logs/log-.log", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
            }).SingleInstance();

            builder.Register(c => Configuration)
                .As<IConfigurationRoot>()
                .SingleInstance();

            builder.RegisterType<AppSettings>()
                .SingleInstance();

            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();

            builder.RegisterType<ActionContextAccessor>()
                .As<IActionContextAccessor>()
                .SingleInstance();

            builder.RegisterType<UrlHelperFactory>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientDbContext>()
                .WithParameter("options", DbContextOptionsFactory.Get())
                .WithParameter("config", Configuration)
                .InstancePerLifetimeScope();

            builder.RegisterType<ChatHub>()
                .ExternallyOwned()
                .SingleInstance();
            //builder.RegisterType<CustomUserIdProvider>()
            //    .As<IUserIdProvider>()
            //    .SingleInstance();

            builder.RegisterType<UserContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseRepository)))
                .Where(t => t.IsSubclassOf(typeof(BaseRepository)))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseService)))
                .Where(t => t.IsSubclassOf(typeof(BaseService)))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        #region Private

        private static void DatabaseInitialize(IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                RoleManager<DaoRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<DaoRole>>();
                UserManager<DaoUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<DaoUser>>();
                IProductRepository productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var dbInitializer = new DbInitializer();
                dbInitializer.Initialize(userManager, roleManager, productRepository).Wait();
            }
        }

        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("ClientConnection"))
                    .ScanIn(typeof(AddIdentityTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        #endregion

        public class DbContextOptionsFactory
        {
            public static DbContextOptions<ClientDbContext> Get()
            {
                var builder = new DbContextOptionsBuilder<ClientDbContext>();
                builder.UseNpgsql(Configuration.GetConnectionString("ClientConnection"));
                return builder.Options;
            }
        }
    }
}
