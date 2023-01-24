using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public static class CharacterFactory
    {
        public static Character GenerateCharacter(CharacterClass characterClass, string name = null) 
        {
            if (string.IsNullOrEmpty(name))
                name = characterClass.ToString();

            Character character = null;
            switch (characterClass) 
            {
                case CharacterClass.Paladin:
                    character = new Paladin(name);
                    break;
                case CharacterClass.Warrior:
                    character = new Warrior(name);
                    break;
                case CharacterClass.Cleric:
                    character = new Cleric(name);
                    break;
                case CharacterClass.Archer:
                    character = new Archer(name);
                    break;
            }

            return character;
        }
    }
}
