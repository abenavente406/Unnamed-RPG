using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using ProjectElements.Data;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UnnamedRpg.GameScreens
{
    /// <summary>
    ///  The screen where you create a new game.
    ///  You type your name and press enter
    /// </summary>
    class NewGameScreen : BaseGameState
    {
        Label lblPrompt;
        TextBox txtName;

        public NewGameScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            ContentManager Content = GameRef.Content;
            SpriteFont menuFont = Content.Load<SpriteFont>("Fonts\\defaultFont");

            lblPrompt = new Label();            
            lblPrompt.SpriteFont = menuFont;
            lblPrompt.Text = "Enter your name:";
            lblPrompt.Position = new Vector2(ProjectData.GameWidth / 2 - menuFont.MeasureString(
                lblPrompt.Text).X / 2 - menuFont.MeasureString("       ").X / 2, ProjectData.GameHeight - 50);
            lblPrompt.Color = Color.White;
            lblPrompt.Size = lblPrompt.SpriteFont.MeasureString(lblPrompt.Text);
            ControlManager.Add(lblPrompt);

            txtName = new TextBox(GameRef.GraphicsDevice);
            txtName.SpriteFont = menuFont;
            txtName.Position = new Vector2(lblPrompt.Position.X + lblPrompt.Size.X + 
                lblPrompt.SpriteFont.MeasureString(" ").X, lblPrompt.Position.Y);
            txtName.BackColor = Color.Transparent;
            txtName.ForeColor = Color.White;
            ControlManager.Add(txtName);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);

            if (InputHandler.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                SwitchState(new NewLevelScreen(Game, StateManager, txtName.Text));
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            {
                ControlManager.Draw(GameRef.spriteBatch, gameTime);
                base.Draw(gameTime);

                FadeOutRect.Draw(GameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            }
            GameRef.spriteBatch.End();
        }
    }
}
