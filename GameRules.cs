using System;

namespace TicTacToe;

/// <summary>
/// This class handles all the rules of the game, like:
/// - Checking if a move is valid
/// - Parsing player input
/// - Making sure players follow the rules
/// Think of it as a rulebook that makes sure everyone plays fairly
/// </summary>
public class GameRules
{
    /// <summary>
    /// Tries to understand what move the player wants to make.
    /// Returns true if the input is valid (like "2 3"), false otherwise.
    /// </summary>
    public bool TryParseMove(string? input, out int row, out int column)
    {
        // Start with invalid values
        row = column = 0;
        
        // If nothing was typed, it's invalid
        if (string.IsNullOrEmpty(input)) return false;

        // Split the input into parts wherever there's a space
        string[] parts = input.Split(' ');
        
        // We need exactly two numbers (row and column)
        if (parts.Length != 2) return false;

        // Try to convert both parts into numbers
        return int.TryParse(parts[0], out row) && 
               int.TryParse(parts[1], out column);
    }

    /// <summary>
    /// Checks if the chosen position is within the game board (1-3 for both row and column)
    /// </summary>
    public bool IsValidPosition(int row, int column)
        => row >= 1 && row <= 3 && column >= 1 && column <= 3;

    /// <summary>
    /// Checks if the player wants to quit (by typing 'q' or 'Q')
    /// </summary>
    public bool IsQuitCommand(string? input)
        => string.Compare(input, "q", StringComparison.OrdinalIgnoreCase) == 0;

    /// <summary>
    /// Tries to make a move and returns whether it was successful.
    /// A move is valid if:
    /// 1. The input format is correct (like "2 3")
    /// 2. The position is within the board (1-3)
    /// 3. The chosen cell is empty
    /// </summary>
    public bool TryMove(string? input, GameBoard board, Player currentPlayer, out string? errorMessage)
    {
        errorMessage = null;

        // Check if player wants to quit
        if (IsQuitCommand(input))
        {
            return false;
        }

        // Try to understand the move
        if (!TryParseMove(input, out int row, out int column))
        {
            errorMessage = "Invalid input format. Please enter row and column (1-3) separated by space";
            return false;
        }

        // Check if the position is valid
        if (!IsValidPosition(row, column))
        {
            errorMessage = "Position must be between 1 and 3";
            return false;
        }

        // Try to make the move
        if (!board.TryPlay(row, column, currentPlayer))
        {
            errorMessage = "Invalid move - this position is already taken";
            return false;
        }

        return true;
    }
} 