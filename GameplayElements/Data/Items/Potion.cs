using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameplayElements.Data
{
    public abstract class Potion : Item
    {
        public Potion() : base("Default", 0f, 0xFF) { }
    }

    public enum PotionEffect
    {
        HEAL,
        HURT
    }
}
