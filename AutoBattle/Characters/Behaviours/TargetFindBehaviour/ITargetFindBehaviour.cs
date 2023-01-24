using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.TargetFindBehaviour
{
    public interface ITargetFindBehaviour
    {
        void FindTarget(Character character);
    }
}
