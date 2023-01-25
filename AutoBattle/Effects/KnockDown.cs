using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    internal class KnockDown : IEffect
    {
        public bool applied = false;

        public bool Passed()
        {
            return applied;
        }

        public void ApplyEffect(Character character)
        {
            if (Passed())
                return;

            character.CanAct = false;
            applied = true;
        }

        public void ResetEffect(Character character)
        {
            character.CanAct = true;
        }
    }
}
