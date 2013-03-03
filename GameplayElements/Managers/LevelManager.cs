using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameplayElements.Data;

namespace GameplayElements.Managers
{
    public class LevelManager
    {
        public Dictionary<string, Level> levels = new Dictionary<string, Level>();

        private static Level currentLevel;

        public LevelManager()
        {

        }

        public static Level GetCurrentLevel()
        {
            return currentLevel;
        }
    }
}
