using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.Effects;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Warrior : Character
    {
        private Vector2 bleedDamageRange = new Vector2(10, 20);
        public Warrior(string name) : base(name)
        {
            SetCharacterBasis(130, 20, new StrongAttackAbility(), new MoveTowardsTarget(1), new SimpleAttackBehaviour(3,1), new FindClosestEnemyBehaviour());
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

        public override void Attack()
        {
            base.Attack();
            TryDoBleed();
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }

        private bool CanDoSpecial() 
        {
            if (SpecialAbility == null) return false;

            int chance = new Random().Next(1, 101);
            return SpecialAbility.CanDoSpecial() && (Health < 30 || chance < 20);
        }

        private void TryDoBleed() 
        {
            var random = new Random();
            int chance = random.Next(1,101);

            if (chance <= 30)
                DoBleed(Target);
        }

        private void DoBleed(Character target) 
        {
            var random = new Random();
            int turns = random.Next(1,3);

            Console.WriteLine($"{Name} adds bleed effect to {target.Name}");

            target.AddEffect(new Bleed(turns,bleedDamageRange));
        }
    }
}
