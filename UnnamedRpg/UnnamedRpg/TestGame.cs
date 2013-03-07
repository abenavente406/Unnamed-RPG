using System;
using System.Collections.Generic;
using GameHelperLibrary;
using ProjectElements.Data;
using ProjectElements.IO;
using GameplayElements.Managers;
using GameplayElements.Data.Entities;
using GameplayElements.Data.Entities.Monsters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnnamedRpg.GameScreens;

namespace UnnamedRpg
{
    public class TestGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;

        public SpriteBatch spriteBatch;

        GameStateManager stateManager;

        public TitleScreen TitleScreen;
        public StartMenuScreen StartMenuScreen;

        public readonly Rectangle ScreenRectangle;

        public TestGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = ProjectData.GameWidth;
            graphics.PreferredBackBufferHeight = ProjectData.GameHeight;
            graphics.IsFullScreen = ProjectData.isFullScreen;
            graphics.ApplyChanges();

            ScreenRectangle = new Rectangle(0, 0, ProjectData.GameWidth, ProjectData.GameHeight);

            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            TitleScreen = new TitleScreen(this, stateManager);
            StartMenuScreen = new GameScreens.StartMenuScreen(this, stateManager);

            stateManager.ChangeState(TitleScreen);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

     }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //em.UpdateAll(gameTime);

            //int dirX = 0;
            //int dirY = 0;

            //if (InputHandler.KeyDown(Keys.Up))
            //    dirY--;
            //if (InputHandler.KeyDown(Keys.Down))
            //    dirY++;
            //if (InputHandler.KeyDown(Keys.Left))
            //    dirX--;
            //if (InputHandler.KeyDown(Keys.Right))
            //    dirX++;

            //camera.Move(new Vector2(dirX * EntityManager.player.Speed, dirY * EntityManager.player.Speed));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            /* Unused for now... too advanced */
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
            //            DepthStencilState.Default, RasterizerState.CullNone, null, camera.get_transformation(GraphicsDevice));

            //spriteBatch.Begin();
            //lm.Draw(spriteBatch);
            //spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
            //            DepthStencilState.Default, RasterizerState.CullNone);
            //em.Draw(spriteBatch, gameTime);
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
