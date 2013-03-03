using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameHelperLibrary;

namespace FileElements.IO
{
    public class SpriteSheetManager
    {
        public static SpriteSheet spriteSheetEntity001;
        public static SpriteSheet spriteSheetItem001;

        private GraphicsDevice graphics;

        public SpriteSheetManager(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public void Initialize(ContentManager content)
        {

        }
    }
}
