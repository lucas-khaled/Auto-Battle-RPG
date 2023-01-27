using AutoBattle.Characters;
using System;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that will heal the character once.
    /// </summary>
    public class Heal : IEffect
    {
        private bool applied = false;
        private int healAmount;

        public Heal(int healAmount)
        {
            this.healAmount = healAmount;
        }

        public void ApplyEffect(Character character)
        {
            if (applied)
                return;

            Console.WriteLine($" - {character.Name} heals {healAmount}");

            character.TakeDamage(-this.healAmount);
            applied = true;
        }

        public bool Passed()
        {
            return applied;
        }
    }
}
