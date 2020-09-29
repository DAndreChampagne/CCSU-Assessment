using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using Assessment.Data.Contexts;
using System.Collections.Generic;

namespace Assessment.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context) {

        }

        [TestMethod]
        public void TestMethod1()
        {
            var expected = "Administrator, Student";
            var actual = String.Empty;

            var user = new Assessment.Models.User {
                Name = "Arthur Pendragon",
                UserRoles = new List<Assessment.Models.UserRole> {
                    new UserRole {
                        Role = new Role {
                            Name = "Administrator"
                        }
                    },
                    new UserRole {
                        Role = new Role {
                            Name = "Student"
                        }
                    },                    
                }
            };

            actual = user.RoleNames;

            Assert.AreEqual(expected, actual);
        }
    }
}
