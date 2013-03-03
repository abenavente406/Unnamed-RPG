using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class GiantZombie : Monster
    {
        public GiantZombie(Vector2 pos)
            : base("Zombie", pos)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
