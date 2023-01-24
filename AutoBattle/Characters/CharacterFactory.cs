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
                    // TODO: Call Paladin Constructor
                    break;
                case CharacterClass.Warrior:
                    // TODO: Call Warrior Constructor
                    break;
                case CharacterClass.Cleric:
                    // TODO: Call Cleric Constructor
                    break;
                case CharacterClass.Archer:
                    // TODO: Call Archer Constructor
                    break;
            }

            return character;
        }
    }
}
