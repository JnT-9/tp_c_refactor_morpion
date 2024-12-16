using System.Collections.Generic;
using System.Linq;

namespace TicTacToe;

/// <summary>
/// This class represents the Tic Tac Toe game board.
/// Think of it as the actual grid where players place their X's and O's.
/// It keeps track of all moves and checks if someone has won.
/// </summary>
public class GameBoard
{
    // This is our game grid - it stores all the cells (squares) on the board
    private readonly List<Cell> _grid;

    /// <summary>
    /// When we create a new game board, we set up all 9 squares (cells)
    /// The board looks like this:
    /// (1,1) | (1,2) | (1,3)
    /// -------------------
    /// (2,1) | (2,2) | (2,3)
    /// -------------------
    /// (3,1) | (3,2) | (3,3)
    /// </summary>
    public GameBoard()
    {
        // Create all 9 empty cells with their positions
        _grid = new List<Cell>()
        {
            // First row
            Cell.EmptyCell(1, 1),  // Top left
            Cell.EmptyCell(1, 2),  // Top middle
            Cell.EmptyCell(1, 3),  // Top right
            
            // Second row
            Cell.EmptyCell(2, 1),  // Middle left
            Cell.EmptyCell(2, 2),  // Center
            Cell.EmptyCell(2, 3),  // Middle right
            
            // Third row
            Cell.EmptyCell(3, 1),  // Bottom left
            Cell.EmptyCell(3, 2),  // Bottom middle
            Cell.EmptyCell(3, 3)   // Bottom right
        };
    }

    /// <summary>
    /// Attempts to place a player's symbol (X or O) in the specified cell.
    /// Returns true if successful, false if the move is invalid (cell already taken).
    /// </summary>
    public bool TryPlay(int row, int column, Player player)
    {
        // Find the cell the player wants to mark
        var cell = GetCell(row, column);
        
        // If cell doesn't exist or is already marked, the move is invalid
        if (cell == null || cell.Value.HasValue)
            return false;

        // Mark the cell with the player's symbol (X or O)
        cell.UpdateValue(player.ToSymbol());
        return true;
    }

    /// <summary>
    /// Removes a player's mark from a cell (used by AI to simulate moves)
    /// </summary>
    public void UndoLastMove(int row, int column)
    {
        var cell = GetCell(row, column);
        if (cell != null)
        {
            cell.ClearValue();
        }
    }

    /// <summary>
    /// Checks if any player has won the game by getting three in a row
    /// (horizontally, vertically, or diagonally)
    /// </summary>
    public bool IsWin()
    {
        return IsAnyRowWin() ||     // Check all rows
               IsAnyColumnWin() ||  // Check all columns
               IsAnyDiagonalWin(); // Check both diagonals
    }

    /// <summary>
    /// Checks if the game board is completely full (no more moves possible)
    /// </summary>
    public bool IsFull() => _grid.All(cell => cell.Value.HasValue);

    /// <summary>
    /// Gets what symbol (X, O, or empty space) is in a specific cell
    /// </summary>
    public char GetCellContent(int row, int column)
        => GetCell(row, column)?.Value ?? ' ';

    /// <summary>
    /// Finds a specific cell on the board based on its row and column
    /// </summary>
    private Cell? GetCell(int row, int column)
        => _grid.FirstOrDefault(cell => cell.Row == row && cell.Column == column);

    /// <summary>
    /// Checks if any row has three matching symbols (all X's or all O's)
    /// </summary>
    private bool IsAnyRowWin()
    {
        // Group cells by their row number and check if any row
        // has all O's or all X's
        return _grid
            .GroupBy(cell => cell.Row)
            .Any(row => row.All(cell => cell.Value == 'O') || 
                       row.All(cell => cell.Value == 'X'));
    }

    /// <summary>
    /// Checks if any column has three matching symbols (all X's or all O's)
    /// </summary>
    private bool IsAnyColumnWin()
    {
        // Group cells by their column number and check if any column
        // has all O's or all X's
        return _grid
            .GroupBy(cell => cell.Column)
            .Any(column => column.All(cell => cell.Value == 'O') || 
                          column.All(cell => cell.Value == 'X'));
    }

    /// <summary>
    /// Checks if either diagonal has three matching symbols (all X's or all O's)
    /// The two diagonals are:
    /// 1. Top-left to bottom-right (1,1 -> 2,2 -> 3,3)
    /// 2. Top-right to bottom-left (1,3 -> 2,2 -> 3,1)
    /// </summary>
    private bool IsAnyDiagonalWin()
    {
        // First diagonal: cells where row equals column (1,1), (2,2), (3,3)
        var firstDiagonal = _grid.Where(c => c.Row == c.Column);
        
        // Second diagonal: cells where row + column = 4 (1,3), (2,2), (3,1)
        var secondDiagonal = _grid.Where(c => (c.Row + c.Column) == 4);

        // Check both diagonals for three matching symbols
        return new[] { firstDiagonal, secondDiagonal }
            .Any(diagonal => diagonal.All(cell => cell.Value == 'O') || 
                           diagonal.All(cell => cell.Value == 'X'));
    }
}