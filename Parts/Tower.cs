using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.Parts {
    internal class Tower : Part {

        public Tower (Tabuleiro tab, Color color) : base(tab, color) {

        }

        public override string ToString() {
            return "T";
        }

        private bool canMove(Position pos) {
            Part p = Tab.Part(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements() {

            bool[,] mat = new bool[Tab.lines, Tab.columns];
            Position pos = new Position(0, 0);

          

            // acima
            pos.setValue(Position.Line - 1, Position.Column);
            while(Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.Line = pos.Line - 1;
            }


            // abaixo
            pos.setValue(Position.Line + 1, Position.Column);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            // direita
            pos.setValue(Position.Line, Position.Column +1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.setValue(Position.Line, Position.Column - 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.Column = pos.Column - 1;
            }




            return mat;
        }
    }
}
