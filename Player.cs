namespace TicTacToe;

/// <summary>
/// This represents the two players in the game.
/// Instead of just using X and O directly, we give them proper names:
/// Player.One (who uses 'O')
/// Player.Two (who uses 'X')
/// </summary>
public enum Player
{
    One,  // First player (uses 'O')
    Two   // Second player (uses 'X')
}

/// <summary>
/// These are helper methods that make it easier to work with players.
/// They add extra abilities to the Player enum.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    /// Converts a player into their game symbol (X or O)
    /// Player.One becomes 'O'
    /// Player.Two becomes 'X'
    /// </summary>
    public static char ToSymbol(this Player player) => player == Player.One ? 'O' : 'X';
    
    /// <summary>
    /// Switches to the other player
    /// If it's Player.One, switch to Player.Two
    /// If it's Player.Two, switch to Player.One
    /// This is used to alternate turns between players
    /// </summary>
    public static Player Toggle(this Player player) => player == Player.One ? Player.Two : Player.One;
}