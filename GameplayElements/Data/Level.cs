using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FuncWorks.XNA.XTiled;
using GameplayElements.Managers;

namespace GameplayElements.Data
{
    public class Level
    {
        public string name;
        private Map map;

        public string Name { get { return name; } }

        public Level(string name, string location)
        {
            this.name = name;
            map = LevelManager.content.Load<Map>(location);
        }

        public void Draw(SpriteBatch batch, Rectangle region)
        {
            map.Draw(batch, region);
        }

        public void DrawLayer(SpriteBatch batch, Rectangle region, int layerId)
        {
            map.DrawLayer(batch, layerId, region, 0.0f);
        }

    }
}
