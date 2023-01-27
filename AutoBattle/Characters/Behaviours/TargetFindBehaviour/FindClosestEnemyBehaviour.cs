using AutoBattle.GameManagement;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBattle.Characters.Behaviours.TargetFindBehaviour
{
    /// <summary>
    /// Behaviour that will find the closest enemy from the character on actual game grid
    /// </summary>
    public class FindClosestEnemyBehaviour : ITargetFindBehaviour
    {
        public void FindTarget(Character character)
        {
            var possibleTargets = GameManager.actualGame.Grid.boxes
                .Where(box => DoesPlaceFits(box, character));

            var myBox = character.GetCurrentPlace();
            var grid = GameManager.actualGame.Grid;
            var orderedTargets = possibleTargets.OrderBy(box => grid.CalculateDistance(myBox, box)).ToArray();

            if (orderedTargets.Length <= 0) 
            {
                character.Target = null;
                return;
            }

            character.Target = orderedTargets[0].ocupiedBy as Character;
        }

        private bool DoesPlaceFits(GridBox box, Character character) 
        {
            return box.ocupiedBy is Character target && target.Visible && target.IsDead is false && target.Team.ID != character.Team.ID;
        }
    }
}
