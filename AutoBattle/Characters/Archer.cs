using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Archer : Character
    {
        public Archer(string name)
        {
            SetCharacterBasis(name, 100, 12, null, null, null);
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
