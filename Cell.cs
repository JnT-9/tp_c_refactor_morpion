namespace TicTacToe;

/// <summary>
/// This class represents one square on the game board.
/// Think of it as a single box where a player can put their X or O.
/// Each cell knows:
/// - Its position (row and column)
/// - What's in it (X, O, or empty)
/// </summary>
internal class Cell
{
    /// <summary>
    /// Which row this cell is in (1, 2, or 3)
    /// </summary>
    public int Row { get; private set; }

    /// <summary>
    /// Which column this cell is in (1, 2, or 3)
    /// </summary>
    public int Column { get; private set; }

    /// <summary>
    /// What's in this cell:
    /// - null means empty
    /// - 'X' means Player Two marked it
    /// - 'O' means Player One marked it
    /// </summary>
    public char? Value { get; private set; }

    /// <summary>
    /// Creates a new cell with a specific position and value
    /// </summary>
    public Cell(int row, int column, char value)
    {
        this.Row = row;
        this.Column = column;
        this.Value = value;
    }

    /// <summary>
    /// Creates a new empty cell at a specific position
    /// </summary>
    public Cell(int row, int column)
    {
        this.Row = row;
        this.Column = column;
        this.Value = null;  // null means empty
    }

    /// <summary>
    /// Puts a player's mark (X or O) in this cell
    /// </summary>
    internal void UpdateValue(char value)
    {
        this.Value = value;
    }

    /// <summary>
    /// Removes any mark from this cell (makes it empty again)
    /// </summary>
    internal void ClearValue()
    {
        this.Value = null;
    }

    /// <summary>
    /// Creates a new empty cell - this is a helper method to make the code cleaner
    /// </summary>
    internal static Cell EmptyCell(int row, int column)
        => new Cell(row, column);
}
