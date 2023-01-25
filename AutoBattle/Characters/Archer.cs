using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Archer : Character
    {
        public Archer(string name) : base(name)
        {
            SetCharacterBasis(100, 12, new InvisibilityAbility(), new MoveTowardsTarget(2), new SimpleAttackBehaviour(6,2), new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            if (Target != null && GameManager.actualGame.Grid.IsInRange(currentBox, Target.currentBox, AttackBehaviour.Range))
            {
                if (CanDoSpecial())
                {
                    TurnAction = DoSpecial;
                    return;
                }

                TurnAction = Attack;
                return;
            }

            TurnAction = Move;
        }

        private bool CanDoSpecial()
        {
            if (SpecialAbility == null) return false;

            int chance = new Random().Next(1, 101);
            return SpecialAbility.CanDoSpecial() && Health < 50 || chance < 30;
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }
    }
}
