using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectElements.Data;
using GameHelperLibrary;
using GameplayElements.Managers;
using GameplayElements.Data.Entities.NPCs;

namespace GameplayElements.Data.Entities
{
    public class Player : Entity
    {
        int hitTimerTicks = 0;
        int hitTimerMax = 200;
        bool isTalking;
        public bool IsTalking
        {
            get { return isTalking; }
            set { isTalking = value; }
        }

        public Player(string name, Vector2 pos)
            : base(name, pos)
        {
            speed = 2f;
            SetCustomTexture(Vector2.Zero, "LinkPlayerSheet", 8, 32, 32, 75);
            SetCustomAttackingAnimCustom("LinkAttackingSheet", 24, 48, 5);

            Scale = 1.3333333f;
            RealWidth = (int)scale * spriteWidth;
            RealHeight = (int)scale * spriteHeight;
        }

        public override void Update(GameTime gameTime)
        {
            speedMultiplier = 1.0f;

            if (InputHandler.KeyPressed(Keys.N))
                NoClip = !NoClip;

            if (attackCoolDownTicks > 0)
                attackCoolDownTicks--;

            int dirX = 0;
            int dirY = 0;

            if (InputHandler.KeyDown(Keys.Up))
                dirY--;
            if (InputHandler.KeyDown(Keys.Down))
                dirY++;
            if (InputHandler.KeyDown(Keys.Left))
                dirX--;
            if (InputHandler.KeyDown(Keys.Right))
                dirX++;
            if (InputHandler.KeyDown(Keys.LeftShift))
                speedMultiplier *= 1.20f;

            if (dirX < 0)
                direction = 2;
            else if (dirX > 0)
                direction = 3;

            if (dirY < 0)
                direction = 0;
            else if (dirY > 0)
                direction = 1;

            if (dirX > 0 && dirY > 0)
                direction = 1;
            if (dirX < 0 && dirY < 0)
                direction = 0;

            isMoving = !(dirX == 0 && dirY == 0);

            float newX = Position.X + dirX * speed * speedMultiplier;
            float newY = Position.Y + dirY * speed * speedMultiplier;

            Move(newX, newY);

            var monsterInRange = ScanForMonster();
            var npcInRange = ScanForNPCs().Count > 0 ? ScanForNPCs().Pop() : null;

            if (InputHandler.KeyPressed(Keys.Z))
            {
                if (npcInRange != null)
                {
                    IsTalking = true;
                }
            }

            if (InputHandler.KeyPressed(Keys.X))
            {
                Attack(monsterInRange);
            }

            if (hasBeenHit)
            {

            }
        }

        private Stack<NPC> ScanForNPCs()
        {
            var result = new Stack<NPC>();

            foreach (NPC n in EntityManager.npcs)
            {
                if (Vector2.Distance(this.Position, n.Position) < detectRange)
                {
                    result.Push(n);
                }
            }
            return result;
        }
    }
}
