
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using board;
using JogoXadrez.board;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JogoXadrez.Parts {
     class King : Part {


        private JogoXadrez.board.Match match;

        public King (Tabuleiro tab, Color color, JogoXadrez.board.Match match) : base (tab, color) {
            this.match = match;
        }


        public override string ToString() {
            return "R";
        }
        private bool canMove(Position pos) {
            Part p = Tab.Part(pos);
            return p == null || p.Color != Color;
        }
        private bool testRoqueTower (Position pos) {
            Part p = Tab.Part(pos);
            return p != null && p is Tower && p.Color == Color && p.quantityMovement == 0;
        }

    

        public override bool[,] PossibleMovements() {

            bool[,] mat = new bool[Tab.lines, Tab.columns];
            Position pos = new Position(0, 0);

            //acima
            pos.setValue(Position.Line - 1, Position.Column);
            if(Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //nordeste
            pos.setValue(Position.Line - 1, Position.Column +1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //direita
            pos.setValue(Position.Line, Position.Column + 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //esquerda
            pos.setValue(Position.Line, Position.Column - 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //baixo
            pos.setValue(Position.Line +1, Position.Column);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //sudeste
            pos.setValue(Position.Line + 1, Position.Column + 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //sudoeste
            pos.setValue(Position.Line + 1, Position.Column - 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //noroeste
            pos.setValue(Position.Line - 1, Position.Column - 1);
            if (Tab.validPosition(pos) && canMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // #jogada Roque

            if(quantityMovement == 0 && !match.Check) {

                // Roque pequeno

                Position posTower1 = new Position(Position.Line, Position.Column + 3);

                if (testRoqueTower(posTower1)) {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if(Tab.Part(p1) == null && Tab.Part(p2) == null) {
                        mat[pos.Line, pos.Column + 2] = true;
                    }

                }

                // Roque pequeno grande

                Position posTower2 = new Position(Position.Line, Position.Column - 4);

                if (testRoqueTower(posTower2)) {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);


                    if (Tab.Part(p1) == null && Tab.Part(p2) == null && Tab.Part(p3) == null) {
                        mat[pos.Line, pos.Column - 2] = true;
                    }

                }
            }

            return mat; 
        }

    }
}
