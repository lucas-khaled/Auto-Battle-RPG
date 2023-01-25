using System;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Characters;
using AutoBattle.GameManagement;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);

            Character player =  GetPlayerChoice();
            Character enemy = CreateEnemyCharacter();
            GameManager.StartNewGame(grid, player, enemy);

            Character GetPlayerChoice()
            {
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                
                string choice = Console.ReadLine();
                CharacterClass characterClass = (CharacterClass)int.Parse(choice);

                return CharacterFactory.GenerateCharacter(characterClass, "Player");
            }

            Character CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");

                return CharacterFactory.GenerateCharacter(enemyClass, "Enemy");
            }
        }
    }
}
