using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class LegendaryRod: Rod
    {
        public LegendaryRod()
        {
            name = "Caña legendaria";
            power = 5;
            price = 1500;
            rarityWeights = new Dictionary<Rarity, float>
            {
                { Rarity.Common, 0 },
                { Rarity.Uncommon, 0 },
                { Rarity.Rare, 0 },
                { Rarity.Epic, 50 },
                { Rarity.Legendary, 50}
            };
            maxRarity = Rarity.Legendary;
            description = "El anzuelo se ve como un koi extrañamente atractivo";
        }
    }
}
