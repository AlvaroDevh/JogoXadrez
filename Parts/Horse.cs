using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JogoXadrez.Parts {
    class Horse : Part {

        public Horse(Tabuleiro tab, Color color) : base(tab, color) {

        }


        public override string ToString() {
            return "C";
        }

        private bool canMove(Position pos) {
            Part p = Tab.Part(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements() {

            bool[,] mat = new bool[Tab.lines, Tab.columns];
            Position pos = new Position(0, 0);

           
            pos.setValue(Position.Line - 1, Position.Column -2);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line - 2, Position.Column - 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line - 2, Position.Column + 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line - 1, Position.Column + 2);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line + 1, Position.Column + 2);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line + 2, Position.Column + 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line + 2, Position.Column - 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setValue(Position.Line + 1, Position.Column - 2);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            return mat;
        }
    }
}
