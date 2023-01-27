using AutoBattle.Characters;
using AutoBattle.Effects;
using System;
using System.Numerics;

namespace AutoBattle.Abilities
{
    /// <summary>
    /// Special Ability that does double <c>BaseDamage</c> and also have the possibility of do <c>Bleed</c> and <c>Weakness</c> to a target.
    /// </summary>
    public class StrongAttackAbility : ISpecialAbility
    {
        public string Name => "Huge Smash";

        public bool hasDone = false;

        private Vector2 bleedDamageRange = new Vector2(8, 12);

        public bool CanDoSpecial()
        {
            return hasDone is false;
        }

        public void DoSpecial(Character character)
        {
            int damage = character.BaseDamage * 2;
            var target = character.Target;

            Console.WriteLine($" {character.Name} DID {Name} ON {character.Target.Name} WITH DAMAGE OF {damage}".ToUpper());

            target.TakeDamage(damage);

            TryDoBleed(target);
            TryDoWeakness(target);

            hasDone = true;
        }

        private void TryDoBleed(Character target) 
        {
            int bleedChance = new Random().Next(0, 101);

            if (bleedChance < 70) 
            {
                Console.WriteLine($" - {Name} adds bleed effect to {target.Name}");
                int bleedTurns = new Random().Next(1, 3);
                target.AddEffect(new Bleed(bleedTurns, bleedDamageRange));
            } 
        }

        private void TryDoWeakness(Character target) 
        {
            int weaknessChance = new Random().Next(0, 101);

            if (weaknessChance < 50)
            {
                Console.WriteLine($" - {Name} adds weakness effect to {target.Name}");
                int weaknessTurns = new Random().Next(1, 3);
                int weaknessAmount = new Random().Next(4,8);
                target.AddEffect(new Weakness(weaknessTurns, weaknessAmount));
            }
        }
    }
}
