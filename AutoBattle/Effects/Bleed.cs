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

        public Bleed(int turnsRemaining, Vector2 damageRange) 
        {
            this.turnsRemaining = turnsRemaining;
            this.damageRange = damageRange;
        }

        public bool ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0)
                return false;

            var damage = random.Next((int)damageRange.X, (int)damageRange.Y);
            character.TakeDamage(damage);
            turnsRemaining--;

            return true;
        }
    }
}
