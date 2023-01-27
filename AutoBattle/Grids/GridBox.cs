namespace AutoBattle.Grids
{
    public struct GridBox
    {
        public int X;
        public int Y;

        /// <summary>
        /// <c>GridObject</c> that is actually ocupying this box
        /// </summary>
        public GridObject ocupiedBy;

        /// <summary>
        /// Variable that holds a Index of a grid
        /// </summary>
        public int Index;

        /// <summary>
        /// Inidicates if it is a possible position
        /// </summary>
        public bool exists;

        public GridBox(int x, int y, GridObject ocupiedBy, int index)
        {
            X = x;
            Y = y;
            this.ocupiedBy = ocupiedBy;
            this.Index = index;
            exists = x >= 0 && y >= 0 && index >= 0;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
