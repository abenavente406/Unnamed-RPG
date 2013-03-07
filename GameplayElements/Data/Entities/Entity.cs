using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectElements.Data;
using ProjectElements.IO;
using GameplayElements.Managers;
using GameHelperLibrary;

namespace GameplayElements.Data.Entities
{
    public abstract class Entity
    {

        #region Fields
        protected string name;

        protected Vector2 pos;
        protected Vector2 onScreenPos;
        protected int direction;
        protected float speed = 1.7f;
        protected float speedMultiplier = 1f;
        protected bool isMoving = false;

        protected float health = 20.0f;
        protected bool dead = false;

        protected Image avatarUp;
        protected Image avatarDown;
        protected Image avatarLeft;
        protected Image avatarRight;

        protected Animation movingUp;
        protected Animation movingDown;
        protected Animation movingLeft;
        protected Animation movingRight;

        protected int spriteWidth;
        protected int spriteHeight;
        protected int realWidth;
        protected int realHeight;

        protected int detectRange = 100;

        protected int attackRange = 40;
        protected bool isAttacking = false;
        protected int attackCoolDown = 350;
        protected int attackCoolDownTicks = 0;

        protected bool noClip = false;
        protected bool god = false;
        protected bool superSpeed = false;

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Vector2 Position
        {
            get { return pos; } 
            set
            {
                pos = Vector2.Clamp(value, Vector2.Zero, new Vector2(LevelManager.GetCurrentLevel().Width - realWidth,
                    LevelManager.GetCurrentLevel().Height - realHeight));
            }
        }
        public virtual float Speed
        {
            get { return speed * speedMultiplier; }
            set { speed = value; }
        }

        public float Health
        {
            get { return health; }
            set { if (value < 0) value = 0; }
        }

        public bool IsDead
        {
            get { return dead; }
            set { dead = value; }
        }

        public int SpriteWidth
        {
            get { return spriteWidth; }
        }
        public int SpriteHeight
        {
            get { return spriteHeight; }
        }

        public int RealWidth
        {
            get { return realWidth; }
            set { realWidth = value; }
        }
        public int RealHeight
        {
            get { return realHeight; }
            set { realHeight = value; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y,
                    RealWidth, RealHeight);
            }
        }

        public virtual Vector2 OnScreenPosition
        {
            get
            {
                return Camera.Transform(this.Position);
            }
        }

        public bool IsMoving { get { return isMoving; } }
        public bool NoClip { get { return noClip; } set { noClip = value; } }
        public bool God { get { return god; } set { god = value; } }
        public bool SuperSpeed { get { return superSpeed; } set { superSpeed = value; } }
        #endregion

        public Entity(string name, Vector2 pos)
        {
            Name = name;
            Position = pos;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        #region Draw Methods
        /// <summary>
        /// Draws the entity to the screen
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (isMoving)
            {
                switch (direction)
                {
                    case 0:
                        movingUp.Draw(batch, gameTime, OnScreenPosition);
                        break;
                    case 1:
                        movingDown.Draw(batch, gameTime, OnScreenPosition);
                        break;
                    case 2:
                        movingLeft.Draw(batch, gameTime, OnScreenPosition);
                        break;
                    case 3:
                        movingRight.Draw(batch, gameTime, OnScreenPosition);
                        break;
                }
            }
            else
            {
                movingUp.CurrentFrame = 0;
                movingDown.CurrentFrame = 0;
                movingLeft.CurrentFrame = 0;
                movingRight.CurrentFrame = 0;

                switch (direction)
                {
                    case 0:
                        avatarUp.Draw(batch, OnScreenPosition);
                        break;
                    case 1:
                        avatarDown.Draw(batch, OnScreenPosition);
                        break;
                    case 2:
                        avatarLeft.Draw(batch, OnScreenPosition);
                        break;
                    case 3:
                        avatarRight.Draw(batch, OnScreenPosition);
                        break;
                }
            }

            DrawShadow();
        }

        /// <summary>
        /// Draws the shadow of the entity (just an ellipse)
        /// </summary>
        public void DrawShadow()
        {
            ProjectData.Drawer.Begin();
            ProjectData.Drawer.DrawFilledEllipse(new Vector2(OnScreenPosition.X, OnScreenPosition.Y + SpriteHeight - 4),
                new Vector2(realWidth, realHeight / 4), new Color(0, 0, 0, 0.3f));
            ProjectData.Drawer.End();
        }
        #endregion

