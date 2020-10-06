using System;
using Assessment.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Assessment.Web.Areas.Identity.IdentityHostingStartup))]
namespace Assessment.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                services.AddAuthentication(options => {

                });

                services
                    .AddDbContext<ApplicationDbContext>(options =>
                        //options.UseSqlServer(context.Configuration.GetConnectionString("ApplicationDbContextConnection"))
                        options.UseMySql(Startup.GetDatabaseConnectionString(context.Configuration))
                    );

                services
                    .AddDefaultIdentity<IdentityUser>(options => {
                        options.SignIn.RequireConfirmedAccount = true;
                    })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                ;
            });
        }
    }
}