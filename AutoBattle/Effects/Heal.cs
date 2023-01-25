using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
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
