using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Ghost : Monster
    {
        public Ghost(Vector2 pos)
            : base("Ghost", pos)
        {
            SetTexture(new Vector2(0, 0), "Entity Sprites 1");
        }
    }
}
