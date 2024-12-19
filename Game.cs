using System;
using System.Threading.Tasks;

namespace TicTacToe;

/// <summary>
/// Represents the game state and manages the game flow
/// </summary>
public class Game
{
    // Store the game board that tracks all moves
    private readonly GameBoard _board;
    // Interface for displaying game state and messages
    private readonly IGameDisplay _display;
    // Rules engine that validates moves and input
    private readonly GameRules _rules;
    // First player (can be human or AI)
    private IPlayer? _player1;
    // Second player (can be human or AI)
    private IPlayer? _player2;
    // Reference to the current player's turn
    private IPlayer? _currentPlayer;
    // Flag indicating if the game has ended
    private bool _gameEnded;

    /// <summary>
    /// Initialize a new game with required components
    /// </summary>
    public Game(GameBoard board, IGameDisplay display, GameRules rules)
    {
        // Store the game board reference
        _board = board;
        // Store the display interface reference
        _display = display;
        // Store the rules engine reference
        _rules = rules;
        // Initialize game as not ended
        _gameEnded = false;
    }

    /// <summary>
    /// Initialize a new game with pre-configured players (for testing)
    /// </summary>
    public Game(GameBoard board, IGameDisplay display, GameRules rules, IPlayer player1, IPlayer player2)
    {
        _board = board;
        _display = display;
        _rules = rules;
        _player1 = player1;
        _player2 = player2;
        _gameEnded = false;
    }

    /// <summary>
    /// Initializes the game with the selected game mode
    /// </summary>
    public bool SelectGameMode(string? choice)
    {
        // Return false if no choice was made
        if (string.IsNullOrEmpty(choice)) return false;

        switch (choice)
        {
            case "1": // Human vs Human mode
                // Show mode selection message
                _display.ShowMessage("Human vs Human mode selected!");
                // Create first human player
                _player1 = new HumanPlayer(Player.One, _rules, _display);
                // Create second human player
                _player2 = new HumanPlayer(Player.Two, _rules, _display);
                return true;

            case "2": // Human vs AI mode
                // Show mode selection message
                _display.ShowMessage("Human vs AI mode selected!");
                // Inform player about their symbol
                _display.ShowMessage("You will play as O (Player One)");
                // Create human player
                _player1 = new HumanPlayer(Player.One, _rules, _display);
                // Create AI opponent with display interface
                _player2 = new AIPlayer(Player.Two, _display);
                return true;

            default: // Invalid choice
                return false;
        }
    }

    /// <summary>
    /// Starts and manages the game flow
    /// </summary>
    public async Task<GameResult> PlayGame()
    {
        // Verify game mode was selected
        if (_player1 == null || _player2 == null)
            throw new InvalidOperationException("Game mode must be selected before starting the game");

        // Set first player as current
        _currentPlayer = _player1;
        // Reset game ended flag
        _gameEnded = false;

        // Clear and show initial board
        _display.Clear();
        _display.Display();

        // Check if game is already in a terminal state
        if (_board.IsWin())
        {
            _gameEnded = true;
            // Determine which player won based on the last move
            var winner = _board.GetLastPlayer();
            if (winner.HasValue)
            {
                _display.ShowWinner(winner.Value);
                return winner.Value == Player.One ? GameResult.Player1Wins : GameResult.Player2Wins;
            }
        }
        
        if (_board.IsFull())
        {
            _gameEnded = true;
            _display.ShowDraw();
            return GameResult.Draw;
        }

        // Main game loop
        while (!_gameEnded)
        {
            // Process one turn, quit if player requested
            if (!await PlayTurn())
                return GameResult.Quit;
        }

        // Return final game result
        return DetermineGameResult();
    }

    /// <summary>
    /// Processes a single turn for the current player
    /// </summary>
    private async Task<bool> PlayTurn()
    {
        // Show whose turn it is
        _display.ShowTurn(_currentPlayer!);

        // Get the player's move
        var moveResult = await _currentPlayer!.GetNextMove(_board);

        // Check if player wants to quit
        if (!moveResult.HasValue)
        {
            _gameEnded = true;  // Marque le jeu comme terminé
            return false;
        }

        // Try to make the move
        if (!_board.TryPlay(moveResult.Value.row, moveResult.Value.column, _currentPlayer.PlayerType))
        {
            // Show error if move was invalid
            _display.ShowInvalidInput("Invalid move. Try again.");
            return true;
        }

        // Update display with new move
        _display.Clear();
        _display.Display();

        // Check for win condition
        if (_board.IsWin())
        {
            // Show appropriate win message
            if (_currentPlayer is AIPlayer)
                _display.ShowMessage("AI wins! Better luck next time!");
            else
                _display.ShowWinner(_currentPlayer.PlayerType);
            // Set game as ended
            _gameEnded = true;
            return true;
        }

        // Check for draw condition
        if (_board.IsFull())
        {
            // Show draw message
            _display.ShowDraw();
            // Set game as ended
            _gameEnded = true;
            return true;
        }

        // Switch to other player's turn
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        return true;
    }

    /// <summary>
    /// Determines the final result of the game
    /// </summary>
    private GameResult DetermineGameResult()
    {
        // Check if game ended in a win
        if (_board.IsWin())
            // Return appropriate winner
            return _currentPlayer == _player1 ? GameResult.Player1Wins : GameResult.Player2Wins;
        
        // Check if game ended in a draw
        if (_board.IsFull())
            return GameResult.Draw;

        // Game was quit
        return GameResult.Quit;
    }
}

/// <summary>
/// Represents the possible outcomes of a game
/// </summary>
public enum GameResult
{
    // First player (O) won the game
    Player1Wins,
    // Second player (X) won the game
    Player2Wins,
    // Game ended in a draw
    Draw,
    // Game was quit before completion
    Quit
} 