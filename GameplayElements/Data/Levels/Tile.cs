using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data
{
    public class Tile
    {
        #region Properties
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
        #endregion

        #region Constructor and Initialization
        public Tile(int width, int height, Texture2D texture)
        {
            Width = width;
            Height = height;
        }
        #endregion

    }
}
