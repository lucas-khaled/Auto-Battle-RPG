using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.GameManagement
{
    public static class GameManager
    {
        public static Game actualGame { get; private set; }

        public static void StartNewGame(Grid grid, params Character[] characters) 
        {
            actualGame = new Game(grid, characters);
            actualGame.StartGame();
            RunGame(actualGame);
        }

        private static void RunGame(Game game) 
        {
            int turn = 1;
            while(game.HasEnded() is false) 
            {
                Console.WriteLine($"Turn {turn}");
                game.RunTurn();
                turn++;
            }
        }
    }
}
