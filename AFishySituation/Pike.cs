using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Pike: Fish
    {
        public Pike() : base("Lucio")
        {
            basePrice = 50;
            rarity = Rarity.Rare;
            maxWeight = 8f;
        }
    }
}
