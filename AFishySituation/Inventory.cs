using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Inventory
    {
        public List<Fish> fish;
        public int maxSlots;
        public Action<int> soldFish;

        public Inventory()
        {
            fish = new List<Fish>();
            maxSlots = 40;
        }

        public void AddFish(Fish newFish)
        {
            if (fish.Count < maxSlots)
            {
                fish.Add(newFish);
                if (isFull())
                {
                    Console.WriteLine("El inventario ahora está lleno!");
                }
            }
            else
            {
                Console.WriteLine("El inventario está lleno! No se pueden agregar más peces.");
            }
        }

        public void RemoveFish(Fish fishToRemove)
        {
            if (fish.Contains(fishToRemove))
            {
                fish.Remove(fishToRemove);
                //Console.WriteLine($"Removed {fishToRemove.name} from inventory.");
            }
        }

        public void SellAll()
        {
            if (fish.Count == 0)
            {
                Console.WriteLine("No hay peces para vender!");
                return;
            }
            if (fish.Count == 1) 
            { 
                Fish f = fish[0];
                soldFish?.Invoke(f.CalculateSellPrice());
                Console.WriteLine($"Vendiste {f.name} por ${f.CalculateSellPrice()}");
                RemoveFish(f);
            }
            else
            {
                int totalSellPrice = GetTotalValue();
                int count = fish.Count;
                foreach (Fish f in fish.ToList())
                {
                    soldFish?.Invoke(f.CalculateSellPrice());
                    RemoveFish(f);
                }
                Console.WriteLine("Vendiste todos tus peces por $" + totalSellPrice + ".");

            }
        }

        public void Display()
        {
            if (fish.Count == 0)
            {
                Console.WriteLine("El inventario está vacío.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Inventario:");
            foreach (Fish f in fish)
            {
                Console.WriteLine($" ");
                switch (f.rarity)
                {
                    case Rarity.Common:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case Rarity.Uncommon:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Rarity.Rare:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case Rarity.Epic:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case Rarity.Legendary:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                Console.WriteLine($"- {f.name} ({f.rarity}, {f.CalculateSellPrice()})");
                Console.ResetColor();
            }
            Console.WriteLine(isFull() ? "El inventario está lleno." : $"Espacio utilizado: {fish.Count}/{maxSlots}");
            Console.WriteLine($"Valor total: ${GetTotalValue()}");
            Console.ReadKey();
        }

        public bool isFull()
        {
            return fish.Count >= maxSlots;
        }

        public int GetTotalValue()
        {
            int totalValue = 0;
            foreach (Fish f in fish)
            {
                totalValue += f.CalculateSellPrice();
            }
            return totalValue;
        }
    }
}
