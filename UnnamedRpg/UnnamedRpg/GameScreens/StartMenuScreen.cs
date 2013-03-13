using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ProjectElements.Data;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnnamedRpg.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {

        // Optional
        protected Animation backGround;

        PictureBox picBackGround;
        PictureBox arrowImage;

        LinkLabel continueGame;
        LinkLabel startGame;
        LinkLabel loadGame;
        LinkLabel exitGame;

        Label title;
        float maxItemWidth = 0f;

        int changes = 0;

        public StartMenuScreen(Game game, GameStateManager manager)
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

            ContentManager Content = Game.Content;
            Color selectedColor = Color.Navy;

            int selectedWallpaper = new Random().Next(4);

            switch (selectedWallpaper)
            {
                case 0:
                    {
                        Texture2D[] images = new Texture2D[8];

                        for (int i = 0; i < 8; i++)
                        {
                            images[i] = Content.Load<Texture2D>("Backgrounds\\exploded_bg\\water" + i.ToString());
                        }

                        backGround = new Animation(images);
                        break;
                    }
                case 1:
                    {
                        Texture2D bg1 = Content.Load<Texture2D>("Backgrounds\\bamboo");
                        picBackGround = new PictureBox(bg1, new Rectangle(0, 0, ProjectData.GameWidth, ProjectData.GameHeight),
                            new Rectangle(0, 0, bg1.Width, bg1.Height));
                        picBackGround.Position = new Vector2(0, 0);
                        ControlManager.Add(picBackGround);
                        break;
                    }
                case 2:
                    {
                        Texture2D bg2 = Content.Load<Texture2D>("Backgrounds\\temple");
                        picBackGround = new PictureBox(bg2, new Rectangle(0, 0, ProjectData.GameWidth, ProjectData.GameHeight),
                            new Rectangle(0, 0, bg2.Width, bg2.Height));
                        picBackGround.Position = new Vector2(0, 0);
                        ControlManager.Add(picBackGround);
                        break;
                    }
                case 3:
                    {
                        Texture2D bg3 = Content.Load<Texture2D>("Backgrounds\\marketplace");
                        picBackGround = new PictureBox(bg3, new Rectangle(0, 0, bg3.Width, ProjectData.GameHeight),
                            new Rectangle(0, 0, bg3.Width, bg3.Height));
                        picBackGround.Position = new Vector2(0, 0);
                        ControlManager.Add(picBackGround);
                        break;

                    }
            }

            Texture2D arrowTexture = Content.Load<Texture2D>("GUI\\leftarrowUp");
            arrowImage = new PictureBox(arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(arrowImage);

            title = new Label();
            title.SpriteFont = Content.Load<SpriteFont>("Fonts\\titleFont");
            title.Text = "O  R  I  G  I  N  S";
            title.Size = title.SpriteFont.MeasureString(title.Text);

            if (selectedWallpaper != 2 && selectedWallpaper != 3)
                title.Color = Color.Black;

            title.Position = new Vector2(ProjectElements.Data.ProjectData.GameWidth / 2 -
                title.Size.X / 2, ProjectData.GameHeight / 8);
            ControlManager.Add(title);

            continueGame = new LinkLabel();
            continueGame.Enabled = (ProjectData.cachedSave != null);
            continueGame.Text = "Continue...";
            continueGame.Size = continueGame.SpriteFont.MeasureString(continueGame.Text);
            continueGame.Selected += new EventHandler(menuItem_Selected);

            if (!continueGame.Enabled)
                continueGame.Color = new Color(30, 30, 30);

            ControlManager.Add(continueGame);

            startGame = new LinkLabel();
            startGame.Text = "New Game";
            startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.Selected += menuItem_Selected;
            ControlManager.Add(startGame);

            loadGame = new LinkLabel();
            loadGame.Text = "Load Game";
            loadGame.Size = loadGame.SpriteFont.MeasureString(loadGame.Text);
            loadGame.Selected += menuItem_Selected;
            ControlManager.Add(loadGame);

            exitGame = new LinkLabel();
            exitGame.Text = "Quit";
            exitGame.Size = exitGame.SpriteFont.MeasureString(exitGame.Text);
            exitGame.Selected += menuItem_Selected;
            ControlManager.Add(exitGame);

            ControlManager.NextControl();

            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);
            Vector2 position = new Vector2(ProjectElements.Data.ProjectData.GameWidth / 2, ProjectData.GameHeight / 1.75f);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    Vector2 offset = new Vector2(c.Size.X / 2, 0);
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;
                    c.Position = position - offset;
                    position.Y += c.Size.Y + 10f;
                    c.Effect = ControlEffect.FLASH;
                    c.FlashDuration = 300;

                    if (selectedWallpaper != 3 && selectedWallpaper != 2)
                    {
                        c.Color = Color.Black;
                        ((LinkLabel)c).SelectedColor = selectedColor;
                    }
                }
            }

            if (continueGame.Enabled)
                ControlManager_FocusChanged(continueGame, null);
            else
                ControlManager_FocusChanged(startGame, null);
        }

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
            if (sender == startGame)
                StateManager.PushState(new NewGameScreen(Game, StateManager));
            else if (sender == loadGame)
                StateManager.PushState(new LoadGameScreen(Game, StateManager));
            else if (sender == continueGame)
                StateManager.PushState(new GamePlayScreen(Game, StateManager, ProjectData.cachedSave));
            else if (sender == exitGame)
                GameRef.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            {
                base.Draw(gameTime);
                if (picBackGround == null)
                    backGround.Draw(GameRef.spriteBatch, gameTime, GameRef.GraphicsDevice.Viewport.Bounds);
                ControlManager.Draw(GameRef.spriteBatch, gameTime);
            }
            GameRef.spriteBatch.End();
        }


    }
}
