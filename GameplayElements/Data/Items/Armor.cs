using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameplayElements.Data
{
    public class Armor : Item
    {

        public static Armor armorTraining = new Armor(ItemMaterial.TRAINING, "Training Armor", 10f, 0x00);
        public static Armor armorWood = new Armor(ItemMaterial.WOOD, "Wooden Armor", 12f, 0x01);
        public static Armor armorStone = new Armor(ItemMaterial.STONE, "Barbaric Armor", 25f, 0x02);
        public static Armor armorIron = new Armor(ItemMaterial.IRON, "Iron Armor", 18f, 0x03);
        public static Armor armorAura = new Armor(ItemMaterial.AURA, "Bound Armor", 0f, 0x04);
        public static Armor armorGold = new Armor(ItemMaterial.GOLD, "Armor of the Kings", 22f, 0x05);
        public static Armor armorPlatinum = new Armor(ItemMaterial.PLATINUM, "Platinum Armor", 17f, 0x06);
        public static Armor armorTitanium = new Armor(ItemMaterial.TITANIUM, "Titanium Armor", 15f, 0x07);
        public static Armor armorDiamond = new Armor(ItemMaterial.DIAMOND, "Diamond Armor", 25f, 0x08);
        public static Armor armorHellstone = new Armor(ItemMaterial.HELLSTONE, "Fiery Armor", 20f, 0x09);

        public Armor(ItemMaterial material, string name, float weight, byte id) :
            base(name, weight, id)
        {

        }
    }
}
