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

            character.SetCurrentPlace(newPosition);
        }

        private List<GridBox> EvaluateMovementPossibilities(GridBox position, GridBox targetPosition, Grid grid) 
        {
            List<GridBox> possibilities = new List<GridBox>();
            int distance = CalculateDistance(position, targetPosition);

            for(int x = -moveRange; x <= moveRange; x++) 
            {
                var newX = position.xIndex + x;
                if (newX <= 0 || newX >= grid.xLenght) continue;

                for (int y = -moveRange; y <= moveRange; y++) 
                {
                    var newY = position.yIndex + y;
                    if (newY <= 0 || newY >= grid.yLength) continue;

                    var newBox = grid.GetBoxInPosition(newX, newY);
                    if (newBox.ocupiedBy != null) continue;

                    var newDistance = CalculateDistance(newBox, targetPosition);
                    if (newDistance > distance) continue;

                    distance = newDistance;
                    possibilities.Add(newBox);
                }
            }

            return possibilities;
        }

        private int CalculateDistance(GridBox from, GridBox to) 
        {
            return Math.Abs(from.xIndex - to.xIndex) + Math.Abs(from.yIndex = to.yIndex);
        }
    }
}
