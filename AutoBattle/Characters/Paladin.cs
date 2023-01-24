using AutoBattle.Abilities;
using AutoBattle.Characters.Behaviours.AttackBehaviours;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Paladin : Character
    {
        public Paladin(string name)
        {
            SetCharacterBasis(name, 200, 10, null, null, null);
        }

        public override void ChooseAction()
        {
            throw new NotImplementedException();
        }

        public override void DoAction()
        {
            throw new NotImplementedException();
        }
    }
}
