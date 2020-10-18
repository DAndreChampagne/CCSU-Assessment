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
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Newtonsoft.Json;
using Assessment.Logic.Services;
using ElmahCore;

namespace Assessment.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public static string GetDatabasePassword(IConfiguration config) {
            return 
                System.Environment.GetEnvironmentVariable("MySQLPassword") // Get password from AWS
                ?? config["Aws:MySQLPassword"]; // Get password from dotnet secrets file
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

            services.AddElmah<XmlFileErrorLog>(options => {
                options.ApplicationName = "OIRA";
                options.LogPath = "~/errorlogs";
                
            });

            services.AddDbContext<AssessmentContext>(options => {
                options.UseLoggerFactory(LoggerFactory.Create(l => l.AddConsole()));
                options.UseMySql(DatabaseConnectionString);
            });

            // services
            //     .AddHealthChecks()
            //     .AddDbContextCheck<AssessmentContext>(tags: new [] { "db" })
            //     .AddCheck("constant healthy", () => { return HealthCheckResult.Healthy(); }, tags: new [] { "misc" })
            // ;

            // services.AddHealthChecksUI(options => {
            //     options.SetEvaluationTimeInSeconds(30);
            //     options.SetMinimumSecondsBetweenFailureNotifications(60);
            //     options.MaximumHistoryEntriesPerEndpoint(60);
            //     options.DisableDatabaseMigrations();
            //     options.AddHealthCheckEndpoint(name: "OIRA Application", uri: "/hc");
            // }).AddInMemoryStorage();

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElmah();

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
                // endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                // {
                //     Predicate = check => true, //check.Tags.Contains(""), // filter by tags
                //     AllowCachingResponses = false,
                //     // ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                //     ResponseWriter = async (ctx, rpt) => {
                //         var result = JsonConvert.SerializeObject(new {  
                //             status = rpt.Status.ToString(),  
                //             errors = rpt.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })  
                //         },
                //         Formatting.Indented,
                //         new JsonSerializerSettings {  
                //             NullValueHandling = NullValueHandling.Ignore  
                //         });  
                //         ctx.Response.ContentType = MediaTypeNames.Application.Json;  
                //         await ctx.Response.WriteAsync(result);
                //     },
                // });

                // endpoints.MapHealthChecksUI(options => {
                //     options.UseRelativeApiPath = false;
                //     options.UseRelativeResourcesPath = false;
                //     options.AsideMenuOpened = false;

                //     options.UIPath = "/health";
                //     // opt.ApiPath = "/healthAPI";
                // });

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
