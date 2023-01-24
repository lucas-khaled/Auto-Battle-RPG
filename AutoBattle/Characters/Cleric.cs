using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    internal class Cleric : Character
    {
        public Cleric(string name) : base(name)
        {
            SetCharacterBasis(200, 8, null, null, null);
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
