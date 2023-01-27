using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoBattle.GameManagement
{
    /// <summary>
    /// Does the control and runs games
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// The actual <c>Game</c> that is running
        /// </summary>
        public static Game actualGame { get; private set; }

        /// <summary>
        /// Starts and run a new game
        /// </summary>
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

            while (game.CanEnd() is false) 
            {
                game.RunTurn();
                Console.WriteLine("\n Press any key for next Turn\n");
                Console.ReadKey();
            }

            game.EndGame();

        }
    }
}
