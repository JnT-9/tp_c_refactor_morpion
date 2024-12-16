namespace TicTacToe;

/// <summary>
/// Interface for any type of player (human or AI)
/// This allows us to treat all players the same way in the game logic
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Gets the player's symbol (X or O)
    /// </summary>
    Player PlayerType { get; }

    /// <summary>
    /// Gets the next move from the player
    /// Returns null if the player wants to quit
    /// </summary>
    (int row, int column)? GetNextMove(GameBoard board);
} 