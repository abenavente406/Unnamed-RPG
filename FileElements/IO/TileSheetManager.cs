using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectElements.IO
{
    /// <summary>
    /// Manages all the tilesheets in the game
    /// </summary>
    public class TileSheetManager
    {
        static Dictionary<string, SpriteSheet> tileSheets = new Dictionary<string, SpriteSheet>();
        public static Dictionary<string, SpriteSheet> TileSheets
        {
            get { return tileSheets; }
        }

        private GraphicsDevice graphics;

        public TileSheetManager(GraphicsDevice graphics, ContentManager content)
        {
            this.graphics = graphics;
            Initialize(content);
        }

        private void Initialize(ContentManager content)
        {
            AddTileSheet(content, "Levels\\TileSheets\\Spritesheet (Huge)", "TileSheet1", 32, 32);
            AddTileSheet(content, "Levels\\TileSheets\\fantasy-tileset", "TileSheet2", 32, 32);
        }

        private void AddTileSheet(ContentManager content, string assetName, string sheetName,
                                    int tileWidth, int tileHeight)
        {
            tileSheets.Add(sheetName, new SpriteSheet(content.Load<Texture2D>(assetName), 
                tileWidth, tileHeight, graphics));
        }
    }
}
