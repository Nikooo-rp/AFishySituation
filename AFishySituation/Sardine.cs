using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Sardine: Fish
    {
        public Sardine() : base("Sardina")
        {
            basePrice = 5;
            rarity = Rarity.Common;
            maxWeight = 1f;

        }
    }
}
