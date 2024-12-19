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
            PlayOneGame();
            keepPlaying = AskToPlayAgain();
        }
    }

    private static void PlayOneGame()
    {
        // Create shared game components
        var board = new GameBoard();
        var display = new GameDisplay(board);
        var rules = new GameRules();
        var game = new Game(board, display, rules);

        // Get game mode from player
        while (true)
        {
            display.ShowMessage("Welcome to Tic Tac Toe!");
            display.ShowMessage("1. Play against another player");
            display.ShowMessage("2. Play against AI");
            display.ShowMessage("Choose game mode (1 or 2):");

            string? choice = Console.ReadLine();
            if (game.SelectGameMode(choice))
                break;

            display.ShowMessage("Invalid choice. Please enter 1 or 2:");
        }

        // Play the game
        game.PlayGame();
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
