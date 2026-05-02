using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AFishySituation
{
    public class SaveManager
    {
        private string SAVE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "fishgameSave.txt");
        List<Rod> allRods = new List<Rod> { new WoodenRod(), new SteelRod(), new ExpertRod(), new LegendaryRod() };
        List<Fish> allFish = new List<Fish> {new Sardine(), new Carp(), new Perch(), new Catfish(), new Salmon(), new Pike(), new SwordFish(), new GiantTuna(), new Oarfish(), new GoldenKoi()};

        public void Save(Player player)
        {
            var lines = new List<string>();

            lines.Add($"name:{player.name}");
            lines.Add($"gold:{player.gold}");
            lines.Add($"rod:{player.rod.name}");

            lines.Add($"stats.totalCasts:{player.stats.totalCasts}");
            lines.Add($"stats.totalGoldEarned:{player.stats.totalGoldEarned}");
            lines.Add($"stats.totalGoldSpent:{player.stats.goldSpent}");
            lines.Add($"stats.totalFishCaught:{player.stats.totalFishCaught}");

            foreach (Fish f in player.inventory.fish)
                lines.Add($"fish:{f.name}|{f.rarity}|{f.weight}|{f.maxWeight}|{f.basePrice}");

            File.WriteAllLines(SAVE_PATH, lines);
            Console.WriteLine("Juego Guardado!");
        }

        public Player? Load()
        {
            if (!File.Exists(SAVE_PATH))
                return null;

            var player = new Player();
            
            foreach (string line in File.ReadAllLines(SAVE_PATH))
            {
                var parts = line.Split(':');
                string key = parts[0];
                string value = parts[1];

                switch (key)
                {
                    case "name":
                        player.name = value; break;
                    case "gold":
                        player.gold = int.Parse(value); break;
                    case "rod":
                        player.rod = allRods.First(r => r.name == value); break;

                    case "stats.totalCasts":
                        player.stats.totalCasts = int.Parse(value); break;
                    case "stats.totalGoldEarned":
                        player.stats.totalGoldEarned = int.Parse(value); break;
                    case "stats.totalGoldSpent":
                        player.stats.goldSpent = int.Parse(value); break;
                    case "stats.totalFishCaught":
                        player.stats.totalFishCaught = int.Parse(value); break;

                    case "fish":
                        var fishParts = value.Split('|');
                        Fish template = allFish.First(f => f.name == fishParts[0]);
                        var caught = new Fish(template.name)
                        {
                            rarity = (Rarity)Enum.Parse(typeof(Rarity), fishParts[1]),
                            weight = float.Parse(fishParts[2], CultureInfo.InvariantCulture),
                            maxWeight = float.Parse(fishParts[3], CultureInfo.InvariantCulture),
                            basePrice = int.Parse(fishParts[4])
                        };
                        player.inventory.AddFish(caught);
                        break;
                }
            }

            return player;
        }

        public bool SaveExists() => File.Exists(SAVE_PATH);
    }
}
