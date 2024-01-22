using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board {
    abstract class Part {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int quantityMovement { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Part(Tabuleiro tab, Color color) {
            Position = null;
            Tab = tab;
            Color = color;
            this.quantityMovement = 0;
        }

        public void Moviments() {
            quantityMovement++;
        }

        public void decrementMoviments() {
            quantityMovement--;
        }

        public bool testMovimentes() {
            bool[,] mat = PossibleMovements();

            for(int i =0; i < Tab.lines; i++) {
                for(int j = 0; j < Tab.columns; j++) {

                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;

        }

        public bool movimentStop(Position pos) {
            return PossibleMovements()[pos.Line, pos.Column];
        }
        public abstract bool[,] PossibleMovements();
    }

}
