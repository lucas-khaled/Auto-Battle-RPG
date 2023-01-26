using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoBattle.GameManagement
{
    public static class GameManager
    {
        public static Game actualGame { get; private set; }

        public static void StartNewGame() 
        {
            actualGame = new Game();
            actualGame.StartGame();
            RunTurns(actualGame);
        }

        private static void RunTurns(Game game) 
        {
            Console.WriteLine("Press any key for start!");
            Console.ReadKey();

            while (game.HasEnded() is false) 
            {
                game.RunTurn();
                Console.WriteLine("\n Press any key for next Turn\n");
                Console.ReadKey();
            }

            game.EndGame();

        }
    }
}
