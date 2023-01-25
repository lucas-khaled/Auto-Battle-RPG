using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle
{
    public abstract class GridObject
    {
        public string Name { get; protected set; }
        public GridBox currentBox;

        public GridObject(string name) 
        {
            this.Name = name;
        }

        public GridBox GetCurrentPlace() 
        {
            return currentBox;
        }
    }
}
