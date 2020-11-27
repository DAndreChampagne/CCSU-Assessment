using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Assessment.Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Collections.Generic;
using Pomelo.EntityFrameworkCore.MySql;

namespace Assessment.Data.Contexts {
    public class AssessmentContext : DbContext {

        private static SqliteConnection _Connection;

        public AssessmentContext(): base() {
            Console.WriteLine("AssessmentContext");
        }
        public AssessmentContext(DbContextOptions<AssessmentContext> options): base(options) {
            Console.WriteLine("AssessmentContext(options)");
        }

        public DbSet<Assessment.Models.Faculty> Faculty { get; set; }
        public DbSet<Assessment.Models.CourseSection> CourseSections { get; set; }
        public DbSet<Assessment.Models.Rubric> Rubrics { get; set; }
        public DbSet<Assessment.Models.RubricCriteria> RubricCriteria { get; set; }
        public DbSet<Assessment.Models.RubricCriteriaElement> RubricCriteriaElements { get; set; }
        public DbSet<Assessment.Models.Score> Scores { get; set; }
        public DbSet<Assessment.Models.Artifact> Artifacts { get; set; }

        public DbSet<Assessment.Models.Error> Errors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("AssessmentContext.OnConfiguring");

            if (!optionsBuilder.IsConfigured) {
                base.OnConfiguring(optionsBuilder);

#if false
                Console.WriteLine("\tusing Sqlite...");

                _Connection = new SqliteConnection("Filename=:memory:");
                _Connection.Open();

                optionsBuilder.UseSqlite(_Connection);
#else
                Console.WriteLine("\tusing AWS MySQL...");

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.Development.json",true)
                    .AddUserSecrets("aspnet-Web-E596AB41-E0FD-4C24-BDFB-32D00FF015E9")
                    .AddEnvironmentVariables()
                    .Build();
                var pwd = config["Aws:MySQLPassword"];
                var cn = $"Server=ccsu.cn1ntjnqysd5.us-east-2.rds.amazonaws.com;Port=3306;Database=ccsu;Uid=admin;Pwd={pwd};";

                optionsBuilder.UseMySql(cn);
#endif

                //     var config = new ConfigurationBuilder()
                //         .SetBasePath(Path.GetFullPath("../../../../Web/"))
                //         .AddJsonFile("appsettings.json", true, true)
                //         .AddJsonFile($"appsettings.Development.json",true)
                //         .AddUserSecrets("aspnet-Web-E596AB41-E0FD-4C24-BDFB-32D00FF015E9")
                //         .AddEnvironmentVariables()
                //         .Build();

                //     optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection").Replace("__SECRET__", config["DefaultConnection:Password"])  );
                // }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // modelBuilder.Entity<UserRole>()
            //     .HasKey(x => new { x.UserId, x.RoleId });
            // modelBuilder.Entity<UserRole>()
            //     .HasOne(x => x.User)
            //     .WithMany(x => x.UserRoles)
            //     .HasForeignKey(x => x.UserId);
            // modelBuilder.Entity<UserRole>()
            //     .HasOne(x => x.Role)
            //     .WithMany(x => x.UserRoles)
            //     .HasForeignKey(x => x.RoleId);

            base.OnModelCreating(modelBuilder);
        }


        public bool Migrate() {
            if (Database.GetPendingMigrations().Any()) {
                Database.Migrate();
                return true;
            }
            return false;
        }

        


    }
}
