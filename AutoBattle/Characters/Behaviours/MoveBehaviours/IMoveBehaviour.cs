using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    /// <summary>
    /// Behaviour that will handle the character movements
    /// </summary>
    public interface IMoveBehaviour
    {
        /// <summary>
        /// The range of the movement in a <c>Grid</c>
        /// </summary>
        int MoveRange { get; }

        /// <summary>
        /// Methos that will handle the <c>Character</c> movement this turn
        /// </summary>
        /// <param name="character"><c>Character</c> that will make a moment</param>
        void Move(Character character);
    }
}
