using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Fish(string name)
    {
        public string name = name;
        public int basePrice;
        public float weight, maxWeight;
        public Rarity rarity;
        protected Random random = new();

        public bool isFavorite = false;
        public void CalculateWeight()
        {
            weight = (float)Math.Ceiling(0.8f + (float)(random.NextDouble() * maxWeight));
        }
        public int CalculateSellPrice()
        {
            float rarityMult = rarity switch
            {
                Rarity.Common => 1f,
                Rarity.Uncommon => 1.3f,
                Rarity.Rare => 2f,
                Rarity.Epic => 3f,
                Rarity.Legendary => 5f,
                _ => 1f
            };

            float weightBonus = 1f + ((float)weight / maxWeight); // Heavier fish are worth more, up to double price at max weight
            return (int)(basePrice * rarityMult * weightBonus);
        }

        public void ToggleFavorite()
        {
            isFavorite = !isFavorite;
        }
    }
}
