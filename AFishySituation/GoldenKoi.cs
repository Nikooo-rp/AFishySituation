using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class GoldenKoi: Fish
    {
        public GoldenKoi() : base("Koi Dorado")
        {
            basePrice = 400;
            rarity = Rarity.Legendary;
            maxWeight = 30f;
        }
    }
}
