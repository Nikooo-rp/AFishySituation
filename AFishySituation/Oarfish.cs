using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Oarfish: Fish
    {
        public Oarfish() : base("Pez remo")
        {
            basePrice = 300;
            rarity = Rarity.Legendary;
            maxWeight = 50f;
        }
    }
}
