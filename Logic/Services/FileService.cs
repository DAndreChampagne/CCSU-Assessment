using System;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Assessment.Models;
using Assessment.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Logic.Services
{
    /// <summary>
    /// Class object that holds results of a artifact ZIP upload.
    /// </summary>
    public class ProcessArtifactResult {
        
        /// <summary>
        /// Total number of files present in the ZIP file.
        /// </summary>
        /// <value></value>
        public int TotalFiles { get; set; }

        /// <summary>
        /// The artifact objects that were processed from the ZIP file. 
        /// </summary>
        /// <typeparam name="Artifact"></typeparam>
        /// <returns></returns>
        public List<Artifact> Artifacts { get; set; } = new List<Artifact>();

        /// <summary>
        /// List of file names that were not processed correctly.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public List<string> FilesNotProcessed { get; set; } = new List<string>();

        /// <summary>
        /// Returns true if all files were processed, and the NotProcessedFiles count is zero.
        /// </summary>
        /// <value></value>
        public bool Success { get { return NotProcessedFiles == 0; } }

        /// <summary>
        /// Number of sucessfully processed files.
        /// </summary>
        /// <returns></returns>
        public int ProcessedFiles { get { return Artifacts.Count(); } }

        /// <summary>
        /// Number of files that were not able to be processed.
        /// </summary>
        /// <returns></returns>
        public int NotProcessedFiles { get { return FilesNotProcessed.Count(); } }
    }


    /// <summary>
    /// 
    /// </summary>
    public static class FileService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="_context"></param>
        /// <returns></returns>
        public static async Task<ProcessArtifactResult> ProcessZipFileIntoArtifacts(string path, AssessmentContext _context) {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var result = new ProcessArtifactResult();
            var tempPath = System.IO.Path.GetTempPath();
            var filesProcessed = new List<string>();

            ZipFile.ExtractToDirectory(path, tempPath, true);

            using (var zipFile = ZipFile.OpenRead(path)) {
                foreach (var entry in zipFile.Entries) {
                    var f = Path.Combine(tempPath, entry.FullName);
                    var values = Path.GetFileNameWithoutExtension(entry.FullName).Split("_");

                    if (!File.Exists(f) || values.Length != 6) {
                        result.FilesNotProcessed.Add(entry.FullName);
                        continue;
                    } 

                    filesProcessed.Add(f);
                    result.Artifacts.Add(new Artifact {
                        Term = values[0],
                        StudentId = values[1],
                        FacultyId = Int32.Parse(values[2]),
                        RubricId = values[3],
                        Level = values[4],
                        CRN = Int32.Parse(values[5]),
                        File = await File.ReadAllBytesAsync(f),
                    });
                }    
            }

            // if (result.Artifacts.Count() > 0) {
            //     var rubrics = await _context.Rubrics
            //         .Select(x => new { x.Id, x.Abbreviation, })
            //         .ToListAsync();
            //     foreach (var artifact in result.Artifacts) {
            //         artifact.RubricId = rubrics.First(x => x.Abbreviation == artifact.LearningObjective).Id;
            //     }
            // }

            foreach (var file in filesProcessed) {
                if (File.Exists(file)) {
                    File.Delete(file);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<string> SaveTempFile(IFormFile file) {
            if (file == null)
                throw new ArgumentNullException("file");

            var path = System.IO.Path.GetTempFileName();

            path = Path.Combine(
                Path.GetDirectoryName(path),
                $"{Path.GetFileNameWithoutExtension(path)}{Path.GetExtension(file.FileName)}"
            );

            using (var fileStream = new FileStream(path, FileMode.Create)) {
                await file.CopyToAsync(fileStream);
                if (!System.IO.File.Exists(path)) {
                    throw new IOException("Cannot save file to disk");
                }
            }

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task DeleteTempFile(string path) {
            if (System.IO.File.Exists(path)) {
                System.IO.File.Delete(path);     
                if (System.IO.File.Exists(path)) {
                    throw new IOException("Cannot delete temporary file");
                }   
            }
        }

        /// <summary>
        /// Get the names of all files inside a zip file.
        /// </summary>
        /// <param name="sourceFile">Path to zip file</param>
        /// <returns>A list of file names</returns>
        public static async Task<List<string>> GetFileNames(string sourceFile) {
            if (String.IsNullOrEmpty(sourceFile))
                throw new ArgumentNullException("sourceFile");

            List<string> results = null;

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException();

            using (var zipFile = ZipFile.OpenRead(sourceFile)) {
                results = zipFile.Entries
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
        public static async Task<List<string>> CheckForExistingFiles(string sourceFile, string outputDirectory) {
            if (String.IsNullOrEmpty(sourceFile))
                throw new ArgumentNullException("sourceFile");
            if (String.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException("outputDirectory");

            if (!Directory.Exists(outputDirectory) || !File.Exists(sourceFile))
                return null;

            var existingFiles = Directory.GetFiles(outputDirectory, "*.*")
                .Select(x => Path.GetFileName(x))
                .ToList();
            var filesInZip = await GetFileNames(sourceFile);
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
        public static async Task<List<string>> UnpackToDirectory(string sourceFile, string outputDirectory, bool overwriteExistingFiles = false) {
            if (String.IsNullOrEmpty(sourceFile))
                throw new ArgumentNullException("sourceFile");
            if (String.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException("outputDirectory");
            List<string> results = null;

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException();

            try {
                results = (await GetFileNames(sourceFile))
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
