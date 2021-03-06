﻿using System;
using System.Collections.Generic;
using GameplayElements.Data;
using GameplayElements.Data.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameplayElements.Data.Entities.Monsters;
using ProjectElements.Data;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Input;
using GameplayElements.Data.Entities.NPCs;

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

        Texture2D pathSquare;

        public LevelManager(ContentManager content, SaveData data, int levelType = 1)
        {
            Content = content;

            switch (levelType)
            {
                case 0:
                    levels.Add("RandomTestLevel", new RandomLevel("RandomTestLevel", 80, 60, 32, 32));
                    SetCurrentLevel("RandomTestLevel");
                    break;
                case 1:
                    levels.Add("RandomTestDungeon", new Dungeon(80, 60, 32, 32));
                    SetCurrentLevel("RandomTestDungeon");
                    break;
            }

            em = new EntityManager(data);

            if (data != null)
                LoadSave(data);

            LoadMonsters();
            EntityManager.AddNpc(new Citizen(new Vector2(128, 128)));
            pathSquare = Content.Load<Texture2D>("Test\\square");

        }

        void LoadMonsters()
        {
            do
            {
                var randPos = new Vector2(rand.Next(currentLevel.WidthInTiles),
                    rand.Next(currentLevel.HeightInTiles));
                if (!currentLevel.mapArr[(int)randPos.X, (int)randPos.Y].IsWallTile)
                    EntityManager.AddMonster(new Skeleton(TileToPoint(randPos)));
                else
                    continue;
            } while (EntityManager.monsters.Count < 10);
            
        }

        public void Update(GameTime gameTime)
        {
            em.UpdateAll(gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentLevel.Draw(batch, Camera.ViewPortRectangle);
            em.Draw(batch, gameTime);
        }

        public static Vector2 PointToTile(Vector2 point)
        {
            return point / new Vector2(currentLevel.TileWidth, currentLevel.TileHeight);
        }

        public static Vector2 TileToPoint(Vector2 point)
        {
            return new Vector2(point.X * currentLevel.tileWidth, point.Y * currentLevel.tileHeight);
        }

        public static bool IsWallTile(float x, float y, int width, int height)
        {
            int atx1 = (int)MathHelper.Clamp((x) / currentLevel.tileWidth, 0, currentLevel.WidthInTiles - 1);
            int atx2 = (int)MathHelper.Clamp((x + width) / currentLevel.tileWidth, 0, currentLevel.WidthInTiles - 1);
            int aty1 = (int)MathHelper.Clamp((y + height / 2) / currentLevel.tileHeight, 0, currentLevel.HeightInTiles - 1);
            int aty2 = (int)MathHelper.Clamp((y + height) / currentLevel.tileHeight, 0, currentLevel.HeightInTiles - 1);

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

            if (testX > currentLevel.WidthInTiles - 1 || testX < 0)
                return true;
            else if (testY > currentLevel.HeightInTiles - 1 || testY < 0)
                return true;

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
            EntityManager.player.ExperienceLevel = data.ExperienceLevel;
            EntityManager.player.ExperiencePoints = data.ExperiencePoints;
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
