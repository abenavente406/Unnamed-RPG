using System;
using System.Collections.Generic;
using GameplayElements.Data;
using GameplayElements.Data.Entities;
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
            foreach (FuncWorks.XNA.XTiled.TileLayer layer in currentLevel.GetLayers())
            {
                if (!(layer.Name == "Collision"))
                    currentLevel.DrawLayer(batch, Camera.ViewPortRectangle, GetCurrentLevel().GetLayers().IndexOf(layer));
            }
        }

        public static bool IsWallTile(Entity entity)
        {
            if (entity.Position.X < entity.RealWidth || entity.Position.Y < entity.RealHeight)
                if (entity.Position.X < 0 || entity.Position.Y < 0)
                    return true;
                else
                    return false;
            else
                return currentLevel.wallTile[(int)(entity.Position.X - entity.SpriteWidth) / currentLevel.TileWidth,
                    (int)(entity.Position.Y - entity.SpriteHeight) / currentLevel.TileHeight];
        }
    }
}
