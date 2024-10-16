using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
    public class TicTacToe
    {
        private TicTacToeConsoleRenderer _boardRenderer;
        private PlayerEnum currentPlayer;
        private PlayerEnum?[,] board;

        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10, 6);
            currentPlayer = PlayerEnum.X; // the first player to start the game
            board = new PlayerEnum?[3, 3]; // board
            _boardRenderer.Render();
        }

        public void Run()
        {
            bool gameOver = false;

            do
            {
                Console.SetCursorPosition(2, 19);
                Console.Write($"Player {currentPlayer}");

                // Get row input
                Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (0-2): ");
                int row = GetValidInput(0, 2);

                // Get column input
                Console.SetCursorPosition(2, 22);
                Console.Write("Please Enter Column (0-2): ");
                int column = GetValidInput(0, 2);

                // Check if the selected cell is available
                if (board[row, column] == null)
                {
                    // Make a move on the board
                    board[row, column] = currentPlayer;
                    _boardRenderer.AddMove(row, column, currentPlayer, true);

                    // Check for win or draw
                    if (CheckWin(row, column))
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else
                    {
                        // player switch
                        currentPlayer = (currentPlayer == PlayerEnum.X) ? PlayerEnum.O : PlayerEnum.X;
                    }
                }
                else
                {
                    Console.SetCursorPosition(2, 24);
                    Console.WriteLine("That cell is already occupied. Try again.");
                    System.Threading.Thread.Sleep(2000); // Show error message for 2 seconds
                    Console.Clear();
                    _boardRenderer.Render(); // Redraw the board after clearing the screen
                }

            } while (!gameOver);
        }

        // Method to get valid input for row/column
        private int GetValidInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.SetCursorPosition(2, 24);
                Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
                System.Threading.Thread.Sleep(2000); // Show error message for 2 seconds
                Console.Clear();
                _boardRenderer.Render(); // Redraw the board after clearing the screen
            }
            return input;
        }

        // Check if the current player has won
        private bool CheckWin(int row, int column)
        {
            // Check the row
            bool rowWin = true;
            for (int i = 0; i < 3; i++)
            {
                if (board[row, i] != currentPlayer)
                {
                    rowWin = false;
                    break;
                }
            }

            // Check the column
            bool colWin = true;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, column] != currentPlayer)
                {
                    colWin = false;
                    break;
                }
            }

            // Check diagonals
            bool diag1Win = (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer);
            bool diag2Win = (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer);

            return rowWin || colWin || diag1Win || diag2Win;
        }

        // Check if the board is full (for a draw)
        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                        return false;
                }
            }
            return true;
        }
    }
}
