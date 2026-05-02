using System;
using System.Collections.Generic;
using System.Text;

namespace AFishySituation
{
    public class Player
    {
        public string name = String.Empty;
        public int gold;
        public Rod rod;
        public Inventory inventory;
        public PlayerStats stats;

        public Player(string name)
        {
            this.name = name;
            this.gold = 0;
            this.inventory = new Inventory();
            this.inventory.soldFish += EarnGold;
            this.stats = new PlayerStats(this);
        }
        public Player() 
        {
            this.gold = 0;
            this.inventory = new Inventory();
            this.inventory.soldFish += EarnGold;
            this.stats = new PlayerStats(this);
        }

        public void EarnGold(int amount)
        {
            gold += amount;
            stats.RecordGold(amount);
        }

        public void SpendGold(int amount)
        {
            gold -= amount;
            stats.RecordGoldSpent(amount);
        }

        public void EquipRod(Rod newRod)
        {
            rod = newRod;
        }
    }
}
