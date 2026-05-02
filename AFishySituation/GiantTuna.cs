using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class GiantTuna: Fish
    {
        public GiantTuna() : base ("Atún gigante")
        {
            basePrice = 130;
            rarity = Rarity.Epic;
            maxWeight = 20f;
        }   
    }
}
