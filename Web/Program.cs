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

                    var roles = new [] { "System Administrator", "Scorer", };
                    foreach (var role in roles) {
                        if (!applicationDbContext.Roles.Any(x => x.Name == role)) {
                            Task.Run(async () => {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }).Wait();
                        }
                    }

#endregion

#region Users
                    
                    if (!applicationDbContext.Users.Any()) {
                        var users = new [] {
                            new User {
                                Name = "Daniel Champagne",
                                Email = "Daniel.Champagne@my.ccsu.edu",
                                NormalizedEmail = "DANIEL.CHAMPAGNE@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "Daniel.Champagne@my.ccsu.edu",
                                NormalizedUserName = "DANIEL.CHAMPAGNE@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Martie Kaczmarek",
                                Email = "mkaczmarek@ccsu.edu",
                                NormalizedEmail = "MKACZMAREK@CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "mkaczmarek@ccsu.edu",
                                NormalizedUserName = "MKACZMAREK@CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Stan Kurkovsky",
                                Email = "kurkovsky@ccsu.edu",
                                NormalizedEmail = "KURKOVSKY@CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "kurkovsky@ccsu.edu",
                                NormalizedUserName = "KURKOVSKY@CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Yvonne Kirby",
                                Email = "ykirby@ccsu.edu",
                                NormalizedEmail = "YKIRBY@CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "ykirby@ccsu.edu",
                                NormalizedUserName = "YKIRBY@CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Parvathy Kumar",
                                Email = "parvathy.kumar@my.ccsu.edu",
                                NormalizedEmail = "PARVATHY.KUMAR@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "parvathy.kumar@my.ccsu.edu",
                                NormalizedUserName = "PARVATHY.KUMAR@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Mansimran Singh",
                                Email = "mansimransingh@my.ccsu.edu",
                                NormalizedEmail = "MANSIMRANSINGH@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "mansimransingh@my.ccsu.edu",
                                NormalizedUserName = "MANSIMRANSINGH@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Jason Smith",
                                Email = "jason.r.smith@my.ccsu.edu",
                                NormalizedEmail = "JASON.R.SMITH@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "jason.r.smith@my.ccsu.edu",
                                NormalizedUserName = "JASON.R.SMITH@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Yash Dalsania",
                                Email = "yash.dalsania@my.ccsu.edu",
                                NormalizedEmail = "YASH.DALSANIA@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "yash.dalsania@my.ccsu.edu",
                                NormalizedUserName = "YASH.DALSANIA@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Luis Gutierrez",
                                Email = "luis.gutierrez@my.ccsu.edu",
                                NormalizedEmail = "LUIS.GUTIERREZ@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "luis.gutierrez@my.ccsu.edu",
                                NormalizedUserName = "LUIS.GUTIERREZ@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Chenyang Lin",
                                Email = "chenyanglin@my.ccsu.edu",
                                NormalizedEmail = "CHENYANGLIN@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "chenyanglin@my.ccsu.edu",
                                NormalizedUserName = "CHENYANGLIN@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Trung Minh Tri Nguyen",
                                Email = "trungminhtri.nguyen@my.ccsu.edu",
                                NormalizedEmail = "TRUNGMINHTRI.NGUYEN@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "trungminhtri.nguyen@my.ccsu.edu",
                                NormalizedUserName = "TRUNGMINHTRI.NGUYEN@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
                            new User {
                                Name = "Paul Pasquarelli",
                                Email = "pasquarellip@my.ccsu.edu",
                                NormalizedEmail = "PASQUARELLIP@MY.CCSU.EDU",
                                EmailConfirmed = true,
                                UserName = "pasquarellip@my.ccsu.edu",
                                NormalizedUserName = "PASQUARELLIP@MY.CCSU.EDU",
                                PasswordHash = hasher.HashPassword(null, "Abcdefg!1"),
                            },
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

                    // if (!assessmentContext.Sessions.Any()) {
                    //     assessmentContext.Sessions.AddRange(new [] {
                    //         new Session { Id=1, Year = 2021, Semester = Semester.Fall, Name = "Fall 2020", StartDate = new DateTime(2020, 08, 24), EndDate = new DateTime(2020, 12, 23), },
                    //         new Session { Id=2, Year = 2021, Semester = Semester.Spring, Name = "Spring 2021", StartDate = new DateTime(2021, 01, 19), EndDate = new DateTime(2021, 5, 31), },
                    //     });
                        
                    //     assessmentContext.SaveChanges();
                    // }

#endregion

#region Sections

                    // if (!assessmentContext.Sections.Any()) {
                    //     assessmentContext.Sections.AddRange(new [] {
                    //         new Section { SessionId=1, CRN = 12345, Name = "CS 510-OL1" }
                    //     });
                        
                    //     assessmentContext.SaveChanges();
                    // }
                    
#endregion

#region Rubrics

                    if (!assessmentContext.Rubrics.Any()) {
                        // TODO: finish seeding data
                        assessmentContext.Rubrics.AddRange(new [] {
                            new Rubric { 
                                Id = "WC", 
                                Name = "Writen Communication", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_WrittenCommunication.pdf")),
                                RubricCriteria = new List<RubricCriteria> {
                                    new RubricCriteria {
                                        Name = "Context of and Purpose for Writing <i>Includes considerations of audience, purpose, and the circumstances surrounding the writing task(s).</i>",
                                        Desciption1 = "Demonstrates minimal attention to context, audience, purpose, and to the assigned tasks(s) (e.g., expectation of instructor or self as audience).",
                                        Desciption2 = "Demonstrates awareness of context, audience, purpose, and to the assigned tasks(s) (e.g., begins to show awareness of audience's perceptions and assumptions).",
                                        Desciption3 = "Demonstrates adequate consideration of context, audience, and purpose and a clear focus on the assigned task(s) (e.g., the task aligns with audience, purpose, and context).",
                                        Desciption4 = "Demonstrates a thorough understanding of context, audience, and purpose that is responsive to the assigned task(s) and focuses all elements of the work.",
                                    },
                                    new RubricCriteria {
                                        Name = "Content Development",
                                        Desciption1 = "Uses appropriate and relevant content to develop simple ideas in some parts of the work.",
                                        Desciption2 = "Uses appropriate and relevant content to develop and explore ideas through most of the work.",
                                        Desciption3 = "Uses appropriate, relevant, and compelling content to explore ideas within the context of the discipline and shape the whole work.",
                                        Desciption4 = "Uses appropriate, relevant, and compelling content to illustrate mastery of the subject, conveying the writer's understanding, and shaping the whole work.",
                                    },
                                    new RubricCriteria {
                                        Name = "Genre and Disciplinary Conventions <i>Formal and informal rules inherent in the expectations for writing in particular forms and/or academic fields (please see glossary).</i>",
                                        Desciption1 = "Attempts to use a consistent system for basic organization and presentation.",
                                        Desciption2 = "Follows expectations appropriate to a specific discipline and/or writing task(s) for basic organization, content, and presentation",
                                        Desciption3 = "Demonstrates consistent use of important conventions particular to a specific discipline and/or writing task(s), including organization, content, presentation, and stylistic choices",
                                        Desciption4 = "Demonstrates detailed attention to and successful execution of a wide range of conventions particular to a specific discipline and/or writing task (s) including organization, content, presentation, formatting, and stylistic choices",
                                    },
                                    new RubricCriteria {
                                        Name = "Sources and Evidence",
                                        Desciption1 = "Demonstrates an attempt to use sources to support ideas in the writing.",
                                        Desciption2 = "Demonstrates an attempt to use credible and/or relevant sources to support ideas that are appropriate for the discipline and genre of the writing.",
                                        Desciption3 = "Demonstrates consistent use of credible, relevant sources to support ideas that are situated within the discipline and genre of the writing.",
                                        Desciption4 = "Demonstrates skillful use of high- quality, credible, relevant sources to develop ideas that are appropriate for the discipline and genre of the writing",
                                    },
                                    new RubricCriteria {
                                        Name = "Control of Syntax and Mechanics",
                                        Desciption1 = "Uses language that sometimes impedes meaning because of errors in usage.",
                                        Desciption2 = "Uses language that generally conveys meaning to readers with clarity, although writing may include some errors.",
                                        Desciption3 = "Uses straightforward language that generally conveys meaning to readers. The language in the portfolio has few errors.",
                                        Desciption4 = "Uses graceful language that skillfully communicates meaning to readers with clarity and fluency, and is virtually error-free.",
                                    },
                                },
                            },
                            new Rubric { 
                                Id = "CT", 
                                Name = "Critical Thinking", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_CriticalThinking.pdf")),
                                RubricCriteria = new List<RubricCriteria> {
                                    new RubricCriteria {
                                        Name = "Explanation of issues",
                                        Desciption1 = "Issue/problem to be considered critically is stated without clarification or description.",
                                        Desciption2 = "Issue/problem to be considered critically is stated but description leaves some terms undefined, ambiguities unexplored, boundaries undetermined, and/or backgrounds unknown.",
                                        Desciption3 = "Issue/problem to be considered critically is stated, described, and clarified so that understanding is not seriously impeded by omissions.",
                                        Desciption4 = "Issue/problem to be considered critically is stated clearly and described comprehensively, delivering all relevant information necessary for full understanding.",
                                    },
                                    new RubricCriteria {
                                        Name = "Evidence <i>Selecting and using information to investigate a point of view or conclusion</i>",
                                        Desciption1 = "Information is taken from source(s) without any interpretation/evaluation. Viewpoints of experts are taken as fact, without question.",
                                        Desciption2 = "Information is taken from source(s) with some interpretation/evaluation, but not enough to develop a coherent analysis or synthesis. Viewpoints of experts are taken as mostly fact, with little questioning.",
                                        Desciption3 = "Information is taken from source(s) with enough interpretation/evaluation to develop a coherent analysis or synthesis. Viewpoints of experts are subject to questioning.",
                                        Desciption4 = "Information is taken from source(s) with enough interpretation/evaluation to develop a comprehensive analysis or synthesis. Viewpoints of experts are questioned thoroughly.",
                                    },
                                    new RubricCriteria {
                                        Name = "Influence of context and assumptions",
                                        Desciption1 = "Shows an emerging awareness of present assumptions (sometimes labels assertions as assumptions). Begins to identify some contexts when presenting a position.",
                                        Desciption2 = "Questions some assumptions. Identifies several relevant contexts when presenting a position. May be more aware of others' assumptions than one's own (or vice versa).",
                                        Desciption3 = "Identifies own and others' assumptions and several relevant contexts when presenting a position.",
                                        Desciption4 = "Thoroughly (systematically and methodically) analyzes own and others' assumptions and carefully evaluates the relevance of contexts when presenting a position.",
                                    },
                                    new RubricCriteria {
                                        Name = "Student's position (perspective, thesis/hypothesis)",
                                        Desciption1 = "Specific position (perspective, thesis/hypothesis) is stated, but is simplistic and obvious.",
                                        Desciption2 = "Specific position (perspective, thesis/hypothesis) acknowledges different sides of an issue.",
                                        Desciption3 = "Specific position (perspective, thesis/hypothesis) takes into account the complexities of an issue. Others' points of view are acknowledged within position (perspective, thesis/hypothesis).",
                                        Desciption4 = "Specific position (perspective, thesis/hypothesis) is imaginative, taking into account the complexities of an issue. Limits of position (perspective, thesis/hypothesis) are acknowledged. Others' points of view are synthesized within position (perspective, thesis/hypothesis).",
                                    },
                                    new RubricCriteria {
                                        Name = "Conclusions and related outcomes (implications and consequences)",
                                        Desciption1 = "Conclusion is inconsistently tied to some of the information discussed; related outcomes (consequences and implications) are oversimplified.",
                                        Desciption2 = "Conclusion is logically tied to information (because information is chosen to fit the desired conclusion); some related outcomes (consequences and implications) are identified clearly.",
                                        Desciption3 = "Conclusion is logically tied to a range of information, including opposing viewpoints; related outcomes (consequences and implications) are identified clearly.",
                                        Desciption4 = "Conclusions and related outcomes (consequences and implications) are logical and reflect student’s informed evaluation and ability to place evidence and perspectives discussed in priority order.",
                                    },
                                },
                            },
                            new Rubric { 
                                Id = "QR",
                                Name = "Quantitative Reasoning", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_QuantitativeLiteracy.pdf")),
                                RubricCriteria = new List<RubricCriteria> {
                                    new RubricCriteria {
                                        Name = "Interpretation <i>Ability to explain information presented in mathematical forms (e.g., equations, graphs, diagrams, tables, words)</i>",
                                        Desciption1 = "Attempts to explain information presented in mathematical forms, but draws incorrect conclusions about what the information means. <i>For example, attempts to explain the trend data shown in a graph, but will frequently misinterpret the nature of that trend, perhaps by confusing positive and negative trends.</i>",
                                        Desciption2 = "Provides somewhat accurate explanations of information presented in mathematical forms, but occasionally makes minor errors related to computations or units. <i>For instance, accurately explains trend data shown in a graph, but may miscalculate the slope of the trend line.</i>",
                                        Desciption3 = "Provides accurate explanations of information presented in mathematical forms. <i>For instance, accurately explains the trend data shown in a graph.</i>",
                                        Desciption4 = "Provides accurate explanations of information presented in mathematical forms. Makes appropriate inferences based on that information. <i>For example, accurately explains the trend data shown in a graph and makes reasonable predictions regarding what the data suggest about future events.</i>",
                                    },
                                    new RubricCriteria {
                                        Name = "Representation <i>Ability to convert relevant information into various mathematical forms (e.g., equations, graphs, diagrams, tables, words)</i>",
                                        Desciption1 = "Completes conversion of information but resulting mathematical portrayal is inappropriate or inaccurate.",
                                        Desciption2 = "Completes conversion of information but resulting mathematical portrayal is only partially appropriate or accurate.",
                                        Desciption3 = "Competently converts relevant information into an appropriate and desired mathematical portrayal.",
                                        Desciption4 = "Skillfully converts relevant information into an insightful mathematical portrayal in a way that contributes to a further or deeper understanding.",
                                    },
                                    new RubricCriteria {
                                        Name = "Calculation",
                                        Desciption1 = "Calculations are attempted but are both unsuccessful and are not comprehensive.",
                                        Desciption2 = "Calculations attempted are either unsuccessful or represent only a portion of the calculations required to comprehensively solve the problem.",
                                        Desciption3 = "Calculations attempted are essentially all successful and sufficiently comprehensive to solve the problem.",
                                        Desciption4 = "Calculations attempted are essentially all successful and sufficiently comprehensive to solve the problem. Calculations are also presented elegantly (clearly, concisely, etc.)",
                                    },
                                    new RubricCriteria {
                                        Name = "Application / Analysis <i>Ability to make judgments and draw appropriate conclusions based on the quantitative analysis of data, while recognizing the limits of this analysis</i>",
                                        Desciption1 = "Uses the quantitative analysis of data as the basis for tentative, basic judgments, although is hesitant or uncertain about drawing conclusions from this work.",
                                        Desciption2 = "Uses the quantitative analysis of data as the basis for workmanlike (without inspiration or nuance, ordinary) judgments, drawing plausible conclusions from this work.",
                                        Desciption3 = "Uses the quantitative analysis of data as the basis for competent judgments, drawing reasonable and appropriately qualified conclusions from this work.",
                                        Desciption4 = "Uses the quantitative analysis of data as the basis for deep and thoughtful judgments, drawing insightful, carefully qualified conclusions from this work.",
                                    },
                                    new RubricCriteria {
                                        Name = "Assumptions <i>Ability to make and evaluate important assumptions in estimation, modeling, and data analysis</i>",
                                        Desciption1 = "Attempts to describe assumptions.",
                                        Desciption2 = "Explicitly describes assumptions.",
                                        Desciption3 = "Explicitly describes assumptions and provides compelling rationale for why assumptions are appropriate.",
                                        Desciption4 = "Explicitly describes assumptions and provides compelling rationale for why each assumption is appropriate. Shows awareness that confidence in final conclusions is limited by the accuracy of the assumptions.",
                                    },
                                    new RubricCriteria {
                                        Name = "Communication <i>Expressing quantitative evidence in support of the argument or purpose of the work (in terms of what evidence is used and how it is formatted, presented, and contextualized)</i>",
                                        Desciption1 = "Presents an argument for which quantitative evidence is pertinent, but does not provide adequate explicit numerical support. (May use quasi-quantitative words such as \"many,\" \"few,\" \"increasing,\" \"small,\" and the like in place of actual quantities.)",
                                        Desciption2 = "Uses quantitative information, but does not effectively connect it to the argument or purpose of the work.",
                                        Desciption3 = "Uses quantitative information in connection with the argument or purpose of the work, though data may be presented in a less than completely effective format or some parts of the explication may be uneven.",
                                        Desciption4 = "Uses quantitative information in connection with the argument or purpose of the work, presents it in an effective format, and explicates it with consistently high quality.",
                                    },
                                },
                            },
                            new Rubric { 
                                Id = "CE", 
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
                                Id = "IL", 
                                Name = "Information Literacy", 
                                File = System.IO.File.ReadAllBytes(Path.Combine(seedDataPath, "Rubric_InformationLiteracy.pdf")),
                                RubricCriteria = new List<RubricCriteria> {
                                    new RubricCriteria {
                                        Name = "Determine the Extent of Information Needed",
                                        Desciption1 = "Has difficulty defining the scope of the research question or thesis. Has difficulty determining key concepts. Types of information (sources) selected do not relate to concepts or answer research question.",
                                        Desciption2 = "Defines the scope of the research question or thesis incompletely (parts are missing, remains too broad or too narrow, etc.). Can determine key concepts. Types of information (sources) selected partially relate to concepts or answer research question.",
                                        Desciption3 = "Defines the scope of the research question or thesis completely. Can determine key concepts. Types of information (sources) selected relate to concepts or answer research question.",
                                        Desciption4 = "Effectively defines the scope of the research question or thesis. Effectively determines key concepts. Types of information (sources) selected directly relate to concepts or answer research question.",
                                    },
                                    new RubricCriteria {
                                        Name = "Access the Needed Information",
                                        Desciption1 = "Accesses information randomly, retrieves information that lacks relevance and quality.",
                                        Desciption2 = "Accesses information using simple search strategies, retrieves information from limited and similar sources.",
                                        Desciption3 = "Accesses information using variety of search strategies and some relevant information sources. Demonstrates ability to refine search.",
                                        Desciption4 = "Accesses information using effective, well- designed search strategies and most appropriate information sources.",
                                    },
                                    new RubricCriteria {
                                        Name = "Evaluate Information and its Sources Critically (Corrected Dimension 3: Evaluate Information and its Sources Critically in July 2013)",
                                        Desciption1 = "Chooses a few information sources. Selects sources using limited criteria (such as relevance to the research question).",
                                        Desciption2 = "Chooses a variety of information sources. Selects sources using basic criteria (such as relevance to the research question and currency).",
                                        Desciption3 = "Chooses a variety of information sources appropriate to the scope and discipline of the research question. Selects sources using multiple criteria (such as relevance to the research question, currency, and authority).",
                                        Desciption4 = "Chooses a variety of information sources appropriate to the scope and discipline of the research question. Selects sources after considering the importance (to the researched topic) of the multiple criteria used (such as relevance to the research question, currency, authority, audience, and bias or point of view).",
                                    },
                                    new RubricCriteria {
                                        Name = "Use Information Effectively to Accomplish a Specific Purpose",
                                        Desciption1 = "Communicates information from sources. The information is fragmented and/or used inappropriately (misquoted, taken out of context, or incorrectly paraphrased, etc.), so the intended purpose is not achieved.",
                                        Desciption2 = "Communicates and organizes information from sources. The information is not yet synthesized, so the intended purpose is not fully achieved.",
                                        Desciption3 = "Communicates, organizes and synthesizes information from sources. Intended purpose is achieved.",
                                        Desciption4 = "Communicates, organizes and synthesizes information from sources to fully achieve a specific purpose, with clarity and depth.",
                                    },
                                    new RubricCriteria {
                                        Name = "Access and Use Information Ethically and Legally",
                                        Desciption1 = "Students use correctly one of the following information use strategies (use of citations and references; choice of paraphrasing, summary, or quoting; using information in ways that are true to original context; distinguishing between common knowledge and ideas requiring attribution) and demonstrates a full understanding of the ethical and legal restrictions on the use of published, confidential, and/or proprietary information.",
                                        Desciption2 = "Students use correctly two of the following information use strategies (use of citations and references; choice of paraphrasing, summary, or quoting; using information in ways that are true to original context; distinguishing between common knowledge and ideas requiring attribution) and demonstrates a full understanding of the ethical and legal restrictions on the use of published, confidential, and/or proprietary information.",
                                        Desciption3 = "Students use correctly three of the following information use strategies (use of citations and references; choice of paraphrasing, summary, or quoting; using information in ways that are true to original context; distinguishing between common knowledge and ideas requiring attribution) and demonstrates a full understanding of the ethical and legal restrictions on the use of published, confidential, and/or proprietary information.",
                                        Desciption4 = "Students use correctly all of the following information use strategies (use of citations and references; choice of paraphrasing, summary, or quoting; using information in ways that are true to original context; distinguishing between common knowledge and ideas requiring attribution) and demonstrate a full understanding of the ethical and legal restrictions on the use of published, confidential, and/or proprietary information.",
                                    },
                                },
                            },
                            new Rubric { 
                                Id = "SR", 
                                Name = "Scientific Reasoning",
                            },
                            new Rubric { 
                                Id = "ED", 
                                Name = "Ethical Dimensions",
                            },
                            new Rubric { 
                                Id = "HU", 
                                Name = "Historical Understanding",
                            },
                            new Rubric { 
                                Id = "OC", 
                                Name = "Oral Communication",
                            },
                            new Rubric { 
                                Id = "AK",
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
