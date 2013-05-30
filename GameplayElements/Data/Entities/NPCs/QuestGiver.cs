using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameplayElements.Data.Entities.NPCs
{
    public class QuestGiver : NPC
    {
        public QuestGiver(string name, Vector2 pos, string speech)
            : base(name, pos)
        {
            this.dialogue = speech;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
