using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    /// <summary>
    /// Do a Single attack in a turn
    /// </summary>
    public class SimpleAttackBehaviour : IAttackBehaviour
    {
        public int Range { get; }

        private int variation;
        private Random random = new Random();

        /// <param name="variation">The randomly choosed variation of the character's <c>BaseDamage</c></param>
        /// <param name="range">The range position of the attack</param>
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

            Console.WriteLine($" {character.Name} attacked {target.Name} with {damage} of damage!");
            target.TakeDamage(damage);
        }
    }
}
