using System;
using System.Collections.Generic;
using ProjectElements.Data;
using ProjectElements.IO;
using GameplayElements.Managers;
using GameplayElements.Data.Entities;
using GameplayElements.Data.Entities.Monsters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnnamedRpg
{
    public class TestGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont defaultFont;
        ProjectData pj;
        SpriteSheetManager ssm;
        EntityManager em;
        LevelManager lm;
        Camera camera;

        Random rand = new Random();

        public TestGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = ProjectData.GameWidth;
            graphics.PreferredBackBufferHeight = ProjectData.GameHeight;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();


        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            pj = new ProjectData(Content, graphics);
            ssm = new SpriteSheetManager(graphics.GraphicsDevice, Content);
            lm = new LevelManager(Content);
            em = new EntityManager();

            EntityManager.AddMonster(new Skeleton(new Vector2(rand.Next(ProjectData.GameWidth),
                rand.Next(ProjectData.GameHeight))));

            camera = new Camera(new Vector2(EntityManager.player.Position.X - ProjectData.GameWidth / 2,
                EntityManager.player.Position.Y - ProjectData.GameHeight / 2), ProjectData.GameWidth, ProjectData.GameHeight);
     }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            em.UpdateAll(gameTime);

            int dirX = 0;
            int dirY = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                dirY--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                dirY++;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                dirX--;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                dirX++;

            camera.Move(new Vector2(dirX * EntityManager.player.Speed, dirY * EntityManager.player.Speed));

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            /* Unused for now... too advanced */
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
            //            null, null, null, camera.get_transformation(GraphicsDevice));

            spriteBatch.Begin();
            lm.Draw(spriteBatch);
            spriteBatch.End();
 
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                DepthStencilState.Default, RasterizerState.CullNone);
            em.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
