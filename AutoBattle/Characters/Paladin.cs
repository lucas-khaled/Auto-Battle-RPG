using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Paladin : Character
    {
        public Paladin(string name) : base(name)
        {
            SetCharacterBasis(200, 10, new KnockDownAbility(), new MoveTowardsTarget(1), new RowAttackBehaviour(1,1, 3), new FindClosestTargetBehaviour());
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
            return SpecialAbility.CanDoSpecial() && chance < 40;
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }
    }
}
