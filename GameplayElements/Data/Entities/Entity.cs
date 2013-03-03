using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        protected Texture2D avatar;

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

        }

        public void Attack(Entity target)
        {

        }

        protected void Move(Vector2 newPos)
        {

        }

        public void Damage(float damage)
        {
            Health -= damage;
        }
    }
}
