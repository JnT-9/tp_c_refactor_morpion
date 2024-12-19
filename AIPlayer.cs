using System;
using System.Linq;
using System.Threading.Tasks;

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
    // Interface pour l'affichage
    private readonly IGameDisplay _display;
    // Générateur de nombres aléatoires pour le délai
    private readonly Random _random = new();
    // Animation de chargement avec slash rotatif et points
    private readonly string[] _thinkingAnimation = { 
        "/ Thinking   ", 
        "- Thinking.  ", 
        "\\ Thinking..", 
        "| Thinking..."
    };
    // Délai entre chaque frame de l'animation (en ms)
    private const int ANIMATION_DELAY = 150; // Réduit pour une rotation plus fluide

    public Player PlayerType { get; }

    public AIPlayer(Player playerType, IGameDisplay display)
    {
        PlayerType = playerType;
        _display = display;
    }

    public async Task<(int row, int column)?> GetNextMove(GameBoard board)
    {
        // Génère un délai aléatoire entre 0.5 et 5 secondes
        int thinkingTime = _random.Next(500, 5001);
        
        // Crée une tâche pour le délai de réflexion
        var thinkingTask = Task.Delay(thinkingTime);
        
        // Démarre l'animation en parallèle
        var animationTask = ShowThinkingAnimation(thinkingTime);
        
        // Attend que les deux tâches soient terminées
        await Task.WhenAll(thinkingTask, animationTask);

        // Efface la dernière frame de l'animation
        _display.Clear();
        _display.Display();

        // Calcule et retourne le prochain mouvement
        return CalculateNextMove(board);
    }

    /// <summary>
    /// Affiche une animation de chargement pendant que l'IA "réfléchit"
    /// </summary>
    private async Task ShowThinkingAnimation(int totalDuration)
    {
        // Continue l'animation jusqu'à ce que le temps de réflexion soit écoulé
        var endTime = DateTime.Now.AddMilliseconds(totalDuration);
        
        while (DateTime.Now < endTime)
        {
            foreach (var frame in _thinkingAnimation)
            {
                Console.Write("\r" + frame); // \r ramène le curseur au début de la ligne
                await Task.Delay(ANIMATION_DELAY);
                
                // Arrête si on a dépassé le temps total
                if (DateTime.Now >= endTime) break;
            }
        }
        Console.WriteLine(); // Nouvelle ligne après l'animation
    }

    /// <summary>
    /// Calcule le prochain mouvement selon la stratégie de l'IA
    /// </summary>
    private (int row, int column)? CalculateNextMove(GameBoard board)
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