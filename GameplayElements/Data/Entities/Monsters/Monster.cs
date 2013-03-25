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
        protected int movementTimer = 0;
        protected int movementTimerMax = 300;
        protected int movementDir = 1;

        protected bool canMove = true;

        Random rand;

        public Monster(string name, Vector2 pos)
            : base(name, pos)
        {
            rand = new Random(System.DateTime.Now.Millisecond);
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
                {
                    //Attack(player);
                    return;
                }
            }

            Vector2 newPos = Position + new Vector2(dirX * speed * speedMultiplier,
                dirY * speed * speedMultiplier);

            Move(newPos);
        }

        public void DrawPathToPlayer(SpriteBatch batch, Player player)
        {
            
        }
    }
}
