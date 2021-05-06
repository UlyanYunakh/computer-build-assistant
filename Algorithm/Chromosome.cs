using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    public class Chromosome
    {
        public List<int> Genes { get; private set; }
        public double Fitness { get; set; }

        public Chromosome()
        {
            Genes = new List<int>();
        }

        public void AddGene(int gene) => Genes.Add(gene);

        public void SetGene(int index, int gene) => Genes[index] = gene;

        public int GetGene(int index) => Genes[index];

        public static bool operator ==(Chromosome c1, Chromosome c2)
        {
            for (int i = 0; i < c1.Genes.Count; i++)
                if (c1.Genes[i] != c2.Genes[i])
                    return false;
            return true;
        }

        public static bool operator !=(Chromosome c1, Chromosome c2)
        {
            for (int i = 0; i < c1.Genes.Count; i++)
                if (c1.Genes[i] != c2.Genes[i])
                    return true;
            return false;
        }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
