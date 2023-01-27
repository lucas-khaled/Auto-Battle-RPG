using AutoBattle.GameManagement;

namespace AutoBattle.Characters
{
    /// <summary>
    /// Base for characters that behaves trying to do Special
    /// </summary>
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

        /// <summary>
        /// Determines if the character can use hiss special movement
        /// </summary>
        /// <returns>True when the character can use his special movement</returns>
        protected abstract bool CanDoSpecial();
    }
}
