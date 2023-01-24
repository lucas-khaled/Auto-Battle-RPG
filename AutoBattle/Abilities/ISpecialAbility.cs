using System;
using System.Collections.Generic;
using System.Text;
using AutoBattle.Characters;

namespace AutoBattle.Abilities
{
    public interface ISpecialAbility
    {
        string Name { get; }
        bool DoSpecial(Character character);
    }
}
