using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static AutoBattle.Types;

namespace AutoBattle.GameManagement
{
    public class Game
    {
        public Grid Grid { get; private set; }
        public int Turn { get; private set; }

        private GameGenerator generator = new GameGenerator();
        private List<Character> characters = new List<Character>();
        private List<Team> teams;
        private bool started = false;

        private const string PLAYER_TEAM_NAME = "Player Party";
        private const string ENEMY_TEAM_NAME = "Bad Guys";
        private const int TEAM_MEMBERS_QUANTITY = 2;

        public void AddCharacter(Character character, Team team) 
        {
            character.Team = team;
            characters.Add(character);
            MoveObject(character, GetRandomFreePosInGrid());
        }

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

        public void EndGame() 
        {
            var winningTeam = characters.First(x => x.IsDead is false).Team;
            Console.WriteLine($"\n\n         The game has ended and {winningTeam.Name} won!           \n\n");
            GameEvents.onCharacterDeath -= OnCharacterDeath;
        }

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

        public bool HasEnded() 
        {
            var teamsAlive = characters.Where(c => c.IsDead is false).Select(c => c.Team).GroupBy(team => team.ID).ToList();
            return started && (teamsAlive.Count <= 1);
        }

        public void MoveObject(GridObject gridObject, GridBox box) 
        {
            if (box.ocupiedBy != null)
            {
                Console.Write($"Cannot move to {box.ToString()} because it's already occupied");
                return;
            }

            GridBox currentBox = gridObject.currentBox;
            currentBox.ocupiedBy = null;

            box.ocupiedBy = gridObject;

            Grid.SetPosition(box);
            Grid.SetPosition(currentBox);
            Grid.DrawBattlefield();

            gridObject.currentBox = box;

            GameEvents.onObjectMoved?.Invoke(gridObject);
        }

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

            AddCharacter(generator.GetPlayerChoice(), playerTeam);

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
