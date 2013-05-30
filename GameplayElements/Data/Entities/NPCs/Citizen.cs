using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectElements.Data;
using GameHelperLibrary;

namespace GameplayElements.Data.Entities.NPCs
{
    class Citizen : NPC
    {
        private string[] names = new string[] {
            "Alisha",
            "Jesse",
            "Elizabeth",
            "Connor",
            "Alexis",
            "Marny",
            "Maryad",
            "Winston",
            "Julia",
            "David",
            "Jackie",
            "Aroma",
            "Jasmine"
        };

        private string[] genericPhrases = new string[] {
            "Hi there!",
            "Who are you?",
            "Are you new around here?"
        };

        public Citizen(Vector2 pos) :
            base("Citizen", pos)
        {
            var rand = new Random();
            var nameIndex = rand.Next(names.Length);
            Name = names[nameIndex];

            Texture2D[] imgs1, imgs2, imgs3;
            imgs1 = new Texture2D[2];
            imgs2 = new Texture2D[2];
            imgs3 = new Texture2D[2];
            for (int i = 0; i < 2; i++)
            {
                imgs1[i] = nameIndex % 2 == 0 ? ProjectData.Content.Load<Texture2D>("Sprites\\Up" + i.ToString()) :
                    ProjectData.Content.Load<Texture2D>("Sprites\\Up" + i.ToString());
            }
            for (int i = 0; i < 2; i++)
            {
                imgs2[i] = nameIndex % 2 == 0 ? ProjectData.Content.Load<Texture2D>("Sprites\\Left" + i.ToString()) :
                    ProjectData.Content.Load<Texture2D>("Sprites\\Left" + i.ToString());
            }
            for (int i = 0; i < 2; i++)
            {
                imgs3[i] = nameIndex % 2 == 0 ? ProjectData.Content.Load<Texture2D>("Sprites\\Down" + i.ToString()) :
                    ProjectData.Content.Load<Texture2D>("Sprites\\Down" + i.ToString());
            }

            movingUp = new Animation(imgs1);
            movingRight = new Animation(imgs2);
            movingDown = new Animation(imgs3);

            avatarUp = new Image(imgs1[0]);
            avatarRight = new Image(imgs2[0]);
            avatarDown = new Image(imgs3[0]);

            dialogue = genericPhrases[rand.Next(genericPhrases.Length)];
        }
    }
}
