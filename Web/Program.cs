using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Assessment.Web
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }


        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {
                    var assessmentContext = services.GetRequiredService<Assessment.Data.Contexts.AssessmentContext>();
                    var applicationDbContext = services.GetRequiredService<Assessment.Web.Areas.Identity.Data.ApplicationDbContext>();

                    var userStore = new UserStore<User>(applicationDbContext);
                    var userManager = services.GetService<UserManager<User>>();
                    var roleManager = services.GetService<RoleManager<IdentityRole>>();
                    var hasher = userManager.PasswordHasher;
                    
                    var pwd = Path.GetFullPath("~");
                    var seedDataPath = Path.GetFullPath("../Data/Contexts/SeedData/");

                    if (assessmentContext.Database.GetPendingMigrations().Any()) {
                        assessmentContext.Database.Migrate();
                    }

                    if (applicationDbContext.Database.GetPendingMigrations().Any()) {
                        applicationDbContext.Database.Migrate();
                    }

#region User Roles

                    var roles = new [] { "System Administrator", "School Administrator", "Scorer", };
                    foreach (var role in roles) {
                        if (!applicationDbContext.Roles.Any(x => x.Name == role)) {
                            Task.Run(async () => {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }).Wait();
                        }
                    }

#endregion

#region Schools

                    if (!assessmentContext.Schools.Any()) {
                        if (!assessmentContext.Schools.Any()) {
                            assessmentContext.Schools.AddRange(new [] {
                                new School { Id = 1, Name = "Central Connecticut State University" },
                                new School { Id = 2, Name = "Tunxis Community College" },
                            });
                            assessmentContext.SaveChanges();
                        }
                    }
                    
#endregion

#region Users
                    
                    if (!applicationDbContext.Users.Any()) {
                        var dan = new User {
                            SchoolId = 1,
                            Name = "Daniel Champagne",
                            Email = "Daniel.Champagne@my.ccsu.edu",
                            NormalizedEmail = "DANIEL.CHAMPAGNE@MY.CCSU.EDU",
                            EmailConfirmed = true,
                            UserName = "Daniel.Champagne@my.ccsu.edu",
                            NormalizedUserName = "DANIEL.CHAMPAGNE@MY.CCSU.EDU",
                        };
                        dan.PasswordHash = hasher.HashPassword(dan, "Abcdefg!1");

                        var users = new [] {
                            dan,
                            // new User {
                            //     SchoolId = 1, 
                            //     Name = "Martie Kaczmarek",
                            // },
                            // new User {
                            //     SchoolId = 1, 
                            //     Name = "Stan Kurkovsky",
                            // },
                            // new User {
                            //     SchoolId = 1, 
                            //     Name = "Yvonne Kirby",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Parvathy Kumar",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Mansimran Singh",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Jason Smith",
                            // },

                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Yash Dalsania",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Luis Gutierrez",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Chenyang Lin",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Trung Minh Tri Nguyen",
                            // },
                            // new User {
                            //     SchoolId = 1,
                            //     Name = "Paul Pasquarelli",
                            // },
                        };

                        foreach (var user in users) {
                            Task.Run(async () => {
                                var result = await userStore.CreateAsync(user);
                                Console.WriteLine($"Creating user {user.Name}... ({result})");
                                if (result.Succeeded) {
                                    var u = await userManager.FindByNameAsync(user.UserName);

                                    if (!await userManager.IsInRoleAsync(u, roles[0])) {
                                        var roleResult = await userManager.AddToRoleAsync(u, roles[0]);
                                        Console.WriteLine($"\tAdding to role {roles[0]} was {roleResult}");
                                    }
                                }
                            }).Wait();
                        }
                    }

#endregion                        

#region Sessions

                    if (!assessmentContext.Sessions.Any()) {
                        assessmentContext.Sessions.AddRange(new [] {
                            new Session { Id=1, SchoolId=1, Year = 2021, Semester = Semester.Fall, Name = "Fall 2020", StartDate = new DateTime(2020, 08, 24), EndDate = new DateTime(2020, 12, 23), },
                            new Session { Id=2, SchoolId=1, Year = 2021, Semester = Semester.Spring, Name = "Spring 2021", StartDate = new DateTime(2021, 01, 19), EndDate = new DateTime(2021, 5, 31), },
                        });
                        
                        assessmentContext.SaveChanges();
                    }

#endregion

#region Sections

                    if (!assessmentContext.Sections.Any()) {
                        assessmentContext.Sections.AddRange(new [] {
                            new Section { SessionId=1, CRN = 12345, Name = "CS 510-OL1" }
                        });
                        
                        assessmentContext.SaveChanges();
                    }
                    
#endregion

#region Rubrics

                    if (!assessmentContext.Rubrics.Any()) {
                        // TODO: finish seeding data
                        assessmentContext.Rubrics.AddRange(new [] {
                            new Rubric { 
                                Id = 1, 
                                SchoolId = 1, 
                                Code = "WC", 
                                Name = "Writen Communication", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_WrittenCommunication.pdf")) 
                            },
                            new Rubric { 
                                Id = 2, 
                                SchoolId = 1, 
                                Code = "CT", 
                                Name = "Critical Thinking", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_CriticalThinking.pdf"))
                            },
                            new Rubric { 
                                Id = 3, 
                                SchoolId = 1, 
                                Code = "QR",
                                Name = "Quantitative Reasoning", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_QuantitativeLiteracy.pdf"))
                            },
                            new Rubric { 
                                Id = 4, 
                                SchoolId = 1, 
                                Code = "CE", 
                                Name = "Civic Engagement", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_CivicEngagement.pdf")),
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
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_InformationLiteracy.pdf"))
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
                        
                        assessmentContext.SaveChanges();
                    }
                    
#endregion

                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred adding users.");
                    throw;
                    
                }
            }

            host.Run();
        }

    }
}
