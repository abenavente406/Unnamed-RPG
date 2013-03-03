using System;
using System.Collections.Generic;
using TiledSharp;
using GameplayElements.Data;

namespace GameplayElements.Managers
{
    public class LevelManager
    {
        public static Dictionary<string, Level> levels = new Dictionary<string, Level>();

        private static Level currentLevel = null;

        public LevelManager()
        {

        }

        public static Level GetCurrentLevel()
        {
            return currentLevel;
        }

        public static void SetCurrentLevel(string name)
        {
            currentLevel = levels[name];
        }
    }
}
