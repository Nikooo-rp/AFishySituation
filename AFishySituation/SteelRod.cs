using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class SteelRod: Rod
    {
        public SteelRod()
        {
            name = "Caña metálica";
            power = 1;
            price = 200;
            rarityWeights = new Dictionary<Rarity, float>
            {
                { Rarity.Common, 50 },
                { Rarity.Uncommon, 35 },
                { Rarity.Rare, 15 },
                { Rarity.Epic, 0 },
                { Rarity.Legendary, 0}
            };
            maxRarity = Rarity.Rare;
            description = "Una caña resistente y brillante, puede atrapar peces más raros";
        }
    }
}
