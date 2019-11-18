using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_Course_Test.AsmCreating
{
    class AsmFileFiller
    {
        private static string componentName = "AsmFileFilter";

        /// <summary>
        /// Path to file .asm
        /// </summary>
        public static string FilePath { get; set; }

        /// <summary>
        /// Writer
        /// </summary>
        public static StreamWriter Writer { get; set; }

        /// <summary>
        /// Initialize StreamWriter
        /// </summary>
        public static void InitializeWriter()
        {
            if (String.IsNullOrWhiteSpace(FilePath))
                throw new Exception($"{componentName}: FilePath is invalid");
            Writer = new StreamWriter(FilePath);
        }

        /// <summary>
        /// Check is StreamWriter is set correctly
        /// </summary>
        public static void CheckWriter()
        {
            if (Writer == null)
                throw new Exception($"{componentName}: Writer is invalid");
        }

        /// <summary>
        /// Call PrintCode, Main PROC
        /// </summary>
        public static void PrintCode(StreamWriter f)
        {

        }

        /// <summary>
        /// Call AND
        /// </summary>
        public static void PrintAND(StreamWriter f)
        {

        }

        /// <summary>
        /// Call OR
        /// </summary>
        public static void PrintOR(StreamWriter f)
        {

        }

        /// <summary>
        /// Call NOT
        /// </summary>
        public static void PrintNOT(StreamWriter f)
        {

        }

        /// <summary>
        /// Call Equal
        /// </summary>
        public static void PrintEQ(StreamWriter f)
        {

        }

        /// <summary>
        /// Call Greater
        /// </summary>
        public static void PrintGE(StreamWriter f)
        {

        }

        /// <summary>
        /// Call Less
        /// </summary>
        public static void PrintLE(StreamWriter f)
        {

        }

        /// <summary>
        /// Call number MOD
        /// </summary>
        public static void PrintMOD(StreamWriter f)
        {
            StreamReader reader = new StreamReader();
        }

        /// <summary>
        /// Print standard ending of file
        /// </summary>
        public static void PrintEnding(StreamWriter f)
        {

        }

        /// <summary>
        /// Call input file
        /// </summary>
        public static void PrintInput(StreamWriter f)
        {

        }

        /// <summary>
        /// Call output file
        /// </summary>
        public static void PrintOutput(StreamWriter f)
        {

        }
    }
}
