using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameplayElements.Data.Entities;

namespace GameplayElements.Managers
{
    public class EntityManager
    {
        public List<Entity> allEntities = new List<Entity>();

        public List<GameplayElements.Data.Entities.Passives.Passive> passives =
            new List<Data.Entities.Passives.Passive>();

        public List<GameplayElements.Data.Entities.NPCs.NPC> npcs =
            new List<Data.Entities.NPCs.NPC>();

        public List<GameplayElements.Data.Entities.Monsters.Monster> monsters =
            new List<Data.Entities.Monsters.Monster>();

        public static Player player;

        public EntityManager()
        {
            player = new Player("Anthony", new Vector2(0, 0));
        }

        public void UpdateAll(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
            UpdatePassives(gameTime);
            UpdateNpcs(gameTime);
            UpdateMonsters(gameTime);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public void UpdateNpcs(GameTime gameTime)
        {
            npcs.ForEach(delegate(GameplayElements.Data.Entities.NPCs.NPC npc)
            {
                npc.Update(gameTime);
            });
        }

        public void UpdatePassives(GameTime gameTime)
        {
            passives.ForEach(delegate(GameplayElements.Data.Entities.Passives.Passive passive)
            {
                passive.Update(gameTime);
            });
        }

        public void UpdateMonsters(GameTime gameTime)
        {
            monsters.ForEach(delegate(GameplayElements.Data.Entities.Monsters.Monster monster)
            {
                monster.Update(gameTime);
            });
        }

    }
}
