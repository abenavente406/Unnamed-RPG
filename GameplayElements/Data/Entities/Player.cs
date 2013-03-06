using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectElements.Data;

namespace GameplayElements.Data.Entities
{
    public class Player : Entity
    {

        private KeyboardState newState;

        public Player(string name, Vector2 pos)
            : base(name, pos)
        {
            speed = 2.1f;
            SetTexture(new Vector2(3, 0), "Entity Sprites 1");
            onScreenPos = new Vector2(ProjectData.GameWidth / 2,
                ProjectData.GameHeight / 2);
        }

        public override void Update(GameTime gameTime)
        {

            newState = Keyboard.GetState();

            int dirX = 0;
            int dirY = 0;

            if (newState.IsKeyDown(Keys.Up))
                dirY--;
            if (newState.IsKeyDown(Keys.Down))
                dirY++;
            if (newState.IsKeyDown(Keys.Left))
                dirX--;
            if (newState.IsKeyDown(Keys.Right))
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
