using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_GUI.ASM
{
    public enum KeyWord
    {
        ltProgram = 0,  // PROGRAM
        ltVar,      // VAR
        ltType,     // LONGINT
        ltBegin,        // BEGIN
        ltEnd,      // END
        ltRead,     // GET
        ltWrite,        // PUT
        ltWhile,      // WHILE
        ltNewValue, // <-
        ltAdd,      // +
        ltSub,      // -
        ltMul,      // *
        ltDiv,      // DIV
        ltMod,      // MOD
        ltEqu,      // =
        ltNotEqu,       // <>
        ltLess,     // LE
        ltGreate,       // GE
        ltNot,      // !!
        ltAnd,      // &&
        ltOr,       // ||
        ltEOF,      // EOF
        ltEndGroup, // ;
        ltComma,        // ,
        ltIdentifier,   // Identificator
        ltNumber,       // Number
        ltLBraket,      // (
        ltRBraket,      // )
        ltQuotes,       // "
        ltLetters,      // "text"
        ltUnknown       //	Unkown
    }
}
