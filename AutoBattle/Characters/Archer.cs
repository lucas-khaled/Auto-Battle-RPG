using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Archer : Character
    {
        public Archer(string name) : base(name)
        {
            SetCharacterBasis(100, 12, null, new MoveTowardsTarget(2), null, new FindClosestTargetBehaviour());
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
