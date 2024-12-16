using System.Linq;

namespace TicTacToe;

/// <summary>
/// Represents an AI player that makes moves automatically
/// Uses a simple strategy:
/// 1. Win if possible
/// 2. Block opponent from winning
/// 3. Take center if available
/// 4. Take a corner if available
/// 5. Take any available space
/// </summary>
public class AIPlayer : IPlayer
{
    public Player PlayerType { get; }

    public AIPlayer(Player playerType)
    {
        PlayerType = playerType;
    }

    public (int row, int column)? GetNextMove(GameBoard board)
    {
        // Try to win
        var winningMove = FindWinningMove(board, PlayerType);
        if (winningMove.HasValue)
            return winningMove;

        // Block opponent from winning
        var opponentWinningMove = FindWinningMove(board, PlayerType.Toggle());
        if (opponentWinningMove.HasValue)
            return opponentWinningMove;

        // Take center if available
        if (board.GetCellContent(2, 2) == ' ')
            return (2, 2);

        // Take a corner if available
        var corners = new[] { (1, 1), (1, 3), (3, 1), (3, 3) };
        var availableCorner = corners.FirstOrDefault(c => board.GetCellContent(c.Item1, c.Item2) == ' ');
        if (availableCorner != default)
            return availableCorner;

        // Take any available space
        for (int row = 1; row <= 3; row++)
        {
            for (int col = 1; col <= 3; col++)
            {
                if (board.GetCellContent(row, col) == ' ')
                    return (row, col);
            }
        }

        return null;  // No moves available
    }

    /// <summary>
    /// Finds a winning move for the specified player
    /// </summary>
    private (int row, int column)? FindWinningMove(GameBoard board, Player player)
    {
        // Try each empty cell
        for (int row = 1; row <= 3; row++)
        {
            for (int col = 1; col <= 3; col++)
            {
                if (board.GetCellContent(row, col) == ' ')
                {
                    // Try the move
                    board.TryPlay(row, col, player);

                    // Check if it's a winning move
                    bool isWinning = board.IsWin();

                    // Undo the move
                    board.UndoLastMove(row, col);

                    if (isWinning)
                        return (row, col);
                }
            }
        }

        return null;
    }
} 