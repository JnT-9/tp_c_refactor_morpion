# Morpion (Tic Tac Toe) - Projet Principal

## Description

Ce projet implémente un jeu de Morpion (Tic Tac Toe) en C# avec une architecture orientée objet. Le jeu permet de jouer contre un autre joueur ou contre une IA.

## Structure du Projet

### Classes Principales

1. **Game** (`Game.cs`)
   - Gère le flux du jeu
   - Coordonne les interactions entre les joueurs
   - Maintient l'état du jeu

2. **GameBoard** (`GameBoard.cs`)
   - Représente le plateau de jeu
   - Gère la grille 3x3
   - Vérifie les conditions de victoire

3. **Cell** (`Cell.cs`)
   - Représente une case du plateau
   - Stocke la valeur (X, O, ou vide)
   - Gère sa position (ligne, colonne)

4. **Players** (`IPlayer.cs`, `HumanPlayer.cs`, `AIPlayer.cs`)
   - Interface commune pour tous les joueurs
   - Implémentation pour joueurs humains
   - Implémentation pour l'IA

5. **GameDisplay** (`GameDisplay.cs`)
   - Gère l'affichage du jeu
   - Affiche les messages aux joueurs
   - Dessine le plateau

6. **GameRules** (`GameRules.cs`)
   - Définit les règles du jeu
   - Valide les mouvements
   - Gère les commandes spéciales

### Fonctionnalités

- Mode 2 joueurs
- Mode contre l'IA
- Interface console interactive
- Validation des mouvements
- Détection automatique des victoires/matchs nuls
- Possibilité de quitter à tout moment

## Comment Jouer

1. Lancez le jeu
2. Choisissez le mode de jeu (1: Humain vs Humain, 2: Humain vs IA)
3. Les joueurs jouent à tour de rôle
4. Entrez les coordonnées au format "ligne colonne" (ex: "2 3")
5. Le jeu continue jusqu'à une victoire, un match nul ou une sortie

## Architecture

Le projet suit les principes SOLID :

- Single Responsibility : chaque classe a une responsabilité unique
- Open/Closed : extensible pour ajouter de nouveaux types de joueurs
- Liskov Substitution : les types de joueurs sont interchangeables
- Interface Segregation : interfaces ciblées et spécifiques
- Dependency Inversion : dépendances via des interfaces
