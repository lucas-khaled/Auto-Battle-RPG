using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public class Invisibility : IEffect
    {
        private int turnsRemaining;
        public Invisibility(int turns) 
        {
            this.turnsRemaining = turns;
        }
        public bool ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0) 
            {
                character.Visible = true; 
                return false;
            }
                
            character.Visible = false;
            return true;
        }
    }
}
