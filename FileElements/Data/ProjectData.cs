using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RaisingStudio.Xna.Graphics;
using ProjectElements.IO;

namespace ProjectElements.Data
{
    public class ProjectData
    {
        public static GraphicsDeviceManager Graphics;
        public static ContentManager Content;

        public static int GameWidth = 1366;
        public static int GameHeight = 768;
        public static bool isFullScreen = false;

        public static DrawingBatch Drawer;

        public static SaveData cachedSave = null;

        public static SaveDataParser parser;
        public static string lastPerson = null;

        public ProjectData(ContentManager content, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Content = content;
            Drawer = new DrawingBatch(graphics.GraphicsDevice);

            parser = new SaveDataParser();

          
            try
            {
                lastPerson = new StreamReader(SaveDataParser.myGamesDir + "\\last_person.txt").ReadLine();
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("last_person file does not exist.\n" + ex.ToString());
            }

            if (lastPerson != null)
                cachedSave = SaveDataParser.LoadGameState(lastPerson);
        }
    }
}
