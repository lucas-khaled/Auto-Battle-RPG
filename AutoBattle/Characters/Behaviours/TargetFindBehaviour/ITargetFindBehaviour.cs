using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.TargetFindBehaviour
{
    /// <summary>
    /// Behaviour that finds a target for a <c>Character</c>.
    /// </summary>
    public interface ITargetFindBehaviour
    {
        /// <summary>
        /// Sets a target for the given character
        /// </summary>
        void FindTarget(Character character);
    }
}
