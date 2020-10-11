using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using Assessment.Data.Contexts;
using Assessment.Web.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Assessment.Tests
{
    [TestClass]
    public class ApplicationDbContextTests
    {
        private static ApplicationDbContext db;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("../../../../Web/"))
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.Development.json",true)
                .AddUserSecrets("aspnet-Web-E596AB41-E0FD-4C24-BDFB-32D00FF015E9")
                .AddEnvironmentVariables()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var cn = String.Format(config["ConnectionStrings:AwsConnection"], config["Aws:MySQLPassword"]);

            optionsBuilder.UseMySql(cn);
            db = new ApplicationDbContext(optionsBuilder.Options);
            db.Database.Migrate();
        }

        [ClassCleanup]
        public static void Cleanup() {
            // db.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
