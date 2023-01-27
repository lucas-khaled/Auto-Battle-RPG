using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using AutoBattle.Effects;
using System;
using System.Numerics;

namespace AutoBattle.Characters
{
    public class Warrior : CharacterWithSpecial
    {
        private Vector2 bleedDamageRange = new Vector2(10, 20);
        public Warrior(string name) : base(name)
        {
            SetCharacterBasis(health: 200, baseDamage: 30, new StrongAttackAbility(), new MoveTowardsTarget(1), new SimpleAttackBehaviour(3,1), new FindClosestEnemyBehaviour());
        }

        public override void Attack()
        {
            base.Attack();
            TryDoBleed();
        }

        protected override bool CanDoSpecial() 
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
