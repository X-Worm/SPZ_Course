using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_GUI.ASM
{
    public class Analize
    {
        /// <summary>
        /// list of tokens
        /// </summary>
        public static List<KeyWordToken> TokensTable { get; set; }

        public static bool IsPresentInput { get; set; }
        public static bool IsPresentOutput { get; set; }

        public static bool IsLight { get; set; }
        public static bool IfSave { get; set; }
        public static string CurrFile { get; set; }
        public static string CurrWay { get; set; }
        public static string CurrName { get; set; }

        public static int NumberOfTokens { get; set; }
        public static List<Identifier> IdentTable { get; set; }
        public static int NumberOfIdent { get; set; }
        public static List<Letters> LettersTable { get; set; }
        public static int NumberOfLetters { get; set; }
        public static int CurrLet { get; set; }

        public static int[] BufExprPostfixForm { get; set; } = new int[100];
        public static int nNumberErrors { get; set; }
        public static bool IsLetters { get; set; }
        public static bool IsQuotes { get; set; }
        public static string InFileName { get; set; }
        public static string OutFileName { get; set; }

        #region filesPath
        public static string Fin { get; set; }
        public static string Fout { get; set; }
        #endregion

        #region codeStack
        public static Stack<int> StackStack { get; set; } = new Stack<int>();
        public static Stack<int> StartBlockStack { get; set; } = new Stack<int>();
        public static Stack<int> CucleStack { get; set; } = new Stack<int>();
        #endregion

        #region codePart
        public void PrintTokensInFile()
        {
            string type = "";
            int i;
            StreamWriter outTokenFiles = new StreamWriter(@"D:\SPZ\Report.txt");

            outTokenFiles.WriteLine("\t\t\t List of tokens.\n");
            outTokenFiles.WriteLine("===================================================================\n");
            outTokenFiles.WriteLine("\tName\t\tType\t\t\tValue\t\tLine\tText\n");
            outTokenFiles.WriteLine("===================================================================\n");
            for (i = 0; i < NumberOfTokens; i++)
            {
                switch (TokensTable[i].Type)
                {
                    case KeyWord.ltProgram: type = "Program\t"; break;
                    case KeyWord.ltVar: type = "Var\t"; break;
                    case KeyWord.ltType: type = "Type\t"; break;
                    case KeyWord.ltBegin: type = "Start\t"; break;
                    case KeyWord.ltEnd: type = "Finish\t"; break;
                    case KeyWord.ltRead: type = "Get\t"; break;
                    case KeyWord.ltWrite: type = "Put\t"; break;
                    case KeyWord.ltWhile: type = "While\t"; break;
                    case KeyWord.ltNewValue: type = "New value"; break;
                    case KeyWord.ltAdd: type = "+\t"; break;
                    case KeyWord.ltSub: type = "-\t"; break;
                    case KeyWord.ltMul: type = "Mul\t"; break;
                    case KeyWord.ltDiv: type = "Div\t"; break;
                    case KeyWord.ltMod: type = "Mod\t"; break;
                    case KeyWord.ltEqu: type = "==\t"; break;
                    case KeyWord.ltNotEqu: type = "!=\t"; break;
                    case KeyWord.ltNot: type = "!!\t"; break;
                    case KeyWord.ltLess: type = "<\t"; break;
                    case KeyWord.ltGreate: type = ">\t"; break;
                    case KeyWord.ltAnd: type = "$$\t"; break;
                    case KeyWord.ltOr: type = "||\t"; break;
                    case KeyWord.ltEOF: type = "end of file"; break;
                    case KeyWord.ltEndGroup: type = "end group"; break;
                    case KeyWord.ltComma: type = "comma\t"; break;
                    case KeyWord.ltIdentifier: type = "identifier\t"; break;
                    case KeyWord.ltNumber: type = "number\t"; break;
                    case KeyWord.ltLBraket: type = "left braket"; break;
                    case KeyWord.ltRBraket: type = "right braket"; break;
                    case KeyWord.ltUnknown: type = "unknown\t"; break;
                    case KeyWord.ltLetters: type = "Text line\t"; break;
                    case KeyWord.ltQuotes: type = "\"\t"; break;
                }
                outTokenFiles.WriteLine($"{(i + 1)}\t{TokensTable[i].Name}\t{type}\t{TokensTable[i].Value}\t{TokensTable[i].Line}\t{TokensTable[i].Text}");
            }
            outTokenFiles.Close();
            Console.WriteLine($"Lexem found: {NumberOfTokens}");
        }

        public static int line = 1;

        public static KeyWordToken GetNextTokens(StreamReader f, int ii)
        {
            string ch;
            char[] buf = new char[50];
            bool isComment = false;

            KeyWordToken res = new KeyWordToken();

            for (; ; )
            {
                if (IsQuotes)
                {
                    string c;
                    string tmp = "";
                    int letcnt = 0;
                    IsLetters = true;
                    c = ((char)f.Read()).ToString();
                    while ((c != "\"") && (letcnt <= 255) && (c != "\n"))
                    {
                        tmp += c;
                        letcnt++;
                        c = ((char)f.Read()).ToString();
                    }
                    if (letcnt > 255) IsQuotes = true;
                    else IsQuotes = false;

                    res.Name = "Letters";
                    res.Type = KeyWord.ltLetters;
                    res.Value = 0;
                    res.Line = line;
                    res.Text = tmp;
                    break;
                }
                ch = ((char)f.Read()).ToString();
                if (ch == "{")
                {
                    string c = "";
                    c = ((char)f.Read()).ToString();

                    isComment = true;
                    while (isComment)
                    {
                        c = ((char)f.Read()).ToString();
                        if (c == "}")
                        {
                            isComment = false;
                        }
                        else if (c == "\n")
                        {
                            line++;
                        }
                    }
                    ch = ((char)f.Read()).ToString();
                }
                if (ch == "\n") line++;
                else if (f.EndOfStream)
                {
                    res.Name = "EOF";
                    res.Type = KeyWord.ltEOF;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "(")
                {
                    res.Name = "(";
                    res.Type = KeyWord.ltLBraket;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == ")")
                {
                    res.Name = ")";
                    res.Type = KeyWord.ltRBraket;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == ";")
                {
                    res.Name = ";";
                    res.Type = KeyWord.ltEndGroup;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == ",")
                {
                    res.Name = ",";
                    res.Type = KeyWord.ltComma;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "+")
                {
                    res.Name = "+";
                    res.Type = KeyWord.ltAdd;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "-")
                {
                    res.Name = "-";
                    res.Type = KeyWord.ltSub;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "*")
                {
                    res.Name = "*";
                    res.Type = KeyWord.ltMul;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "!")
                {
                    string c = ((char)f.Read()).ToString();
                    if (c == "!") // !!
                    {
                        res.Name = "!!";
                        res.Type = KeyWord.ltNot;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = "Unknown start with !";
                        res.Type = KeyWord.ltUnknown;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                }
                else if (ch == "<")
                {
                    string c = ((char)f.Read()).ToString();
                    if (c == "-") // <<
                    {
                        res.Name = "<-";
                        res.Type = KeyWord.ltNewValue;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    else if (c == ">") // not equal
                    {
                        res.Name = "<>";
                        res.Type = KeyWord.ltNotEqu;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = "<";
                        res.Type = KeyWord.ltUnknown;
                        res.Line = line;
                        break;
                    }
                }
                else if (ch == "=")
                {
                    res.Name = "=";
                    res.Type = KeyWord.ltEqu;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
                else if (ch == "&")
                {
                    string c = ((char)f.Read()).ToString();
                    if (c == "&") // &&
                    {
                        res.Name = "&&";
                        res.Type = KeyWord.ltAnd;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = "&";
                        res.Type = KeyWord.ltUnknown;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                }
                else if (ch == "|")
                {
                    string c = ((char)f.Read()).ToString();
                    if (c == "|") // ||
                    {
                        res.Name = "||";
                        res.Type = KeyWord.ltOr;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = "|";
                        res.Type = KeyWord.ltUnknown;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                }
                else if ((ch == "\"") && !(IsQuotes)) // text
                {
                    IsQuotes = true;
                    res.Name = "\"";
                    res.Type = KeyWord.ltQuotes;
                    res.Line = line;
                    res.Value = 0;
                    break;
                }
                else if (Char.IsLetter(ch[0]))
                {
                    int i = 0;
                    buf[0] = ch[0];
                    for (i = 1; ; ++i)
                    {
                        ch = ((char)f.Peek()).ToString();
                        if (Char.IsDigit(ch[0]) || Char.IsLetter(ch[0]) || (ch == "_")) { buf[i] = ch[0]; f.Read(); }
                        else break;
                    }
                    buf[i] = '\0';
                    res.Name = new string(buf).Substring(0, i);

                    string localBuf = new string(buf).Substring(0, i);
                    if (localBuf == "PROGRAM")
                    {
                        res.Type = KeyWord.ltProgram;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "VAR")
                    {
                        res.Type = KeyWord.ltVar;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "BEGIN")
                    {
                        res.Type = KeyWord.ltBegin;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "END")
                    {
                        res.Type = KeyWord.ltEnd;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "GET")
                    {
                        res.Type = KeyWord.ltRead;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "PUT")
                    {
                        res.Type = KeyWord.ltWrite;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "WHILE")
                    {
                        res.Type = KeyWord.ltWhile;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "LONGINT")
                    {
                        res.Type = KeyWord.ltType;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "DIV")
                    {
                        res.Type = KeyWord.ltDiv;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "MOD")
                    {
                        res.Type = KeyWord.ltMod;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "LE")
                    {
                        res.Type = KeyWord.ltLess;
                        res.Line = line;
                        break;
                    }
                    else if (localBuf == "GE")
                    {
                        res.Type = KeyWord.ltGreate;
                        res.Line = line;
                        break;
                    }
                    else if (Char.IsUpper(buf[0]) && (i < 8) || (TokensTable[ii - 1].Type == KeyWord.ltProgram))
                    {
                        res.Name = localBuf;
                        res.Type = KeyWord.ltIdentifier;
                        res.Value = 1;
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = localBuf;
                        res.Type = KeyWord.ltUnknown;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                    res.Value = 0;
                    res.Line = line;
                }
                else if (ch == "-") // -
                {
                    char c;
                    c = (char)f.Read();
                    if (Char.IsDigit(c))
                    {
                        int i = 2;
                        buf[0] = ch[0];
                        buf[1] = c;
                        for (; Char.IsDigit((ch = ((char)f.Peek()).ToString()).ElementAt(0)); ++i)
                        {
                            buf[i] = ch[0];
                            f.Read();
                        }
                        buf[i] = '\0';
                        res.Name = new string(buf).Substring(0, i);
                        res.Type = KeyWord.ltNumber;
                        res.Value = Convert.ToInt32(buf);
                        res.Line = line;
                        break;
                    }
                    else
                    {
                        res.Name = "-";
                        res.Type = KeyWord.ltSub;
                        res.Value = 0;
                        res.Line = line;
                        break;
                    }
                }
                else if (Char.IsDigit(ch[0])) // додатнє число
                {
                    int i = 1;
                    buf[0] = ch[0];
                    for (; Char.IsDigit((ch = ((char)f.Peek()).ToString()).ElementAt(0)); ++i)
                    {
                        buf[i] = ch[0];
                        f.Read();
                    }
                    buf[i] = '\0';
                    res.Name = new string(buf).Substring(0, i);
                    res.Type = KeyWord.ltNumber;
                    res.Value = Convert.ToInt32(new string(buf).Substring(0, i));
                    res.Line = line;
                    break;

                }
                else if ((ch[0] != '\n') && (ch[0] != '\t') && (ch[0] != ' ') && (ch[0] != '\r'))
                {
                    char[] buffer = new char[50];
                    char c;
                    int i;
                    buffer[0] = ch[0];
                    for (i = 1; ; i++)
                    {
                        c = (char)f.Peek();
                        if ((c == '\n') || (c == '\t') || (c == ';'))
                        {
                            if (c == '\n')
                            {
                                line++;
                                break;
                            }
                            else break;
                        }
                        if ((int)c == 65535) break;
                        buffer[i] = c;
                        f.Read();
                    }
                    buffer[i] = '\0';
                    res.Name = new string(buffer).Substring(0, i);
                    res.Type = KeyWord.ltUnknown;
                    res.Value = 0;
                    res.Line = line;
                    break;
                }
            }
            return res;
        }

        public static List<KeyWordToken> AnalisisTokens(StreamReader fi)
        {
            KeyWordToken tempTokens = new KeyWordToken();
            TokensTable = new List<KeyWordToken>();
            IsPresentInput = false; IsPresentOutput = false;
            IsLetters = false; IsLight = false;
            IfSave = false;
            NumberOfTokens = 0;
            IdentTable = new List<Identifier>();
            NumberOfIdent = 0;
            LettersTable = new List<Letters>();
            NumberOfLetters = 0;
            CurrLet = 0;
            BufExprPostfixForm = new int[100];
            nNumberErrors = 0;
            IsLetters = false;
            IsQuotes = false;
            InFileName = "";
            OutFileName = "";
            int i = 0;
            int localLine = line;
            char[] type = new char[50];
            IsLetters = false;
            IsQuotes = false;
            do
            {
                TokensTable.Add(new KeyWordToken());
                tempTokens = GetNextTokens(fi, i);
                if (IsLetters)
                {
                    if (IsQuotes)
                    {
                        TokensTable[i].Name = tempTokens.Name;
                        TokensTable[i].Value = tempTokens.Value;
                        TokensTable[i].Type = tempTokens.Type;
                        TokensTable[i].Line = tempTokens.Line;
                        TokensTable[i].Text = tempTokens.Text;
                        i++;
                    }
                    else
                    {
                        TokensTable[i].Name = tempTokens.Name;
                        TokensTable[i].Value = tempTokens.Value;
                        TokensTable[i].Type = tempTokens.Type;
                        TokensTable[i].Line = tempTokens.Line;
                        TokensTable[i].Text = tempTokens.Text;
                        i++;
                        TokensTable.Add(new KeyWordToken());
                        TokensTable[i].Name = "\"";
                        TokensTable[i].Value = 0;
                        TokensTable[i].Type = KeyWord.ltQuotes;
                        TokensTable[i].Line = tempTokens.Line;
                        i++;
                    }
                    IsLetters = false;
                }
                else
                {
                    TokensTable[i].Name = tempTokens.Name;
                    TokensTable[i].Value = tempTokens.Value;
                    TokensTable[i].Type = tempTokens.Type;
                    TokensTable[i].Line = tempTokens.Line;
                    TokensTable[i].Text = tempTokens.Text;
                    i++;
                }

            } while (tempTokens.Type != KeyWord.ltEOF);
            return TokensTable;
        }

        public static ConsoleColor color = ConsoleColor.Cyan;

        /// <summary>
        /// Syntactic Analyzer
        /// </summary>
        public static bool IsOperation(KeyWord t)
        {
            bool r;
            r = ((t == KeyWord.ltAdd) || (t == KeyWord.ltSub) || (t == KeyWord.ltDiv) || (t == KeyWord.ltMul) || (t == KeyWord.ltMod) ||
        (t == KeyWord.ltAnd) || (t == KeyWord.ltOr) || (t == KeyWord.ltEqu) || (t == KeyWord.ltNotEqu) || (t == KeyWord.ltLess) || (t == KeyWord.ltGreate));
            return r;
        }

        public static int IsExpression(int i, StreamWriter ef)
        {
            int nom, error = 0;
            nom = i;
            if ((TokensTable[nom].Type != KeyWord.ltLBraket) && (TokensTable[nom].Type != KeyWord.ltIdentifier) && (TokensTable[nom].Type != KeyWord.ltNumber))
            {
                ef.Write($"line: {TokensTable[nom].Line}, Expression must begin from identifier, number or '('!\n");
                error++;
            }
            for (; (TokensTable[nom].Type == KeyWord.ltRBraket); nom++)
            {
                if (TokensTable[nom].Type == KeyWord.ltRBraket)
                {
                    if (!(IsOperation(TokensTable[nom + 1].Type)) && (TokensTable[nom + 1].Type != KeyWord.ltEndGroup) && (TokensTable[nom + 1].Type != KeyWord.ltRBraket))
                    {
                        ef.Write($"line: {TokensTable[nom].Line}, After ')' must be ')', operation ';'!\n ");
                        error++;
                    }
                }
                if (TokensTable[nom].Type == KeyWord.ltLBraket)
                {
                    if ((TokensTable[nom + 1].Type != KeyWord.ltIdentifier) && (TokensTable[nom + 1].Type != KeyWord.ltLBraket) && (TokensTable[nom + 1].Type != KeyWord.ltNumber) && (TokensTable[nom + 1].Type != KeyWord.ltNot))
                    {
                        ef.Write($"line: {TokensTable[nom].Line}, After '(' must be '(' or identifier!\n ");
                        error++;
                    }
                }
                if (IsOperation(TokensTable[nom].Type))
                {
                    if ((TokensTable[nom + 1].Type != KeyWord.ltIdentifier) && (TokensTable[nom + 1].Type != KeyWord.ltLBraket) && (TokensTable[nom + 1].Type != KeyWord.ltNumber))
                    {
                        ef.Write($"line: {TokensTable[nom].Line}, After operation must be '(' or idetifier!\n");
                        error++;
                    }
                }
                if ((TokensTable[nom].Type == KeyWord.ltIdentifier) || (TokensTable[nom].Type == KeyWord.ltNumber))
                {
                    if (!(IsOperation(TokensTable[nom + 1].Type)) && (TokensTable[nom + 1].Type != KeyWord.ltRBraket) && (TokensTable[nom + 1].Type != KeyWord.ltEndGroup))
                    {
                        ef.Write($"line: {TokensTable[nom].Line}, After identifier must be ')',';',or operation!\n");
                        error++;
                    }
                }
            }
            return error;
        }

        public static int Balans(int nom, KeyWord ends, KeyWord ltBegin, KeyWord ltEnd)
        {
            Stack<int> ss = new Stack<int>();
            //ss.Push(-1);
            int j = 0, i;
            i = nom;
            for (; TokensTable[i].Type != ends; i++)
            {
                if (TokensTable[i].Type == ltBegin)
                {
                    ss.Push(i);
                }
                if (TokensTable[i].Type == ltEnd)
                {
                    if (ss.Count() == 0)
                    {
                        j = 1; // too much ')'
                        break;
                    }
                    else
                    {
                        ss.Pop();
                        if ((TokensTable[nom - 2].Type == KeyWord.ltType) && (ss.Count() == 0))
                        {
                            j = 3;
                            break;
                        }
                    }
                }
            }
            if (ss.Count() != 0)
            {
                j = 2;
            }
            return j;

        }

        public static string ErrorPatern(int errorNumber, int line, string message)
        {
            string errorPattern = $"Error: {errorNumber}, Line: {line}, Message: {message}";
            return errorPattern;
        }

        public static Tuple<int, List<Letters>, List<Identifier>> ErrorChecking(StreamWriter ef)
        {
            int label = 0;
            int i = 0, j = 1, temp = 0, ValNum = 0;

            int Err;
            int while_num = 0, STARTBLOK_ENDBLOK_num = 0;//, r1, r2;
            int Err_num = 0;

            ef.WriteLine("Error output file. Next errors found:\n\n");
            NumberOfIdent = 1;
            IdentTable = new List<Identifier>();
            LettersTable = new List<Letters>();
            IdentTable.Add(new Identifier());
            NumberOfTokens = TokensTable.Count();



            //перевірка чи першим словом у програмі є program
            if (TokensTable[0].Type != KeyWord.ltProgram)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t'Program' expected! (program must begin from the keyword 'Program')"));
            }
            //перевірка, чи другим словом в програмі є ім'я програми
            if (TokensTable[1].Type != KeyWord.ltIdentifier)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\tProgram name expected!"));
            }
            if (TokensTable[2].Type != KeyWord.ltEndGroup)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t';' expected!"));
            }
            //перевірка, чи  другим словом в програмі є var
            if (TokensTable[4].Type != KeyWord.ltVar)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t'Var' expected!"));
            }

            if (TokensTable[4].Type == KeyWord.ltVar)
            {
                i = 5;
                if (TokensTable[i].Type != KeyWord.ltIdentifier)    //перевірка, чи після VARIABLE йдуть ідентифікатори
                {
                    Err_num++;
                    ef.WriteLine(ErrorPatern(Err_num, line, "\tIdentifier expected!"));
                }
                else
                {
                    do
                    {
                        if ((TokensTable[i].Type == KeyWord.ltIdentifier) && (TokensTable[i + 1].Type == KeyWord.ltComma))
                        {
                            IdentTable.Add(new Identifier());
                            IdentTable[NumberOfIdent].Name = TokensTable[i].Name;
                            NumberOfIdent++;
                            i = i + 2;
                        }
                    } while ((TokensTable[i].Type == KeyWord.ltIdentifier) && (TokensTable[i + 1].Type == KeyWord.ltComma));
                    if ((TokensTable[i].Type == KeyWord.ltIdentifier) && (TokensTable[i + 1].Type == KeyWord.ltType))
                    {
                        IdentTable.Add(new Identifier());
                        IdentTable[NumberOfIdent].Name = TokensTable[i].Name;
                        NumberOfIdent++;
                        i = i + 2;
                        goto label1;
                    }
                    if (TokensTable[i].Type != KeyWord.ltType)
                    {
                        if (TokensTable[i].Type == KeyWord.ltComma)
                        {
                            Err_num++;
                            ef.WriteLine(ErrorPatern(Err_num, line, "\tToo much commas!"));
                        }
                        else
                        {
                            Err_num++;
                            ef.WriteLine(ErrorPatern(Err_num, line, "\tType of variable ('LONGINT') expected!"));
                        }
                    }
                    else
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, line, "\tToo much commas or identifier expected!"));
                    }
                }
                i++;
            label1:;
            }
            if (TokensTable[i].Type != KeyWord.ltEndGroup)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t';' expected!"));
            }
            else i++;
            //перевірка, чи  після оголошення змінних йде BEGIN
            if (TokensTable[3].Type != KeyWord.ltBegin)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t'BEGIN' expected!"));
            }
            else i++;
            if (TokensTable[NumberOfTokens - 2].Type != KeyWord.ltEnd)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t'END' expected!"));
            }
            if (TokensTable[NumberOfTokens - 1].Type != KeyWord.ltEOF)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\tEnd of file expected!"));
            }
            for (j = 0; TokensTable[j].Type != KeyWord.ltBegin; j++) ;
            Err = Balans(j - 1, KeyWord.ltEOF, KeyWord.ltBegin, KeyWord.ltEnd);
            if (Err == 1)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\tToo much 'BEGIN'!"));

            }
            if (Err == 2)
            {
                Err_num++;
                ef.WriteLine(ErrorPatern(Err_num, line, "\t'END' expected!"));
            }
            for (j = 0; ; j++)
            {
                int BraketErr;
                if (TokensTable[j].Type == KeyWord.ltUnknown)        //Пошук невідомих слів(не ідентифікаторів)
                {
                    Err_num++;
                    ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tUnknown identifier! ") + TokensTable[j].Name);
                }
                if ((TokensTable[j].Type == KeyWord.ltIdentifier) && (TokensTable[j - 1].Type != KeyWord.ltProgram))
                {
                    int k = 1, flag = 0;
                    for (k = 1; k <= NumberOfIdent; k++)
                    {
                        if(IdentTable.Count == 1)
                        {
                            ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tIdentifier not found: (cannot initialize identifier after declaring, or identifier is not declared).") + TokensTable[j].Name);
                        }
                        else if(TokensTable[j].Name == IdentTable[k].Name)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag != 1)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tUnknown identifier! ") + TokensTable[j].Name);
                    }
                }
                if (TokensTable[j].Type == KeyWord.ltNewValue)
                {
                    int buf;
                    BraketErr = Balans(j, KeyWord.ltEndGroup, KeyWord.ltLBraket, KeyWord.ltRBraket);
                    if (BraketErr == 1)                     //Баланс дужок після знаку <-
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tToo much ')'!"));
                    }
                    if (BraketErr == 2)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[i].Line, "\t')' expected!"));
                    }
                    if (TokensTable[j - 2].Type != KeyWord.ltWhile) buf = IsExpression((j + 1), ef);
                    else buf = 0;
                    Err_num = Err_num + buf;
                }
                if (TokensTable[j].Type == KeyWord.ltWrite) //перевірка правильності виклику виводу на екран
                {
                    int buf, brak;
                    if (TokensTable[j + 1].Type != KeyWord.ltLBraket)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "tToo much ')'!"));
                    }
                    //buf = IsExpression((j+1),ef);
                    //Err_num=Err_num+buf;
                    brak = Balans(j, KeyWord.ltEndGroup, KeyWord.ltLBraket, KeyWord.ltRBraket);
                    if (brak == 1)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tToo much ')'!"));
                    }
                    if (brak == 2)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[i].Line, "\t')' expected!"));
                    }
                    if (TokensTable[j + 2].Type == KeyWord.ltQuotes)
                    {
                        //о
                        string tmp;
                        j = j + 2;
                        if (TokensTable[j + 1].Type == KeyWord.ltLetters)
                        {
                            tmp = "line" + NumberOfLetters.ToString();
                            LettersTable.Add(new Letters());
                            LettersTable[NumberOfLetters].Name = tmp;
                            LettersTable[NumberOfLetters].Text = TokensTable[j + 1].Text;
                            NumberOfLetters++;
                        }
                        if (TokensTable[j + 2].Type != KeyWord.ltQuotes)
                        {
                            Err_num++;
                            ef.WriteLine(ErrorPatern(Err_num, line, "\t\" expected!"));
                        }
                    }
                    if (TokensTable[j + 3].Type == KeyWord.ltIdentifier)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\t, expected!"));
                    }
                    if (TokensTable[j + 3].Type == KeyWord.ltComma)
                    {
                        j = j + 3;
                        if (TokensTable[j + 1].Type != KeyWord.ltIdentifier)
                        {
                            Err_num++;
                            ef.WriteLine(ErrorPatern(Err_num, TokensTable[j].Line, "\tIdentifier expected!"));
                        }
                    }
                }
                if (TokensTable[j].Type == KeyWord.ltRead)
                {
                    if (TokensTable[j + 1].Type != KeyWord.ltLBraket)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 1].Line, "\t'(' expected!"));
                    }
                    if (TokensTable[j + 2].Type != KeyWord.ltIdentifier)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 2].Line, "\tIdentifier expected!"));
                    }
                    if (TokensTable[j + 3].Type != KeyWord.ltRBraket)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 3].Line, "\t')' expected!"));
                    }
                    if (TokensTable[j + 4].Type != KeyWord.ltEndGroup)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 4].Line, "\t';' expected!"));
                    }
                }
                if (TokensTable[j].Type == KeyWord.ltWhile)               //перевірка WHILE
                {
                    if (TokensTable[j + 1].Type != KeyWord.ltLBraket)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 4].Line, "\'(\' after WHILE expected!"));
                    }
                    if (!(TokensTable[j + 2].Type == KeyWord.ltIdentifier || TokensTable[j+2].Type == KeyWord.ltNumber))
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 4].Line, "Identifier in WHILE () expected!"));
                    }
                    if(!(TokensTable[j+3].Type == KeyWord.ltNotEqu || TokensTable[j+3].Type == KeyWord.ltEqu || TokensTable[j+3].Type == KeyWord.ltLess || TokensTable[j+3].Type == KeyWord.ltGreate))
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 3].Line, "while support only operations: GE, LE, <>, ="));
                    }
                    if (!(TokensTable[j + 4].Type != KeyWord.ltIdentifier || TokensTable[j + 4].Type != KeyWord.ltNumber))
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 4].Line, "Identifier or number in WHILE () expected!"));
                    }
                    if (TokensTable[j + 5].Type != KeyWord.ltRBraket)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 5].Line, "\')\' after WHILE expected!"));
                    }
                    if(TokensTable[j+6].Type != KeyWord.ltBegin)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, TokensTable[j + 6].Line, "BEGIN after WHILE expected!"));
                    }
                    BraketErr = Balans(j+5, KeyWord.ltEndGroup, KeyWord.ltBegin, KeyWord.ltEnd);
                }
                if (TokensTable[j].Type == KeyWord.ltEOF) break;
            }

            List<CheckBeginEnd> checkingBE = new List<CheckBeginEnd>();
            // checking for Begin - end 
            for(i = 0; i < TokensTable.Count; i++)
            {
                if(TokensTable[i].Type == KeyWord.ltBegin || TokensTable[i].Type == KeyWord.ltEnd)
                {
                    checkingBE.Add(new CheckBeginEnd { KeyWord = TokensTable[i].Type, Position = i });
                }
                    
            }
            for(i = 0; i < checkingBE.Count; i++)
            {
                if(checkingBE[i].KeyWord == KeyWord.ltBegin && !checkingBE[i].Checked)
                {
                    // check if exists ltEnd for this token
                    var isExists = checkingBE.Any(item => item.KeyWord == KeyWord.ltEnd && !item.Checked);

                    if (!isExists)
                    {
                        Err_num++;
                        ef.WriteLine(ErrorPatern(Err_num, 0, "BEGIN - END pairs unmatched, or END precedes BEGIN"));
                    }
                    else
                    {
                        // checked this items
                        checkingBE[i].Checked = true;
                        checkingBE.Where(item => item.KeyWord == KeyWord.ltEnd && !item.Checked).FirstOrDefault().Checked = true;

                    }
                }
            }


            if (Err_num == 0) ; // implement
            ef.Close();
            return  new Tuple<int, List<Letters>, List<Identifier>>( Err_num, LettersTable, IdentTable);

        }

        public static void GenerateCode(StreamWriter f)
        {
            BeginASMFile(f);
            CheckPresentInputOutput(f);
            PrintData(f);
            BeginCodeSegm(f);
            PrintCode(f);
            PrintEnding(f);
        }

        public enum FileNames
        {
            SPZ_PrintAND,
            SPZ_PrintEQ,
            SPZ_PrintGE,
            SPZ_PrintLE,
            SPZ_PrintMOD,
            SPZ_PrintNOT,
            SPZ_PrintOR
        }

        public static void PrintEnding(StreamWriter f)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory;

            string FileName = string.Format("{0}Resources\\", Path.GetFullPath(Path.Combine(filePath, @"..\..\")));

            f.Write(";======================================\n");
            f.Write("MOV AH,4Ch\nINT 21h\n");
            if (IsPresentInput) { AsmFileFiller.PrintInput(f); }
            if (IsPresentOutput) { AsmFileFiller.PrintOutput(f); }

            AsmFileFiller.PrintMOD(f, FileName);
            AsmFileFiller.PrintAND(f, FileName);
            AsmFileFiller.PrintOR(f, FileName);
            AsmFileFiller.PrintNOT(f, FileName);
            AsmFileFiller.PrintEQ(f, FileName);
            AsmFileFiller.PrintGE(f, FileName);
            AsmFileFiller.PrintLE(f, FileName);
            f.Write("\n;======================================\n");
            f.Write("END START");
        }

        public static void BeginASMFile(StreamWriter f)
        {
            f.WriteLine("DOSSEG\n.MODEL SMALL\n.STACK 100h\n.486\n.DATA\n");
        }

        public static void CheckPresentInputOutput(StreamWriter f)
        {
            int i = 0;
            do
            {
                ++i;
            } while (TokensTable[i - 1].Type != KeyWord.ltBegin);
            for (; TokensTable[i].Type != KeyWord.ltEOF; ++i)
            {
                if (TokensTable[i].Type == KeyWord.ltRead) IsPresentInput = true;
                if (TokensTable[i].Type == KeyWord.ltWrite) IsPresentOutput = true;
                if (IsPresentInput && IsPresentOutput) break;
            }
        }

        public static void PrintData(StreamWriter f) // //Друк сегмету даних
        {
            if (IsPresentInput)
            {
                // f.WriteLine();
                f.WriteLine("MY_MUL MACRO X,Y,Z");
                f.WriteLine("\tmov z,0"); f.WriteLine("\tmov z+2,0"); f.WriteLine("\tmov ax,x");
                f.WriteLine("\tmul y"); f.WriteLine("\tmov z,ax"); f.WriteLine("\tmov z+2,dx");
                f.WriteLine("\tmov ax,x+2"); f.WriteLine("\tmul y"); f.WriteLine("\tadd z+2,ax");
                f.WriteLine("\tmov ax,z"); f.WriteLine("\tmov dx,z+2"); f.WriteLine("ENDM");
            }

            int i;
            f.WriteLine(";======Declaration of variables =======");

            for (i = 1; i < NumberOfIdent; ++i)
            {
                f.WriteLine($"\t{IdentTable[i].Name}\tdd\t0{IdentTable[i].Value}h");
            }
            f.WriteLine("\tlb1\tdw\t0h"); //Змінні для обробки логічних операцій
            f.WriteLine("\tlb2\tdw\t0h");
            f.WriteLine("\tbuf_if\tdw\t0h");
            f.WriteLine("\tbuf\tdd\t0\n\trc\tdw\t0");
            if (IsPresentInput)
            {
                f.WriteLine(";======Data for Get() functions========\n");
                f.WriteLine("\terFlag db 0");
                f.WriteLine("\tTStr db 10 dup (0)");
                f.WriteLine("\tTBin dw 0,0");
                f.WriteLine("\tMaxLen dw 0");
                f.WriteLine("\tFlagS db 0");
                f.WriteLine("\tMul10 dw 1,0");
                f.WriteLine("\tmy_z dw 0,0");
                f.WriteLine("\terStr1 db 13,10,'Data not input_variable',13,10,'$'");
                f.WriteLine("\terStr2 db 13,10,'Incorrectly data ',13,10,'$'");
                f.WriteLine("\terStr3 db 13,10,'Data is too long ',13,10,'$'");
            }
            if (IsPresentOutput)
            {
                f.WriteLine(";=======Data for Put() functions=======\n");
                f.WriteLine("\tMSign\tdb\t\'+\',\'$\'");
                f.WriteLine("\tX_Str\tdb\t12 dup (0)");
                f.WriteLine("\tten\tdw\t10");
                f.WriteLine("\tX1\tdw\t0h");
                f.WriteLine("\tMX1\tdb\t13,10,\'>> $\'");
                f.WriteLine("\tper\tdb\t10,13,\'$\'");
                for (i = 0; i < NumberOfLetters; i++)
                {
                    f.WriteLine($"\t{LettersTable[i].Name}\tdb\t\'{LettersTable[i].Text}\',\'$\'");
                };
            }

        }

        public static void BeginCodeSegm(StreamWriter f)
        {
            f.WriteLine(".CODE\nSTART:\n\tmov ax,@data\n\tmov ds,ax");
            f.WriteLine("\tfinit\n");
        }

        public static int labelnom { get; set; }

        public static void PrintCode(StreamWriter f)
        {
            int lab = 0;
            KeyWordToken l = new KeyWordToken();                               //Поточна лексема
            int i = 0;
            do
            {
                ++i;
            } while (TokensTable[i].Type != KeyWord.ltBegin);
            i++;
            if (TokensTable[i].Type == KeyWord.ltVar)
            {
                do
                {
                    i++;
                } while (TokensTable[i].Type != KeyWord.ltEndGroup);
                i++;
            }

            CucleStack = new Stack<int>();
            StartBlockStack = new Stack<int>();
            for (; ; ++i)
            {
                labelnom = 0;
                l.Type = TokensTable[i].Type;
                l.Name = TokensTable[i].Name;
                l.Value = TokensTable[i].Value;
                if (l.Type == KeyWord.ltEOF) break;
                if (l.Type == KeyWord.ltBegin)
                {
                    StartBlockStack.Push(++lab);
                }
                if ((l.Type == KeyWord.ltEnd) && (TokensTable[i + 1].Type != KeyWord.ltEOF))
                {
                    if (StartBlockStack.Peek() >= 0)
                    {
                        int temp = CucleStack.Pop();
                        //f.WriteLine($"\tmov ax, word ptr {TokensTable[temp].Name}");
                        //f.WriteLine("\tdec ax\n");
                        //f.WriteLine($"\tmov word ptr {TokensTable[temp].Name}, ax");
                        temp = StartBlockStack.Pop();
                        f.WriteLine($"\tjmp forStart{temp}");
                        f.WriteLine($"forFinish{temp}:");
                    }
                    else
                    {
                        StartBlockStack.Pop();
                    }
                }
                if (l.Type == KeyWord.ltWrite)    //write
                {
                    if (TokensTable[i + 2].Type == KeyWord.ltQuotes)
                    {
                        if (TokensTable[i + 3].Text.Length == 0)
                        {
                            f.WriteLine("\tlea dx,per");
                            f.WriteLine("\tmov ah,09\n\tint 21h");
                        }
                        else
                        {
                            if (LettersTable[CurrLet].Text.Contains("\\n"))
                            {
                                AsmFileFiller.PrintNewLine(f);
                            }
                            else
                            {
                                f.WriteLine($"\tlea dx,{LettersTable[CurrLet].Name}");
                                f.WriteLine("\tmov ah,09\n\tint 21h");
                            }
                        }
                        CurrLet++;
                        i += 4;
                        if (TokensTable[i + 1].Type == KeyWord.ltComma)
                        {
                            f.WriteLine($"\tfild {TokensTable[i + 2].Name}");
                            f.WriteLine("\tfistp buf");
                            f.WriteLine("\tcall output");
                            i += 4;
                        }
                        if (TokensTable[i + 1].Type == KeyWord.ltRBraket)
                        {
                            i += 2;
                        }
                    }
                    if (TokensTable[i + 2].Type == KeyWord.ltIdentifier)
                    {
                        i = ConvertToPostfixForm(i + 1);
                        GenASMCode("buf", f);
                        f.WriteLine("\tcall output", f);
                    }
                }
                if (l.Type == KeyWord.ltRead)
                {
                    f.WriteLine("\tcall input");
                    f.WriteLine("\tfild buf");
                    f.WriteLine($"\tfistp {TokensTable[i + 2].Name}");
                    i += 4;
                }
                if (l.Type == KeyWord.ltWhile)
                {
                    CucleStack.Push(i + 2);
                    if (TokensTable[i + 2].Type ==  KeyWord.ltNumber)
                    {
                        string buf = $"0{TokensTable[i + 2].Value:X4}";
                        f.Write($"\tmov word ptr buf,{buf}h\n");
                        f.Write("\tfild buf\n");
                    }
                    else
                    {
                        f.Write($"\tfild {TokensTable[i + 2].Name}\n");
                    }
                    f.Write($"\tfistp {TokensTable[i + 2].Name}\n");
                    f.Write($"forStart{lab + 1}:\n");
                    f.Write($"\tfild {TokensTable[i + 2].Name}\n");
                    if (TokensTable[i + 4].Type == KeyWord.ltNumber)
                    {
                        string buf = $"0{TokensTable[i + 4].Value:X4}";
                        f.Write($"\tmov word ptr buf,{buf}h\n");
                        f.Write("\tfild buf\n");
                    }
                    else
                    {
                        f.Write($"\tfild {TokensTable[i + 4].Name}\n");
                    }
                    switch (TokensTable[i + 3].Type)
                    {
                        case KeyWord.ltEqu:
                            f.Write("\tcall eq_\n");
                            break;
                        case KeyWord.ltNotEqu:
                            f.Write("\tcall eq_\n");
                            f.Write("\tcall ltNot\n");
                            break;
                        case KeyWord.ltGreate:
                            f.Write("\tcall ltGreate\n");
                            break;
                        case KeyWord.ltLess:
                            f.Write("\tcall ltLess\n");
                            break;
                        default:
                            throw new ArgumentException($"No correct type {TokensTable[i + 3].Type}");
                    }
                    f.Write("\tfistp buf\n");
                    f.Write("\tmov ax,word ptr buf\n");
                    f.Write("\tcmp ax,0\n");
                    f.Write($"\tjz forFinish{lab + 1}\n");
                }
                if ((l.Type == KeyWord.ltNewValue))
                {
                    int bufi;
                    bufi = i;
                    i = ConvertToPostfixForm(i + 1);//Генерація постфіксного виразу
                    if (i < 0)
                    {
                        i = -i;
                        //
                        continue;
                    }           //Генерація асемблерного коду з постфіксного виразу
                    GenASMCode(TokensTable[bufi - 1].Name, f);
                }
            }
        }

        public static void GenASMCode(string str, StreamWriter f)
        {
            int n;
            for (n = 0; BufExprPostfixForm[n] != 3000; ++n)
            {
                StackStack.Clear();
                //puts("pf");
                if ((!IsOperation(TokensTable[BufExprPostfixForm[n]].Type)) && (TokensTable[BufExprPostfixForm[n]].Type != KeyWord.ltNot))
                {
                    if (TokensTable[BufExprPostfixForm[n]].Type == KeyWord.ltIdentifier)
                    {
                        f.Write($"\tfild { TokensTable[BufExprPostfixForm[n]].Name}\n");
                    }
                    else if (TokensTable[BufExprPostfixForm[n]].Type == KeyWord.ltNumber)
                    {
                        string buf = "0";
                        buf += String.Format("{0:X}", TokensTable[BufExprPostfixForm[n]].Value);
                        f.Write($"\tmov word ptr buf,{buf}h\n", buf);
                        f.Write("\tfild buf\n");
                    }
                    else if ((TokensTable[BufExprPostfixForm[n]].Type == KeyWord.ltLBraket) || (TokensTable[BufExprPostfixForm[n]].Type == KeyWord.ltRBraket))
                    {
                        continue;
                    }
                }
                else
                {
                    switch (TokensTable[BufExprPostfixForm[n]].Type)
                    {
                        case KeyWord.ltAdd: f.Write("\tfadd\n"); break;
                        case KeyWord.ltSub: f.Write("\tfsub\n"); break;
                        case KeyWord.ltDiv: f.Write("\tfdiv\n"); break;
                        case KeyWord.ltMod: f.Write("\tcall mod_\n"); break;
                        case KeyWord.ltMul: f.Write("\tfmul\n"); break;
                        case KeyWord.ltAnd: f.Write("\tcall ltAnd\n"); break;
                        case KeyWord.ltOr: f.Write("\tcall ltOr\n"); break;
                        case KeyWord.ltNot: f.Write("\tcall ltNot\n"); break;
                        case KeyWord.ltEqu: f.Write("\tcall eq_\n"); break;
                        case KeyWord.ltNotEqu:
                            f.Write("\tcall eq_\n");
                            f.Write("\tcall ltNot\n"); break;
                        case KeyWord.ltLess: f.Write("\tcall ltLess\n"); break;
                        case KeyWord.ltGreate: f.Write("\tcall ltGreate\n"); break;
                    }
                }
            }
            f.Write($"\tfistp {str}\n");
        }

        public static int ConvertToPostfixForm(int i) //Формує в масиві послідовність номерів лексем яка відповідає постфіксній формі
        {
            StackStack = new Stack<int>();
            int n, z;
            n = 0;
            for (; (TokensTable[i + n].Type != KeyWord.ltEndGroup); ++n) ;      //Встановлення коректності та довжини вхідного масиву
            int k;
            k = i + n;
            for (z = 0; i < k; ++i)
            {
                KeyWord inVar;
                inVar = TokensTable[i].Type;
                if ((inVar == KeyWord.ltIdentifier) || (inVar == KeyWord.ltNumber) || (inVar == KeyWord.ltNot))
                {
                    BufExprPostfixForm[z] = i;
                    ++z;
                }
                else if (IsOperation(inVar))
                {
                    if (StackStack.Count == 0 || Prioritet(inVar, StackStack))
                    {
                        StackStack.Push(i);
                    }
                    else
                    {
                        if (TokensTable[StackStack.Peek()].Type != KeyWord.ltLBraket)
                        {
                            do
                            {
                                BufExprPostfixForm[z] = StackStack.Pop();
                                ++z;
                            } while ((!(Prioritet(inVar, StackStack))) && (!(StackStack.Count == 0) && (TokensTable[StackStack.Peek()].Type != KeyWord.ltLBraket)));
                        }
                        StackStack.Push(i);
                    }
                }
                if (inVar == KeyWord.ltLBraket)
                {
                    StackStack.Push(i);
                    BufExprPostfixForm[z] = i;
                    ++z;
                }
                if (inVar == KeyWord.ltRBraket)
                {
                    for (; (TokensTable[StackStack.Peek()].Type != KeyWord.ltLBraket);)
                    {
                        BufExprPostfixForm[z] = StackStack.Pop();  //stackStack.S.st[stackStack.S.top];
                        ++z;
                    }
                    StackStack.Pop();
                    BufExprPostfixForm[z] = i;
                    ++z;
                }
            }
            for (; !(StackStack.Count == 0);)
            {
                BufExprPostfixForm[z] = StackStack.Pop();
                ++z;
            }
            BufExprPostfixForm[z] = 3000;
            z++;
            return k;
        }

        public static bool Prioritet(KeyWord t, Stack<int> stackStack)
        {
            bool r = false;
            r = (
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAdd)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltSub)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltOr)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAnd)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltEqu)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltNotEqu)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltLess)) ||
                ((t == KeyWord.ltDiv) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltGreate)) ||

                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAdd)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltSub)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltOr)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAnd)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltEqu)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltNotEqu)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltLess)) ||
                ((t == KeyWord.ltMul) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltGreate)) ||

                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAdd)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltSub)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltOr)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltAnd)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltEqu)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltNotEqu)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltLess)) ||
                ((t == KeyWord.ltMod) && (TokensTable[stackStack.Peek()].Type == KeyWord.ltGreate))
                );
            return r;
        }
        #endregion

        static Analize()
        {
            CurrLet = 0;
            TokensTable = new List<KeyWordToken>();
        }

        public static void CheckPresentInputOutput()
        {
            int i = 0;
            do
            {
                ++i;
            } while (TokensTable[i - 1].Type != KeyWord.ltBegin);

            for (; TokensTable[i - 1].Type != KeyWord.ltEOF; ++i)
            {
                if (TokensTable[i].Type == KeyWord.ltRead) IsPresentInput = true;
                if (TokensTable[i].Type == KeyWord.ltWrite) IsPresentOutput = true;
                if (IsPresentInput && IsPresentOutput) break;
            }
        }


    }

    public class CheckBeginEnd
    {
        public KeyWord KeyWord { get; set; }
        public int Position { get; set; }

        public bool Checked { get; set; } = false;
    }
}

