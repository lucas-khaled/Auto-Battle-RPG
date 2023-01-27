using AutoBattle.Characters;
using System;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that will block that character from acting this turn
    /// </summary>
    public class KnockDown : IEffect
    {
        public bool applied = false;
        public bool reseted = false;

        public bool Passed()
        {
            return reseted;
        }

        public void ApplyEffect(Character character)
        {
            if (applied) 
            {
                ResetEffect(character);
                return;
            }
                
            Console.WriteLine($" - {character.Name} was knocked down");
            character.CanAct = false;
            applied = true;
        }

        public void ResetEffect(Character character)
        {
            character.CanAct = true;
            reseted = true;
        }
    }
}
