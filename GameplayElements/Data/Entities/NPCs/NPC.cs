using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ProjectElements.Data;

namespace GameplayElements.Data.Entities.NPCs
{
    public class NPC : Entity
    {

        protected Player player;
        protected string dialogue;

        public string Dialogue { get { return dialogue; } set { dialogue = value; } }

        public NPC(string name, Vector2 pos)
            : base(name, pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            player = this.ScanForPlayer();

            if (!(player == null))
            {
                var angle = Math.Atan2(player.Position.Y - Position.Y, player.Position.X - pos.X);
                if (angle > MathHelper.PiOver4 && angle < MathHelper.PiOver4 * 3) direction = 1;
                else if (angle < -MathHelper.PiOver4 && angle > -MathHelper.PiOver4 * 3) direction = 0;
                else if (angle < MathHelper.PiOver4 && angle > -MathHelper.PiOver4) direction = 3;
                else if (angle > MathHelper.PiOver4 * 3 && angle < MathHelper.PiOver4 * 5) direction = 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, GameTime gameTime)
        {
            base.Draw(batch, gameTime);

            if (player != null)
                batch.DrawString(ProjectData.smallFont, dialogue, new Vector2(pos.X - (Position.X / 2), pos.Y -
                    ProjectData.smallFont.MeasureString(dialogue).Y - 2), Color.White);
        }
    }
}
