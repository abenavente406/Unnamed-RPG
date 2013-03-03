using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Zombie : Monster
    {
        public Zombie(Vector2 pos)
            : base("Zombie", pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
