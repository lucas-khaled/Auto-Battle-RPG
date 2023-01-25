using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    public class SimpleAttackBehaviour : IAttackBehaviour
    {
        public int Range { get; }

        private int variation;
        private Random random = new Random();

        public SimpleAttackBehaviour(int variation, int range) 
        {
            this.variation = variation;
            this.Range = range;
        }

        public void Attack(Character character)
        {
            var baseDamage = character.BaseDamage;
            var damage = random.Next(baseDamage - variation, baseDamage + variation + 1);
            var target = character.Target;

            Console.WriteLine($" - {character.Name} attacked {target.Name} with {damage} of damage!");
            target.TakeDamage(damage);
        }
    }
}
