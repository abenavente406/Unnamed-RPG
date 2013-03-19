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

        protected float health = 100.0f;
        protected bool dead = false;

        protected Image avatarUp;
        protected Image avatarDown;
        protected Image avatarRight;

        protected Animation movingUp;
        protected Animation movingDown;
        protected Animation movingRight;
        protected Animation attackingUp;
        protected Animation attackingDown;
        protected Animation attackingRight;

        protected int spriteWidth;
        protected int spriteHeight;
        protected int realWidth;
        protected int realHeight;
        protected float scale = 1.0f;

        protected int detectRange = 100;

        protected int attackRange = 40;
        protected bool isAttacking = false;
        protected int attackCoolDown = 350;
        protected int attackCoolDownTicks = 0;

        protected bool noClip = false;
        protected bool god = false;
        protected bool superSpeed = false;

        protected const int MAX_LEVEL = 100;
        protected int expLevel = 1;
        protected int experience = 0;
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
                pos = Vector2.Clamp(value, Vector2.Zero, new Vector2(LevelManager.GetCurrentLevel().Width - spriteWidth,
                    LevelManager.GetCurrentLevel().Height - spriteHeight));
            }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
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

        public int ExperienceLevel
        {
            get { return expLevel; }
            set 
            {
                if (value < 0) value = 0;
                if (value > MAX_LEVEL) value = MAX_LEVEL;
                expLevel = MAX_LEVEL;
            }
        }

        public int ExperiencePoints
        {
            get { return experience; }
            set
            {
                if (value < 0) value = 0;
                if (value > Int32.MaxValue) value = Int32.MaxValue;
                experience = value;
            }
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
        public float Scale
        {
            get { return scale; }
            set { scale = MathHelper.Clamp(value, 0.1f, 30.0f); }
        }

        public Rectangle SpriteBoundingBox
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y,
                    SpriteWidth, SpriteHeight);
            }
        }
        public Rectangle CollisionBoundingBox
        {
            get
            {
                return new Rectangle((int)(pos.X + (SpriteWidth - realWidth) / 2),
                    (int)(pos.Y + (SpriteHeight - RealHeight) / 2), RealWidth, RealHeight);
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
            if (!isAttacking)
            {
                if (isMoving)
                {
                    switch (direction)
                    {
                        case 0:
                            movingUp.Draw(batch, gameTime, OnScreenPosition, false, scale);
                            break;
                        case 1:
                            movingDown.Draw(batch, gameTime, OnScreenPosition, false, scale);
                            break;
                        case 2:
                            movingRight.Draw(batch, gameTime, OnScreenPosition, true, scale);
                            break;
                        case 3:
                            movingRight.Draw(batch, gameTime, OnScreenPosition, false, scale);
                            break;
                    }
                }
                else
                {
                    movingUp.CurrentFrame = 0;
                    movingDown.CurrentFrame = 0;
                    movingRight.CurrentFrame = 0;
                    movingRight.CurrentFrame = 0;

                    switch (direction)
                    {
                        case 0:
                            avatarUp.Draw(batch, OnScreenPosition, false, scale);
                            break;
                        case 1:
                            avatarDown.Draw(batch, OnScreenPosition, false, scale);
                            break;
                        case 2:
                            avatarRight.Draw(batch, OnScreenPosition, true, scale);
                            break;
                        case 3:
                            avatarRight.Draw(batch, OnScreenPosition, false, scale);
                            break;
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        attackingUp.Draw(batch, gameTime, OnScreenPosition, false, scale);
                        if (attackingUp.CurrentFrame == attackingUp.Images.Length - 1)
                            isAttacking = false;
                        break;
                    case 1:
                        attackingDown.Draw(batch, gameTime, OnScreenPosition, false, scale);
                        if (attackingDown.CurrentFrame == attackingUp.Images.Length - 1)
                            isAttacking = false;
                        break;
                    case 2:
                        attackingRight.Draw(batch, gameTime, OnScreenPosition, true, scale);
                        if (attackingRight.CurrentFrame == attackingUp.Images.Length - 1)
                            isAttacking = false;
                        break;
                    case 3:
                        attackingRight.Draw(batch, gameTime, OnScreenPosition, false, scale);
                        if (attackingRight.CurrentFrame == attackingUp.Images.Length - 1)
                            isAttacking = false;
                        break;
                }
            }

            if (!isAttacking)
            {
                attackingUp.CurrentFrame = 0;
                attackingDown.CurrentFrame = 0;
                attackingRight.CurrentFrame = 0;
            }
            //DrawShadow();
        }

        /// <summary>
        /// Draws the shadow of the entity (just an ellipse)
        /// </summary>
        public void DrawShadow()
        {
            ProjectData.Drawer.Begin(RaisingStudio.Xna.Graphics.DrawingSortMode.Sprite);
            ProjectData.Drawer.DrawFilledEllipse(new Vector2(OnScreenPosition.X, OnScreenPosition.Y + SpriteHeight - 4),
                new Vector2(realWidth, realHeight / 4), new Color(0, 0, 0, 0.3f));
            ProjectData.Drawer.End();
        }
        #endregion

        protected void Move(Vector2 newPos)
        {
                
            Vector2 topLeft = newPos + new Vector2((spriteWidth - realWidth) / 2, (spriteHeight - realHeight) / 2);
            Vector2 topRight = newPos + new Vector2(spriteWidth - (spriteWidth - realWidth) / 2, (spriteHeight - realHeight) / 2);
            Vector2 bottomLeft = newPos + new Vector2((spriteWidth - realWidth) / 2, spriteHeight);
            Vector2 bottomRight = newPos + new Vector2(spriteWidth - (spriteWidth - realWidth) / 2, spriteHeight);

            if (!NoClip)
            {
                if (!LevelManager.IsWallTile(topLeft) && !LevelManager.IsWallTile(topRight) &&
                    !LevelManager.IsWallTile(bottomLeft) && !LevelManager.IsWallTile(bottomRight))
                {
                    Position = newPos;
                }
                else
                {
                    isMoving = false;
                }
            }
            else
                Position = newPos;
        }
        protected void Move(float testX, float testY)
        {
            if (!NoClip)
            {
                if (!LevelManager.IsWallTile(testX, Position.Y, RealWidth, (int)(SpriteHeight * scale)))
                    pos.X = testX;
                if (!LevelManager.IsWallTile(Position.X, testY, RealWidth, (int)(SpriteHeight * scale)))
                    pos.Y = testY;

                Position = pos;
            }
            else
                Position = new Vector2(testX, testY);
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
                if (target != null)
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

        protected void SetCustomTexture(Vector2 start, string sheetName, int frames,
                                        int realWidth = 0, int realHeight = 0, float duration = 100f)
        {
            SpriteSheet sheet = SpriteSheetManager.EntitySprites[sheetName];

            int x = (int)start.X;
            int y = (int)start.Y;

            avatarDown = new Image(sheet.GetSubImage(x + frames - 1, y));
            avatarUp = new Image(sheet.GetSubImage(x + frames - 1, y + 1));
            avatarRight = new Image(sheet.GetSubImage(x, y + 2));

            Texture2D[] imagesDown = new Texture2D[frames];
            Texture2D[] imagesRight = new Texture2D[frames];
            Texture2D[] imagesUp = new Texture2D[frames];

            for (int i = 0; i < frames; i++)
            {
                imagesDown[i] = sheet.GetSubImage(x + i, y);
                imagesUp[i] = sheet.GetSubImage(x + i, y + 1);
                imagesRight[i] = sheet.GetSubImage(x + i, y + 2);
            }

            movingDown = new Animation(imagesDown, duration);
            movingRight = new Animation(imagesRight, duration);
            movingUp = new Animation(imagesUp, duration);

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

        protected void SetAttackingAnim(Vector2 start, string sheetName, int spriteWidth, int spriteHeight, 
                                        float duration = 100f)
        {

        }

        protected void SetCustomAttackingAnimCustom(string sheetName, int spriteWidth, int spriteHeight,
                                                    int frames, float duration = 100f)
        {
            SpriteSheet sheet = SpriteSheetManager.EntitySprites[sheetName];

            int x = 0;
            int y = 0;

            Texture2D[] imagesDown = new Texture2D[frames];
            Texture2D[] imagesRight = new Texture2D[frames];
            Texture2D[] imagesUp = new Texture2D[frames];

            for (int i = 0; i < frames; i++)
            {
                imagesDown[i] = sheet.GetSubImage(x + i, y);
                imagesUp[i] = sheet.GetSubImage(x + i, y + 1);
                imagesRight[i] = sheet.GetSubImage(x + i, y + 2);
            }

            attackingDown = new Animation(imagesDown, duration);
            attackingRight = new Animation(imagesRight, duration);
            attackingUp = new Animation(imagesUp, duration);

            spriteWidth = avatarDown.Width;
            spriteHeight = avatarDown.Height;
        }
    }
}
