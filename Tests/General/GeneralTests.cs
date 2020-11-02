using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using Assessment.Data.Contexts;
using System.Collections.Generic;

namespace Assessment.Tests.General
{
    [TestClass]
    public class GeneralTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context) {

        }

        // [TestMethod]
        // public void CheckEnvironmentVariable()
        // {
        //     var pwd = Environment.GetEnvironmentVariable("MySQLPassword"); // only valid when deployed

        //     Console.WriteLine($"Pwd = {pwd}");

        //     Assert.IsFalse(String.IsNullOrEmpty(pwd));
        // }
    }
}
