using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectElements.Data;
using GameHelperLibrary;
using GameplayElements.Managers;

namespace GameplayElements.Data.Entities
{
    public class Player : Entity
    {

        public Player(string name, Vector2 pos)
            : base(name, pos)
        {
            speed = 2f;
            SetTexture(new Vector2(3, 0), "Entity Sprites 1", 28, 28);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.N))
                NoClip = !NoClip;

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

            if (dirX < 0)
                direction = 2;
            else if (dirX > 0)
                direction = 3;

            if (dirY < 0)
                direction = 0;
            else if (dirY > 0)
                direction = 1;

            if (dirX == 0 & dirY == 0)
                isMoving = false;
            else
                isMoving = true;

            Vector2 testPostion = Position + new Vector2(dirX * speed * speedMultiplier, dirY * speed * speedMultiplier);

            Move(testPostion);
        }

    }
}
