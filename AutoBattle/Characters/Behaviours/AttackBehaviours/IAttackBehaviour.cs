using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    public interface IAttackBehaviour
    {
        void Attack(Character character);
    }
}
