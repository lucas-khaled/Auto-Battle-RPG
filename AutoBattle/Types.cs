using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {

        public struct CharacterClassSpecific
        {
            CharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;
        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public GridObject ocupiedBy;
            public int Index;
            public bool exists;

            public GridBox(int x, int y, GridObject ocupiedBy, int index)
            {
                xIndex = x;
                yIndex = y;
                this.ocupiedBy = ocupiedBy;
                this.Index = index;
                exists = x>=0 && y>=0 && index>=0;
            }

            public override string ToString()
            {
                return $"({xIndex},{yIndex})";
            }
        }

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }

    }
}
