using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Problem__cmd_
{
    public class Chromosome
    {
        List<int> genes;
        double fitness;

        public List<int> Genes
        {
            get
            {
                return genes;
            }
        }

        public double Fitness
        {
            get
            {
                return fitness;
            }
            set
            {
                fitness = value;
            }
        }

        public Chromosome()
        {
            genes = new List<int>();
        }

        public void AddGene(int gene)
        {
            genes.Add(gene);
        }

        public void SetGene(int index, int gene)
        {
            genes[index] = gene;
        }

        public int GetGene(int index)
        {
            return genes[index];
        }

        public static bool operator ==(Chromosome c1, Chromosome c2)
        {
            for(int i = 0; i < c1.Genes.Count; i++)
            {
                if (c1.Genes[i] != c2.Genes[i])
                {
                    return false;
                }
            }
            return true;
        }

        static public bool operator !=(Chromosome c1, Chromosome c2)
        {
            for (int i = 0; i < c1.Genes.Count; i++)
            {
                if (c1.Genes[i] != c2.Genes[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
