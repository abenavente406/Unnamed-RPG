using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Slime : Monster
    {
        public Slime(Vector2 pos)
            : base("Slime", pos)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