        protected void Move(Vector2 newPos)
        {
            Vector2 testPos = new Vector2(newPos.X - realWidth, newPos.Y - realHeight);
            if (!LevelManager.IsWallTile(this))
                Position = newPos;
            else
                isMoving = false;
        }

        protected float DistanceTo(Entity from, Entity to)
        {
            return Vector2.Distance(from.Position, to.Position);
        }

        #region Attacking methods
        public void Attack(Entity target)
        {
            if (attackCoolDownTicks <= 0)
            {
                target.Damage(10);
                attackCoolDownTicks = attackCoolDown;
            }
        }
        public void Damage(float damage)
        {
            Health -= damage;
        }
        #endregion

        #region Scanning For Entities
        protected Entity ScanForEntity()
        {
            foreach (Entity e in EntityManager.allEntities)
            {
                if (Vector2.Distance(this.Position, e.Position) < detectRange)
                    return e;
            }

            return null;
        }

        protected Entity ScanForMonster()
        {
            foreach (Monsters.Monster m in EntityManager.monsters)
            {
                if (Vector2.Distance(this.Position, m.Position) < detectRange)
                    return m;
            }
            return null;
        }

        protected Entity ScanForAmbientEntity()
        {
            foreach (Passives.Passive p in EntityManager.passives)
            {
                if (Vector2.Distance(this.pos, p.Position) < detectRange)
                    return p;
            }
            return null;
        }

        protected Player ScanForPlayer()
        {
            Player player = EntityManager.player;

            if (Vector2.Distance(this.Position, player.Position) < detectRange)
                return player;
            else
                return null;
        }
        #endregion

        /// <summary>
        /// Sets the animations and avatars of an entity . . .
        /// Follow the guidelines for making spritesheets
        /// </summary>
        /// <param name="start">Start position on the sprite sheet</param>
        /// <param name="sheetName">Key for spritesheet dictionary</param>
        /// <param name="realWidth"></param>
        /// <param name="realHeight"></param>
        protected void SetTexture(Vector2 start, string sheetName, int realWidth = 0, int realHeight = 0)
        {
            SpriteSheet sheet = SpriteSheetManager.EntitySprites[sheetName];

            int x = (int)start.X;
            int y = (int)start.Y;

            avatarDown = new Image(sheet.GetSubImage(x + 1, y));
            avatarUp = new Image(sheet.GetSubImage(x + 1, y + 3));
            avatarLeft = new Image(sheet.GetSubImage(x + 1, y + 1));
            avatarRight = new Image(sheet.GetSubImage(x + 1, y + 2));

            Texture2D[] imagesDown = new Texture2D[] { sheet.GetSubImage(x, y), 
                sheet.GetSubImage(x + 1, y), sheet.GetSubImage(x + 2, y)};
            Texture2D[] imagesLeft = new Texture2D[] { sheet.GetSubImage(x, y + 1), 
                sheet.GetSubImage(x + 1, y + 1), sheet.GetSubImage(x + 2, y + 1)};
            Texture2D[] imagesRight = new Texture2D[] {sheet.GetSubImage(x, y + 2), 
                sheet.GetSubImage(x + 1, y + 2), sheet.GetSubImage(x + 2, y + 2) };
            Texture2D[] imagesUp = new Texture2D[] { sheet.GetSubImage(x, y + 3), 
                sheet.GetSubImage(x + 1, y + 3), sheet.GetSubImage(x + 2, y + 3)};

            movingDown = new Animation(imagesDown);
            movingLeft = new Animation(imagesLeft);
            movingRight = new Animation(imagesRight);
            movingUp = new Animation(imagesUp);

            spriteWidth = avatarDown.Width;
            spriteHeight = avatarDown.Height;

            if (realWidth != 0)
                this.realWidth = realWidth;
            else
                this.realWidth = spriteWidth;

            if (realHeight != 0)
                this.realHeight = realHeight;
            else
                this.realHeight = spriteHeight;
        }
    }
}
