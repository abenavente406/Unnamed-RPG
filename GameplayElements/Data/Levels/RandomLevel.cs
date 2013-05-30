using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using ProjectElements.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameplayElements.Managers;

namespace GameplayElements.Data
{
    public class RandomLevel: Level
    {
        public RandomLevel(string name, int width, int height, int tileWidth, int tileHeight)
            : base(width, tileHeight, tileWidth, tileHeight)
        {
            this.name = name;

            GenerateLevel();
        }

        private void GenerateLevel()
        {
            SpriteSheet tileSheet = TileSheetManager.TileSheets["TileSheet2"];
            Random rand = new Random();

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

        public override void Draw(SpriteBatch batch, Rectangle region)
        {
            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    if (Camera.IsOnCamera(new Rectangle(x * 32, y * 32, tileWidth, tileHeight)))
                        batch.Draw(mapArr[x, y].Texture, new Vector2(x * tileWidth,
                            y * tileHeight), Color.White);
                }
            }
        }
    }
}
