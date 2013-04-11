using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameHelperLibrary;

namespace ProjectElements.IO
{
    /**
     * TODO:
     *  - Change the identifiers of sprite sheets to be enum values
     *  */

    /// <summary>
    /// Container that holds all the sprite sheets that the game uses
    /// </summary>
    public class SpriteSheetManager
    {
        /**
         * Sprite sheets are seperated into two categories:
         *     - Entities
         *     - Items
         *     
         * The sprite sheets are stored in a dictionary (see below) and 
         * in order to access them, you need to know the keys.  I want to
         * eventually make the key an enum
         * */

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
            // If you need to access spritesheets, use the strings in the identifiers
            AddEntitySheet(content, "Sprites\\EntitySprites", "Entity Sprites 1", 32, 32);
            AddEntitySheet(content, "Sprites\\link-playersheet", "LinkPlayerSheet", 24, 24);
            AddEntitySheet(content, "Sprites\\link-attackingsheet", "LinkAttackingSheet", 24, 48);
        }

        public void AddEntitySheet(ContentManager content, string assetName, 
                                    string sheetName, int spriteWidth, int spriteHeight)
        {
            entitySprites.Add(sheetName, new SpriteSheet(content.Load<Texture2D>(assetName), 
                spriteWidth, spriteHeight, graphics));
        }

        /// <summary>
        /// Add an item sheet to quickly access it
        /// </summary>
        /// <param name="content"></param>
        /// <param name="assetName"></param>
        /// <param name="sheetName"></param>
        /// <param name="spriteWidth"></param>
        /// <param name="spriteHeight"></param>
        public void AddItemSheet(ContentManager content, string assetName, string sheetName,
                                  int spriteWidth, int spriteHeight)
        {
            itemSprites.Add(sheetName, new SpriteSheet(content.Load<Texture2D>(assetName),
                spriteWidth, spriteHeight, graphics));
        }
    }
}
