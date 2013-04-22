using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameplayElements.PathFinding;
using GameplayElements.Managers;
using ProjectElements.Data;

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

        Pathfinder pathFinder;

        public Monster(string name, Vector2 pos)
            : base(name, pos)
        {
            rand = new Random(System.DateTime.Now.Millisecond);
            pathFinder = new Pathfinder(LevelManager.GetCurrentLevel());

            detectRange = 32 * 5;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead) return;

            speedMultiplier = 1.0f;

            Vector2 newPos = Position;
            int dirX = 0;
            int dirY = 0;

            if (attackCoolDownTicks > 0)
                attackCoolDownTicks--;

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

            switch (aiState)
            {
                case AiState.ROAMING:   // If the player has not been found, move randomly
                    {
                        if (canMove)
                        {
                            movementTimer = rand.Next(250) + 1;
                            movementDir = rand.Next(4);
                            canMove = false;
                        }

                        direction = movementDir;

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

                        newPos = Position + new Vector2(dirX * speed * speedMultiplier,
                             dirY * speed * speedMultiplier);
                        break;
                    }
                case AiState.TARGETTING:    // If the player HAS been found, move torwards it and sprint
                    {
                        isMoving = true;
                        speedMultiplier *= 1.085f;

                        var firstPoint = GetTargetDirection(player);

                        dirX = (int)(firstPoint.X - (int)Math.Round(Position.X / (double)32) * 32);
                        dirY = (int)(firstPoint.Y - (int)Math.Round(Position.Y / (double)32) * 32);

                        var to = new Vector2(dirX, dirY);
                        to.Normalize();

                        newPos = Position + to;
                        break;
                    }
                case AiState.ATTACKING:     // Attack the player. Attacking prevents moving
                    {
                        isMoving = false;
                        Attack(player as Entity);
                        return;
                    }
            }

            Move(newPos);
        }

        public void DrawPathToPlayer(SpriteBatch batch, Player player)
        {
            List<Vector2> path = pathFinder.FindPath(this.GridPosition, player.GridPosition);
            foreach (Vector2 v in path)
                batch.Draw(avatarDown.ImageTexture, v, Color.Red * .5f);
        }

        private Vector2 GetTargetDirection(Player player)
        {
            List<Vector2> path = pathFinder.FindPath(this.GridPosition, player.GridPosition);

            if (path.Count < 1)
                return Vector2.Zero;

            return path.Count > 2 ? path[1] : path[0];
        }
    }
}
