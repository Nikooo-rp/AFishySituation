using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AFishySituation
{
    public class FishingMinigame(Fish fish)
    {
        private readonly Fish fish = fish;
        private readonly Random random = new();

        // El progreso es un float entre 0 y 1, se incrementa con cada pull exitoso y se decrementa con cada pull fallido o input prematuro. El jugador gana al llegar a 1, pierde al llegar a 0.
        private float progress = 0f;
        private const int BAR_WIDTH = 30;

        // Cuántos golpes se necesitan para atrapar el pez según su rareza.
        private int HitsRequired => fish.rarity switch
        {
            Rarity.Common => 5,
            Rarity.Uncommon => 6,
            Rarity.Rare => 8,
            Rarity.Epic => 12,
            Rarity.Legendary => 16,
            _ => 3
        };

        // Tiempo de reacción por rareza en milisegundos.
        private int ReactionWindowMs => fish.rarity switch
        {
            Rarity.Common => 1800,
            Rarity.Uncommon => 1400,
            Rarity.Rare => 800,
            Rarity.Epic => 430,
            Rarity.Legendary => 360,
            _ => 1000
        };

        // Run ejecuta el minijuego, devuelva una tupla según el pez es atrapado o no y si el jugador decide volver a lanzar o no.
        public (bool caught, bool recast) Run()
        {
            Console.Clear();
            Console.WriteLine($"Algo mordió!");
            Console.WriteLine("Reacciona cuando el pez hale! (Presiona ENTER).\n");
            Thread.Sleep(1000);

            // Recalculate hit/miss increments based on rarity
            float hitIncrement = 1f / HitsRequired;
            float missDecrement = hitIncrement * 0.8f; // miss costs ~80% of one hit

            while (true)
            {
                // Delay aleatorio.
                int delay = random.Next(200, 1500);
                bool stillOn = WaitForPull(delay, missDecrement);
                if (!stillOn)
                {
                    Console.WriteLine("\nEl pez se escapó!");
                    Console.WriteLine("Presiona ENTER para continuar pescando, o cualquier otra tecla para volver a la costa.");
                    var key = Console.ReadKey(true);
                    return (false, key.Key == ConsoleKey.Enter);
                }

                // Cuando el pez hala.
                int reactionMs = ReactionWindowMs;
                bool reacted = WaitForReaction(reactionMs); // Test de reacción.

                if (reacted)
                {
                    progress = Math.Min(1f, progress + hitIncrement); // Avanza la barra.
                }
                else
                {
                    Console.Beep(100, 50);
                    Console.Beep(100, 50);
                    progress = Math.Max(0f, progress - missDecrement); // Penaliza por no reaccionar a tiempo.
                }

                Console.ResetColor();
                DrawProgressBar();

                if (progress >= 1f)
                {
                    ReportCatch();
                    Console.WriteLine("Presiona ENTER para continuar pescando, o cualquier otra tecla para volver a la costa.");
                    var key = Console.ReadKey(true);
                    return (caught: true, recast: key.Key == ConsoleKey.Enter);
                }
                if (progress <= 0f)
                {
                    Console.Beep(150, 300);
                    Console.WriteLine("\nEl pez se escapó...");
                    Console.WriteLine("Presiona ENTER para continuar pescando, o cualquier otra tecla para volver a la costa.");
                    var key = Console.ReadKey(true);
                    return (false, key.Key == ConsoleKey.Enter);
                }
            }
        }

        public void ReportCatch()
        {
            fish.CalculateWeight();
            switch (fish.rarity)
            {
                case Rarity.Common:
                    Console.ForegroundColor = ConsoleColor.Gray;
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
            Console.Beep(200, 50);
            Console.Beep(350, 50);
            Console.Beep(600, 50);
            Console.Beep(800, 150); // celebratory beep
            Console.WriteLine($"Atrapaste un(a) {fish.name}! ({fish.weight}kg)");
            Console.ResetColor();
        }

        private bool WaitForReaction(int windowMs)
        {
            // Evitamos inputs previos que puedan interferir con el test de reacción.
            while (Console.KeyAvailable) Console.ReadKey(true);

            var sw = Stopwatch.StartNew();

            Console.Beep(200, 100);
            while (sw.ElapsedMilliseconds < windowMs)
            {
                double ratio = 1.0 - (sw.ElapsedMilliseconds / (double)windowMs);
                string prompt = ratio > 0.8 ? "  !!!!!"
                                : ratio > 0.6 ? "  !!!! "
                                : ratio > 0.4 ? "  !!!  "
                                : ratio > 0.2 ? "  !!   "
                                : "  !    ";

                Console.Write($"\r{prompt}  ");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Beep(600, 100);
                        Console.Write("\r                              \r");
                        return true;
                    }
                }

                Thread.Sleep(16); // Pequeña pausa para evitar un loop demasiado rápido, también ayuda a que el prompt se actualice de manera fluida.
            }

            Console.Write("\r                              \r");
            return false;
        }

        private bool WaitForPull(int delayMs, float missDecrement)
        {
            while (Console.KeyAvailable) Console.ReadKey(true); // Evitamos un input accidental.

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < delayMs)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    progress = Math.Max(0f, progress - missDecrement * 0.9f);

                    Console.Beep(100, 50);
                    Console.Beep(100, 50);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DrawProgressBar();
                    Console.ResetColor();

                    if (progress <= 0f) return false; // si se llega acá, el pez se escapa por un input prematuro
                }
                Thread.Sleep(16);
            }
            return true;
        }
        private void DrawProgressBar()
        {
            int filled = (int)(progress * BAR_WIDTH);
            string bar = new string('#', filled) + new string('-', BAR_WIDTH - filled); // Esta operación genera un numero de # y - dependiendo del progreso actual, para mostrar una barra de progreso visual.
            Console.WriteLine($"\r [{bar}] {(int)(progress * 100)}%");
        }
    }
}
