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
            Console.Clear();
            if (fish.Count == 0)
            {
                Console.WriteLine("El inventario está vacío.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Inventario:");
            for (int i = 0; i < fish.Count; i++)
            {
                Fish f = fish[i];
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
                string line = string.Empty;
                if (f.isFavorite)
                    line += "(Fav) ";

                line += $"({i + 1}) {f.name} ({f.rarity}, {f.CalculateSellPrice()})";
                Console.WriteLine(line);
                Console.ResetColor();
            }
            Console.WriteLine(isFull() ? "El inventario está lleno." : $"Espacio utilizado: {fish.Count}/{maxSlots}");
            Console.WriteLine($"Valor total: ${GetTotalValue()}");

            //FavoriteFish();
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

        //public void FavoriteFish() i give up
        //{
        //    Console.WriteLine("\n Ingresa el número de un pez para agregarlo/quitarlo de tus favoritos...\n Presiona enter o ingresa 0 para salir del inventario");

        //    int idx = 0;
        //    bool valid = false;

        //    while (!valid)
        //    {
        //        string? rta = Console.ReadLine();

        //        if (string.IsNullOrWhiteSpace(rta) || rta == "0" || (idx - 1) < 1 || idx - 1 > fish.Count)
        //            return;

        //        valid = int.TryParse(rta, out idx) && (idx >= 1 && idx <= fish.Count);
        //        if (!valid)
        //        {
        //            Display();
        //            Console.WriteLine("Ingresa un índice válido entre 1 y " + fish.Count);
        //        }
        //    }

        //    fish.ElementAt(idx - 1).ToggleFavorite();
        //    Display();
        //}
    }
}
