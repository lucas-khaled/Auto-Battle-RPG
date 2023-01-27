namespace AutoBattle.Characters
{
    /// <summary>
    /// Factory class for handling <c>Character</c>'s creation.
    /// </summary>
    public static class CharacterFactory
    {
        /// <summary>
        /// Generates a <c>Character</c> base on a given class and a given name.
        /// </summary>
        /// <param name="characterClass">Class that a <c>Character</c> will be created from.</param>
        /// <param name="name"><c>Character</c> given name. It will be the character class name in case of null</param>
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
