using System;
using System.Collections.Generic;
using System.IO;

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

        public static ProjectData projectData;

        GameStateManager stateManager;

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

        }

        protected override void Initialize()
        {
            projectData = new ProjectData(Content, graphics);
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            StartMenuScreen = new GameScreens.StartMenuScreen(this, stateManager);

            stateManager.ChangeState(StartMenuScreen);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            if (SaveDataParser.savedStatus)
            {
                StreamWriter lastPerson;
                lastPerson = File.CreateText(SaveDataParser.myGamesDir + "\\last_person.txt");

                try
                {
                    lastPerson.WriteLine(EntityManager.player.Name);
                }
                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    lastPerson.Close();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
