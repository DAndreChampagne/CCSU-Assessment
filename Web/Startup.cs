using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

namespace Assessment.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services) {
            var pwd =
                System.Environment.GetEnvironmentVariable("MySQLPassword") // Get password from AWS
                ?? Configuration["Aws:MySQLPassword"] // Get password from dotnet secrets file
            ;
            var cn = String.Format(Configuration["ConnectionStrings:AwsConnection"], pwd);


            services.AddElmah();

            services.AddDbContext<AssessmentContext>(options => {
                options.UseLoggerFactory(LoggerFactory.Create(l => l.AddConsole()));
                options.UseMySql(cn);
            });


            services.AddControllersWithViews();

            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation();


            services
                .AddHealthChecks()
                .AddDbContextCheck<AssessmentContext>();

            services.AddHealthChecksUI(opt => {
                opt.SetEvaluationTimeInSeconds(30);
                opt.MaximumHistoryEntriesPerEndpoint(60);

                opt.AddHealthCheckEndpoint(name: "app", uri: "~/hc");
            }).AddInMemoryStorage();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    // ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                    ResponseWriter = async (ctx, rpt) => {
                        var result = JsonConvert.SerializeObject(new {  
                            status = rpt.Status.ToString(),  
                            errors = rpt.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })  
                        },
                        Formatting.Indented,
                        new JsonSerializerSettings {  
                            NullValueHandling = NullValueHandling.Ignore  
                        });  
                        ctx.Response.ContentType = MediaTypeNames.Application.Json;  
                        await ctx.Response.WriteAsync(result);
                    },
                });

                endpoints.MapHealthChecksUI(opt => {
                    opt.UseRelativeApiPath = false;
                    opt.UseRelativeResourcesPath = false;
                    opt.AsideMenuOpened = false;

                    opt.UIPath = "/health";
                    opt.ApiPath = "/healthAPI";
                });


                endpoints.MapControllerRoute(
                    name: "AdminArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                

            });
        }
    }
}
