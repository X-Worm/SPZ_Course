using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPZ_GUI.ASM
{
   public class KeyWordToken
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public KeyWord Type { get; set; }
        public int Line { get; set; }
        public string Text { get; set; }
    }
}
