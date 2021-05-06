using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    class Generator
    {
        public InitialData GenerateItems()
        {
            int MAX_ITEM_COST = 100;
            int MAX_ITEM_WORTH = 100;
            int newItemCost;
            int newItemWorth;

            InitialData initialData = new InitialData();
            Random rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    newItemCost = rnd.Next(1, MAX_ITEM_COST + 1);
                    if (newItemCost < MAX_ITEM_COST / 2)
                        newItemWorth = rnd.Next(1, MAX_ITEM_WORTH / 2);
                    else
                        newItemWorth = rnd.Next(MAX_ITEM_WORTH / 2 - MAX_ITEM_WORTH / 8, MAX_ITEM_WORTH + 1);

                    Item item = new Item($"item{j}n{i + 1}", newItemCost, newItemWorth);
                    initialData.categorys[j].Add(item);
                }

            }
            return initialData;
        }
    }
}
