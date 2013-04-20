using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameplayElements.Data.Entities.Monsters
{
    public enum AiState
    {
        ROAMING,
        TARGETTING,
        ATTACKING,
        SEARCHING,
        FLEEING
    }
        
    public class Monster : Entity
    {
        public AiState aiState = AiState.ROAMING;

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
            if (IsDead) return;

            speedMultiplier = 1.0f;

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
                if (rand.NextDouble() > 0.9)
                    canMove = true;
            }

            Player player = this.ScanForPlayer();

            if (!(player == null))
            {
                aiState = AiState.TARGETTING;
                if (DistanceTo(this, player) < attackRange)
                {
                    aiState = AiState.ATTACKING;
                }

                var angle = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - pos.X);
                if (angle > MathHelper.PiOver4 && angle < MathHelper.PiOver4 * 3) direction = 1;
                else if (angle < -MathHelper.PiOver4 && angle > -MathHelper.PiOver4 * 3) direction = 0;
                else if (angle < MathHelper.PiOver4 && angle > -MathHelper.PiOver4) direction = 3;
                else if (angle > MathHelper.PiOver4 * 3 && angle < MathHelper.PiOver4 * 5) direction = 2;

            }
            else
            {
                aiState = AiState.ROAMING;
            }

            Vector2 newPos = Position + new Vector2(dirX * speed * speedMultiplier,
                dirY * speed * speedMultiplier);

            switch (aiState)
            {
                case AiState.TARGETTING:
                    speedMultiplier *= 1.085f;
                    var dist = Vector2.Distance(Position, player.Position);
                    var distV = new Vector2(player.Position.X - Position.X,
                        player.Position.Y - Position.Y);
                    newPos = Position + (distV / dist) * speed * speedMultiplier;

                    break;
                case AiState.ATTACKING:
                    return;
            }

            Move(newPos);
        }

        public void DrawPathToPlayer(SpriteBatch batch, Player player)
        {
            
        }
    }
}
