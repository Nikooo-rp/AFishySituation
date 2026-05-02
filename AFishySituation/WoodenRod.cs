using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class WoodenRod: Rod
    {
        public WoodenRod()
        {
            name = "Caña de madera";
            power = 0;
            price = 0;
            rarityWeights = new Dictionary<Rarity, float>
            {
                { Rarity.Common, 75 },
                { Rarity.Uncommon, 25 },
                { Rarity.Rare, 0 },
                { Rarity.Epic, 0 },
                { Rarity.Legendary, 0}
            };
            maxRarity = Rarity.Uncommon;
            description = "Es un palito con una cuerda atada a la punta.";
        }
    }
}
