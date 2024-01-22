using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.Parts {
    class Queen : Part {

        public Queen(Tabuleiro tab, Color color) : base(tab, color) {

        }


        public override string ToString() {
            return "D";
        }

        private bool canMove(Position pos) {
            Part p = Tab.Part(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements() {

            bool[,] mat = new bool[Tab.lines, Tab.columns];
            Position pos = new Position(0, 0);

            // NOROESTE
            pos.setValue(Position.Line - 1, Position.Column - 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line - 1, pos.Column - 1);
            }

            // NORDESTE

            pos.setValue(Position.Line - 1, Position.Column + 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line - 1, pos.Column + 1);
            }


            // SUDOESTE
            pos.setValue(Position.Line + 1, Position.Column + 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line + 1, pos.Column + 1);
            }



            // SUDESTE
            pos.setValue(Position.Line + 1, Position.Column - 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line + 1, pos.Column - 1);
            }


            // acima
            pos.setValue(Position.Line - 1, Position.Column);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line - 1, pos.Column);
            }


            // abaixo
            pos.setValue(Position.Line + 1, Position.Column);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line + 1, pos.Column);
            }

            // direita
            pos.setValue(Position.Line, Position.Column + 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line, pos.Column + 1);
            }

            // esquerda
            pos.setValue(Position.Line, Position.Column - 1);
            while (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;

                if (Tab.Part(pos) != null && Tab.Part(pos).Color != Color) {
                    break;
                }
                pos.setValue(pos.Line, pos.Column - 1);
            }




            return mat;
        }
    }
}
