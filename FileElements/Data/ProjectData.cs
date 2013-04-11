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
using Microsoft.Xna.Framework.Audio;

namespace ProjectElements.Data
{
    public class ProjectData
    {
        public static GraphicsDeviceManager Graphics;
        public static ContentManager Content;

        /// <summary>
        /// Change these values if you want to change the game's window's dimensions
        /// </summary>
        public static int GameWidth = 960;
        public static int GameHeight = 544;
        public static bool isFullScreen = false;

        public static DrawingBatch Drawer;

        public static SaveData cachedSave = null;   // The most recent save

        public static SaveDataParser parser;
        public static string lastPerson = null;     // The most recent person to play

        public static SoundEffect guiBeep;
        public static SoundEffect guiSelect;

        /// <summary>
        /// Set up the project elements
        /// </summary>
        /// <param name="content">General content manager so we can load files from different classes</param>
        /// <param name="graphics">Graphics device manager that is created in the main game class</param>
        public ProjectData(ContentManager content, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Content = content;
            Drawer = new DrawingBatch(graphics.GraphicsDevice);

            parser = new SaveDataParser();  // Initialize the save data parser... duh right?

            try
            {
                lastPerson = new StreamReader(SaveDataParser.myGamesDir + "\\last_person.txt").ReadLine();  // Figure out who the last person was
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("last_person file does not exist.\n" + ex.ToString());
            }

            if (lastPerson != null)
                cachedSave = SaveDataParser.LoadGameState(lastPerson);  // Make the game able to "Continue" game

            guiBeep = Content.Load<SoundEffect>("Sounds\\gui_beep");      // Basic UI sound when "Up" or "Down" are pressed
            guiSelect = Content.Load<SoundEffect>("Sounds\\gui_select");  // Basic UI sound when "Enter" is pressed
        }
    }
}
