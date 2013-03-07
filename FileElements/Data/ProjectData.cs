using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RaisingStudio.Xna.Graphics;

namespace ProjectElements.Data
{
    public class ProjectData
    {
        public static GraphicsDeviceManager Graphics;
        public static ContentManager Content;

        public static int GameWidth = 1366;
        public static int GameHeight = 768;
        public static bool isFullScreen = true;

        public static DrawingBatch Drawer;

        public ProjectData(ContentManager content, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Content = content;
            Drawer = new DrawingBatch(graphics.GraphicsDevice);
        }
    }
}
