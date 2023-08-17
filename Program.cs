using System;
using System.ComponentModel;
using TicTacToe;

new TicTacToeGame().Run();

namespace TicTacToe
{
    internal class TicTacToeGame
    {
        public void Run()
        {
            BoardRenderer boardRenderer = new BoardRenderer();
            Board board = new Board();
            Player playerOne = new Player(Cell.X);
            Player playerTwo = new Player(Cell.O);
            int numberOfTurns = 0;
            
            Player currentPlayer = playerOne;
            while (numberOfTurns < 9)
            {
                Console.Clear();
                boardRenderer.DrawBoard(board);
                //Player one turn
                Console.WriteLine($"{currentPlayer.Symbol} please select desired Square");
                Square square = currentPlayer.PickSquare(board);
                board.FillCell(square.Row, square.Column, currentPlayer.Symbol);
                boardRenderer.DrawBoard(board);
                if (HasWon(board, currentPlayer.Symbol))
                {
                    Console.WriteLine($"{currentPlayer.Symbol} has won!");
                    return;
                }

                currentPlayer = currentPlayer == playerOne ? playerTwo : playerOne;
                numberOfTurns++;
            }
            Console.WriteLine("Draw!");
        }

        private bool HasWon(Board board, Cell value)
        {
            //check rows
            if(board.GetState(0, 0) == value && board.GetState(0, 1) == value && board.GetState(0, 2) == value) return true;
            if(board.GetState(1, 0) == value && board.GetState(1, 1) == value && board.GetState(1, 2) == value) return true;
            if(board.GetState(2, 0) == value && board.GetState(2, 1) == value && board.GetState(2, 2) == value) return true;
            
            //check columns
            if(board.GetState(0, 0) == value && board.GetState(1, 1) == value && board.GetState(2, 1) == value) return true;
            if(board.GetState(0, 1) == value && board.GetState(1, 1) == value && board.GetState(2, 1) == value) return true;
            if(board.GetState(0, 2) == value && board.GetState(1, 2) == value && board.GetState(2, 2) == value) return true;
            //check diagonals
            if (board.GetState(0, 0) == value && board.GetState(1, 1) == value && board.GetState(2, 2) == value) return true;
            if (board.GetState(2, 0) == value && board.GetState(1, 1) == value && board.GetState(0, 2) == value) return true;


            return false;
        }
    }

    public class Player
    {
        public Cell Symbol { get; }

        public Player(Cell symbol)
        {
            Symbol = symbol;
        }

        public Square PickSquare(Board board)
        {
            while (true)
            {
                Console.WriteLine("What square do you want to pick?");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                
                Square choice = key switch
                {
                    ConsoleKey.D1 => new Square(0, 0),
                    ConsoleKey.D2 => new Square(0, 1),
                    ConsoleKey.D3 => new Square(0, 2),
                    ConsoleKey.D4 => new Square(1, 0),
                    ConsoleKey.D5 => new Square(1, 1),
                    ConsoleKey.D6 => new Square(1, 2),
                    ConsoleKey.D7 => new Square(2, 0),
                    ConsoleKey.D8 => new Square(2, 1),
                    ConsoleKey.D9 => new Square(2, 2)
                };
                if (board.IsEmpty(choice.Row, choice.Column))
                    return choice;
                else
                    Console.WriteLine("That square is already taken.");
                 
            }
        }
    }

    public class Square
    {
        public int Row { get; }
        public int Column { get; }

        public Square(int row, int column)
        {
            Column = column;
            Row = row;
        }
    }

    public class BoardRenderer
    {
        private char[,] symbols = new char[3, 3] ;

        public void DrawBoard(Board board)
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    symbols[i, j] = FillCell(board.GetState(i, j));
                }
            }

            Console.WriteLine($" {symbols[0, 0]} | {symbols[0, 1]} | {symbols[0, 2]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {symbols[1, 0]} | {symbols[1, 1]} | {symbols[1, 2]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {symbols[2, 0]} | {symbols[2, 1]} | {symbols[2, 2]}");
        }

        private char FillCell(Cell cell) => cell switch { Cell.X => 'X', Cell.O => 'O', Cell.Empty => ' ' };
    }

    public class Board
    {
        public Cell[,] Cells { get; }= new Cell[3, 3];

        public Cell GetState(int row, int column) => Cells[row, column];
        public void FillCell(int row, int column, Cell value) => Cells[row, column] = value;
        public bool IsEmpty(int row, int column) => Cells[row, column] == Cell.Empty;
    }

    public enum Cell
    {
        Empty,
        X,
        O,
    }
}