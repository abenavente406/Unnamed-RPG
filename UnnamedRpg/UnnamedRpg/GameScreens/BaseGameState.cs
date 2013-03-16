using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UnnamedRpg.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        protected TestGame GameRef;

        protected ControlManager ControlManager;

        protected PlayerIndex playerIndexInControl;

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (TestGame)game;
            playerIndexInControl = PlayerIndex.One;
        }

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            SpriteFont menuFont = Content.Load<SpriteFont>("Fonts\\menuFont");
            ControlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void DrawState(SpriteBatch batch, GameTime gameTime)
        {
            base.DrawState(batch, gameTime);
        }

        public void SwitchState(GameState targetState)
        {
            StateManager.TargetState = targetState;

            IsExiting = true;
        }
    }
}
