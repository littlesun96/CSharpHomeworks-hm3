using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading;
using System.Threading.Tasks;

namespace hm2
{
    class Program
    {
        static void Main(string[] args)
        {

            String path = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine(path);
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(path);
            WalkDirectoryTree(rootDir);
       
        }

        static int CountLines(System.IO.FileInfo file)
        {
            int counter = 0;
            string line;
            System.IO.StreamReader reader = file.OpenText();
            while ((line = reader.ReadLine()) != null)
            {
                int i = 0;
                //Console.WriteLine(line);
                Boolean onlyWhiteSpace = String.IsNullOrWhiteSpace(line);
                if (!onlyWhiteSpace)
                {
                    while (char.IsWhiteSpace(line[i]))
                    {
                        i++;
                    }
                }
                if (!onlyWhiteSpace && !(i + 1 < line.Length && (line[i] == line[i + 1]) && line[i] == '/'))
                {
                    counter++;
                   // Console.WriteLine(line);
                }
            }
            reader.Close();
            return counter;
        }
        static void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    try
                    {
                        if (fi.Extension.EndsWith(".cs"))
                        {
                            Console.WriteLine(fi.FullName);
                            Console.WriteLine(CountLines(fi));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
              
                subDirs = root.GetDirectories();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
    }
}