using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using Assessment.Data.Contexts;
using Assessment.Web.Areas.Admin.Controllers;
using Assessment.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Assessment.Tests.Web.Areas.Admin.Controllers
{
    [TestClass]
    public class RolesControllerTests
    {
        private static AssessmentContext db;
        private static ApplicationDbContext applicationDbContext;
        private static RolesController controller;
        private static UserManager<User> userManager;
        private static RoleManager<Assessment.Models.Role> roleManager;
        private static SqliteConnection _connection;

        [ClassInitialize]
        public static void Initialize(TestContext context) {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("../../../../Web/"))
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.Development.json",true)
                .AddUserSecrets("aspnet-Web-E596AB41-E0FD-4C24-BDFB-32D00FF015E9")
                .AddEnvironmentVariables()
                .Build();
            var options1 = new DbContextOptionsBuilder<AssessmentContext>();
            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>();
            var cn = String.Format(config["ConnectionStrings:AwsConnection"], config["Aws:MySQLPassword"]);

            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            options1.UseSqlite(_connection);
            options2.UseSqlite(_connection);

            db = new AssessmentContext(options1.Options);
            applicationDbContext = new ApplicationDbContext(options2.Options);
            userManager = new UserManager<User>(new UserStore<User>(applicationDbContext), null, null, null, null, null, null, null, null);
            roleManager = new RoleManager<Assessment.Models.Role>(new RoleStore<Assessment.Models.Role>(applicationDbContext), null, null, null, null);
            controller = new RolesController(applicationDbContext, userManager, roleManager);

            if (db.Database.GetPendingMigrations().Any()) {
                db.Database.Migrate();
            }


            if (applicationDbContext.Database.GetPendingMigrations().Any()) {
                applicationDbContext.Database.Migrate();
            }
            if (!applicationDbContext.Roles.Any()) {
                applicationDbContext.Roles.Add(new Assessment.Models.Role {
                    Id = Guid.NewGuid().ToString(),
                    Name = "System Administrator",
                });
            }
            if (!applicationDbContext.Users.Any()) {
                applicationDbContext.Users.Add(new User {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dan Champagne",
                });
                applicationDbContext.SaveChanges();
            }
        }

        [ClassCleanup]
        public static void Cleanup() {
            _connection.Close();
        }


        [TestMethod]
        public async Task IndexPageRenders()
        {
            var view = await controller.Index();
            var result = view as ViewResult;
            
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task IndexHasData()
        {
            var view = await controller.Index();
            var result = view as ViewResult;
            var data = result.Model as List<Assessment.Models.Role>;
            var users = applicationDbContext.Users.ToList();
            
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, data.Count());
        }

        [TestMethod]
        public async Task CreateNewRole()
        {
            var expected = applicationDbContext.Roles.Count() + 1;
            var view = await controller.Create(new Assessment.Models.Role { Name = "Test", Id = Guid.NewGuid().ToString(), });
            var result = view as ViewResult;            
            
            var actual = applicationDbContext.Roles.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task EditExistingRole()
        {
            var role = new Assessment.Models.Role { Name = "Test", Id = Guid.NewGuid().ToString(), };
            var expectedRoleCount = applicationDbContext.Roles.Count() + 1;
            var actualRoleCount = -1;

            await controller.Create(role);
            
            role.Name = "Updated Test";
            var view = await controller.Edit(role.Id, role);
            var result = view as RedirectToActionResult;

            var updatedRole = await applicationDbContext.Roles
                .AsNoTracking()
                .Where(x => x.Id == role.Id)
                .FirstOrDefaultAsync();

            actualRoleCount = applicationDbContext.Roles.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(role.Name, updatedRole.Name);
            Assert.AreEqual(expectedRoleCount, actualRoleCount);
        }

        [TestMethod]
        public async Task DeleteExistingRole()
        {
            var role = new Assessment.Models.Role { Name = "Test", Id = Guid.NewGuid().ToString(), };
            var expectedRoleCount = applicationDbContext.Roles.Count();
            var actualRoleCount = -1;

            await controller.Create(role);
            
            var view = await controller.DeleteConfirmed(role.Id);
            var result = view as RedirectToActionResult;

            actualRoleCount = applicationDbContext.Roles.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRoleCount, actualRoleCount);
        }

    }
}
