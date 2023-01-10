# Rising_Joker Prototype branch

### General information

This is the prototype of a videogame without any design patterns for Design Patterns module. Game is written in **C#**, using **Windows Forms App (.NET Framework)**
project template. Implemented game features are general player movement between constantly falling platforms, desired color player selection and 3 people
online multiplayer. The game is divided into two separate projects: client and server.

### Client functionality

Each player plays the game on their own instance of a client. There can be up to three clients running at one time connected to a single server. Client controlls most
of the game logic.

### Server functionality

There is only supposed to be one server instance running for all clients. Server synchronizes the start of the game, tells the clients where and what kind
of platforms to spawn and also broadcasts the positions of player's opponents so that they could be displayed on the game window.

### Online configuration

The game can be configured to run on localhost (ws://127.0.0.1) or any working online IP address. We used Hamachi to configure online play, but it should be possible
to play online without Hamachi with correct IP configuration.

In order to change the IP configuration of the game, the IP address needs to be changed in two different locations:
1. Line 8 in Program.cs in the Server project.
2. Line 41 in Form1.cs in the Client project

Three different computers are needed to fully test out the multiplayer functionality so for the sake of simplicity the game is currently configured to require only
two players (Red and Blue) in order to hold a functional multiplayer session. **Three player multiplayer will not work with the current configuration**. Server project file PlayerPositionBroadcastSocket.cs
needs to be configured slightly (there are comments explaining it) in order to require all three players to be joined.

### Tutorials used

Two different youtube tutorials were used to create this project. One explains WebSockets that were used for multiplayer features, the other is a general tutorial describing
how to create a game with a Windows Forms App.

1. WebSockets tutorial: https://www.youtube.com/watch?v=ThiAQAB5Dp4
2. Windows Forms tutorial: https://www.youtube.com/watch?v=rQBHwdEEL9I
