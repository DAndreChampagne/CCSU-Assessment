using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Models;
using Data.Contexts;

namespace Tests.General
{
    [TestClass]
    public class GeneralTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context) {

        }

        [TestMethod]
        public void CheckEnvironmentVariable()
        {
            var pwd = Environment.GetEnvironmentVariable("MySQLPassword");

            Console.WriteLine($"Pwd = {pwd}");

            Assert.IsFalse(String.IsNullOrEmpty(pwd));
        }
    }
}
