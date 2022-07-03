using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer
{
    class Program
    {
        private const string ROOT = @"F:\C# organizer test folder";

        static void Main(string[] args)
        {
            string[] files = SetFiles(ROOT);

            if(files.Length != 0)
            {
                CreateSubDirs(files);
                OrganizeFiles(files);
                
            } else
            {
                Console.WriteLine("The directory is clean...");
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadLine();
        }

        private static string[] SetFiles(string path)
        {
            string[] files = null;

            try
            {
                files =  Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return files;
        }
            
        private static void CreateSubDirs(string[] files)
        {
            foreach (string file in files)
            {
                try
                {
                    string path = $@"{ROOT}\{new FileInfo(file).Extension}";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        Console.WriteLine($"Created directory: {path}...");
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }

        private static void OrganizeFiles(string[] files)
        {
            foreach (string file in files)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string fileName = fileInfo.Name;
                    string newPath = $@"{ROOT}\{fileInfo.Extension}\{fileName}";

                    File.Move(fileInfo.FullName, newPath);
                    Console.WriteLine($"Moved {fileName} to {newPath}...");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
