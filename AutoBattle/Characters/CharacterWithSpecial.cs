using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Characters
{
    public abstract class CharacterWithSpecial : Character
    {
        protected CharacterWithSpecial(string name) : base(name)
        {
        }

        public override void ChooseAction() 
        {
            if (Target != null && GameManager.actualGame.Grid.IsInRange(GetCurrentPlace(), Target.GetCurrentPlace(), AttackBehaviour.Range))
            {
                if (CanDoSpecial())
                {
                    TurnAction = DoSpecial;
                    return;
                }

                TurnAction = Attack;
                return;
            }

            TurnAction = Move;
        }

        public override void DoAction()
        {
            TurnAction?.Invoke();
        }

        protected abstract bool CanDoSpecial();
    }
}
