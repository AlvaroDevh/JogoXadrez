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
    class Pawn : Part {

        private JogoXadrez.board.Match match;

        public Pawn(Tabuleiro tab, Color cor, JogoXadrez.board.Match match) : base(tab, cor) {
            this.match = match;
        }

        public override string ToString() {
            return "P";
        }

        private bool existOponent(Position pos) {
            Part p = Tab.Part(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos) {
            return Tab.Part(pos) == null;
        }

        public override bool[,] PossibleMovements() {

            bool[,] mat = new bool[Tab.lines, Tab.columns];
            Position pos = new Position(0, 0);

            if (Color == Color.Branca) {

                pos.setValue(Position.Line - 1, Position.Column);
                if (Tab.validPosition(pos) && Free(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line - 2, Position.Column);
                Position P2 = new Position(Position.Line - 1, Position.Column);
                if (Tab.validPosition(P2) && Free(P2) && Tab.validPosition(pos)
                    && Free(pos) && quantityMovement == 0) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line - 1, Position.Column - 1);
                if (Tab.validPosition(pos) && existOponent(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line - 1, Position.Column + 1);
                if (Tab.validPosition(pos) && existOponent(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                //# En Passant 

                if (Position.Line == 3) {
                    Position left = new Position(Position.Line, Position.Column - 1);

                    if (Tab.validPosition(left) && existOponent(left) && Tab.Part(left) == match.vunerableEnPassant) {
                        mat[left.Line -1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Tab.validPosition(right) && existOponent(right) && Tab.Part(right) == match.vunerableEnPassant) {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else {

                pos.setValue(Position.Line + 1, Position.Column);
                if (Tab.validPosition(pos) && Free(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line + 2, Position.Column);
                Position P3 = new Position(Position.Line + 1, Position.Column);
                if (Tab.validPosition(P3) && Free(P3) && Tab.validPosition(pos)
                    && Free(pos) && quantityMovement == 0) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line + 1, Position.Column - 1);
                if (Tab.validPosition(pos) && existOponent(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setValue(Position.Line + 1, Position.Column + 1);
                if (Tab.validPosition(pos) && existOponent(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                //# En Passant 

                if (Position.Line == 4) {
                    Position left = new Position(Position.Line, Position.Column + 1);

                    if (Tab.validPosition(left) && existOponent(left) && Tab.Part(left) == match.vunerableEnPassant) {
                        mat[left.Line +1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Tab.validPosition(right) && existOponent(right) && Tab.Part(right) == match.vunerableEnPassant) {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

    }

}