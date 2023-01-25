using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public class Invisibility : IEffect
    {
        private int turnsRemaining;
        private bool reseted = false;

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

        public void ResetEffect(Character character)
        {
            character.Visible = true;
            Console.WriteLine($" - {character.Name} is now visible!");

            reseted = true;
        }
    }
}
