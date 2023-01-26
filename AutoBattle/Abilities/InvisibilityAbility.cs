using AutoBattle.Characters;
using AutoBattle.Effects;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Abilities
{
    public class InvisibilityAbility : ISpecialAbility
    {
        public string Name => "Hiding";

        private int turnsOfWaiting = 5;
        private int lastActivationTurn = 1;
        private int turnsOfInvisibility = 2;

        public bool CanDoSpecial()
        {
            return GameManager.actualGame.Turn - lastActivationTurn > turnsOfWaiting;
        }

        public void DoSpecial(Character character)
        {
            lastActivationTurn = GameManager.actualGame.Turn;

            Console.WriteLine($" - {character.Name} used {Name}".ToUpper());

            character.AddEffect(new Invisibility(turnsOfInvisibility));
        }
    }
}
