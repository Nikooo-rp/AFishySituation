using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class PlayerStats
    {
        public int totalCasts, totalGoldEarned, totalFishCaught, goldSpent;
        Player player;

        public PlayerStats(Player player)
        {
            this.player = player;
        }

        public void RecordCatch()
        {
            totalFishCaught++;
        }
        public void RecordCast()
        {
            totalCasts++;
        }
        public void RecordGold(int goldEarned)
        {
            totalGoldEarned += goldEarned;
        }
        public void RecordGoldSpent(int gold)
        {
            goldSpent += gold;
        }
        public void Show()
        {
            Console.WriteLine($"Lanzamientos totales: {totalCasts}");
            Console.WriteLine($"Peces atrapados: {totalFishCaught}");
            Console.WriteLine($"Oro ganado: {totalGoldEarned}");
            Console.WriteLine($"Oro gastado: {goldSpent}");
            if (player.rod != null)
            {
                Console.WriteLine($"Caña actual: {player.rod.name}");
                Console.WriteLine(player.rod.description);
            }
            Console.ReadLine();
        }
    }
}
