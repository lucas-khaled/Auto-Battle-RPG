using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    public interface IAttackBehaviour
    {
        int Range { get; }
        void Attack(Character character);
    }
}
