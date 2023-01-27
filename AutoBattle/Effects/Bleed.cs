using AutoBattle.Characters;
using System;
using System.Numerics;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that adds damage on a character during certain time.
    /// </summary>
    public class Bleed : IEffect
    {
        private int turnsRemaining;
        private Vector2 damageRange;
        private Random random = new Random();

        private bool reseted = false;

        /// <param name="turns">Number of turns that the effect will last.</param>
        /// <param name="damageRange">Possible range of damage the character can take from this effect.</param>
        public Bleed(int turns, Vector2 damageRange) 
        {
            this.turnsRemaining = turns;
            this.damageRange = damageRange;
        }

        public bool Passed() 
        {
            return reseted;
        }

        public void ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0) 
            {
                ResetEffect(character);
                return;
            }

            var damage = random.Next((int)damageRange.X, (int)damageRange.Y);

            Console.WriteLine($" - {character.Name} bled and took {damage} of damage");

            character.TakeDamage(damage);
            turnsRemaining--;
        }

        private void ResetEffect(Character character)
        {
            Console.WriteLine($" - {character.Name} stopped bleeding");
            reseted = true;
        }
    }
}
