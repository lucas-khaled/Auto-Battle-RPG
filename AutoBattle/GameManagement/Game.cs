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
        public int Turn { get; private set; }

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
            MoveObject(character, GetRandomFreePosInGrid());
        }

        public void StartGame() 
        {
            started = true;

            //characters.Sort();
            PlaceCharacters(characters);
        }

        public void EndGame() 
        {
            
        }

        public void RunTurn() 
        {
            if (started is false) return;

            Turn++;
            Console.WriteLine("TURN: "+Turn);
            foreach (var character in characters)
                character.DoTurn();
        }

        public bool HasEnded() 
        {
            return started && (characters.Count(x => x.IsDead is false) <= 1);
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

        private void PlaceCharacters(List<Character> characters) 
        {
            characters.ForEach(character => MoveObject(character, GetRandomFreePosInGrid()));
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
    }
}
