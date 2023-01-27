using AutoBattle.Characters;
using AutoBattle.Effects;
using AutoBattle.GameManagement;
using System;

namespace AutoBattle.Abilities
{
    /// <summary>
    /// Special ability that adds <c>Fear</c> effect on the target
    /// </summary>
    public class FrightenAbility : ISpecialAbility
    {
        public string Name => "Fear Shout";

        private int turnsOfWaiting = 5;
        private int lastActivationTurn = 1;

        public bool CanDoSpecial()
        {
            return GameManager.actualGame.Turn - lastActivationTurn > turnsOfWaiting;
        }

        public void DoSpecial(Character character)
        {
            lastActivationTurn = GameManager.actualGame.Turn;

            Console.WriteLine($" {character.Name} used {Name} on {character.Target.Name}".ToUpper());
            character.Target.AddEffect(new Fear());
        }
    }
}
