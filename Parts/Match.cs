using board;
using JogoXadrez.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JogoXadrez.board {
    class Match {

        public Tabuleiro Tab { get; private set; }
        public int Rounds { get; private set; }
        public Color currentePlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Part> setParts;
        private HashSet<Part> setCaptureds;
        public bool Check { get; private set; }


        public Match() {
            Tab = new Tabuleiro(8, 8);
            Rounds = 1;
            currentePlayer = Color.Branca;
            Finished = false;
            Check = false;
            setParts = new HashSet<Part>();
            setCaptureds = new HashSet<Part>();
            placeParts();
        }

        public Part Moviment(Position origin, Position destiny) {

            Part p = Tab.removePart(origin);
            p.Moviments();
            Part caputredPart = Tab.removePart(destiny);
            Tab.addPart(p, destiny);

            if (caputredPart != null) {
                setCaptureds.Add(caputredPart);
            }

            //#jogada roque pequeno
            if (p is King && destiny.Column == origin.Column + 2) {
                Position oringTower = new Position(origin.Line, origin.Column + 3);
                Position destinyTower = new Position(origin.Line, origin.Column + 1);
                Part T = Tab.removePart(oringTower);
                T.Moviments();
                Tab.addPart(T, destinyTower);
            }


            //#jogada roque grande
            if (p is King && destiny.Column == origin.Column - 2) {
                Position oringTower = new Position(origin.Line, origin.Column - 4);
                Position destinyTower = new Position(origin.Line, origin.Column - 1);
                Part T = Tab.removePart(oringTower);
                T.Moviments();
                Tab.addPart(T, destinyTower);
            }

            return caputredPart;
        }

        public void undoMovement(Position origin, Position destiny, Part caputredPart) {
            Part p = Tab.removePart(destiny);
            p.decrementMoviments();

            if (caputredPart != null) {
                Tab.addPart(caputredPart, destiny);
                setCaptureds.Remove(caputredPart);
            }
            Tab.addPart(p, origin);

            //#jogada roque pequeno
            if (p is King && destiny.Column == origin.Column + 2) {
                Position oringTower = new Position(origin.Line, origin.Column + 3);
                Position destinyTower = new Position(origin.Line, origin.Column + 1);
                Part T = Tab.removePart(oringTower);
                T.decrementMoviments();
                Tab.addPart(T, oringTower);
            }

            //#jogada roque pequeno
            if (p is King && destiny.Column == origin.Column + 2) {
                Position oringTower = new Position(origin.Line, origin.Column + 3);
                Position destinyTower = new Position(origin.Line, origin.Column + 1);
                Part T = Tab.removePart(destinyTower);
                T.decrementMoviments();
                Tab.addPart(T, oringTower);
            }

            //#jogada roque grande
            if (p is King && destiny.Column == origin.Column - 2) {
                Position oringTower = new Position(origin.Line, origin.Column - 4);
                Position destinyTower = new Position(origin.Line, origin.Column - 1);
                Part T = Tab.removePart(destinyTower);
                T.decrementMoviments();
                Tab.addPart(T, oringTower);
            }
        }

        public void makePlayer(Position origin, Position destiny) {
            Part caputredPart = Moviment(origin, destiny);
            if (isInCheck(currentePlayer)) {

                undoMovement(origin, destiny, caputredPart);
                throw new TabuleiroException("Jogada Inválida, o rei ficaria em xeque.");
            }

            if (isInCheck(Oponent(currentePlayer))) {
                Check = true;
            }

            else {
                Check = false;
            }

            if (testCheckMate(Oponent(currentePlayer))) {
                Finished = true;
            }

            else {
                Rounds++;
                changePlayer();
            }
        }

        public void validetePositionOrigin(Position pos) {

            if (Tab.Part(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição escolhida");
            }

            if (currentePlayer != Tab.Part(pos).Color) {
                throw new TabuleiroException("Peça da cor errada");
            }

            if (!Tab.Part(pos).testMovimentes()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça");
            }

        }

        public void validatePostionDestiny(Position origin, Position destiny) {

            if (!Tab.Part(origin).movimentStop(destiny)) {
                throw new TabuleiroException("Posição de destino Inválida");
            }
        }

        private void changePlayer() {
            if (currentePlayer == Color.Amarela) {
                currentePlayer = Color.Branca;
            }

            else {
                currentePlayer = Color.Amarela;
            }

        }

        public HashSet<Part> partsCaptureds(Color color) {
            HashSet<Part> aux = new HashSet<Part>();
            foreach (Part x in setCaptureds) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Part> partsInGame(Color color) {
            HashSet<Part> aux = new HashSet<Part>();
            foreach (Part x in setParts) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(partsCaptureds(color));
            return aux;
        }

        private Color Oponent(Color color) {
            if (color == Color.Branca) {
                return Color.Amarela;
            }
            else {
                return Color.Branca;
            }
        }

        private Part kingPart(Color color) {
            foreach (Part x in partsInGame(color)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color) {

            Part K = kingPart(color);

            foreach (Part x in partsInGame(Oponent(color))) {
                bool[,] mat = x.PossibleMovements();

                if (mat[K.Position.Line, K.Position.Column]) {
                    return true;
                }
            }
            return false;
        }


        public bool testCheckMate(Color color) {

            if (!isInCheck(color)) {
                return false;
            }

            foreach (Part x in partsInGame(color)) {
                bool[,] mat = x.PossibleMovements();

                for (int i = 0; i < Tab.lines; i++) {
                    for (int j = 0; j < Tab.columns; j++) {

                        if (mat[i, j]) {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Part partCaptured = Moviment(origin, destiny);
                            bool testCheck = isInCheck(color);
                            undoMovement(origin, destiny, partCaptured);
                            if (!testCheck) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void addNewParts(char column, int line, Part part) {
            Tab.addPart(part, new PositionXadrez(column, line).toPosition());
            setParts.Add(part);
        }

        private void placeParts() {

            //branca
            addNewParts('a', 1, (new Tower(Tab, Color.Branca)));
            addNewParts('b', 1, (new Horse(Tab, Color.Branca)));
            addNewParts('c', 1, (new Bishop(Tab, Color.Branca)));
            addNewParts('d', 1, (new Queen(Tab, Color.Branca)));
            addNewParts('e', 1, (new King(Tab, Color.Branca, this)));
            addNewParts('f', 1, (new Bishop(Tab, Color.Branca)));
            addNewParts('g', 1, (new Horse(Tab, Color.Branca)));
            addNewParts('h', 1, (new Tower(Tab, Color.Branca)));
            addNewParts('a', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('b', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('c', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('d', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('e', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('f', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('g', 2, (new Pawn(Tab, Color.Branca)));
            addNewParts('h', 2, (new Pawn(Tab, Color.Branca)));


            //preta
            addNewParts('a', 8, (new Tower(Tab, Color.Amarela)));
            addNewParts('b', 8, (new Horse(Tab, Color.Amarela)));
            addNewParts('c', 8, (new Bishop(Tab, Color.Amarela)));
            addNewParts('d', 8, (new Queen(Tab, Color.Amarela)));
            addNewParts('e', 8, (new King(Tab, Color.Amarela, this)));
            addNewParts('f', 8, (new Bishop(Tab, Color.Amarela)));
            addNewParts('g', 8, (new Horse(Tab, Color.Amarela)));
            addNewParts('h', 8, (new Tower(Tab, Color.Amarela)));
            addNewParts('a', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('b', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('c', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('d', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('e', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('f', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('g', 7, (new Pawn(Tab, Color.Amarela)));
            addNewParts('h', 7, (new Pawn(Tab, Color.Amarela)));








        }
    }
}
