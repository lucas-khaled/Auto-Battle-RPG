using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.Effects;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Cleric : Character
    {
        private Vector2 healRange = new Vector2(5, 15);

        public Cleric(string name) : base(name)
        {
            SetCharacterBasis(200, 8, new FrightenAbility(), new MoveAwayFromTarget(1), new SimpleAttackBehaviour(0,1), new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            if (CanDoHeal())
            {
                TurnAction = DoHeal;
                return;
            }

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

        private bool CanDoHeal() 
        {
            int chance = new Random().Next(1, 101);

            return chance < 50 && Effects.Any(x => x is Heal) is false;
        }

        private void DoHeal() 
        {
            int heal = new Random().Next((int)healRange.X, (int)healRange.Y);
            Console.WriteLine($" - {Name} do healing");
            AddEffect(new Heal(heal));
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }
    }
}
