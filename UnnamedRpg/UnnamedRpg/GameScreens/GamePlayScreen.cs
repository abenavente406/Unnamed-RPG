﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectElements.Data;
using ProjectElements.IO;
using GameplayElements.Managers;
using GameplayElements.Data.Entities.Monsters;

namespace UnnamedRpg.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        SpriteSheetManager ssm;
        TileSheetManager tsm;
        LevelManager lm;

        Camera camera;

        Random rand = new Random();

        public GamePlayScreen(Game game, GameStateManager manager, SaveData data, int levelType)
            : base(game, manager)
        {
            ssm = new SpriteSheetManager(GameRef.GraphicsDevice, GameRef.Content);
            tsm = new TileSheetManager(GameRef.GraphicsDevice, GameRef.Content);
            lm = new LevelManager(GameRef.Content, data, levelType);
        }

        public GamePlayScreen(Game game, GameStateManager manager, SaveData data)
            : base(game, manager)
        {
            ssm = new SpriteSheetManager(GameRef.GraphicsDevice, GameRef.Content);
            tsm = new TileSheetManager(GameRef.GraphicsDevice, GameRef.Content);
            lm = new LevelManager(GameRef.Content, data); 
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            camera = new Camera(new Vector2(EntityManager.player.Position.X - ProjectData.GameWidth / 2,
                EntityManager.player.Position.Y - ProjectData.GameHeight / 2), ProjectData.GameWidth, ProjectData.GameHeight);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            lm.Update(gameTime);

            if (InputHandler.KeyDown(Keys.Add) ||
                InputHandler.KeyDown(Keys.OemPlus))
                camera.Zoom += .05f;

            if (InputHandler.KeyDown(Keys.Subtract) ||
                InputHandler.KeyDown(Keys.OemMinus))
                camera.Zoom -= .05f;

            camera.Update(EntityManager.player.Position);

            if (InputHandler.KeyPressed(Keys.Escape))
                SaveDataParser.SaveState(new SaveData()
                {
                    Name = EntityManager.player.Name,
                    Health = EntityManager.player.Health,
                    Position = EntityManager.player.Position,
                    Direction = EntityManager.player.Direction,
                    NoClipEnabled = EntityManager.player.NoClip,
                    GodModeEnabled = EntityManager.player.God,
                    SuperSpeedEnabled = EntityManager.player.SuperSpeed,
                    Time = System.DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"),
                    ExperienceLevel = EntityManager.player.ExperienceLevel,
                    ExperiencePoints = EntityManager.player.ExperiencePoints
                });

            if (InputHandler.KeyReleased(Keys.Escape))
                Game.Exit();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                        DepthStencilState.Default, RasterizerState.CullNone, null ,camera.GetTransformation(GraphicsDevice));
            {
                base.Draw(gameTime);
                lm.Draw(GameRef.spriteBatch, gameTime);
                FadeOutRect.Draw(GameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            }
            GameRef.spriteBatch.End();
        }

    }
}
