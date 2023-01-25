using AutoBattle.Characters;
using AutoBattle.Characters.Behaviours.MoveBehaviours;
using AutoBattle.Characters.Behaviours.TargetFindBehaviour;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Effects
{
    public class Fear : IEffect
    {
        private MoveAwayFromTarget moveAwayBehaviour;
        private FindClosestTargetBehaviour closestTargetBehaviour;
        private bool applied = false;
        private bool reset = false;

        public void ApplyEffect(Character character)
        {
            if(moveAwayBehaviour == null) 
                moveAwayBehaviour = new MoveAwayFromTarget(character.MoveBehaviour.MoveRange);

            if (closestTargetBehaviour == null)
                closestTargetBehaviour = new FindClosestTargetBehaviour();

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
