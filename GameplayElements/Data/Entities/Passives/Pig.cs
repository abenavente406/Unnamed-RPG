using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Passives
{
    public class Pig : Passive
    {
        public Pig(Vector2 pos)
            : base("Piggy", pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
