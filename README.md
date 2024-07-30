<p align="center">
    <img src="https://img.shields.io/badge/Unity%20Version-2021.3.33f-success" alt="Unity Version">
</p>

# TicTacToe
Welcome to the Tic Tac Toe game project! This is a  implementation of the Tic Tac Toe game using Unity and C#. The game offers multiple modes, allowing you to play against a friend, against the computer, or watch the computer play against itself.

# Architecture
This project uses a Service-Model based architecture with Dependency Injection (DI) and a State Machine to control the entire game flow.

# Patterns
In this project, the following design patterns and practices were used:

<br> Dependency Injection (DI): Implemented using Zenject.
<br> Entry Point
<br> Model-View-Presenter (MVP)
<br> Strategy Pattern 
<br> Factory Pattern
<br> Command Pattern (with Undo functionality)
<br> State Machine

# Unit Testing
This project provides unit testing with Zenject for the following functionalities:
* Win
* Lose
* Draw
* Hint
* Undo

# Asset bundle creator
This project includes a custom editor window designed to streamline asset bundle creation. The editor provides an intuitive interface for quickly creating asset bundles and saving them to the StreamingAssets folder.

# Features
* Game Modes:
   - Player vs Player: Play with a friend in the local multiplayer mode.
   - Player vs Computer: Challenge the computer with adjustable difficulty levels.
   - Computer vs Computer: Watch two computer players compete against each other.

* Asset bundle runtime loading
* Undo
* Hints
* Restarting
* Win/Lose/Draw detection

# Main Project Flow 
The Boostrapper registers states in the state machine and transitions the state machine to the BootstrapState. In the BootstrapState, static data for the levels and the standard game bundle are loaded. Next, the state machine transitions to the MainMenuState. This state loads the MainMenuScene and initializes the menu. When the Start button is pressed, the game transitions to the LoadMainGameSceneState. Here, the MainGameScene is loaded, the game world is initialized, UI is created, and a 3x3 grid is generated. Afterwards, the game transitions to the GameLoopInitialState. In this state, players and the DrawWinService are initialized. Then, the game transitions to the PlayerTurnState. In this state, a player makes a move, and after the player's turn, the DrawWinService checks for win or draw conditions. If the game continues, it transitions back to the PlayerTurnState with the next player. If the DrawWinService detects a win, the game transitions to the WinState. If it's a draw, the game transitions to the DrawState. From here, the game can return to the MainMenuState, or if the Restart button is pressed, it transitions back to the GameLoopInitialState.

