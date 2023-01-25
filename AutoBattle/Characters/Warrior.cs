using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            SetCharacterBasis(130, 20, null, new MoveTowardsTarget(1), null, new FindClosestTargetBehaviour());
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
