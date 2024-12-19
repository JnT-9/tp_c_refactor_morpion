namespace TicTacToe;

/// <summary>
/// Represents a human player who inputs moves through the console
/// </summary>
public class HumanPlayer : IPlayer
{
    private readonly GameRules _rules;
    private readonly IGameDisplay _display;

    public Player PlayerType { get; }

    public HumanPlayer(Player playerType, GameRules rules, IGameDisplay display)
    {
        PlayerType = playerType;
        _rules = rules;
        _display = display;
    }

    public (int row, int column)? GetNextMove(GameBoard board)
    {
        while (true)
        {
            // Show prompt and get input
            _display.ShowTurn(this);
            string? input = Console.ReadLine();

            // Check for quit
            if (_rules.IsQuitCommand(input))
                return null;

            // Try to parse the move
            if (_rules.TryParseMove(input, out int row, out int column) && 
                _rules.IsValidPosition(row, column))
            {
                // Check if the cell is empty
                if (board.GetCellContent(row, column) == ' ')
                {
                    return (row, column);
                }
            }

            _display.ShowInvalidInput("Invalid move. Try again.");
        }
    }
} 