using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SPZ_GUI.ASM.Analize;

namespace SPZ_GUI.ASM
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
        public static void PrintAND(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintAND.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call OR
        /// </summary>
        public static void PrintOR(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintOR.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call NOT
        /// </summary>
        public static void PrintNOT(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintNOT.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call Equal
        /// </summary>
        public static void PrintEQ(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintEQ.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call Greater
        /// </summary>
        public static void PrintGE(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintGE.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call Less
        /// </summary>
        public static void PrintLE(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintLE.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Call number MOD
        /// </summary>
        public static void PrintMOD(StreamWriter f, string filePath)
        {
            filePath += FileNames.SPZ_PrintMOD.ToString() + ".txt";
            StringBuilder codeText = new StringBuilder();

            StreamReader reader = new StreamReader(filePath);
            codeText.Append(reader.ReadToEnd());

            f.Write(codeText.ToString());
        }

        /// <summary>
        /// Print standard ending of file
        /// </summary>
        public static void PrintEnding(StreamWriter f)
        {

        }

        public static void PrintNewLine(StreamWriter f)
        {
            f.WriteLine("MOV dl, 10");
            f.WriteLine("MOV ah, 02h");
            f.WriteLine("INT 21h");
            f.WriteLine("MOV dl, 13");
            f.WriteLine("MOV ah, 02h");
            f.WriteLine("INT 21h");
        }

        /// <summary>
        /// Call input file
        /// </summary>
        public static void PrintInput(StreamWriter f)
        {
            f.Write(";====Input procedure Get()=============\n");
            f.Write("input PROC\n\tpush ax\n\tpush bx\n\tpush cx\n\tpush dx\n\tpush di\n\tpush si\n");
            f.Write("\t; save registers\n");
            f.Write("\tmov di,offset buf\n\tmov MaxLen,10\n\tmov cx,MaxLen\n\tmov si,0\n");
            f.Write("In_00:\tmov ah,01\n\tint 21h\n\tcmp al,0Dh\n\tje In_1\n\tcmp al,'-'\n");
            f.Write("\tjne In_0\n\tmov FlagS,1\n\tjmp In_00\n");
            f.Write("In_0:\tmov dl,al\n\tcall CHECK_BYTE\n\tmov TStr[si],dl\n\tinc si\n\tloop In_00\n");
            f.Write("In_1:\tpush si\n\tdec si\n\tcmp cx,MaxLen\n\tjne In_2\n\tlea dx,erStr1\n");
            f.Write("\tmov ah,09\n\tint 21h\n\tmov erFlag,1\n\tjmp In_5\n");
            f.Write("In_2:\tmov bh,0\n\tmov bl,TStr[si]\n\tMY_MUL Mul10,bx,my_z\n\tadd TBin,ax\n");
            f.Write("\tadc TBin+2,dx\n\tmov bh,0\n\tmov bl,10\n\tMY_MUL Mul10,bx,my_z\n\tmov Mul10,ax\n");
            f.Write("\tmov Mul10+2,dx\n\tdec si\n\tcmp si,0\n\tjge In_2\n\tmov ax,TBin\n\tmov dx,TBin+2\n");
            f.Write("\tpop si\n\tcmp si,MaxLen\n\tjl In_3\n\tcmp MaxLen,10\n\tjl In_2_1\n\tjs In_Err\n");
            f.Write("\tcmp dx,7FFFh\n\tja In_Err\n\tjmp In_3\n\tIn_2_1:cmp MaxLen,5\n\tjl In_2_2\n");
            f.Write("\tcmp dx,00\n\tja In_Err\n\tcmp ah,7fh\n\tja In_Err\n\tjmp In_3\n");
            f.Write("In_2_2:\tcmp ax,007Fh\n\tjbe In_3\n");
            f.Write("In_Err:\tlea dx,erStr3\n\tmov ah,09\n\tint 21h\n\tmov erFlag,1\n\tjmp In_5\n");
            f.Write("In_3:\tcmp FlagS,1\n\tjne In_4\n\tmov bx,0\n\tsub bx,ax\n\tmov ax,bx\n");
            f.Write("\tmov bx,0\n\tsbb bx,dx\n\tmov dx,bx\n");
            f.Write("In_4:\tmov [di],ax\n\tmov [di+2],dx\n\tmov TBin,0\n\tmov TBin+2,0\n\tmov Mul10,1\n");
            f.Write("\tmov Mul10+2,0\n\tmov FlagS,0\n\t;next line restore registers;\n");
            f.Write("In_5:\tlea dx,per\n\tmov ah,09\n\tint 21h\n");
            f.Write("\tpop si\n\tpop di\n\tpop dx\n\tpop cx\n\tpop bx\n\tpop ax\n");
            f.Write("ret\ninput ENDP\n\n");
            f.Write("CHECK_BYTE  PROC\nPUBLIC CHECK_BYTE\n");
            f.Write("\tsub dl,30h\n\tcmp dl,00\n\tjl ErS\n\tcmp dl,0Ah\n\tjl GO\n");
            f.Write("ErS:\tlea DX,erSTR2\n\tmov AH,09\n\tint 21h\n\tmov ah,4Ch\n\tint 21h\n");
            f.Write("GO:\tRET\nCHECK_BYTE ENDP\n");

        }

        /// <summary>
        /// Call output file
        /// </summary>
        public static void PrintOutput(StreamWriter f)
        {
            f.Write(";===Output procedure Put()=============\n");
            f.Write("output PROC\n\tpush ax\n\tpush bx\n\tpush cx\n\tpush dx\n\tpush di\n\tpush si\n");
            f.Write("\t;saveregisters\n");
            f.Write("\tcmp buf,0\n\tje exit_0\n\tmov cl,byte ptr buf+3\n\tand cl,80h\n\tje m6\n");
            f.Write("\tfild buf\n\tfchs\n\tfistp buf\n\tmov MSign,\'-\'\n");
            f.Write("M6:\tmov cx,10\n\tmov di,0\n");
            f.Write("O_1:\tffree st(0)\n\tffree st(1)\n\tfild ten\n\tfild buf\n\tfprem\n");
            f.Write("\tfistp X1\n\tmov dl,byte ptr X1\n\tadd dl,30h\n\tmov X_Str[di],dl\n");
            f.Write("\tinc di\n\tfild buf\n\tfxch st(1)\n\tfdiv\n\tfild X1\n\tfild ten\n");
            f.Write("\tfdiv\n\tfsub\n\tfrndint\n\tfistp buf\n\tloop O_1\n\tcmp MSign,'+'\n");
            f.Write("\tje O_3\n\tmov dl,MSign\n\tmov ah,02\n\tint 21h\n");
            f.Write("O_3:\tinc di\n\tmov cx,12\nO_2:\tmov dl,X_Str[di]\n\tcmp dl,31h\n");
            f.Write("\tjge O_4\n\tdec di\n\tloop O_2\n\tjmp O_5\n");
            f.Write("O_4:\tmov dl,X_Str[di]\n\tmov ah,02h\n\tint 21h\n\tdec di\n\tloop O_4\n");
            f.Write("O_5:\tmov MSign,'+'\n\tjmp exit1\n");
            f.Write("exit_0:\tmov dl,30h\n\tmov ah,02\n\tint 21h");
            f.Write("\t;restore registers\n");
            f.Write("exit1:\tpop si\n\tpop di\n\tpop dx\n\tpop cx\n\tpop bx\n\tpop ax\n");
            f.Write("ret\noutput ENDP\n");
        }
    }
}
