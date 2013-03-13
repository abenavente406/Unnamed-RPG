using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Content;
using GameHelperLibrary.Controls;

namespace UnnamedRpg.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        Texture2D backgroundImage;
        LinkLabel startLabel;

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>("Backgrounds\\title");

            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.SpriteFont = Content.Load<SpriteFont>("Fonts\\defaultFont");

            if (InputHandler.GamePadConnected)
                startLabel.Text = "Press A to play";
            else
                startLabel.Text = "Press ENTER to play";

            startLabel.Position = new Vector2(ProjectElements.Data.ProjectData.GameWidth / 2 -
                startLabel.SpriteFont.MeasureString(startLabel.Text).X / 2, ProjectElements.Data.ProjectData.GameHeight 
                / 2 - startLabel.SpriteFont.MeasureString(startLabel.Text).Y / 2);
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_selected);

            ControlManager.Add(startLabel);
        }

        void startLabel_selected(object sender, EventArgs e)
        {
            StateManager.PushState(GameRef.StartMenuScreen);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(backgroundImage, GameRef.ScreenRectangle, Color.White);

            // Flash the words to the screen
            ControlManager.Draw(GameRef.spriteBatch, gameTime);

            GameRef.spriteBatch.End();
        }
    }
}
