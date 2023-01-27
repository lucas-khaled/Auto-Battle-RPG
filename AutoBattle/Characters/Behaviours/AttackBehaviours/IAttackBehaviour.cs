using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    /// <summary>
    /// Behaviours of some character Attack.
    /// </summary>
    public interface IAttackBehaviour
    {
        /// <summary>
        /// The range position of a attack.
        /// </summary>
        int Range { get; }

        /// <summary>
        /// Method that will do an attack for the given player on his target.
        /// </summary>
        /// <param name="character">The character that is doing the attack</param>
        void Attack(Character character);
    }
}
