using System;
using System.Collections.Generic;
using System.Linq;
using ProjectElements.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameplayElements.Data.Entities;

namespace GameplayElements.Managers
{
    public class Camera
    {

        private static int viewportWidth;
        private static int viewportHeight;

        private static Vector2 pos;

        #region Properties
        public static Vector2 Position
        {
            get { return pos; }
            set
            {
                //pos = Vector2.Clamp(value, Vector2.Zero, new Vector2(LevelManager.GetCurrentLevel().Width - viewportWidth,
                //    LevelManager.GetCurrentLevel().Height - viewportHeight));
                pos = value;
            }
        }
        #endregion

        public Camera(Vector2 startPos, int viewPortWidth, int viewPortHeight)
        {
            Position = startPos;

            viewportWidth = viewPortWidth;
            viewportHeight = viewPortHeight;
        }

        #region Advanced Camera Functions
        private Matrix transform;

        static float zoom = 1f;
        protected float rotation;

        public float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.1f, 3.0f); }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = MathHelper.WrapAngle(value); }
        }
        public Matrix TransformMatrix
        {
            get { return transform; }
            set { transform = value; }
        }


        public static Rectangle ViewPortRectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - viewportWidth / zoom / 2),
                    (int)(Position.Y - viewportHeight / zoom / 2), (int)(viewportWidth / zoom), (int)(viewportHeight / zoom));
            }
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Vector3 matrixRotOrigin = Vector3.Clamp(new Vector3(Position.X, Position.Y, 0), Vector3.Zero,
                new Vector3(LevelManager.GetCurrentLevel().width, LevelManager.GetCurrentLevel().height, 0));
            Vector3 matrixScreenPos = new Vector3(new Vector2(viewportWidth / 2, viewportHeight / 2), 0);

            transform = Matrix.CreateTranslation(-matrixRotOrigin) *
                         Matrix.CreateRotationZ(Rotation) *
                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1.0f)) *
                         Matrix.CreateTranslation(matrixScreenPos);

            return transform;
        }

        public void Move(Vector2 amount)
        {
            if (EntityManager.player.IsMoving)
            {

                Position += Vector2.Clamp(pos, Vector2.Zero, new Vector2(LevelManager.GetCurrentLevel().Width,
                    LevelManager.GetCurrentLevel().Height));
            }
        }

        public void Update(Vector2 newPos)
        {
            Position = newPos;
        }


        public static Vector2 Transform(Vector2 point)
        {
            return point;
        }

        public static bool IsOnCamera(Entity entity)
        {
            return IsOnCamera(entity.SpriteBoundingBox);
        }

        public static bool IsOnCamera(Rectangle rect)
        {
            return ViewPortRectangle.Intersects(rect);
        }
        #endregion
    }
}