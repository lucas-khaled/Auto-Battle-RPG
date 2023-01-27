using AutoBattle.Characters;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoBattle.GameManagement
{
    /// <summary>
    /// Runs and manage the logic for a game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The actual game. <c>Grid</c>
        /// </summary>
        public Grid Grid { get; private set; }

        /// <summary>
        /// The actual game turn. It will be incremented after each turn has passed.
        /// </summary>
        public int Turn { get; private set; }

        private GameGenerator generator = new GameGenerator();
        private List<Character> characters = new List<Character>();
        private List<Team> teams;
        private bool started = false;

        private const string PLAYER_TEAM_NAME = "Player Party";
        private const string ENEMY_TEAM_NAME = "Bad Guys";
        private const int TEAM_MEMBERS_QUANTITY = 2;

        /// <summary>
        /// Adds a new character to this game.
        /// </summary>
        /// <param name="character">The character that will be added.</param>
        /// <param name="team">The character's team he will be associated.</param>
        public void AddCharacter(Character character, Team team) 
        {
            character.Team = team;
            characters.Add(character);
            MoveObject(character, GetRandomFreePosInGrid());
        }

        /// <summary>
        /// Setups the game so it can start.
        /// </summary>
        public void StartGame() 
        {
            GameEvents.onCharacterDeath += OnCharacterDeath;

            Grid = generator.GetGridChoice();
            teams = generator.CreateTeams(PLAYER_TEAM_NAME, ENEMY_TEAM_NAME);

            CreatePlayerTeam();
            CreateEnemyTeam();

            started = true;
            characters.Shuffle();
        }

        /// <summary>
        /// Ends the game and do whatever is necessary on it's end.
        /// </summary>
        public void EndGame() 
        {
            var winningTeam = characters.First(x => x.IsDead is false).Team;
            Console.WriteLine($"\n\n         The game has ended and {winningTeam.Name} won!           \n\n");
            GameEvents.onCharacterDeath -= OnCharacterDeath;
        }

        /// <summary>
        /// Run the next turn of the game and make all characters do their turns to.
        /// </summary>
        public void RunTurn() 
        {
            if (started is false) return;

            Turn++;
            Console.WriteLine("TURN: "+Turn);
            foreach (var character in characters) 
            {
                Thread.Sleep(500);
                character.DoTurn();
            }
                
        }

        /// <summary>
        /// Says if the game has the condition to end or not
        /// </summary>
        /// <returns>If the can be finished</returns>
        public bool CanEnd() 
        {
            var teamsAlive = characters.Where(c => c.IsDead is false).Select(c => c.Team).GroupBy(team => team.ID).ToList();
            return started && (teamsAlive.Count <= 1);
        }

        /// <summary>
        /// Says if the game has the condition to end or not
        /// </summary>
        /// <returns>If the can be finished</returns>
        public void MoveObject(GridObject gridObject, GridBox box) 
        {
            if (box.ocupiedBy != null)
            {
                Console.Write($"Cannot move to {box.ToString()} because it's already occupied");
                return;
            }

            GridBox currentBox = gridObject.GetCurrentPlace();
            currentBox.ocupiedBy = null;

            box.ocupiedBy = gridObject;

            Grid.SetPosition(box);
            Grid.SetPosition(currentBox);
            Grid.DrawBattlefield();

            gridObject.SetCurrentPlace(box);

            GameEvents.onObjectMoved?.Invoke(gridObject);
        }

        /// <summary>
        /// Gets a random position in actual grid that is not occupied.
        /// </summary>
        public GridBox GetRandomFreePosInGrid()
        {
            var random = new Random();
            int x = random.Next(0, Grid.xLength);
            int y = random.Next(0, Grid.yLength);

            var box = Grid.GetBoxInPosition(x, y);

            if (box.ocupiedBy != null)
                return GetRandomFreePosInGrid();

            return box;
        }

        private void OnCharacterDeath(Character character) 
        {
            GridBox outOfMapBox = new GridBox(-1, -1, null, -1);
            MoveObject(character, outOfMapBox);
        }

        private void CreatePlayerTeam() 
        {
            Console.WriteLine($"\n Creating {PLAYER_TEAM_NAME}\n\n");

            Team playerTeam = teams.Find(x => x.Name == PLAYER_TEAM_NAME);

            AddCharacter(generator.GetPlayerCharacterChoice(), playerTeam);

            for(int i = 1; i< TEAM_MEMBERS_QUANTITY; i++) 
                AddCharacter(generator.CreateNPC(), playerTeam);
        }

        private void CreateEnemyTeam()
        {
            Console.WriteLine($"\n Creating {ENEMY_TEAM_NAME}\n\n");

            Team enemyTeam = teams.Find(x => x.Name == ENEMY_TEAM_NAME);

            for (int i = 0; i < TEAM_MEMBERS_QUANTITY; i++)
                AddCharacter(generator.CreateNPC(), enemyTeam);
        }
    }
}
