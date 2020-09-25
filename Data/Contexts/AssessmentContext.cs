using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Collections.Generic;
using Pomelo.EntityFrameworkCore.MySql;

namespace Data.Contexts {
    public class AssessmentContext : DbContext {

        private static SqliteConnection _Connection;

        public AssessmentContext(): base() {
            Console.WriteLine("AssessmentContext");
        }
        public AssessmentContext(DbContextOptions<AssessmentContext> options): base(options) {
            Console.WriteLine("AssessmentContext(options)");
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<RubricData> RubricData { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }

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
            //     .HasNoKey();
            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);


            base.OnModelCreating(modelBuilder);
        }


        public bool MigrateAndSeed() {
            return Migrate() && SeedData();
        }

        public bool Migrate() {
            if (Database.GetPendingMigrations().Any()) {
                Database.Migrate();

                return true;
            }

            return false;
        }

        public bool SeedData() {
            try {
                var seedData = Path.GetFullPath("../../../../Data/Contexts/SeedData/");

                Schools.AddRange(new [] {
                    new School { Id = 1, Name = "Central Connecticut State University" },
                    new School { Id = 2, Name = "Tunxis Community College" },
                });
                SaveChanges();


                Roles.AddRange(new [] {
                    new Role { Id = 1, Name = "Administrator", },
                    new Role { Id = 2, Name = "Faculty", },
                    new Role { Id = 3, Name = "Evaluator", },
                    new Role { Id = 4, Name = "Student", }
                });
                SaveChanges();


                Users.AddRange(new [] {
                    new User {
                        Id = 1, 
                        SchoolId = 1, 
                        Name = "MK",
                        // Name = "Martie Kaczmarek",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 2 },
                        }
                    },
                    new User {
                        Id = 2, 
                        SchoolId = 1, 
                        Name = "SK",
                        // Name = "Stan Kurkovsky",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 2 },
                        }
                    },
                    new User {
                        Id = 3,
                        SchoolId = 1,
                        Name = "DC",
                        // Name = "Daniel Champagne",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 4,
                        SchoolId = 1,
                        Name = "PK",
                        // Name = "Parvathy Kumar",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 5,
                        SchoolId = 1,
                        Name = "MS",
                        // Name = "Mansimran Singh",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 6,
                        SchoolId = 1,
                        Name = "JS",
                        // Name = "Jason Smith",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                });
                SaveChanges();

                Sessions.AddRange(new [] {
                    new Session { Id=1, SchoolId=1, Year = 2021, Semester = Semester.Fall, Name = "Fall 2020", StartDate = new DateTime(2020, 08, 24), EndDate = new DateTime(2020, 12, 23), },
                    new Session { Id=2, SchoolId=1, Year = 2021, Semester = Semester.Spring, Name = "Spring 2021", StartDate = new DateTime(2021, 01, 19), EndDate = new DateTime(2021, 5, 31), },
                });
                SaveChanges();

                Sections.AddRange(new [] {
                    new Section { SessionId=1, CRN = 42679, Name = "LSC 150-OL1" }
                });
                SaveChanges();

                Rubrics.AddRange(new [] {
                    new Rubric { Id = 1, SchoolId = 1, Code = "WC", Name = "Writen Communication", File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_WrittenCommunication.pdf")) },
                    new Rubric { Id = 2, SchoolId = 1, Code = "CT", Name = "Critical Thinking", File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_CriticalThinking.pdf")) },
                    new Rubric { Id = 3, SchoolId = 1, Code = "QR", Name = "Quantitative Reasoning", File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_QuantitativeLiteracy.pdf")) },
                    new Rubric { Id = 4, SchoolId = 1, Code = "CE", Name = "Civic Engagement", File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_CivicEngagement.pdf")) },
                    new Rubric { Id = 5, SchoolId = 1, Code = "IL", Name = "Information Literacy", File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_InformationLiteracy.pdf")) },
                    new Rubric { Id = 6, SchoolId = 1, Code = "SR", Name = "Scientific Reasoning", },
                    new Rubric { Id = 7, SchoolId = 1, Code = "ED", Name = "Ethical Dimensions", },
                    new Rubric { Id = 8, SchoolId = 1, Code = "HU", Name = "Historical Understanding", },
                    new Rubric { Id = 9, SchoolId = 1, Code = "OC", Name = "Oral Communication", },
                    new Rubric { Id = 10, SchoolId = 1, Code = "AK", Name = "Aesthetic Knowledge", },
                });
                SaveChanges();

                // Artifacts.AddRange(new [] {
                //     new Artifact {
                //         Id = 1,
                //         SchoolId = 1,
                //         RubricId = 1,
                //         Name = "Rubric_CivicEngagement.pdf",
                //         Term = "202140",
                //         StudentId = "1",
                //         UserId = 3,
                //         LearningObjective = "CT",
                //         Level = "5",
                //         CRN = "12345",
                //         File = System.IO.File.ReadAllBytes("/Users/dan/Downloads/GenEd_Rubrics_5/Rubric_CivicEngagement.pdf"),
                //     }
                // });
                // SaveChanges();


                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
