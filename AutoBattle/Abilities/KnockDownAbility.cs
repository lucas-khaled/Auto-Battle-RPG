using AutoBattle.Characters;
using AutoBattle.Effects;
using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Abilities
{
    public class KnockDownAbility : ISpecialAbility
    {
        public string Name => "Knock Out";

        private int turnsOfWaiting = 3;
        private int lastActivationTurn = 1;

        public bool CanDoSpecial()
        {
            return GameManager.actualGame.Turn - lastActivationTurn > turnsOfWaiting;
        }

        public void DoSpecial(Character character)
        {
            lastActivationTurn = GameManager.actualGame.Turn;

            Console.WriteLine($" - {character.Name} used {Name} on {character.Target.Name}".ToUpper());

            character.Target.AddEffect(new KnockDown());
        }
    }
}
