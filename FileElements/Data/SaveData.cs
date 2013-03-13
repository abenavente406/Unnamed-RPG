using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ProjectElements.Data
{
    public class SaveData
    {

        public string Name { get; set; }
		
        public float Health { get; set; }
		
        public Vector2 Position { get; set; }
        public int Direction { get; set; }
		
		public bool NoClipEnabled { get; set; }
		public bool GodModeEnabled { get; set; }
		public bool SuperSpeedEnabled { get; set; }

        public string Time { get; set; }

        public static SaveData CreateNewSave(string name)
        {
            return new SaveData()
            {
                Name = name,
                Health = 100,
                Position = new Vector2(ProjectData.GameWidth / 2, ProjectData.GameHeight / 2),
                NoClipEnabled = false,
                GodModeEnabled = false,
                SuperSpeedEnabled = false,
                Time = System.DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt")
            };
        }
    }
}
