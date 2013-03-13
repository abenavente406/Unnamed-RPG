using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelperLibrary.Controls
{
    public class Label : Control
    {
        #region Constructor Region

        public Label()
        {
            tabStop = false;
        }

        #endregion

        #region Abstract Methods

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (Effect)
            {
                case ControlEffect.NONE:
                    {
                        spriteBatch.DrawString(SpriteFont, Text, Position, Color);
                        break;
                    }
                case ControlEffect.FLASH:
                    {
                        if (gameTime.TotalGameTime.Milliseconds % flashDuration > flashDuration / 2)
                            spriteBatch.DrawString(spriteFont, Text, Position, Color);
                        break;
                    }
            }
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
        }

        #endregion
    }
}
