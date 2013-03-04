using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FileElements.Data;
using FileElements.IO;
using GameHelperLibrary;

namespace GameplayElements.Data.Entities
{
    public abstract class Entity
    {
        protected string name;

        protected Vector2 pos;
        protected int direction;
        protected float speed;
        protected float speedMultiplier = 1f;
        protected bool isMoving = false;

        protected float health = 20.0f;
        protected bool dead = false;

        protected Image avatar;

        protected Animation movingUp;
        protected Animation movingDown;
        protected Animation movingLeft;
        protected Animation movingRight;

        protected int spriteWidth;
        protected int spriteHeight;
        protected int realWidth;
        protected int realHeight;

        protected bool noClip = false;
        protected bool god = false;
        protected bool superSpeed = false;

        #region Properties
        public string Name { get { return name; } set { name = value; } }
        public Vector2 Position { get { return pos; } set { pos = value; } }
        public virtual float Speed { get { return speed * speedMultiplier; } set { speed = value; } }

        public float Health { get { return health; } set { if (value < 0) value = 0; } }

        public bool IsDead { get { return dead; } set { dead = value; } }

        public int SpriteWidth { get { return spriteWidth; } }
        public int SpriteHeight { get { return spriteHeight; } }
        public int RealWidth { get { return realWidth; } set { realWidth = value; } }
        public int RealHeight { get { return realHeight; } set { realHeight = value; } }

        public bool NoClip { get { return noClip; } set { noClip = value; } }
        public bool God { get { return god; } set { god = value; } }
        public bool SuperSpeed { get { return superSpeed; } set { superSpeed = value; } }
        #endregion

        public Entity(string name, Vector2 pos)
        {
            Name = name;
            Position = pos;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            avatar.Draw(batch, Position);
            DrawShadow();
        }

        public void DrawShadow()
        {
            ProjectData.Drawer.Begin();
            ProjectData.Drawer.DrawEllipse(new Vector2(Position.X, Position.Y + SpriteHeight),
                new Vector2(realWidth, realHeight / 5), new Color(0, 0, 0, 0.3f));
            ProjectData.Drawer.End();
        }

        public void Attack(Entity target)
        {
            target.Damage(10);
        }

        protected void Move(Vector2 newPos)
        {
            if (NoClip)
            {
                Position = newPos;
            }
        }

        public void Damage(float damage)
        {
            Health -= damage;
        }
    }
}
