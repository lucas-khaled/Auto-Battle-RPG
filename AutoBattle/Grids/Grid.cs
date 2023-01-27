using System;
using System.Collections.Generic;

namespace AutoBattle.Grids
{
    public class Grid
    {
        /// <summary>
        /// List of ordered positions of the Grid
        /// </summary>
        /// <remarks>The index are calculated by <c>xLength * y + x</c></remarks>
        public List<GridBox> boxes { get; private set; } = new List<GridBox>();

        /// <summary>
        /// The number of columns of the grid
        /// </summary>
        public int xLength;

        /// <summary>
        /// The number of lines of the grid
        /// </summary>
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLength = Columns;
            yLength = Lines;
            Console.WriteLine($"\nThe battle field has been created as {xLength} X {yLength}\n");
            for (int y = 0; y < Lines; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    GridBox newBox = new GridBox(x, y, null, Columns * y + x);
                    boxes.Add(newBox);
                }
            }
        }

        /// <summary>
        /// Updates a position inside the grid
        /// </summary>
        /// <param name="box">The position to be updated. Must exist and have a index inside the size of the Grid</param>
        public void SetPosition(GridBox box)
        {
            if (box.exists is false || box.Index >= boxes.Count) return;

            int index = box.Index;
            boxes[index] = box;
        }

        /// <summary>
        /// Calculate a distance between two positions on a Grid
        /// </summary>
        /// <returns>Pythagorean theorem of the positions</returns>
        public float CalculateDistance(GridBox from, GridBox to)
        {
            return (float)Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

        /// <summary>
        /// Method to calculate if the distance between two positions are in a certain range
        /// </summary>
        /// <remarks>Floors the distance to compare with the range</remarks>
        public bool IsInRange(GridBox from, GridBox to, int range)
        {
            var actualRange = Math.Floor(CalculateDistance(from, to));
            return actualRange <= range;
        }

        /// <summary>
        /// Returns the <c>GridBox</c> representing the indicated postion
        /// </summary>
        public GridBox GetBoxInPosition(int x, int y)
        {
            int WrapX = (x % xLength + xLength) % xLength;
            int WrapY = (y % yLength + yLength) % yLength;
            int index = xLength * WrapY + WrapX;
            return boxes[index];
        }

        /// <summary>
        /// Prints the matrix that indicates the tiles of the battlefield
        /// </summary>
        public void DrawBattlefield()
        {
            Console.Write(Environment.NewLine + Environment.NewLine);
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    GridBox currentgrid = boxes[xLength * y + x];
                    if (currentgrid.ocupiedBy != null)
                    {
                        Console.Write($"[{currentgrid.ocupiedBy.Name.Substring(0, 3)}]\t");
                    }
                    else
                    {
                        Console.Write($"[   ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
