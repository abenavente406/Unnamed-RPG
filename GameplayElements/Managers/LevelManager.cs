using System;
using System.Collections.Generic;
using GameplayElements.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameplayElements.Managers
{
    public class LevelManager
    {
        public static Dictionary<string, Level> levels = new Dictionary<string, Level>();
        public static ContentManager Content;

        private static Level currentLevel = null;

        public LevelManager(ContentManager content)
        {
            Content = content;
            levels.Add("MainWorld", new Level("Main", "Levels\\world_1"));
            SetCurrentLevel("MainWorld");
        }

        public static Level GetCurrentLevel()
        {
            return currentLevel;
        }

        public static void SetCurrentLevel(string name)
        {
            currentLevel = levels[name];
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch batch)
        {
            currentLevel.Draw(batch, Camera.ViewPortRectangle);
        }
    }
}
