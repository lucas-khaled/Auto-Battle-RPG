using AutoBattle.GameManagement;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Grids
{
    /// <summary>
    /// Base class to represent objects that can be inside a <c>Grid</c>
    /// </summary>
    public abstract class GridObject
    {
        public string Name { get; protected set; }

        private GridBox currentBox = new GridBox(-1, -1, null, -1);

        public GridObject(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Current position on Grid
        /// </summary>
        /// <remarks>Can be a non existed position in a Grid with -1 on X and Y</remarks>
        public GridBox GetCurrentPlace()
        {
            return currentBox;
        }

        public void SetCurrentPlace(GridBox currentBox) 
        {
            this.currentBox = currentBox;
        }
    }
}
