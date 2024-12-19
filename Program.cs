using System;
using System.Threading.Tasks;

namespace TicTacToe;

/// <summary>
/// This is the main program that runs our Tic Tac Toe game.
/// Think of it as the game referee who manages the whole game.
/// </summary>
internal class Program
{
    static async Task Main(string[] args)
    {
        bool keepPlaying = true;
        while (keepPlaying)
        {
            await PlayOneGame();
            keepPlaying = AskToPlayAgain();
        }
    }

    private static async Task PlayOneGame()
    {
        Console.Clear();
        DisplayHeader();

        // Create shared game components
        var board = new GameBoard();
        var display = new GameDisplay(board);
        var rules = new GameRules();
        var game = new Game(board, display, rules);

        // Get game mode from player
        while (true)
        {
            display.ShowMessage("\n1. Play against another player");
            display.ShowMessage("2. Play against AI");
            display.ShowMessage("\nChoose game mode (1 or 2):");

            string? choice = Console.ReadLine();
            if (game.SelectGameMode(choice))
                break;

            display.ShowMessage("Invalid choice. Please enter 1 or 2:");
        }

        // Play the game
        await game.PlayGame();
    }

    private static void DisplayHeader()
    {
        string[] header = {
            @"  _______ _        _______           _______         ",
            @" |__   __(_)      |__   __|         |__   __|       ",
            @"    | |   _  ___     | | __ _  ___     | | ___   ___ ",
            @"    | |  | |/ __|    | |/ _` |/ __|    | |/ _ \ / _ \",
            @"    | |  | | (__     | | (_| | (__     | | (_) |  __/",
            @"    |_|  |_|\___|    |_|\__,_|\___|    |_|\___/ \___|",
            @"",
            @"                   Let's Play!                        ",
            @""
        };

        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (string line in header)
        {
            Console.WriteLine(line);
        }
        Console.ResetColor();
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
