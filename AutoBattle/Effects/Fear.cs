using AutoBattle.Characters;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;

namespace AutoBattle.Effects
{
    /// <summary>
    /// Effect that will make the specified character flee from closest target for one turn.
    /// </summary>
    public class Fear : IEffect
    {
        private MoveAwayFromTarget moveAwayBehaviour;
        private FindClosestEnemyBehaviour closestTargetBehaviour;
        private bool applied = false;
        private bool reset = false;

        public void ApplyEffect(Character character)
        {
            if(moveAwayBehaviour == null) 
                moveAwayBehaviour = new MoveAwayFromTarget(character.MoveBehaviour.MoveRange);

            if (closestTargetBehaviour == null)
                closestTargetBehaviour = new FindClosestEnemyBehaviour();

            if (applied)
            {
                ResetEffect(character);
                return;
            }

            Console.WriteLine($" - {character.Name} is on Fear!");

            character.CanAct = false;
            closestTargetBehaviour.FindTarget(character);
            moveAwayBehaviour.Move(character);
            applied = true;
        }

        public bool Passed()
        {
            return reset;
        }

        private void ResetEffect(Character character) 
        {
            character.CanAct = true;
            reset = true;

            Console.WriteLine($" - {character.Name} is not on Fear anymore");
        }
    }
}
