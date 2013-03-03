using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameplayElements.Data
{
    public abstract class Weapon : Item
    {

        public Weapon(ItemMaterial material, string name, float weight, byte id)
            : base(name, weight, id)
        {

        }
    }
}
