using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public interface IEffect
    {
        void ApplyEffect(Character character);

        bool Passed();
    }
}
