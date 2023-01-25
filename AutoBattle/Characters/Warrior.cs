using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            SetCharacterBasis(130, 20, null, new MoveTowardsTarget(1), new SimpleAttackBehaviour(3,1), new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            if (GameManager.actualGame.Grid.IsInRange(currentBox, Target.currentBox, attackBehaviour.Range))
                TurnAction = Attack;
            else
                TurnAction = Move;
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }
    }
}
