using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Newtonsoft.Json;
using Assessment.Logic.Services;

namespace Assessment.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public static string GetDatabasePassword(IConfiguration config) {
            var envPassword = System.Environment.GetEnvironmentVariable("MySQLPassword");
            var localPwd = config["Aws:MySQLPassword"];
            var pwd = envPassword ?? localPwd;

            if (String.IsNullOrEmpty(pwd))
                throw new InvalidProgramException("Cannot determine application password");
                
            return pwd;
        }

        public static string GetDatabaseConnectionString(IConfiguration config) {
            return String.Format(config["ConnectionStrings:AwsConnection"], GetDatabasePassword(config));
        }

        internal string DatabasePassword { get { return GetDatabasePassword(Configuration); } }
        public string DatabaseConnectionString { get { return GetDatabaseConnectionString(Configuration); } }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<AssessmentContext>(options => {
                options.UseLoggerFactory(LoggerFactory.Create(l => l.AddConsole()));
                options.UseMySql(DatabaseConnectionString, options => { 
                    options.EnableRetryOnFailure();
                });
            });

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "AreasRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            
                endpoints.MapControllerRoute(
                    name: "defaultRoute",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages(); // required for Identity area
            });
        }
    }
}
