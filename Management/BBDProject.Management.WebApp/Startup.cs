using Autofac;
using AutoMapper;
using BBDProject.Management.Db;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Db.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using BBDProject.Clients.Db;
using BBDProject.Management.Models;
using BBDProject.Management.Services;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Management.WebApp
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
                options.Cookie.Name = ".NoSqlProject.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddIdentity<DaoEmployee, DaoRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<DaoRole>()
                .AddRoleManager<RoleManager<DaoRole>>()
                .AddSignInManager<SignInManager<DaoEmployee>>()
                .AddEntityFrameworkStores<ManagementDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User";
                options.SlidingExpiration = true;
            });

            var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
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
                .WithParameter("options", DbContextOptionsFactory.GetClientDbContextOptions())
                .WithParameter("config", Configuration)
                .InstancePerLifetimeScope();

            builder.RegisterType<ManagementDbContext>()
                .WithParameter("options", DbContextOptionsFactory.GetManagementDbContextOptions())
                .WithParameter("config", Configuration)
                .InstancePerLifetimeScope();

            builder.RegisterType<UserContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Clients.Repositories.BaseRepository)))
                .Where(t => t.IsSubclassOf(typeof(Clients.Repositories.BaseRepository)))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Management.Repositories.BaseRepository)))
                .Where(t => t.IsSubclassOf(typeof(Management.Repositories.BaseRepository)))
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
                UserManager<DaoEmployee> userManager = scope.ServiceProvider.GetRequiredService<UserManager<DaoEmployee>>();
                var dbInitializer = new DbInitializer();
                dbInitializer.Initialize(userManager, roleManager).Wait();
            }
        }

        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("ManagementConnection"))
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
            public static DbContextOptions<ManagementDbContext> GetManagementDbContextOptions()
            {
                var builder = new DbContextOptionsBuilder<ManagementDbContext>();
                builder.UseNpgsql(Configuration.GetConnectionString("ManagementConnection"));
                return builder.Options;
            }

            public static DbContextOptions<ClientDbContext> GetClientDbContextOptions()
            {
                var builder = new DbContextOptionsBuilder<ClientDbContext>();
                builder.UseNpgsql(Configuration.GetConnectionString("ClientConnection"));
                return builder.Options;
            }
        }
    }
}
