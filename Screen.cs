using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using board;
using JogoXadrez.Parts;
using JogoXadrez;
using JogoXadrez.board;

namespace JogoXadrez {
    class Screen {

        public static void printMatch(Match match) {

            PrintBoard(match.Tab);
            Console.WriteLine();
            partsCaptureds(match);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            if (!match.Finished) {
                Console.WriteLine("Jogadas: " + match.Rounds);
                Console.WriteLine("Vez do jogador " + match.currentePlayer);
                if (match.Check) {
                    Console.WriteLine("XEQUE!");
                }
            }

            else {
                Console.WriteLine("XEQUE-MATE");
                Console.WriteLine($"A peça {match.currentePlayer} ganhou o jogo");
            }
        }


        public static void partsCaptureds(Match match) {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            printConjunt(match.partsCaptureds(Color.Branca));
            Console.WriteLine();
            Console.Write("Amarelas: ");
            printConjunt(match.partsCaptureds(Color.Amarela));
            Console.WriteLine();
        }

        public static void printConjunt(HashSet<Part> conjunt) {

            Console.Write("[");
            foreach (Part x in conjunt) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void PrintBoard(Tabuleiro tab) {
            for (int i = 0; i < tab.lines; i++) {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.columns; j++) {
                    printPart(tab.Part(i, j));

                }
                Console.WriteLine();
            }
            string letras = "   A B C D E F G H ";
            Console.WriteLine(letras);



        }


        public static void PrintBoard(Tabuleiro tab, bool[,] possiblePositions) {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkBlue;



            for (int i = 0; i < tab.lines; i++) {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.columns; j++) {

                    if (possiblePositions[i, j]) {

                        Console.BackgroundColor = changedBackground;
                    }

                    else {

                        Console.BackgroundColor = originalBackground;
                    }

                    printPart(tab.Part(i, j));
                    Console.BackgroundColor = originalBackground;


                }
                Console.WriteLine();
            }
            string letras = "   A B C D E F G H ";
            Console.WriteLine(letras);
            Console.BackgroundColor = originalBackground;
        }



        public static void printPart(Part part) {

            if (part == null) {
                Console.Write("-");
            }

            else {
                if (part.Color == Color.Branca) {
                    Console.Write(part);
                }



                else if (part.Color == Color.Amarela) {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(part);
                    Console.ForegroundColor = aux;
                }
            }
            Console.Write(" ");
        }

        public static PositionXadrez readPosition() {

            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + " ");
            return new PositionXadrez(column, line);
        }
    }
}

