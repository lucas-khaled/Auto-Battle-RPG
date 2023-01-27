using AutoBattle.GameManagement;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    public class MoveTowardsTarget : TargetOrientedMoveBehaviour
    {
        private float distance = 0;

        public MoveTowardsTarget(int moveRange) : base(moveRange)
        {
        }

        protected override List<GridBox> EvaluateMovementPossibilities(GridBox position, GridBox targetPosition, Grid grid)
        {
            if (grid.IsInRange(position, targetPosition, MoveRange)) return null;

            return base.EvaluateMovementPossibilities(position, targetPosition, grid);
        }

        protected override bool IsGoodPosition(GridBox newPosition, GridBox targetPosition, List<GridBox> possibilities, Grid grid)
        {
            var newDistance = grid.CalculateDistance(newPosition, targetPosition);
            if (newDistance > distance) return false;

            if (newDistance != distance)
            {
                distance = newDistance;
                possibilities.Clear();
            }

            return true;
        }

        protected override void StartedEvaluation(GridBox position, GridBox targetPosition, Grid grid)
        {
            distance = grid.CalculateDistance(position, targetPosition);
        }
    }
}
