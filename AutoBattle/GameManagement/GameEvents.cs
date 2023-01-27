using AutoBattle.Characters;
using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.GameManagement
{
    public static class GameEvents
    {
        public static Action<GridObject> onObjectMoved;
        public static Action<Character> onCharacterDeath;
    }
}
