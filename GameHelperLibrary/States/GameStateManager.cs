using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameHelperLibrary
{
    public class GameStateManager : GameComponent
    {
        public event EventHandler OnStateChanged;

        Stack<GameState> gameStates = new Stack<GameState>();

        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;

        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        public GameStateManager(Game game)
            : base(game)
        {
            drawOrder = startDrawOrder;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;

                if (OnStateChanged != null)
                    OnStateChanged(this, null);
            }
        }

        public void RemoveState()
        {
            GameState State = gameStates.Peek();
            OnStateChanged -= State.StateChange;
            Game.Components.Remove(State);
            gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            drawOrder += drawOrderInc;
            newState.DrawOrder = drawOrder;

            AddState(newState);

            if (OnStateChanged != null)
                OnStateChanged(this, null);
        }

        private void AddState(GameState newState)
        {
            gameStates.Push(newState);

            Game.Components.Add(newState);

            OnStateChanged += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (gameStates.Count > 0)
                RemoveState();

            newState.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;

            AddState(newState);

            if (OnStateChanged != null)
                OnStateChanged(this, null);
        }
    }
}
