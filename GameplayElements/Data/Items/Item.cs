using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameplayElements.Data
{
    public abstract class Item
    {

        public string Name { get; private set; }
        public float Weight { get; private set; }
        public byte Id { get; private set; }

        public Item(string name, float weight, byte id)
        {
            Name = name;
            Weight = weight;
            Id = id;
        }
    }

    public enum ItemMaterial
    {
        TRAINING,
        WOOD,
        STONE,
        IRON,
        AURA,
        GOLD,
        PLATINUM,
        TITANIUM,
        DIAMOND,
        HELLSTONE
    }
}
