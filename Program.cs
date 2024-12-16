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
        bool keepPlaying = true;
        while (keepPlaying)
        {
            PlayGame();
            keepPlaying = AskToPlayAgain();
        }
    }

    private static void PlayGame()
    {
        // Create shared game components
        var board = new GameBoard();
        var display = new GameDisplay(board);
        var rules = new GameRules();

        // Get game mode from player
        IPlayer player1, player2;
        SelectGameMode(display, rules, out player1, out player2);

        // Show the empty game board to start
        display.Clear();
        display.Display();
        
        // This is the main game loop - it keeps running until someone wins or quits
        IPlayer currentPlayer = player1;
        bool gameEnded = false;

        while (!gameEnded)
        {
            // Show whose turn it is
            display.ShowTurn(currentPlayer);

            // Get the next move from the current player
            var move = currentPlayer.GetNextMove(board);

            // Check if player wants to quit
            if (!move.HasValue)
            {
                gameEnded = true;
                break;
            }

            // Make the move
            if (!board.TryPlay(move.Value.row, move.Value.column, currentPlayer.PlayerType))
            {
                display.ShowInvalidInput("Invalid move. Try again.");
                continue;
            }

            // Clear the screen and show the updated board
            display.Clear();
            display.Display();

            // Check if the current player has won
            if (board.IsWin())
            {
                if (currentPlayer is AIPlayer)
                    display.ShowMessage("AI wins! Better luck next time!");
                else
                    display.ShowWinner(currentPlayer.PlayerType);
                gameEnded = true;
                break;
            }

            // Check if the board is full (a draw)
            if (board.IsFull())
            {
                display.ShowDraw();
                gameEnded = true;
                break;
            }

            // Switch to the other player's turn
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }
    }

    private static void SelectGameMode(GameDisplay display, GameRules rules, out IPlayer player1, out IPlayer player2)
    {
        display.ShowMessage("Welcome to Tic Tac Toe!");
        display.ShowMessage("1. Play against another player");
        display.ShowMessage("2. Play against AI");
        display.ShowMessage("Choose game mode (1 or 2):");

        while (true)
        {
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                display.ShowMessage("Human vs Human mode selected!");
                player1 = new HumanPlayer(Player.One, rules, display);
                player2 = new HumanPlayer(Player.Two, rules, display);
                break;
            }
            else if (choice == "2")
            {
                display.ShowMessage("Human vs AI mode selected!");
                display.ShowMessage("You will play as O (Player One)");
                player1 = new HumanPlayer(Player.One, rules, display);
                player2 = new AIPlayer(Player.Two);
                break;
            }
            else
            {
                display.ShowMessage("Invalid choice. Please enter 1 or 2:");
            }
        }
    }

    private static bool AskToPlayAgain()
    {
        Console.WriteLine("\nWould you like to:");
        Console.WriteLine("1. Play again");
        Console.WriteLine("2. Exit");
        Console.Write("Enter your choice (1 or 2): ");

        while (true)
        {
            string? choice = Console.ReadLine();
            if (choice == "1")
                return true;
            if (choice == "2")
                return false;
            
            Console.Write("Invalid choice. Please enter 1 or 2: ");
        }
    }
}
