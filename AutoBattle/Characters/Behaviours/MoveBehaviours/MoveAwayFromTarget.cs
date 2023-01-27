using AutoBattle.Grids;
using System.Collections.Generic;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    /// <summary>
    /// Moves away the target of the given <c>Character</c>.
    /// </summary>
    public class MoveAwayFromTarget : TargetOrientedMoveBehaviour
    {
        private float distance = 0;

        public MoveAwayFromTarget(int moveRange) : base(moveRange)
        {
        }

        protected override bool IsGoodPosition(GridBox newPosition, GridBox targetPosition, List<GridBox> possibilities, Grid grid)
        {
            var newDistance = grid.CalculateDistance(newPosition, targetPosition);
            if (newDistance < distance) return false;

            if (newDistance != distance)
            {
                distance = newDistance;
                possibilities.Clear();
            }

            return true;
        }

        protected override void StartedEvaluation(GridBox position, GridBox targetPosition, Grid grid)
        {
            float distance = grid.CalculateDistance(position, targetPosition);
        }
    }
}
