using System;
using System.Collections.Generic;
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
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {

                    using (var c2 = services.GetRequiredService<Assessment.Data.Contexts.AssessmentContext>()) {
                        using (var context = services.GetRequiredService<Assessment.Web.Areas.Identity.Data.ApplicationDbContext>()) {

                            if (c2.Database.GetPendingMigrations().Any()) {
                                c2.Database.Migrate();
                            }

                            if (context.Database.GetPendingMigrations().Any()) {
                                context.Database.Migrate();
                            }

                            var hasher = new PasswordHasher<User>();
                            var userStore = new UserStore<User>(context);
                            var userManager = services.GetService<UserManager<User>>();
                            var roleManager = services.GetService<RoleManager<IdentityRole>>();


                            var roles = new [] { "System Administrator", "School Administrator", "Scorer", };
                            foreach (var role in roles) {
                                if (!context.Roles.Any(x => x.Name == role)) {
                                    Task.Run(async () => {
                                        await roleManager.CreateAsync(new IdentityRole(role));
                                    }).Wait();
                                }
                            }


                            

                            if (!c2.Schools.Any()) {
                                if (!c2.Schools.Any()) {
                                    c2.Schools.AddRange(new [] {
                                        new School { Id = 1, Name = "Central Connecticut State University" },
                                        new School { Id = 2, Name = "Tunxis Community College" },
                                    });
                                    c2.SaveChanges();
                                }
                            }
                            

                            
                            if (!context.Users.Any()) {
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
                        }
                    }
                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred adding users.");
                    throw;
                    
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
