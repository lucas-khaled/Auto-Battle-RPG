using AutoBattle.GameManagement;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    public abstract class TargetOrientedMoveBehaviour : IMoveBehaviour
    {
        public int MoveRange { get; }

        public TargetOrientedMoveBehaviour(int moveRange)
        {
            this.MoveRange = moveRange;
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

            Console.WriteLine($"{character.Name} moved to {character.GetCurrentPlace().ToString()}");
        }

        protected virtual List<GridBox> EvaluateMovementPossibilities(GridBox position, GridBox targetPosition, Grid grid)
        {
            StartedEvaluation(position, targetPosition, grid);

            List<GridBox> possibilities = new List<GridBox>();

            for (int x = -MoveRange; x <= MoveRange; x++)
            {
                var newX = position.X + x;
                if (newX < 0 || newX >= grid.xLength) continue;

                for (int y = -MoveRange; y <= MoveRange; y++)
                {
                    var newY = position.Y + y;
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
