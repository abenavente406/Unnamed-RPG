using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.NPCs
{
    public class QuestGiver : NPC
    {
        public QuestGiver(string name, Vector2 pos)
            : base(name, pos)
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
