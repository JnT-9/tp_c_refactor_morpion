using System;
using System.Threading;

namespace TicTacToe;

/// <summary>
/// This class handles everything related to showing the game on the screen.
/// It's like an artist that draws the game board and shows messages to the players.
/// </summary>
public class GameDisplay
{
    // Keep track of the game board so we can show its current state
    private readonly GameBoard _board;

    /// <summary>
    /// When we create a new display, we need to know which game board to show
    /// </summary>
    public GameDisplay(GameBoard board)
    {
        _board = board;
    }

    /// <summary>
    /// Shows a general message to the players
    /// </summary>
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Draws the current state of the game board on the screen
    /// Shows a nice title and border around the board
    /// </summary>
    public void Display()
    {
        // Draw a line across the screen using = symbols
        Console.WriteLine(new string('=', Console.WindowWidth));
        
        // Show the game title in the middle of the screen
        Console.WriteLine("Tic Tac Toe".PadLeft(Console.WindowWidth / 2));
        
        // Draw another line across the screen
        Console.WriteLine(new string('=', Console.WindowWidth));

        // Draw the top border of the game board
        Console.WriteLine("|-----------------|");
        
        // Draw each row of the game board with separators
        DisplayLine(1);  // First row
        Console.WriteLine("|-----|-----|-----|");  // Separator
        DisplayLine(2);  // Second row
        Console.WriteLine("|-----|-----|-----|");  // Separator
        DisplayLine(3);  // Third row
        
        // Draw the bottom border of the game board
        Console.WriteLine("|-----------------|");
    }

    /// <summary>
    /// Draws one row of the game board, showing X's, O's, or empty spaces
    /// </summary>
    private void DisplayLine(int row)
    {
        // Get the content of each cell in this row and display it with borders
        Console.WriteLine($"|  {_board.GetCellContent(row, 1)}  |  {_board.GetCellContent(row, 2)}  |  {_board.GetCellContent(row, 3)}  |");
    }

    /// <summary>
    /// Clears everything from the screen to prepare for the next display
    /// </summary>
    public void Clear()
    {
        Console.Clear();
    }

    /// <summary>
    /// Shows whose turn it is and what they should do
    /// </summary>
    public void ShowTurn(IPlayer player)
    {
        if (player is HumanPlayer)
        {
            ShowTurnPrompt(player.PlayerType);
        }
        else if (player is AIPlayer)
        {
            Console.WriteLine($"AI Player ({player.PlayerType.ToSymbol()}) is thinking...");
            // Add a small delay to make it feel more natural
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Shows a message asking the human player to make their move
    /// </summary>
    private void ShowTurnPrompt(Player player)
    {
        Console.WriteLine($"Player {player.ToSymbol()} - Enter row (1-3) and column (1-3), separated by a space, or 'q' to quit...");
    }

    /// <summary>
    /// Shows a message when a player tries to make an invalid move
    /// (like trying to play in a spot that's already taken)
    /// </summary>
    public void ShowInvalidMove()
    {
        Console.WriteLine("Invalid move");
    }

    /// <summary>
    /// Shows a celebration message when a player wins
    /// </summary>
    public void ShowWinner(Player player)
    {
        Console.WriteLine($"Player {player.ToSymbol()} has won the game!!!!");
    }

    /// <summary>
    /// Shows a message when the game ends in a draw (no winner)
    /// </summary>
    public void ShowDraw()
    {
        Console.WriteLine("It's a draw");
    }

    /// <summary>
    /// Shows an error message when the player types something invalid
    /// </summary>
    public void ShowInvalidInput(string message)
    {
        Console.WriteLine(message);
    }
}