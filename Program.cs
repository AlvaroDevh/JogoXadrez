using board;
using JogoXadrez;
using JogoXadrez.board;
using JogoXadrez.Parts;
using System.Reflection.Metadata.Ecma335;

namespace Program {

    public class Xadrez {

        static void Main(string[] args) {

            try {

                Match match = new Match();

                while (!match.Finished) {

                    try { 
                        Console.Clear();
                        Screen.printMatch(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Console.ResetColor();

                        Position origin = Screen.readPosition().toPosition();
                        match.validetePositionOrigin(origin);

                        bool[,] possiblePositions = match.Tab.Part(origin).PossibleMovements();
                        Console.Clear();
                        Screen.PrintBoard(match.Tab, possiblePositions);


                        Console.Write("Destino: ");
                        Position destiny = Screen.readPosition().toPosition();
                        match.validatePostionDestiny(origin, destiny);


                        match.makePlayer(origin, destiny);
                    }

                    catch(TabuleiroException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.printMatch(match);

            }

            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}