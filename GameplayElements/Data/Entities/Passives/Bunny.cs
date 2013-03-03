using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Passives
{
    public class Bunny : Passive
    {
        public Bunny(Vector2 pos)
            : base("Bunny!", pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
