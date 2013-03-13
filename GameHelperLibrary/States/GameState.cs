using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameHelperLibrary
{
    public abstract partial class GameState : Microsoft.Xna.Framework.DrawableGameComponent
    {

        List<GameComponent> childComponents;

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        GameState tag;
        public GameState Tag
        {
            get { return tag; }
        }

        protected GameStateManager StateManager;

        public GameState(Game game, GameStateManager manager)
            : base(game)
        {
            StateManager = manager;
            childComponents = new List<GameComponent>();
            tag = this;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;

                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
                if (component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        protected void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
    }
}
