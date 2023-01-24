using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public class Heal : IEffect
    {
        private bool applied = false;;
        private int healAmount;
        public Heal(int healAmount)
        {
            this.healAmount = healAmount;
        }

        public bool ApplyEffect(Character character)
        {
            if (applied)
                return false;

            character.TakeDamage(-this.healAmount);
            applied = true;
            return true;
        }
    }
}
