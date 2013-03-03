using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.NPCs
{
    public class NPC : Entity
    {

        protected string dialogue;

        public string Dialogue { get { return dialogue; } set { dialogue = value; } }

        public NPC(string name, Vector2 pos)
            : base(name, pos)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
