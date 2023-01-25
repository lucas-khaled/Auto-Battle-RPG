using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    public abstract class TargetOrientedMoveBehaviour : IMoveBehaviour
    {
        private int moveRange;
        public TargetOrientedMoveBehaviour(int moveRange)
        {
            this.moveRange = moveRange;
        }

        public void Move(Character character)
        {
            var target = character.Target;
            if (target == null) return;

            var targetPos = target.GetCurrentPlace();
            var characterPos = character.GetCurrentPlace();

            var possibilities = EvaluateMovementPossibilities(characterPos, targetPos, GameManager.actualGame.Grid);
            if (possibilities == null || possibilities.Count <= 0) return;

            ChoosePossibilities(possibilities, character);
        }

        protected virtual void ChoosePossibilities(List<GridBox> possibilities, Character character) 
        {
            var random = new Random();
            var newPosition = possibilities[random.Next(0, possibilities.Count)];

            GameManager.actualGame.MoveObject(character, newPosition);
        }

        protected virtual List<GridBox> EvaluateMovementPossibilities(GridBox position, GridBox targetPosition, Grid grid)
        {
            if (grid.IsInRange(position, targetPosition, moveRange)) return null;

            StartedEvaluation(position, targetPosition, grid);

            List<GridBox> possibilities = new List<GridBox>();

            for (int x = -moveRange; x <= moveRange; x++)
            {
                var newX = position.xIndex + x;
                if (newX < 0 || newX >= grid.xLength) continue;

                for (int y = -moveRange; y <= moveRange; y++)
                {
                    var newY = position.yIndex + y;
                    if (newY < 0 || newY >= grid.yLength) continue;

                    var newBox = grid.GetBoxInPosition(newX, newY);
                    if (newBox.ocupiedBy != null) continue;

                    if (IsGoodPosition(newBox, targetPosition, possibilities, grid) is false) continue;

                    possibilities.Add(newBox);
                }
            }

            return possibilities;
        }

        protected abstract bool IsGoodPosition(GridBox newPosition, GridBox targetPosition, List<GridBox> possibilities, Grid grid);
        protected abstract void StartedEvaluation(GridBox position, GridBox targetPosition, Grid grid);
    }
}
