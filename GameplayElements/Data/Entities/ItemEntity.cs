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
        public Vector2 OnScreenPosition { get { return Camera.Transform(Position); } }

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
            avatar.Draw(batch, OnScreenPosition);
            DrawShadow();
        }

        private void DrawShadow()
        {
            ProjectData.Drawer.Begin();
            ProjectData.Drawer.DrawFilledEllipse(new Vector2(OnScreenPosition.X, OnScreenPosition.Y + SpriteHeight - 4),
                new Vector2(SpriteWidth, SpriteWidth / 5), new Color(0, 0, 0, .3f));
            ProjectData.Drawer.End();
        }
    }
}
