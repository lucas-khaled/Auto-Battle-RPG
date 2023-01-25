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
            SetCharacterBasis(200, 8, null, new MoveTowardsTarget(1), new SimpleAttackBehaviour(0,1), new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            if (CanDoHeal())
            {
                TurnAction = DoHeal;
                return;
            }

            if (Target != null && GameManager.actualGame.Grid.IsInRange(currentBox, Target.currentBox, attackBehaviour.Range)) 
            {
                TurnAction = Attack;
                return;
            }

            TurnAction = Move;
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
