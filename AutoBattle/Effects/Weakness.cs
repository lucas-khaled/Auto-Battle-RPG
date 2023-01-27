using AutoBattle.Characters;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that will reduce the <c>BaseDamage</c> of a character.
    /// </summary>
    /// <remarks> The <c>BaseDamage</c>c> will be awayas a minimum of 0. It will not become less than that.</remarks>
    public class Weakness : IEffect
    {
        private int turnsRemaining;
        private int amount;
        private bool applied = false;
        private bool reseted = false;

        /// <param name="turns">Number of turns that the effect will last.</param>
        /// <param name="amount">Amount of reducing of damage.</param>
        public Weakness(int turns, int amount) 
        {
            this.turnsRemaining = turns;
            this.amount = amount;
        }

        public bool Passed()
        {
            return reseted;
        }

        public void ApplyEffect(Character character)
        {
            if (turnsRemaining <= 0) 
            {
                ResetEffect(character);
                return;
            }  
            
            if(applied is false) 
            {
                amount = (character.BaseDamage - amount > 0) ? amount : character.BaseDamage;
                character.BaseDamage -= amount;
                applied = true;
            }

            turnsRemaining--;
        }

        public void ResetEffect(Character character)
        {
            character.BaseDamage += amount;
            reseted = true;
        }
    }
}
