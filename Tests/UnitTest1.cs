using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Models;
using Data.Contexts;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static AssessmentContext db;

        [ClassInitialize]
        public static void Initialize(TestContext context) {
            db = new AssessmentContext();
            db.MigrateAndSeed();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
