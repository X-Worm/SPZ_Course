using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_Course_Test.Enum
{
    enum KeyWord
    {
        ltProgram = 0,  // program
        ltVar,      // var
        ltType,     // int32
        ltBegin,        // start
        ltEnd,      // finish
        ltRead,     // get
        ltWrite,        // put
        ltFor,      // For
        ltDownTo,   // DownTo
        ltNewValue, // <<
        ltAdd,      // +
        ltSub,      // -
        ltMul,      // mul
        ltDiv,      // div
        ltMod,      // mod
        ltEqu,      // ==
        ltNotEqu,       // !=
        ltLess,     // <
        ltGreate,       // >
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
