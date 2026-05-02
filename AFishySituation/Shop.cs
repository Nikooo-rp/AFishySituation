using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Shop
    {
        public List<Rod> availableRods = new List<Rod> { new WoodenRod(), new SteelRod(), new ExpertRod(), new LegendaryRod() };
        private Player player;

        public Shop(Player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            int choice = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a la tienda, ¿qué te gustaría hacer? (Tienes: $" + player.gold + ").");
                Console.WriteLine("1. Ver cañas disponibles");
                Console.WriteLine("2. Comprar una caña");
                Console.WriteLine("3. Vender peces");
                Console.WriteLine("4. Salir de la tienda.");
                Console.Write("Ingresa tu elección: ");
                
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayStock();
                            Console.ReadKey();
                            break;
                        case 2:
                            DisplayStock();
                            Console.WriteLine("----------------------------------------------------------");
                            Console.Write("Ingresa el número de la caña que quieres comprar: ");
                            if (int.TryParse(Console.ReadLine(), out int rodIndex))
                            {
                                BuyRod(rodIndex);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Entrada inválida. Ingresa un valor.");
                                Console.ResetColor();
                            }
                            break;
                        case 3:
                            player.inventory.SellAll();
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.WriteLine("Gracias por visitar la tienda!");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Elección inválida, por favor ingresa un número entre el 1 y el 4.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor ingresa un número.");
                }
            } while (choice != 4);
        }
        public void DisplayStock()
        {
            Console.WriteLine("Aquí están las cañas disponibles:");
            for (int i = 0; i < availableRods.Count; i++)
            {
                Rod rod = availableRods[i];

                //if !isUnlocked(rod) // Skips rods that are not unlocked yet
                //    continue;


                if (GetAffordableRods().Contains(rod))
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.WriteLine($"{i + 1}. {rod.name} - Poder: {rod.power}, Precio: ${rod.price}");
            }
            Console.ResetColor();
        }

        public void BuyRod(int rodIndex)
        {
            if (rodIndex < 1 || rodIndex > availableRods.Count)
            {
                Console.WriteLine("Selección inválida, por favor ingresa un número válido.");
                return;
            }
            Rod selectedRod = availableRods[rodIndex - 1];
            if (player.gold >= selectedRod.price)
            {
                player.SpendGold(selectedRod.price);
                player.EquipRod(selectedRod);
                availableRods.Remove(selectedRod);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Has comprado la {selectedRod.name}!");
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.WriteLine("...");
                Thread.Sleep(500);
                Console.WriteLine(selectedRod.description);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No tienes suficiente oro para comprar esta caña.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        public List<Rod> GetAffordableRods()
        {
            List<Rod> r = new List<Rod>();
            foreach (Rod rod in availableRods)
            {
                if (player.gold >= rod.price)
                {
                    r.Add(rod);
                }
            }
            return r;
        }
    }
}
