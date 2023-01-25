using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using System.Drawing;
using AutoBattle.GameManagement;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> boxes { get; private set; } = new List<GridBox>();
        public int xLength;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLength = Lines;
            yLength = Columns;
            Console.WriteLine($"The battle field has been created as {xLength} X {yLength}\n");
            for (int y = 0; y < Lines; y++)
            {
                for(int x = 0; x < Columns; x++)
                {
                    GridBox newBox = new GridBox(x, y, null, (Columns * y + x));
                    boxes.Add(newBox);
                }
            }
        }

        public void SetPosition(GridBox box) 
        {
            if (box.Index < 0 || box.Index >= boxes.Count) return;

            int index = box.Index;
            boxes[index] = box;
        }

        public float CalculateDistance(GridBox from, GridBox to)
        {
            return (float)Math.Sqrt(Math.Pow(from.xIndex - to.xIndex, 2) + Math.Pow(from.yIndex - to.yIndex, 2));
        }

        public bool IsInRange(GridBox from, GridBox to, int range) 
        {
            var actualRange = Math.Floor(CalculateDistance(from, to));
            return actualRange <= range;
        }

        public GridBox GetBoxInPosition(int x, int y) 
        {
            int WrapX = ((x % xLength) + xLength) % xLength;
            int WrapY = ((y % yLength) + yLength) % yLength;
            int index = xLength * WrapY + WrapX;
            return boxes[index];
        }

        // prints the matrix that indicates the tiles of the battlefield
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
                        Console.Write($"[{currentgrid.ocupiedBy.Name[0]}]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
