using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameplayElements.Data.Entities.PathFinding;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Monster : Entity
    {

        int movementTimer = 0;
        int movementTimerMax = 300;
        int movementDir = 1;

        bool canMove = true;

        Random rand = new Random();

        public Monster(string name, Vector2 pos)
            : base(name, pos)
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            if (attackCoolDownTicks > 0)
                attackCoolDownTicks--;

            if (canMove)
            {
                movementTimer = rand.Next(250) + 1;
                movementDir = rand.Next(4);
                canMove = false;
            }

            direction = movementDir;

            int dirX = 0;
            int dirY = 0;

            if (movementTimer < movementTimerMax)
            {
                switch (movementDir)
                {
                    case 0:
                        dirY--;
                        break;
                    case 1:
                        dirY++;
                        break;
                    case 2:
                        dirX--;
                        break;
                    case 3:
                        dirX++;
                        break;
                }

                movementTimer++;
                isMoving = true;
            }
            else
            {
                isMoving = false;
                if (rand.NextDouble() > 0.97)
                    canMove = true;
            }

            Player player = this.ScanForPlayer();

            if (!(player == null))
            {
                if (DistanceTo(this, player) < attackRange)
                    Attack(player);
                return;
            }

            Vector2 newPos = Position + new Vector2(dirX * speed * speedMultiplier,
                dirY * speed * speedMultiplier);

            Move(newPos);
        }

        public void DrawPathToPlayer(SpriteBatch batch, Player player)
        {
            List<Rectangle> rects = new List<Rectangle>();
            List<DrawableRectangle> rectsd = new List<DrawableRectangle>();

            foreach (Vector2 v in PathFinder.FindPath(this.Position, player.Position))
                rects.Add(new Rectangle((int)v.X, (int)v.Y, spriteWidth, spriteHeight));

            foreach (Rectangle r in rects)
                rectsd.Add(new DrawableRectangle(batch.GraphicsDevice, new Vector2(r.Width, r.Height),
                    Color.Red * .5f, true));
        }

            
    }
}
