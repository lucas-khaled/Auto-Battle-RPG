using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters.Behaviours.AttackBehaviours
{
    internal class RowAttackBehaviour : IAttackBehaviour
    {
        public int Range { get; }

        private int variation;
        private int steps;
        private Random random = new Random();

        public RowAttackBehaviour(int variation, int range, int steps)
        {
            Range = range;
            this.variation = variation;
            this.steps = steps;
        }

        public void Attack(Character character)
        {
            var totalDamage = random.Next(character.BaseDamage - variation, character.BaseDamage + variation + 1);
            var stepIdealDamage = (float)totalDamage / steps;
            var remainingDamage = totalDamage;

            Console.WriteLine($"{character.Name} do Row Attack on {character.Target.Name}!");

            for (int i = 1; i<=steps; i++) 
            {
                var stepDamage = 0;
                if (i == steps)
                    stepDamage = remainingDamage;
                else
                {
                    var stepVariationAmount = (int)Math.Floor((double)variation / steps);
                    var stepVariation = random.Next(-stepVariationAmount, stepVariationAmount + 1);

                    stepDamage = (int)Math.Clamp(Math.Floor(stepIdealDamage) + stepVariation, 1, remainingDamage - 1);
                }

                DoAttackStep(character, stepDamage, i);
                remainingDamage -= stepDamage;
            }
        }

        private void DoAttackStep(Character character, int damage, int step) 
        {
            var target = character.Target;

            Console.WriteLine($" - {step}º attack did {damage} of damage");
            target.TakeDamage(damage);
        }
    }
}
