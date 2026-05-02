using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Salmon: Fish
    {
        public Salmon() : base("Salmón")
        {
            basePrice = 45;
            rarity = Rarity.Rare;
            maxWeight = 6f;
        }

    }
}
