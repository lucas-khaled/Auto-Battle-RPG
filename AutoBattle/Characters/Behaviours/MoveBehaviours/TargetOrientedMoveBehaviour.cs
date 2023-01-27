using AutoBattle.GameManagement;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;

namespace AutoBattle.Characters.Behaviours.MoveBehaviours
{
    /// <summary>
    /// Base Movement oriented by the <c>Character</c>'s <c>Target</c>.
    /// </summary>
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

        /// <summary>
        /// Chooses a random possible position to move and does the movement.
        /// </summary>
        /// <param name="possibilities">The given pre-calculated possibilities.</param>
        /// <param name="character">The character that will move</param>
        protected virtual void ChoosePossibilities(List<GridBox> possibilities, Character character) 
        {
            var random = new Random();
            var newPosition = possibilities[random.Next(0, possibilities.Count)];

            GameManager.actualGame.MoveObject(character, newPosition);

            Console.WriteLine($"{character.Name} moved to {character.GetCurrentPlace().ToString()}");
        }

        /// <summary>
        /// Evaluates the possible and best movements that something on given position can do, following the behaviour principles.
        /// </summary>
        /// <param name="position">The actual position to be evaluated</param>
        /// <param name="targetPosition">The target position</param>
        /// <param name="grid">The grid that the movement will be made</param>
        /// <returns>A list of the best movements given the behaviour principles</returns>
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

        /// <summary>
        /// Says if a position is a good position of movement.
        /// </summary>
        /// <remarks>This method will define the principles of the behaviour</remarks>
        /// <param name="newPosition">The position to be evaluated</param>
        /// <param name="targetPosition">The target position</param>
        /// <param name="possibilities">The actual possibilities</param>
        /// <param name="grid">The grid that the movement will be made</param>
        /// <returns>True if the newPosition matches the principles of the behaviour</returns>
        protected abstract bool IsGoodPosition(GridBox newPosition, GridBox targetPosition, List<GridBox> possibilities, Grid grid);

        /// <summary>
        /// This method is for initialize anything before start a evaluation.
        /// </summary>
        /// <param name="position">The actual position that will be evaluated</param>
        /// <param name="targetPosition">The target position</param>
        /// <param name="grid">The grid that the movement will be made</param>
        protected abstract void StartedEvaluation(GridBox position, GridBox targetPosition, Grid grid);
    }
}
