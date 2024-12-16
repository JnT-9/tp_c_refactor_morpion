using System;

namespace TicTacToe;

/// <summary>
/// This is the main program that runs our Tic Tac Toe game.
/// Think of it as the game referee who manages the whole game.
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        // Create all the game components
        var board = new GameBoard();
        var display = new GameDisplay(board);
        var rules = new GameRules();
        var currentPlayer = Player.One;

        // Show the empty game board to start
        display.Display();
        
        // This is the main game loop - it keeps running until someone wins or quits
        while (true)
        {
            // Ask the current player to make their move
            display.ShowTurnPrompt(currentPlayer);
            
            // Wait for the player to type their move and press Enter
            string? input = Console.ReadLine();

            // Try to make the move according to the game rules
            if (!rules.TryMove(input, board, currentPlayer, out string? errorMessage))
            {
                // If it's a quit command, end the game
                if (rules.IsQuitCommand(input))
                    break;

                // If it's an invalid move, show the error and try again
                display.ShowInvalidInput(errorMessage ?? "Invalid move");
                continue;
            }

            // Clear the screen and show the updated board
            display.Clear();
            display.Display();

            // Check if the current player has won
            if (board.IsWin())
            {
                display.ShowWinner(currentPlayer);
                break;  // End the game
            }

            // Check if the board is full (a draw)
            if (board.IsFull())
            {
                display.ShowDraw();
                break;  // End the game
            }

            // Switch to the other player's turn
            currentPlayer = currentPlayer.Toggle();
        }
    }
}
