using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Models;
using Assessment.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Assessment.Tests.Data.Contexts
{
    [TestClass]
    public class AssessmentContextTests
    {
        private static AssessmentContext db;

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
            var optionsBuilder = new DbContextOptionsBuilder<AssessmentContext>();
            var cn = String.Format(config["ConnectionStrings:AwsConnection"], config["Aws:MySQLPassword"]);

#region Test code, to drop and create database each time
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.ToLowerInvariant().Contains("mstest") );
            if (assemblies.Any()) {
                var cn_base = String.Format(config["ConnectionStrings:AwsConnectionBase"], config["Aws:MySQLPassword"]);

                var db1 = new MySql.Data.MySqlClient.MySqlConnection(cn_base);
                var cmd = db1.CreateCommand();

                db1.Open();
                cmd.CommandText = "drop database if exists `ccsu`";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "create database `ccsu`";
                cmd.ExecuteNonQuery();
                db1.Close();
            }
#endregion

            optionsBuilder.UseMySql(cn);
            db = new AssessmentContext(optionsBuilder.Options);
            db.Migrate();
            db.SeedData();
        }

        [ClassCleanup]
        public static void Cleanup() {
            // db.Database.EnsureDeleted();
        }

        [TestMethod]
        public void ConnectToMySQL()
        {
            var expected = System.Data.ConnectionState.Open;
            var actual = System.Data.ConnectionState.Broken;

            db.Database.GetDbConnection().Open();
            actual = db.Database.GetDbConnection().State;
            db.Database.GetDbConnection().Close();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnsureDatabaseSeeded()
        {
            var item = db.Schools.FirstOrDefault();

            Assert.AreEqual(1, item.Id);
        }
    }
}
