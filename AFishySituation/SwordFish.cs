using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class SwordFish: Fish
    {
        public SwordFish() : base("Pez espada")
        {
            basePrice = 110;
            rarity = Rarity.Epic;
            maxWeight = 15f;
        }
    }
}
