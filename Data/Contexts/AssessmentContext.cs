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

        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<RubricCriteria> RubricCriteria { get; set; }
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
                    new Role { Id = 3, Name = "Scorer", },
                    new Role { Id = 4, Name = "Student", }
                });
                SaveChanges();


                Users.AddRange(new [] {
                    new User {
                        Id = 1, 
                        SchoolId = 1, 
                        Name = "Martie Kaczmarek",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 2 },
                        }
                    },
                    new User {
                        Id = 2, 
                        SchoolId = 1, 
                        Name = "Stan Kurkovsky",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 2 },
                        }
                    },
                    new User {
                        Id = 3, 
                        SchoolId = 1, 
                        Name = "Yvonne Kirby",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 2 },
                        }
                    },
                    new User {
                        Id = 4,
                        SchoolId = 1,
                        Name = "Daniel Champagne",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 5,
                        SchoolId = 1,
                        Name = "Parvathy Kumar",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 6,
                        SchoolId = 1,
                        Name = "Mansimran Singh",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 7,
                        SchoolId = 1,
                        Name = "Jason Smith",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },

                    new User {
                        Id = 8,
                        SchoolId = 1,
                        Name = "Yash Dalsania",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 9,
                        SchoolId = 1,
                        Name = "Luis Gutierrez",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 10,
                        SchoolId = 1,
                        Name = "Chenyang Lin",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 11,
                        SchoolId = 1,
                        Name = "Trung Minh Tri Nguyen",
                        UserRoles = new List<UserRole> {
                            new UserRole { RoleId = 1 },
                            new UserRole { RoleId = 4 },
                        }
                    },
                    new User {
                        Id = 12,
                        SchoolId = 1,
                        Name = "Paul Pasquarelli",
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
                    new Section { SessionId=1, UserId=2, CRN = 12345, Name = "CS 510-OL1" }
                });
                SaveChanges();

                // TODO: finish seeding data
                Rubrics.AddRange(new [] {
                    new Rubric { 
                        Id = 1, 
                        SchoolId = 1, 
                        Code = "WC", 
                        Name = "Writen Communication", 
                        File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_WrittenCommunication.pdf")) 
                    },
                    new Rubric { 
                        Id = 2, 
                        SchoolId = 1, 
                        Code = "CT", 
                        Name = "Critical Thinking", 
                        File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_CriticalThinking.pdf"))
                    },
                    new Rubric { 
                        Id = 3, 
                        SchoolId = 1, 
                        Code = "QR",
                        Name = "Quantitative Reasoning", 
                        File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_QuantitativeLiteracy.pdf"))
                    },
                    new Rubric { 
                        Id = 4, 
                        SchoolId = 1, 
                        Code = "CE", 
                        Name = "Civic Engagement", 
                        File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_CivicEngagement.pdf")),
                        RubricCriteria = new List<RubricCriteria> {
                            new RubricCriteria {
                                Name = "Diversity of Communities and Cultures",
                                Desciption1 = "Expresses attitudes and beliefs as an individual, from a one-sided view. Is indifferent or resistant to what can be learned from diversity of communities and cultures.",
                                Desciption2 = "Has awareness that own attitudes and beliefs are different from those of other cultures and communities. Exhibits little curiosity about what can be learned from diversity of communities and cultures.",
                                Desciption3 = "Reflects on how own attitudes and beliefs are different from those of other cultures and communities. Exhibits curiosity about what can be learned from diversity of communities and cultures.",
                                Desciption4 = "Demonstrates evidence of adjustment in own attitudes and beliefs because of working within and learning from diversity of communities and cultures. Promotes others' engagement with diversity.",
                            },
                            new RubricCriteria {
                                Name = "Analysis of Knowledge",
                                Desciption1 = "Begins to identify knowledge (facts, theories, etc.) from one's own academic study/field/discipline that is relevant to civic engagement and to one's own participation in civic life, politics, and government.",
                                Desciption2 = "Begins to connect knowledge (facts, theories, etc.) from one's own academic study/field/discipline to civic engagement and to tone's own participation in civic life, politics, and government.",
                                Desciption3 = "Analyzes knowledge (facts, theories, etc.) from one's own academic study/field/discipline making relevant connections to civic engagement and to one's own participation in civic life, politics, and government.",
                                Desciption4 = "Connects and extends knowledge (facts, theories, etc.) from one's own academic study/field/discipline to civic engagement and to one's own participation in civic life, politics, and government.",
                            },
                            new RubricCriteria {
                                Name = "Civic Identity and Commitment",
                                Desciption1 = "Provides little evidence of her/his experience in civic-engagement activities and does not connect experiences to civic identity.",
                                Desciption2 = "Evidence suggests involvement in civic-engagement activities is generated from expectations or course requirements rather than from a sense of civic identity.",
                                Desciption3 = "Provides evidence of experience in civic-engagement activities and describes what she/he has learned about her or himself as it relates to a growing sense of civic identity and commitment.",
                                Desciption4 = "Provides evidence of experience in civic- engagement activities and describes what she/he has learned about her or himself as it relates to a reinforced and clarified sense of civic identity and continued commitment to public action.",
                            },
                            new RubricCriteria {
                                Name = "Civic Communication",
                                Desciption1 = "Communicates in civic context, showing ability to do one of the following: express, listen, and adapt ideas and messages based on others' perspectives.",
                                Desciption2 = "Communicates in civic context, showing ability to do more than one of the following: express, listen, and adapt ideas and messages based on others' perspectives.",
                                Desciption3 = "Effectively communicates in civic context, showing ability to do all of the following: express, listen, and adapt ideas and messages based on others' perspectives.",
                                Desciption4 = "Tailors communication strategies to effectively express, listen, and adapt to others to establish relationships to further civic action",
                            },
                            new RubricCriteria {
                                Name = "Civic Action and Reflection",
                                Desciption1 = "Has experimented with some civic activities but shows little internalized understanding of their aims or effects and little commitment to future action.",
                                Desciption2 = "Has clearly participated in civically focused actions and begins to reflect or describe how these actions may benefit individual(s) or communities.",
                                Desciption3 = "Demonstrates independent experience and team leadership of civic action, with reflective insights or analysis about the aims and accomplishments of one’s actions.",
                                Desciption4 = "Demonstrates independent experience and shows initiative in team leadership of complex or multiple civic engagement activities, accompanied by reflective insights or analysis about the aims and accomplishments of one’s actions.",
                            },
                            new RubricCriteria {
                                Name = "Civic Contexts/ Structures",
                                Desciption1 = "Experiments with civic contexts and structures, tries out a few to see what fits.",
                                Desciption2 = "Demonstrates experience identifying intentional ways to participate in civic contexts and structures.",
                                Desciption3 = "Demonstrates ability and commitment to work actively within community contexts and structures to achieve a civic aim.",
                                Desciption4 = "Demonstrates ability and commitment to collaboratively work across and within community contexts and structures to achieve a civic aim.",
                            },
                        }
                    },
                    new Rubric { 
                        Id = 5, 
                        SchoolId = 1, 
                        Code = "IL", 
                        Name = "Information Literacy", 
                        File = System.IO.File.ReadAllBytes(Path.Combine(seedData, "Rubric_InformationLiteracy.pdf"))
                    },
                    new Rubric { 
                        Id = 6, 
                        SchoolId = 1, 
                        Code = "SR", 
                        Name = "Scientific Reasoning",
                    },
                    new Rubric { 
                        Id = 7, 
                        SchoolId = 1, 
                        Code = "ED", 
                        Name = "Ethical Dimensions",
                    },
                    new Rubric { 
                        Id = 8, 
                        SchoolId = 1, 
                        Code = "HU", 
                        Name = "Historical Understanding",
                    },
                    new Rubric { 
                        Id = 9, 
                        SchoolId = 1, 
                        Code = "OC", 
                        Name = "Oral Communication",
                    },
                    new Rubric { 
                        Id = 10, 
                        SchoolId = 1, 
                        Code = "AK",
                        Name = "Aesthetic Knowledge",
                    },
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
