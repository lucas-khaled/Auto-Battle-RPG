using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    public class MoveTowardsTarget : IMoveBehaviour
    {
        private int moveRange;
        public MoveTowardsTarget(int moveRange)
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

            var random = new Random();
            var newPosition = possibilities[random.Next(0, possibilities.Count)];

            GameManager.actualGame.MoveObject(character,newPosition);
        }

        private List<GridBox> EvaluateMovementPossibilities(GridBox position, GridBox targetPosition, Grid grid) 
        {
            if (grid.IsInRange(position, targetPosition, moveRange)) return null;

            List<GridBox> possibilities = new List<GridBox>();
            float distance = grid.CalculateDistance(position, targetPosition);

            for(int x = -moveRange; x <= moveRange; x++) 
            {
                var newX = position.xIndex + x;
                if (newX < 0 || newX >= grid.xLength) continue;

                for (int y = -moveRange; y <= moveRange; y++) 
                {
                    var newY = position.yIndex + y;
                    if (newY < 0 || newY >= grid.yLength) continue;

                    var newBox = grid.GetBoxInPosition(newX, newY);
                    if (newBox.ocupiedBy != null) continue;

                    var newDistance = grid.CalculateDistance(newBox, targetPosition);
                    if (newDistance > distance) continue;

                    if (newDistance != distance)
                    {
                        distance = newDistance;
                        possibilities.Clear();
                    }

                    possibilities.Add(newBox);
                }
            }

            return possibilities;
        }
    }
}
