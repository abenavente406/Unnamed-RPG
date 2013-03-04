using System;
using System.Collections.Generic;
using GameplayElements.Data;
using Microsoft.Xna.Framework.Content;

namespace GameplayElements.Managers
{
    public class LevelManager
    {
        public static Dictionary<string, Level> levels = new Dictionary<string, Level>();
        public static ContentManager content;

        private static Level currentLevel = null;

        public LevelManager(ContentManager content)
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
