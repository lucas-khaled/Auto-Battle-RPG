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

        public bool ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0) 
            {
                character.BaseDamage += amount;
                return false;
            }
            
            if(applied is false) 
            {
                amount = (character.BaseDamage - amount > 0) ? amount : character.BaseDamage;
                character.BaseDamage -= amount;
                applied = true;
            }

            turnsRemaining--;
            return true;
        }
    }
}
