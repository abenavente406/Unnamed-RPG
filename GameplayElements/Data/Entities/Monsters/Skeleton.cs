using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Skeleton : Monster
    {
        
        public Skeleton(Vector2 pos)
            : base("Skeleton", pos)
        {
            SetTexture(new Vector2(6, 0), "Entity Sprites 1");
        }

       
    }
}
