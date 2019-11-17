using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_Course_Test.Code
{
    class CodeMain
    {
        public static string CompileFolder = @"C:\SPZ_Folder\";
        public static string FileName = $"{DateTime.Now} - {Guid.NewGuid()}.txt";
        public static void PrintTokenFiles()
        {
            StreamWriter writer = new StreamWriter(CompileFolder + FileName, true, Encoding.UTF8);
            writer.WriteLine("\t\t\t List of tokens.\n");
            writer.WriteLine(" ============================== ");

            writer.WriteLine("\tName\t\tType\t\t\tValue\t\tLine\tText\n");
            writer.WriteLine(" ============================== ");


        }
    }
}
