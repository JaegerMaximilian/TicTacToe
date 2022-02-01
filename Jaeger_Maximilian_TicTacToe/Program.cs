using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaeger_Maximilian_TicTacToe
{
    class Program
    {
        static int[,] Move(bool Player, int[,] Feld)
        {
            int row;
            int col;
            do
            {
                row = AskInputAndConvert("Reihe");
                col = AskInputAndConvert("Spalte");

            } while (Feld[row-1,col-1] == 3 || Feld[row-1,col-1] == 5);
            if (Player) Feld[row-1, col-1] = 3;
            else Feld[row-1, col-1] = 5;
            PrintFeld(Feld);
            return Feld;
        }

        static int AskInputAndConvert(string Aufforderung)
        {
            int Output = 0;
            do
            {
                Console.WriteLine(Aufforderung);
            } while (!int.TryParse(Console.ReadLine(), out Output) || Output > 3 || (Output < 1 && Output != 0)) ; //hier noch weitermachen bedingungen
            return Output;
        }

        static void PrintFeld(int[,] Feld)
        {
            Console.Write("\n");
            for (int row = 0; row < Feld.GetLength(0); row++)
            {
                for (int col = 0; col < Feld.GetLength(1); col++)
                {
                    int Zelle = Feld[row, col];
                    if (Zelle == 3) Console.Write("X");
                    else if (Zelle == 5) Console.Write("O");
                    else Console.Write("-");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
        static bool GameWon(int[,] Feld, out bool NoMoreMoves)
        {
            NoMoreMoves = false;
            int diagonalSum1 = 0;
            int diagonalSum2 = 0;
            int CheckIfFull = 0; //für Untenschieden?
            for (int row = 0; row < Feld.GetLength(0); row++)
            {
                int rowSum = 0;
                int colSum = 0;
                for (int col = 0; col < Feld.GetLength(1); col++)
                {
                    if (Feld[row,col] != 0) CheckIfFull += 1;

                    rowSum += Feld[row, col];
                    colSum += Feld[col, row];
                    if (row == col) diagonalSum1 += Feld[row, col];
                    if ((row == 0 && col == 2) || (row == col && row == 1) || (row == 2 && col == 0)) 
                        diagonalSum2 += Feld[row, col];
                   
                }
                if (rowSum == 9 || colSum == 9 || rowSum == 15 || colSum == 15) return true;
            }
            if (diagonalSum1 == 9 || diagonalSum2 == 9 || diagonalSum1 == 15 || diagonalSum2 == 15) return true;



            if (CheckIfFull == 9)
            {
                NoMoreMoves = true; //if no more moves are possible
                return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            bool NoMoreMoves;
            bool Player = true;

            int[,] Feld = new int[3, 3];

            do
            {
                if (Player) //bool ob erster oder zweiter Player
                {
                    Console.WriteLine("Spieler 1, du bist dran!");
                    Feld = Move(Player, Feld); 
                }
                else
                {
                    Console.WriteLine("Spieler 2, du bist dran!");
                    Feld = Move(Player, Feld);
                } 
                    
                Player = !Player;
            } while (!GameWon(Feld, out NoMoreMoves)); //hört nicht gscheid auf
            if (!NoMoreMoves)
            {
                if (Player) Console.WriteLine($"Herzlichen Glückwunsch, Spieler 2, du hast gewonnen!");
                else Console.WriteLine($"Herzlichen Glückwunsch, Spieler 1, du hast gewonnen!");
            }
        }
    }
}
