using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    internal class KnockDown : IEffect
    {
        public bool applied = false;

        public bool ApplyEffect(Character character)
        {
            if (applied)
            {
                character.CanAct = true;
                return false;
            }

            character.CanAct = false;
            applied = true;
            return true;
        }
    }
}
