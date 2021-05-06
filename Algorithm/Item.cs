using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    public class Item
    {
        string id;
        int itemCost;
        int itemWorth;
        public int ItemCost
        {
            get
            {
                return itemCost;
            }
        }
        public string Id
        {
            get
            {
                return id;
            }
        }

        public int ItemWorth
        {
            get
            {
                return itemWorth;
            }
        }

        public Item(int capacity, int cost)
        {
            itemCost = capacity;
            itemWorth = cost;
        }
        public Item(string name, int cost, int worth)
        {
            id = name;
            itemCost = cost;
            itemWorth = worth;
        }
    }
}
