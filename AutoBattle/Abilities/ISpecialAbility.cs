using System;
using System.Collections.Generic;
using System.Text;
using AutoBattle.Characters;

namespace AutoBattle.Abilities
{
    /// <summary>
    /// Special abilities that a <c>Character</c> can do.
    /// </summary>
    public interface ISpecialAbility
    {
        /// <summary>
        /// The ability's name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Defines if the special ability can be done or not
        /// </summary>
        /// <returns>True if matches the ability activation's requirements </returns>
        bool CanDoSpecial();

        /// <summary>
        /// Does the special logic.
        /// </summary>
        /// <param name="character">The character that is casting the special ability</param>
        void DoSpecial(Character character);
    }
}
