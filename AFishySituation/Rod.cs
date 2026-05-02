using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Rod
    {
        public string name;
        public int power;
        public int price;
        protected Dictionary<Rarity, float> rarityWeights;
        protected Rarity maxRarity;
        public string description = "";

        public Rarity RollFishRarity()
        {
            // Llenamos un diccionario con las rarezas disponibles según el maxRarity y sus pesos correspondientes
            var pool = new Dictionary<Rarity, float>();
            foreach (Rarity r in Enum.GetValues(typeof(Rarity)))
            {
                if (r <= maxRarity)
                    pool[r] = rarityWeights[r];
            }

            float total = pool.Values.Sum(); // "Peso" total de todas las rarezas disponibles
            float roll = (float)new Random().NextDouble() * total; // Obtener un número aleatorio entre 0 y el total

            float cumulative = 0f;
            foreach (var entry in pool)
            {
                cumulative += entry.Value; // Sumar el peso de la rareza actual
                if (roll < cumulative) // Cuando superamos el número aleatorio, esa es la rareza seleccionada
                {
                    return entry.Key;
                }
            }
            return Rarity.Common; // Fallback.
        }
    }
}
