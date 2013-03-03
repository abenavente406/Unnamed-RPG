using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace GameplayElements.Data
{
    public class Level
    {

        public TmxMap map;
        public string name;

        public string Name { get { return name; } }

        public Level(string name, string location)
        {
            this.name = name;
            map = new TmxMap(location);
        }

        public void Draw(SpriteBatch batch)
        {
            
        }

    }
}
