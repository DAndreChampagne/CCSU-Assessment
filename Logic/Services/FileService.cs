using System;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;

namespace Assessment.Logic.Services
{
    public class FileService
    {
        /// <summary>
        /// Get the names of all files inside a zip file.
        /// </summary>
        /// <param name="sourceFile">Path to zip file</param>
        /// <returns>A list of file names</returns>
        public static List<string> GetFileNames(string sourceFile) {
            List<string> results = null;

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException();

            using (var zipFile = ZipFile.OpenRead(sourceFile)) {
                results = zipFile.Entries
                    .Where(x => x.Length == 0) // TODO: verify this. it looks like file names are stored with a 0 length, actual files have lengths
                    .Select(x => x.Name)
                    .ToList();
            }

            return results;
        }

        /// <summary>
        /// Compares files already in a directory to the files within a zip file and returns a list of duplicates.
        /// </summary>
        /// <param name="sourceFile">Path to the zip file</param>
        /// <param name="outputDirectory">Directory that may contain existing files</param>
        /// <returns>A list of files that already exist on the file system.</returns>
        public static List<string> CheckForExistingFiles(string sourceFile, string outputDirectory) {

            if (!Directory.Exists(outputDirectory) || !File.Exists(sourceFile))
                return null;

            var existingFiles = Directory.GetFiles(outputDirectory, "*.*")
                .Select(x => Path.GetFileName(x))
                .ToList();
            var filesInZip = GetFileNames(sourceFile);
            var results = existingFiles
                .Where(x => filesInZip.Contains(x))
                .ToList();

            return results;
        }

        /// <summary>
        /// Unzips a zip file to a given directory.
        /// </summary>
        /// <param name="sourceFile">The zip file to be decompressed</param>
        /// <param name="outputDirectory">The location the files will be decompressed to</param>
        /// <param name="overwriteExistingFiles">True to overwrite files. Setting this to false will result in an exception if there are existing files of the same name.</param>
        /// <returns>A list of files that have been uncompressed.</returns>
        public static List<string> UnpackToDirectory(string sourceFile, string outputDirectory, bool overwriteExistingFiles = false) {
            List<string> results = null;

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException();

            try {
                results = GetFileNames(sourceFile)
                    .Select(x => Path.Combine(outputDirectory, x))
                    .ToList();

                ZipFile.ExtractToDirectory(sourceFile, outputDirectory, overwriteExistingFiles);

                return results;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


    }
}
