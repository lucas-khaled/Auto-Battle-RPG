using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.GameManagement
{
    public class Game
    {
        public Grid Grid { get; private set; }

        private List<Character> characters = new List<Character>();

        private bool started = false;

        public Game(Grid grid, params Character[] characters) 
        {
            Grid = grid;
            this.characters.AddRange(characters);
        }

        public void AddCharacter(Character character) 
        {
            characters.Add(character);
            character.SetCurrentPlace(GetRandomFreePosInGrid());
        }

        public void StartGame() 
        {
            started = true;
            PlaceCharacters(characters);
        }

        public void RunTurn() 
        {
            if (started is false) return;
        }

        public bool HasEnded() 
        {
            return started && characters.Count(x => x.IsDead is false) <= 1;
        }

        private void PlaceCharacters(List<Character> characters) 
        {
            characters.ForEach(character => character.SetCurrentPlace(GetRandomFreePosInGrid()));
        }

        public GridBox GetRandomFreePosInGrid() 
        {
            var random = new Random();
            int x = random.Next(0, Grid.xLenght);
            int y = random.Next(0, Grid.yLength);

            var box = Grid.GetBoxInPosition(x, y);

            if (box.ocupiedBy != null) 
                return GetRandomFreePosInGrid();

            return box;
        }
    }
}
