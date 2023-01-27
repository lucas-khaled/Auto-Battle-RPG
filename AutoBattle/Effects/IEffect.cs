using AutoBattle.Characters;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Interface that represents the effects that can be applied to the characters.
    /// </summary>
    public interface IEffect
    {
        /// <summary>
        /// Method that applies the effect
        /// </summary>
        /// <param name="character">Character witch the effect will be applied over</param>
        void ApplyEffect(Character character);

        /// <summary>
        /// Method that inidicates if the effect has passed away
        /// </summary>
        /// <returns>True if the effect was fully applied</returns>
        bool Passed();
    }
}
