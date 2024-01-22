using JogoXadrez.board;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace board {
      class Tabuleiro {

        public int lines { get; set; }
        public int columns { get; set; }

        private Part [,] parts;

        public Tabuleiro(int lines, int columns) {
            this.lines = lines;
            this.columns = columns;
            parts = new Part[lines, columns];
        }

        public Part Part (int lines, int columns) {

            return parts[lines, columns];
        }

        public Part Part(Position pos) {

            return parts[pos.Line, pos.Column];
        }

        public void addPart(Part p, Position pos) {

            if (existPart(pos)) {
                throw new TabuleiroException("Posição já preenchida");
            }
            else {
                parts[pos.Line, pos.Column] = p;
                p.Position = pos;
            }
        }

        public Part removePart(Position pos) {
            if(Part(pos) == null) {
                return null;
            }

            Part aux = Part(pos);
            parts[pos.Line, pos.Column] = null;
            return aux;

        }

        public bool validPosition(Position pos) {

            if(pos.Line < 0 || pos.Line > 7 || pos.Column < 0 || pos.Column > 7) {
                return false;
            }

                return true;
       

        }

        public void validatePosition(Position pos) {

            if( !validPosition(pos) ) {
                throw new TabuleiroException("Posição Inválida"); 
            }

        }

        public bool existPart(Position pos) {
            validatePosition(pos);
            return Part(pos) != null;
        }
    }
}
