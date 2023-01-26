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

        private List<Character> characters = new List<Character>();
        private bool started = false;

        private const int MIN_GRID_SIZE = 4;
        private const int MAX_GRID_SIZE = 15;

        public void AddCharacter(Character character) 
        {
            characters.Add(character);
            MoveObject(character, GetRandomFreePosInGrid());
        }

        public void StartGame() 
        {
            Grid = GetGridChoice();

            AddCharacter(GetPlayerChoice());
            AddCharacter(CreateEnemyCharacter());

            started = true;
            characters.Shuffle();
        }

        public void EndGame() 
        {
            var winningCharacter = characters.First(x => x.IsDead is false);
            if (winningCharacter == null)
            {
                Console.WriteLine("\n\n         The game has ended and nobody won           ");
                return;
            }

            Console.WriteLine($"\n\n         The game has ended and {winningCharacter.Name} won!           \n\n");
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
            Console.WriteLine("\nPlacing Characters...");
            characters.ForEach(character =>
            {
                Thread.Sleep(1000);

                Console.WriteLine($"\nPlacing {character.Name}...");

                Thread.Sleep(500);

                MoveObject(character, GetRandomFreePosInGrid());
            });
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

        private Grid GetGridChoice() 
        {
            Console.Write("\nWrite the x size of the battlefield : ");
            int x = Math.Clamp(int.Parse(Console.ReadLine()), MIN_GRID_SIZE, MAX_GRID_SIZE);

            Console.Write("\nWrite the y size of the battlefield : ");
            int y = Math.Clamp(int.Parse(Console.ReadLine()), MIN_GRID_SIZE, MAX_GRID_SIZE);

            return new Grid(x, y);
        }

        private Character GetPlayerChoice()
        {
            Console.WriteLine("Choose Between One of this Classes:\n");
            Console.Write("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer    ");

            string choice = Console.ReadLine();
            CharacterClass characterClass = (CharacterClass)int.Parse(choice);

            Thread.Sleep(500);
            Console.WriteLine($"Your class Choice: {characterClass}");

            return CharacterFactory.GenerateCharacter(characterClass, "Player");
        }

        private Character CreateEnemyCharacter()
        {
            Thread.Sleep(500);

            var rand = new Random();
            int randomInteger = rand.Next(1, 4);

            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"\nEnemy Class Choice: {enemyClass}");

            Thread.Sleep(500);

            return CharacterFactory.GenerateCharacter(enemyClass, "Enemy");
        }
    }
}
