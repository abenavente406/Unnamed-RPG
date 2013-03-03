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

namespace GameplayElements
{
    public class Camera
    {

        private int ViewportWidth;
        private int ViewportHeight;

        private Vector2 pos;
        private Matrix transform;

        protected float zoom;
        protected float rotation;

        #region Properties
        public Vector2 Position 
        {
            get { return pos; }
            set { pos = value; }
        }
        public Matrix Transform 
        { 
            get { return transform; }
            set { transform = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = MathHelper.WrapAngle(value); }
        }
        #endregion

        public Camera(Vector2 startPos, int viewPortWidth, int viewPortHeight)
        {
            Position = startPos;
            Rotation = 0.0f;
            Zoom = 1.0f;

            ViewportWidth = viewPortWidth;
            ViewportHeight = viewPortHeight;
        }

        public void Move(Vector2 amount)
        {
            pos += amount;
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transform =  Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                         Matrix.CreateRotationZ(Rotation) *
                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                         Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
            return transform;
        }
    }
}
