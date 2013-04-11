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

        // Initialize global data used throughout the project
        public static ProjectData projectData;

        // Manages the game states/screens
        GameStateManager stateManager;

        // The title screen for our game
        public StartMenuScreen StartMenuScreen;

        // The size of our game screen/viewport
        public readonly Rectangle ScreenRectangle;

        /**
         * Constructor!
         * */
        public TestGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = ProjectData.GameWidth;
            graphics.PreferredBackBufferHeight = ProjectData.GameHeight;
            graphics.IsFullScreen = ProjectData.isFullScreen;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            ScreenRectangle = new Rectangle(0, 0, ProjectData.GameWidth, ProjectData.GameHeight);

        }

        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            projectData = new ProjectData(Content, graphics);
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            StartMenuScreen = new GameScreens.StartMenuScreen(this, stateManager);

            // Make the state manager jump to 
            stateManager.ChangeState(StartMenuScreen);

            base.Initialize();
        }

        /// <summary>
        /// This code snippet creates a text file that shows what save file was
        /// last to be open.  This allows the game to load the most recent save
        /// file for the "Continue" function
        /// </summary>
        protected override void UnloadContent()
        {
            if (SaveDataParser.savedStatus)
            {
                StreamWriter lastPerson;    // The stream that holds the data for the last person who played
                lastPerson = File.CreateText(SaveDataParser.myGamesDir + "\\last_person.txt");

                try
                {
                    lastPerson.WriteLine(EntityManager.player.Name);    // Write the current player to last player
                }
                catch (NullReferenceException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    // Cleanup
                    lastPerson.Close();
                }
            }
        }

        /**
         * ALL GAME LOGIC TAKES PLACE IN THE STATE CLASSES
         * */

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
