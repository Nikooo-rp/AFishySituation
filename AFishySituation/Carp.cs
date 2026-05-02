using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Carp: Fish
    {
        public Carp() : base("Carpa")
        {
            basePrice = 8;
            rarity = Rarity.Common;
            maxWeight = 2f;
        }
    }
}
