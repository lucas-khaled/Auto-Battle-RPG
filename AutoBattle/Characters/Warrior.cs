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
            SetCharacterBasis(130, 20, null, new MoveTowardsTarget(1), new SimpleAttackBehaviour(3,1), new FindClosestTargetBehaviour());
        }

        public override void ChooseAction()
        {
            if (GameManager.actualGame.Grid.IsInRange(currentBox, Target.currentBox, attackBehaviour.Range))
                TurnAction = Attack;
            else
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
