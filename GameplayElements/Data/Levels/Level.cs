using System;
using System.Collections.Generic;
using ProjectElements.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameplayElements.Managers;
using ProjectElements.IO;
using GameHelperLibrary;

namespace GameplayElements.Data         
{
    public abstract class Level
    {
        #region Fields
        public string name = "";
        public bool[,] wallTile;
        public Tile[,] mapArr;
        public int tileWidth;
        public int tileHeight;
        public int width;
        public int height;
        Random rand = new Random();
        #endregion

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
                    return tileWidth;
            }
        }
        public int TileHeight
        {
            get
            {
                    return tileHeight;
            }
        }
        public int WidthInTiles
        {
            get { return Width / TileWidth; }
        }
        public int HeightInTiles
        {
            get { return Height / TileHeight; }
        }
        public string Name { get { return name; } }
        #endregion

        #region Constructor and Initialization
        /// <summary>
        /// Generates a level with default attributes at 0
        /// </summary>
        public Level()
        {
            mapArr = new Tile[0, 0];
            wallTile = new bool[0, 0];
            tileWidth = 0;
            tileHeight = 0;
            width = 0;
            height = 0;
        }


        /// <summary>
        /// Generates a level
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
        }
        #endregion

        #region Drawing
        public abstract void Draw(SpriteBatch batch, Rectangle region);
        #endregion
    }
}
