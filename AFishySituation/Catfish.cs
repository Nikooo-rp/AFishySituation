using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Catfish: Fish
    {
        public Catfish() : base("Bagre")
        {
            basePrice = 22;
            rarity = Rarity.Uncommon;
            maxWeight = 4f;
        }
    }
}
