using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.Monsters
{
    public class Monster : Entity
    {
        public Monster(string name, Vector2 pos)
            : base(name, pos)
        {

        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
