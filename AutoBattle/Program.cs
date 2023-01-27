using System;
using static AutoBattle.Grids.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Characters;
using AutoBattle.GameManagement;
using System.Threading;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager.StartNewGame();
        }
    }
}
