using AutoBattle.Characters;
using System;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that makes a character not visible for others during a certain time
    /// </summary>
    public class Invisibility : IEffect
    {
        private int turnsRemaining;
        private bool reseted = false;

        /// <param name="turns">Number of turns that the effect will last.</param>
        public Invisibility(int turns) 
        {
            this.turnsRemaining = turns;
        }

        public void ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0) 
            {
                ResetEffect(character);
                return;
            }

            Console.WriteLine($" - {character.Name} have become invisible!");
                
            character.Visible = false;
            turnsRemaining--;
        }

        public bool Passed()
        {
            return reseted;
        }

        private void ResetEffect(Character character)
        {
            character.Visible = true;
            Console.WriteLine($" - {character.Name} is now visible!");

            reseted = true;
        }
    }
}
