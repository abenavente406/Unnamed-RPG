using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameHelperLibrary;

namespace ProjectElements.IO
{
    public class SpriteSheetManager
    {

        private static Dictionary<string, SpriteSheet> entitySprites = 
            new Dictionary<string,SpriteSheet>();
        private static Dictionary<string, SpriteSheet> itemSprites = 
            new Dictionary<string,SpriteSheet>();

        public static Dictionary<string, SpriteSheet> EntitySprites
        {
            get { return entitySprites; }
        }
        public static Dictionary<string, SpriteSheet> ItemSprites
        {
            get { return ItemSprites; }
        }

        private GraphicsDevice graphics;

        public SpriteSheetManager(GraphicsDevice graphics, ContentManager content)
        {
            this.graphics = graphics;
            Initialize(content);
        }

        private void Initialize(ContentManager content)
        {
            AddEntitySheet(content, "Sprites\\EntitySprites", "Entity Sprites 1", 32, 32);
        }


        public void AddEntitySheet(ContentManager content, string assetName, 
                                    string sheetName, int spriteWidth, int spriteHeight)
        {
            entitySprites.Add(sheetName, new SpriteSheet(content.Load<Texture2D>(assetName), 
                spriteWidth, spriteHeight, graphics));
        }

        public void AddItemSheet(ContentManager content, string assetName, string sheetName,
                                  int spriteWidth, int spriteHeight)
        {
            itemSprites.Add(sheetName, new SpriteSheet(content.Load<Texture2D>(assetName),
                spriteWidth, spriteHeight, graphics));
        }
    }
}
