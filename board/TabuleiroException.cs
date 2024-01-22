using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.board {
     class TabuleiroException : Exception {

        public TabuleiroException (string msg)  : base (msg) {
        
        }
    }
}
