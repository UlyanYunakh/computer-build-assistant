using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    public class InitialData
    {
        public List<Item> cpu { get; set; }
        public List<Item> longTermStorage { get; set; }
        public List<Item> motherboard { get; set; }
        public List<Item> collingSystem { get; set; }
        public List<Item> ram { get; set; }
        public List<Item> powerSupply { get; set; }
        public List<Item> shell { get; set; }

        public List<List<Item>> categorys { get; set; }
        public InitialData()
        {
            categorys = new List<List<Item>>();
            cpu = new List<Item>();
            longTermStorage = new List<Item>();
            motherboard = new List<Item>();
            collingSystem = new List<Item>();
            ram = new List<Item>();
            powerSupply = new List<Item>();
            shell = new List<Item>();

            categorys.Add(cpu);
            categorys.Add(longTermStorage);
            categorys.Add(motherboard);
            categorys.Add(collingSystem);
            categorys.Add(ram);
            categorys.Add(powerSupply);
            categorys.Add(shell);
        }
    }
}
