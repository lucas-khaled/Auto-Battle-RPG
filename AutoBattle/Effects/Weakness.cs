using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public class Weakness : IEffect
    {
        private int turnsRemaining;
        private int amount;
        private bool applied = false;

        public Weakness(int turns, int amount) 
        {
            this.turnsRemaining = turns;
            this.amount = amount;
        }

        public bool Passed()
        {
            return applied && turnsRemaining < 0;
        }

        public void ApplyEffect(Character character)
        {
            if (Passed()) 
                return;
            
            if(applied is false) 
            {
                amount = (character.BaseDamage - amount > 0) ? amount : character.BaseDamage;
                character.BaseDamage -= amount;
                applied = true;
            }

            turnsRemaining--;
        }

        public void ResetEffect(Character character)
        {
            character.BaseDamage += amount;
        }
    }
}
