using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBattle.Characters.Behaviours.TargetFindBehaviour
{
    public class FindClosestTargetBehaviour : ITargetFindBehaviour
    {
        public void FindTarget(Character character)
        {
            var myBox = character.GetCurrentPlace();
            var orderedTargets = GameManager.actualGame.Grid.boxes
                .Where(box => box.ocupiedBy is Character target && target.Visible && box.ocupiedBy != character)
                .OrderBy(box => Math.Abs(box.yIndex - myBox.yIndex) + Math.Abs(box.xIndex - box.xIndex))
                .ToArray();

            if(orderedTargets.Length <= 0) 
            {
                character.Target = null;
                return;
            }

            character.Target = orderedTargets[0].ocupiedBy as Character;
        }
    }
}
