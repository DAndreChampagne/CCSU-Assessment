using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using Assessment.Data.Contexts;
using System.IO;
using System.IO.Compression;
using Assessment.Logic.Services;

namespace Assessment.Tests.Logic.Services
{
    [TestClass]
    public class UnitTest1
    {
        static AssessmentContext db;
        static string _filePath;

        [ClassInitialize]
        public static void Initialize(TestContext context) {
            db = new AssessmentContext();
            db.Migrate();

            _filePath = Path.GetFullPath("../../../Logic/Services/SampleData/Rubric 1.zip");
        }

        [ClassCleanup]
        public static void Cleanup() {
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(_filePath), "*.pdf")) {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void ParseFileNames()
        {
            var outputDirectory = Path.GetDirectoryName(_filePath);
            var expected = new[] { "202040", "508309", "61817", "CT", "3","41850" };
            string[] actual = null;

            using (var zipFile = ZipFile.OpenRead(_filePath)) {
                foreach (ZipArchiveEntry file in zipFile.Entries.Where(x => x.Length == 0)) {
                    var data = Path.GetFileNameWithoutExtension(file.Name).Split("_");

                    if (actual == null)
                        actual = data;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckForExistingFiles()
        {
            var outputDirectory = Path.GetDirectoryName(_filePath);
            var actual = -1;
            var expected = 0;

            var files = FileService.CheckForExistingFiles(_filePath, outputDirectory);
            files.ForEach(x => Console.WriteLine($"Existing file: {x}"));

            actual = files.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnpackZipFile()
        {
            var outputDirectory = Path.GetDirectoryName(_filePath);
            var actual = false;

            FileService.UnpackToDirectory(_filePath, outputDirectory);
            actual = File.Exists(Path.Combine(outputDirectory, "202040_508309_61817_CT_3_41850.pdf"));

            Assert.IsTrue(actual);
        }
    }
}
