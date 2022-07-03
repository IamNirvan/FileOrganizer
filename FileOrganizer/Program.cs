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
        private static string root = null;
        private static string[] files = null;


        static void Main(string[] args)
        {
            root = GetDirectory();

            if(Directory.Exists(root))
            {
                files = IdentifyFiles();

                if (files.Length != 0)
                {
                    CreateDirectories();
                    OrganizeFiles();
                }
                else
                {
                    Console.WriteLine("The directory is clean...");
                }
            }
            else
            {
                Console.WriteLine("\nWARNING: The directory doesn't exist");
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadLine();
        }

        private static string[] IdentifyFiles()
        {
            string[] files = null;

            try
            {
                files =  Directory.GetFiles(root);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return files;
        }
            
        private static void CreateDirectories()
        {
            int count = 0;

            foreach (string file in files)
            {
                try
                {
                    string path = $@"{root}\{new FileInfo(file).Extension}";

                    if (!Directory.Exists(path))
                    {
                        count++;
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

            Console.WriteLine($"\nINFO: [{count}] directories created\n\n");
        }

        private static void OrganizeFiles()
        {
            int count = 0;

            foreach (string file in files)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string fileName = fileInfo.Name;
                    string newPath = $@"{root}\{fileInfo.Extension}\{fileName}";

                    count++;
                    File.Move(fileInfo.FullName, newPath);
                    Console.WriteLine($"Moved {fileName} to {newPath}...");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            Console.WriteLine($"\nINFO: [{count}] files moved.");
        }

        private static string GetDirectory()
        {
            Console.WriteLine("Enter the directory: ");
            return Console.ReadLine();
        }
    }
}
