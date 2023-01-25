using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Paladin : Character
    {
        public Paladin(string name) : base(name)
        {
            SetCharacterBasis(200, 10, null, new MoveTowardsTarget(1), null, new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            TurnAction = Move;
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }
    }
}
