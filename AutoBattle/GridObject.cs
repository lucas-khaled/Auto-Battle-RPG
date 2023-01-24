using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle
{
    public abstract class GridObject
    {
        public string Name { get; protected set; }
        protected GridBox currentBox;

        public GridObject(string name) 
        {
            this.Name = name;
        }

        public void SetCurrentPlace(GridBox box)
        {
            if(box.ocupiedBy != null)
            {
                Console.Write($"Cannot move to ({box.xIndex},{box.yIndex}) because it's already occupied");
                return;
            }

            currentBox.ocupiedBy = null;
            box.ocupiedBy = this;
            currentBox = box;
        }

        public GridBox GetCurrentPlace() 
        {
            return currentBox;
        }
    }
}
