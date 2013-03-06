using System;
using System.Collections.Generic;
using ProjectElements.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FuncWorks.XNA.XTiled;
using GameplayElements.Managers;

namespace GameplayElements.Data
{
    public class Level
    {
        public string name;
        private Map map;

        public bool[,] wallTile;

        #region Properties
        public int Width { get { return map.Width * map.TileWidth; } }
        public int Height { get { return map.Height * map.TileHeight; } }

        public int TileWidth { get { return map.TileWidth; } }
        public int TileHeight { get { return map.TileHeight; } }
        #endregion

        public string Name { get { return name; } }

        public Level(string name, string location)
        {
            this.name = name;
            Map.InitObjectDrawing(ProjectData.Graphics.GraphicsDevice);
            map = LevelManager.Content.Load<Map>(location);

            wallTile = new bool[map.Width, map.Height];

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    wallTile[x, y] = map.TileLayers["Collision"].Tiles[x][y] != null ? true : false;
                }
            }

        }

        public void Draw(SpriteBatch batch, Rectangle region)
        {
            map.Draw(batch, region);
        }

        public void DrawLayer(SpriteBatch batch, Rectangle region, int layerId)
        {
            map.DrawLayer(batch, layerId, region, 0.0f);
        }


        public TileLayerList GetLayers()
        {
            return map.TileLayers;
        }

    }
}
