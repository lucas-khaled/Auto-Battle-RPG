using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    public interface IMoveBehaviour
    {
        int MoveRange { get; }
        void Move(Character character);
    }
}
