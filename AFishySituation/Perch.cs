using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Perch: Fish
    {
        public Perch() : base("Perca")
        {
            basePrice = 18;
            rarity = Rarity.Uncommon;
            maxWeight = 3f;
        }
    }
}
