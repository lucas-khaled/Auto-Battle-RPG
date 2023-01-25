using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Cleric : Character
    {
        public Cleric(string name) : base(name)
        {
            SetCharacterBasis(200, 8, null, new MoveTowardsTarget(1), null, new FindClosestTargetBehaviour());
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
