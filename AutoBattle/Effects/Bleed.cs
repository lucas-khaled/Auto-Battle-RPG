using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AutoBattle.Effects
{
    public class Bleed : IEffect
    {
        private int turnsRemaining;
        private Vector2 damageRange;
        private Random random = new Random();

        public Bleed(int turns, Vector2 damageRange) 
        {
            this.turnsRemaining = turns;
            this.damageRange = damageRange;
        }

        public bool Passed()
        {
            return turnsRemaining <= 0;
        }

        public void ApplyEffect(Character character)
        {
            if (Passed())
                return;

            var damage = random.Next((int)damageRange.X, (int)damageRange.Y);

            Console.WriteLine($" - {character.Name} bled and took {damage} of damage");

            character.TakeDamage(damage);
            turnsRemaining--;
        }

        public void ResetEffect(Character character)
        {
            Console.WriteLine($" - {character.Name} stopped bleeding");
        }
    }
}
