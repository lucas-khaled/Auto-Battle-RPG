namespace AutoBattle.GameManagement
{
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
