using System;
using System.Collections.Generic;
using GameplayElements.Data;
using GameplayElements.Data.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameplayElements.Data.Entities.Monsters;
using ProjectElements.Data;

namespace GameplayElements.Managers
{

    public enum LayerID 
    {
        GROUND = 0,
        BACKLAYER = 1,
        FORELAYER = 2,
        COLLISION = 3
    }

    public class LevelManager
    {
        public static Dictionary<string, Level> levels = new Dictionary<string, Level>();
        public static ContentManager Content;
        public static EntityManager em;

        private static Level currentLevel = null;

        Random rand = new Random();

        public LevelManager(ContentManager content, SaveData data)
        {
            Content = content;
            //levels.Add("MainWorld", new Level("MainWorlds", "Levels\\world_1"));
            //SetCurrentLevel("MainWorld");
            SetCurrentLevel(new Level(50, 30, 32, 32));

            em = new EntityManager(data);

            if (data != null)
                LoadSave(data);

            EntityManager.AddMonster(new Skeleton(new Vector2(rand.Next(400), rand.Next(400))));

        }

        public void Update(GameTime gameTime)
        {
            em.UpdateAll(gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (currentLevel.map != null)
            {
                currentLevel.DrawLayer(batch, Camera.ViewPortRectangle, (int)LayerID.GROUND);
                currentLevel.DrawLayer(batch, Camera.ViewPortRectangle, (int)LayerID.BACKLAYER);
                em.Draw(batch, gameTime);
                currentLevel.DrawLayer(batch, Camera.ViewPortRectangle, (int)LayerID.FORELAYER);

                // Enable this if you want to see the collision bounds of tiles
                currentLevel.DrawLayer(batch, Camera.ViewPortRectangle, (int)LayerID.COLLISION);
            }
            else
            {
                currentLevel.Draw(batch, Camera.ViewPortRectangle);
                em.Draw(batch, gameTime);
            }
        }

        public static Vector2 PointToTile(Vector2 point)
        {
            return point / new Vector2(currentLevel.TileWidth, currentLevel.TileHeight);
        }

        public static bool IsWallTile(Vector2 testPos)
        {

            int testX = (int)PointToTile(testPos).X;
            int testY = (int)PointToTile(testPos).Y;

            return currentLevel.wallTile[testX, testY];
        }

        public void LoadSave(SaveData data)
        {
            EntityManager.player.God = data.GodModeEnabled;
            EntityManager.player.Name = data.Name;
            EntityManager.player.NoClip = data.NoClipEnabled;
            EntityManager.player.Health = data.Health;
            EntityManager.player.Position = data.Position;
            EntityManager.player.Direction = data.Direction;
            EntityManager.player.SuperSpeed = data.SuperSpeedEnabled;
        }

        public static Level GetCurrentLevel()
        {
            return currentLevel;
        }

        public static void SetCurrentLevel(Level level)
        {
            currentLevel = level;
        }
        public static void SetCurrentLevel(string name)
        {
            currentLevel = levels[name];
        }

    }
}
