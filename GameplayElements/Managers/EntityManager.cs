using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameplayElements.Data.Entities;
using ProjectElements.Data;
using GameplayElements.PathFinding;
using GameHelperLibrary.Shapes;

namespace GameplayElements.Managers
{
    public class EntityManager
    {
        public static List<Entity> allEntities = new List<Entity>();

        public static List<GameplayElements.Data.Entities.Passives.Passive> passives =
            new List<Data.Entities.Passives.Passive>();

        public static List<GameplayElements.Data.Entities.NPCs.NPC> npcs =
            new List<Data.Entities.NPCs.NPC>();

        public static List<GameplayElements.Data.Entities.Monsters.Monster> monsters =
            new List<Data.Entities.Monsters.Monster>();

        public static Player player;
        public Pathfinder pathFinder;

        DrawableRectangle bounds;

        public EntityManager(SaveData data)
        {
            player = new Player(data.Name, new Vector2(0, 0));
            pathFinder = new Pathfinder(LevelManager.GetCurrentLevel());

            bounds = new DrawableRectangle(ProjectData.Graphics.GraphicsDevice, new Vector2(32, 32), Color.Red, true);
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

                if (monster.IsDead) monsters.Remove(monster);
            });
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            player.Draw(batch, gameTime);

            npcs.ForEach(delegate(GameplayElements.Data.Entities.NPCs.NPC npc)
            {
                if (Camera.IsOnCamera(npc as Entity))
                    npc.Draw(batch, gameTime);
            });
            passives.ForEach(delegate(GameplayElements.Data.Entities.Passives.Passive passive)
            {
                if (Camera.IsOnCamera(passive as Entity))
                    passive.Draw(batch, gameTime);
            });
            monsters.ForEach(delegate(GameplayElements.Data.Entities.Monsters.Monster monster)
            {
                if (Camera.IsOnCamera(monster as Entity))
                    monster.Draw(batch, gameTime);

                if (monster.aiState == Data.Entities.Monsters.AiState.TARGETTING)
                 monster.DrawPathToPlayer(batch, player);
            });
        }

        public static void AddNpc(Data.Entities.NPCs.NPC npc)
        {
            npcs.Add(npc);
        }

        public static void AddMonster(Data.Entities.Monsters.Monster monster)
        {
            monsters.Add(monster);
        }

        public static void AddPassive(Data.Entities.Passives.Passive passive)
        {
            passives.Add(passive);
        }
    }
}
