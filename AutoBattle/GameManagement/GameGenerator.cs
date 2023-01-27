using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;
using System.Threading;

namespace AutoBattle.GameManagement
{
    public class GameGenerator
    {
        private const int MIN_GRID_SIZE = 4;
        private const int MAX_GRID_SIZE = 15;

        private List<string> randomNames = new List<string>
            {
                "Jão", "Robervaldo", "Grória", "Ronaldinho Gaúcho", "Dumb John",
                "Mighty One", "Jalim Rabei", "Cuca Beludo", "Gh0st Kn1g4t 666", "System of a Down",
                "Tássia Chando", "Jusepa", "Baby Shark", "DovahKiin", "Kratos", "Your mom"
            };

        public Grid GetGridChoice()
        {
            Console.Write("\nWrite the x size of the battlefield : ");
            int x = Math.Clamp(int.Parse(Console.ReadLine()), MIN_GRID_SIZE, MAX_GRID_SIZE);

            Console.Write("\nWrite the y size of the battlefield : ");
            int y = Math.Clamp(int.Parse(Console.ReadLine()), MIN_GRID_SIZE, MAX_GRID_SIZE);

            return new Grid(x, y);
        }

        public Character GetPlayerChoice()
        {
            Console.WriteLine("Choose Between One of this Classes:\n");
            Console.Write("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer    ");

            string choice = Console.ReadLine();
            CharacterClass characterClass = (CharacterClass)int.Parse(choice);

            Thread.Sleep(500);
            Console.WriteLine($"Your class choice: {characterClass}");

            return CharacterFactory.GenerateCharacter(characterClass, "Player");
        }

        public Character CreateNPC(string name)
        {
            Thread.Sleep(1000);

            var rand = new Random();
            int randomInteger = rand.Next(1, 4);

            CharacterClass npcClass = (CharacterClass)randomInteger;
            Console.WriteLine($"\n{name} was created with class {npcClass}");

            Thread.Sleep(500);

            return CharacterFactory.GenerateCharacter(npcClass, name);
        }

        public Character CreateNPC() 
        {
            string name = randomNames[new Random().Next(0, randomNames.Count)];
            randomNames.Remove(name);
            return CreateNPC(name);
        }

        public List<Team> CreateTeams(params string[] names) 
        {
            List<Team> teams = new List<Team>();
            int id = 1;

            foreach(var name in names) 
            {
                teams.Add(new Team(id, name));
                id++;
            }

            return teams;
        }
    }
}
