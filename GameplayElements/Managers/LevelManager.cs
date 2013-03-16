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
            SetCurrentLevel(new Dungeon(80, 60, 32, 32) as Level);

            em = new EntityManager(data);

            if (data != null)
                LoadSave(data);

            //LoadMonsters();

        }
        void LoadMonsters()
        {
            foreach (Room r in ((Dungeon)currentLevel).rooms)
            {
                EntityManager.AddMonster(new Skeleton(new Vector2(rand.Next((int)r.Position.X,
                    (int)r.Position.X + r.Width - currentLevel.tileWidth), rand.Next((int)r.Position.Y,
                    (int)r.Position.Y + r.Height - currentLevel.tileHeight))));
            }
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

        public static bool IsWallTile(float x, float y, int width, int height)
        {
            int atx1 = (int)(x) / currentLevel.tileWidth;
            int atx2 = (int)(x + width) / currentLevel.tileWidth;
            int aty1 = (int)(y + height / 2) / currentLevel.tileHeight;
            int aty2 = (int)(y + height) / currentLevel.tileHeight;

            if (IsTileBlocked(atx1, aty1))
                return true;
            if (IsTileBlocked(atx1, aty2))
                return true;
            if (IsTileBlocked(atx2, aty1))
                return true;
            if (IsTileBlocked(atx2, aty2))
                return true;

            return false;
        }
        public static bool IsWallTile(Vector2 testPos)
        {
            int testX = (int)PointToTile(testPos).X;
            int testY = (int)PointToTile(testPos).Y;

            return IsTileBlocked(testX, testY);
        }
        public static bool IsTileBlocked(int tx, int ty)
        {
            return currentLevel.mapArr[tx, ty].IsWallTile;
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
