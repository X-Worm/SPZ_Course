﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_Course_Test
{
    class Program
    {
        public static void Main()
        {
            //MainMenu();
            StreamReader reader = new StreamReader(@"C:\log\code.txt");
            reader.DiscardBufferedData();
            CodeAnalize.Analize.AnalisisTokens(reader);

            // Error Checking
            Console.WriteLine(CodeAnalize.Analize.ErrorChecking());

            // GenerateCode
            StreamWriter writer = new StreamWriter(@"C:\log\CodeGen.asm");
            CodeAnalize.Analize.GenerateCode(writer);
            writer.Close();
            Console.ReadKey();
        }

        public static void MainMenu()
        {
            string compileCode = "CompileCode - 1";
            string RunCode = "RunCode - 2";
            Console.WriteLine(compileCode);
            Console.WriteLine(RunCode);
            Console.WriteLine("Choice: ");
            int choice = 0;

        }
    }
}
