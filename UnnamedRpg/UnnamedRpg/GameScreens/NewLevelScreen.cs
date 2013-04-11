using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework.Graphics;
using ProjectElements.Data;

namespace UnnamedRpg.GameScreens
{
    /// <summary>
    /// Allows you to choose the type of level you want
    /// </summary>
    public class NewLevelScreen : BaseGameState
    {
        #region Fields
        PictureBox arrowImage;
        LinkLabel lblDungeon;
        LinkLabel lblRandom;
        LinkLabel lblTMX;

        float maxItemWidth = 0.0f;
        uint changes = 0;

        string playerName;
        #endregion

        #region Initialization
        public NewLevelScreen(Game game, GameStateManager manager, string playerName)
            : base(game, manager)
        {
            this.playerName = playerName;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Texture2D arrowTexture = Game.Content.Load<Texture2D>("GUI\\leftarrowUp");
            arrowImage = new PictureBox(arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(arrowImage);

            lblRandom = new LinkLabel();
            lblRandom.Text = "RANDOMLY GENERATED MAP";
            ControlManager.Add(lblRandom);

            lblDungeon = new LinkLabel();
            lblDungeon.Text = "DUNGEON";
            ControlManager.Add(lblDungeon);

            lblTMX = new LinkLabel();
            lblTMX.Text = "TMX MAP";
            ControlManager.Add(lblTMX);
            
            Vector2 position = new Vector2(ProjectData.GameWidth / 2, ProjectData.GameHeight / 1.75f);

            foreach (Control l in ControlManager)
            {
                if (l is LinkLabel)
                {
                    l.Size = l.SpriteFont.MeasureString(l.Text);
                    Vector2 offset = new Vector2(l.Size.X / 2, 0);
                    if (l.Size.X > maxItemWidth)
                        maxItemWidth = l.Size.X;
                    l.Position = position - offset;
                    position.Y += l.Size.Y + 10f;
                    l.Effect = ControlEffect.GLOW;
                    l.Selected += new EventHandler(menuItem_Selected);
                }
            }

            lblTMX.Color = Color.DarkGray;
            lblTMX.Enabled = false;

            ControlManager.NextControl();
            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);
            ControlManager_FocusChanged(lblRandom, null);
        }
        #endregion

        #region Event Delegates
        protected void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + control.Size.X + 10f,
                control.Position.Y + 15f);
            arrowImage.SetPosition(position);

            if (changes > 0)
                ProjectData.guiBeep.Play(.5f, 0.0f, 0.0f);

            changes++;
        }

        public virtual void menuItem_Selected(object sender, EventArgs e)
        {
            ProjectData.guiSelect.Play(.5f, 0.0f, 0.0f);
            if (sender == lblDungeon)
                SwitchState(new GamePlayScreen(Game, StateManager, SaveData.CreateNewSave(playerName), 1));
            else if (sender == lblTMX)
                SwitchState(new GamePlayScreen(Game, StateManager, SaveData.CreateNewSave(playerName), 2));
            else if (sender == lblRandom)
                SwitchState(new GamePlayScreen(Game, StateManager, SaveData.CreateNewSave(playerName), 0));
        }
        #endregion

        #region Update and Draw
        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);
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
        #endregion
    }
}
