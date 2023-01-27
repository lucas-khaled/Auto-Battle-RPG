namespace AutoBattle.GameManagement
{
    /// <summary>
    /// Represents a Team with a name and ID
    /// </summary>
    public struct Team
    {
        public int ID;
        public string Name;

        public Team(int ID, string name)
        {
            this.ID = ID;
            Name = name;
        }
    }
}
