using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
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
