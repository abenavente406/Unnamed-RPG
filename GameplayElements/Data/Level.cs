using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameplayElements.Data
{
    public class Level
    {
        public string name;

        public string Name { get { return name; } }

        public Level(string name, string location)
        {
            this.name = name;
        }

        public void Draw(SpriteBatch batch)
        {
            
        }

    }
}
