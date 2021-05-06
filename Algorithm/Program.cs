using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm algorithm = Algorithm.Create();
            algorithm.Capacity = 100;
            algorithm.Data = new Generator().GenerateItems();
            bool[] markers = { true, true, false, false, true };

            while (true)
            {
                algorithm.Start(100,markers);
                Console.ReadKey();
            }
        }
        
    }
}
