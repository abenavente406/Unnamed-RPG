using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectElements.IO;
using ProjectElements.Data;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UnnamedRpg.GameScreens
{
	public class LoadGameScreen : BaseGameState
	{
        PictureBox arrowImage;
        PictureBox background;
        Animation backgroundAnim;

        float maxItemWidth = 0f;

        List<LinkLabel> labels = new List<LinkLabel>();
        List<SaveData> states = new List<SaveData>();

        int changes = 0;

        public LoadGameScreen(Game game, GameStateManager manager)
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

            var Content = GameRef.Content;
            states = SaveDataParser.SaveStates;

            if (StartMenuScreen.Background is PictureBox)
            {
                background = StartMenuScreen.Background as PictureBox;
                ControlManager.Add(background);
            }
            else
                backgroundAnim = StartMenuScreen.Background as Animation;


            Texture2D arrowTexture = Content.Load<Texture2D>("GUI\\leftarrowUp");
            arrowImage = new PictureBox(arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(arrowImage);

            for (int i = 0; i < 5; i++)
            {
                LinkLabel label = new LinkLabel();

                if (SaveDataParser.GetSaveAtIndex(i) == null)
                {
                    label.Text = "-- No Save Game Found --";
                    label.Enabled = false;
                    label.Color = Color.DarkGray;
                }
                else
                    label.Text = SaveDataParser.GetSaveAtIndex(i).Name + " -- " + SaveDataParser.GetSaveAtIndex(i).Time;

                label.Size = label.SpriteFont.MeasureString(label.Text);
                label.Selected += new EventHandler(menuItem_Selected);
                label.Effect = ControlEffect.GLOW;
                labels.Add(label);
                ControlManager.Add(label);
            }

            ControlManager.NextControl();
            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            Vector2 position = new Vector2(ProjectElements.Data.ProjectData.GameWidth / 2, 100);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    Vector2 offset = new Vector2(c.Size.X / 2, 0);
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;
                    c.Position = position - offset;
                    position.Y += c.Size.Y + 10f;
                }
            }
            ControlManager_FocusChanged(labels[0], null);
        }

        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + control.Size.X + 10f,
                control.Position.Y + 15f);
            arrowImage.SetPosition(position);

            if (changes > 0)
                ProjectData.guiBeep.Play(.5f, 0, 0);

            changes++;
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            ProjectData.guiSelect.Play(.5f, 0, 0);
            foreach (LinkLabel label in labels)
            {
                if (sender == label)
                {
                    StateManager.PushState(new GamePlayScreen(Game, StateManager, states[labels.IndexOf(label)]));
                }
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
 
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            {
                base.Draw(gameTime);
                if (backgroundAnim != null)
                    backgroundAnim.Draw(GameRef.spriteBatch, gameTime, new Rectangle(0, 0, ProjectData.GameWidth, ProjectData.GameHeight));
                ControlManager.Draw(GameRef.spriteBatch, gameTime);
                FadeOutRect.Draw(GameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            }
            GameRef.spriteBatch.End();
        }

	}
}