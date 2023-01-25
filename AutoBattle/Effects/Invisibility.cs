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
        public void ApplyEffect(Character character)
        {
            if (Passed()) 
                return;
                
            character.Visible = false;
        }

        public bool Passed()
        {
            return turnsRemaining <= 0;
        }

        public void ResetEffect(Character character)
        {
            character.Visible = true;
        }
    }
}
