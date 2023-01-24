using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Warrior : Character
    {
        public Warrior(string name)
        {
            SetCharacterBasis(name, 130, 20, null, null, null);
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
