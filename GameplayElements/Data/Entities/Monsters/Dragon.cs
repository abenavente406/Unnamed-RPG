using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Dragon : Monster
    {
        public Dragon(Vector2 pos)
            : base("Dragon", pos)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
