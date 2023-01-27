using AutoBattle.Characters;
using AutoBattle.Grids;
using System;

namespace AutoBattle.GameManagement
{
    /// <summary>
    /// Events that can happen within the game
    /// </summary>
    public static class GameEvents
    {
        /// <summary>
        /// Triggered when any <c>GridObject</c> moves inside a <c>Grid</c> 
        /// </summary>
        public static Action<GridObject> onObjectMoved;

        /// <summary>
        /// Triggered when any <c>Character</c> dies; 
        /// </summary>
        public static Action<Character> onCharacterDeath;
    }
}
