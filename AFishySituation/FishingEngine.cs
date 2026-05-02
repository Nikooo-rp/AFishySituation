using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AFishySituation
{
    public class FishingEngine(Player player)
    {
        public Player player = player;
        private List<Fish> availableFish = [new Sardine(), new Carp(), new Perch(), new Catfish(), new Salmon(), new Pike(), new SwordFish(), new GiantTuna(), new Oarfish(), new GoldenKoi()];
        private Random random = new();

        public void StartFishing()
        {
            player.stats.RecordCast();
            Console.WriteLine("Lanzas tu línea al agua...");
            WaitForBite();
        }

        public void WaitForBite()
        {
            Console.WriteLine("Esperando a que piquen...");
            System.Threading.Thread.Sleep(random.Next(1000, 4000)); // Simulate waiting time

            Console.Beep(500, 20);
            Console.Beep(650, 50);
            Console.WriteLine("Sientes un jalón!");
            Thread.Sleep(800); // Brief pause before reaction test
            RunReactionTest(SelectFish(player.rod.RollFishRarity()));
        }
        public Fish SelectFish(Rarity rarity)
        {
            var candidates = availableFish.Where(f => f.rarity == rarity).ToList();

            if (candidates.Count == 0)
            {
                return availableFish.First(f => f.rarity == Rarity.Common); // Fallback to common fish if no fish of the desired rarity are available
            }

            return candidates[random.Next(candidates.Count)];

        }

        public void RunReactionTest(Fish fish)
        {
            var minigame = new FishingMinigame(fish);
            var (caught, recast) = minigame.Run();
            
            if (caught)
            {
                player.stats.RecordCatch();
                player.inventory.AddFish(fish);
                Console.ReadKey(true);
            }
            bool keepFishing = recast;

            if (keepFishing)
            {
                player.stats.RecordCast();
                Console.WriteLine("Lanzas tu línea de vuelta al agua...");
                WaitForBite();
            }
        }
    }
}
