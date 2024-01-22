using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.Parts {
     class PositionXadrez {

        public char Column { get; set; }
        public int Line { get; set; }

        public PositionXadrez(char column, int line) {
            Column = column;
            Line = line;
        }


        public Position toPosition() {

            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString() {
            return " " + Column + Line;
        }
    }
}
