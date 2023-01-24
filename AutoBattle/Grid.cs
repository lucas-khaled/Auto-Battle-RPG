using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using System.Drawing;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> boxes = new List<GridBox>();
        public int xLenght;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    Console.Write($"{newBox.Index}\n");
                    boxes.Add(newBox);
                }
            }
        }

        public GridBox GetBoxInPosition(int x, int y) 
        {
            int WrapX = ((x % xLenght) + xLenght) % xLenght;
            int WrapY = ((y % yLength) + yLength) % yLength;
            int index = yLength * WrapX + WrapY;
            return boxes[index];
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox currentgrid = new GridBox();
                    if (currentgrid.ocupied)
                    {
                        Console.Write("[X]\t");
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
