using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary;
using FileElements.Data;
using FileElements.IO;
using RaisingStudio.Xna.Graphics;

namespace GameplayElements.Data.Entities
{
    public class ItemEntity
    {
        public Vector2 pos;
        protected Image avatar;

        private DrawingBatch drawer;

        #region Properties
        public Vector2 Position { get { return pos; } }

        public int SpriteWidth { get { return avatar.Width; } }
        public int SpriteHeight { get { return avatar.Height; } }
        #endregion

        public ItemEntity(Vector2 pos, Texture2D avatar)
        {
            this.pos = pos;
            this.avatar = new Image(avatar);

            drawer = new DrawingBatch(ProjectData.Graphics.GraphicsDevice);

        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch batch, GameTime gametime)
        {
            avatar.Draw(batch, Position);
            drawer.Begin();
            drawer.DrawFilledEllipse(new Vector2(pos.X, pos.Y + SpriteHeight), 
                new Vector2(SpriteWidth, SpriteWidth / 5), new Color(0, 0, 0, .3f));
            drawer.End();
        }
    }
}
