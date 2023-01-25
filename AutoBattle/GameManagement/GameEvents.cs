using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.GameManagement
{
    public static class GameEvents
    {
        public static Action<GridObject> onObjectMoved;
    }
}
