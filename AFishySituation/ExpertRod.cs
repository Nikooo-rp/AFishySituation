using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class ExpertRod: Rod
    {
        public ExpertRod()
        {
            name = "Caña experta";
            power = 3;
            price = 600;
            rarityWeights = new Dictionary<Rarity, float>
            {
                { Rarity.Common, 25 },
                { Rarity.Uncommon, 35 },
                { Rarity.Rare, 25 },
                { Rarity.Epic, 15 },
                { Rarity.Legendary, 0}
            };
            maxRarity = Rarity.Epic;
            description = "Tiene un diploma por un torneo que ganó hace unos años... Si, la caña.";
        }
    }
}
