using System;
using System.Collections.Generic;
using ProjectElements.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FuncWorks.XNA.XTiled;
using GameplayElements.Managers;
using ProjectElements.IO;
using GameHelperLibrary;

namespace GameplayElements.Data         
{
    public class Level
    {
        public string name = "";
        public Map map;

        public bool[,] wallTile;

        public Tile[,] mapArr;
        public int tileWidth;
        public int tileHeight;

        public int width;
        public int height;
        public int widthInTiles;
        public int heightInTiles;

        #region Properties
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }

        public int TileWidth
        {
            get
            {
                if (map != null)
                    return map.TileWidth;
                else
                    return tileWidth;
            }
        }
        public int TileHeight
        {
            get
            {
                if (map != null)
                    return map.TileHeight;
                else
                    return tileHeight;
            }
        }
        #endregion

            Random rand = new Random();

        public string Name { get { return name; } }

        /// <summary>
        /// Generates a level randomly
        /// </summary>
        /// <param name="mapwidth"></param>
        /// <param name="mapheight"></param>
        public Level(int mapwidth, int mapheight, int tileWidth, int tileHeight)
        {
            mapArr = new Tile[mapwidth, mapheight];
            wallTile = new bool[mapwidth, mapheight];
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            width = mapwidth * TileWidth;
            height = mapheight * TileHeight;
            widthInTiles = width / TileWidth;
            heightInTiles = height / TileHeight;

            GenerateLevel();
        }
        private void GenerateLevel()
        {
            SpriteSheet tileSheet = TileSheetManager.TileSheets["TileSheet2"];

            for (int x = 0; x < mapArr.GetLength(0); x++)
                for (int y = 0; y < mapArr.GetLength(1); y++)
                {
                    mapArr[x, y] = new Tile(tileWidth, tileHeight, null);
                    mapArr[x, y].TrueLocation = new Vector2(x * tileWidth, y * tileHeight);
                    mapArr[x, y].IsWallTile = false;
                }

            for (int x = 0; x < mapArr.GetLength(0); x++)
            {
                for (int y = 0; y < mapArr.GetLength(1); y++)
                {
                    if (rand.NextDouble() > .9)
                    {
                        wallTile[x, y] = true;
                        mapArr[x, y].IsWallTile = true;
                        mapArr[x, y].Texture = tileSheet.GetSubImage(1, 1);
                    }
                    else
                    {
                        wallTile[x, y] = false;
                        mapArr[x, y].Texture = tileSheet.GetSubImage(0, 1);
                    }
                }
            }
        }

        /// <summary>
        /// Generates a level from a tmx file
        /// </summary>
        /// <param name="name">Name of the level</param>
        /// <param name="location">File location</param>
        public Level(string name, string location)
        {
            this.name = name;
            Map.InitObjectDrawing(ProjectData.Graphics.GraphicsDevice);
            map = LevelManager.Content.Load<Map>(location);

            width = map.Width * tileWidth;
            height = map.Height * tileHeight;

            wallTile = new bool[map.Width, map.Height];

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    wallTile[x, y] = map.TileLayers["Collision"].Tiles[x][y] != null ? true : false;
                }
            }

        }

        public virtual void Draw(SpriteBatch batch, Rectangle region)
        {
            if (map != null)
            {
                map.Draw(batch, region);
            }
            else
            {
                for (int x = 0; x < widthInTiles; x++)
                {
                    for (int y = 0; y < heightInTiles; y++)
                    {
                        if (Camera.IsOnCamera(new Rectangle(x * 32, y * 32, tileWidth, tileHeight)))
                            batch.Draw(mapArr[x, y].Texture, new Vector2(x * tileWidth,
                                y * tileHeight), Color.White);
                    }
                }
            }
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

    public class Tile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsWallTile { get; set; }
        public Texture2D Texture { get; set; }

        public Vector2 TrueLocation { get; set; }
        public Vector2 ScaledLocation
        {
            get { return new Vector2(TrueLocation.X / Width, TrueLocation.Y / Height); }
        }

        public Rectangle TrueBounds
        {
            get
            {
                return new Rectangle((int)TrueLocation.X, (int)TrueLocation.Y,
                    Width, Height);
            }
        }
        public Rectangle ScaledBounds
        {
            get
            {
                return new Rectangle((int)ScaledLocation.X, (int)ScaledLocation.Y,
                    1, 1);
            }
        }

        public Tile(int width, int height, Texture2D texture)
        {
            Width = width;
            Height = height;
        }
    }
}
