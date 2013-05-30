using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary;
using ProjectElements.Data;
using ProjectElements.IO;
using GameplayElements.Managers;

namespace GameplayElements.Data.Entities
{
    public class ItemEntity
    {
        public Vector2 pos;
        public Vector2 onScreenPos;
        protected Image avatar;

        #region Properties
        public Vector2 Position { get { return pos; } }

        public int SpriteWidth { get { return avatar.Width; } }
        public int SpriteHeight { get { return avatar.Height; } }
        #endregion

        public ItemEntity(Vector2 pos, Texture2D avatar)
        {
            this.pos = pos;
            this.avatar = new Image(avatar);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch batch, GameTime gametime)
        {
            avatar.Draw(batch, Position);
        }
    }
}
