using System;
using System.Collections.Generic;

namespace Knapsack_Problem__cmd_
{
    public class Algorithm
    {
        int capacity;
        
        Chromosome BestChromosome;

        static int MAX_POP = 50;
        static int MAX_ITEM_COST = 100;
        static int MAX_ITEM_WORTH = 100;
        static int MIN_CAPACITY = 1;
        static int CATEGORIES_COUNT = 7;

        static int ITERATION_COUNT = 500;

        static double CROSSOVER_PROPOBILITY = 0.3;
        static double MUTATE_PROPOBILITY = 0.01;

        Chromosome[] currPopulation;
        Chromosome[] nextPopulation;
        bool[] markers = { false,false,false,false,false,false,false};
        
        static Random rnd;

        public InitialData Data { get; set; }
        public int Capacity
        {
            set
            {
                if(value >= MIN_CAPACITY)
                {
                    capacity = value;
                }
                else
                {
                    capacity = MIN_CAPACITY;
                }
            }
            get
            {
                return capacity;
            }
        }

        private Algorithm() { }
        
        public static Algorithm Create()
        {
             rnd = new Random();
             return new Algorithm();
        }

        public List<string> Start()
        {
            bool[] componentMarkers = { true, true, true, true, true, true, true };
            return Start(MIN_CAPACITY,componentMarkers);
        }
        public List<string> Start(int capasityValue, bool[] componentMarkers)
        {
            capacity = capasityValue;
            int markersCount;
            if (markers.Length > componentMarkers.Length)
            {
                markersCount = componentMarkers.Length;
            }
            else
            {
                markersCount = markers.Length;
            }
            for(int i = 0; i < markersCount; i++)
            {
                markers[i] = componentMarkers[i];
            }
            double agefit=0;
            BestChromosome = new Chromosome(); //особь т.е. подборка предметов
            currPopulation = new Chromosome[MAX_POP];
            nextPopulation = new Chromosome[MAX_POP];

            GenerateStartPopulation();                 // начальная популяция
    
            for (int j = 0; j < MAX_POP; j++)
            {
                BestChromosome = currPopulation[j];
                Fitness(BestChromosome);
                            
            }

            double[] fitnesses = new double[currPopulation.Length];
            double sum = 0;

            for (int iter = 0; iter < ITERATION_COUNT; iter++)
            {
                sum = 0;
                fitnesses = new double[currPopulation.Length];

                for (int i = 0; i < fitnesses.Length; i++)      //высчитываем приспособленость
                {
                    fitnesses[i] = Fitness(currPopulation[i]);

                    agefit += fitnesses[i];                               

                    if (currPopulation[i].Fitness > BestChromosome.Fitness)
                    {
                        for (int j = 0; j < CATEGORIES_COUNT; j++)
                        {
                            BestChromosome.SetGene(j, currPopulation[i].Genes[j]);
                        }
                        BestChromosome.Fitness = currPopulation[i].Fitness;
                    }
                    sum += fitnesses[i];
                }
                agefit = 0;

                if (iter == ITERATION_COUNT - 1)
                {
                    break;
                }

                nextPopulation = new Chromosome[MAX_POP];

                for (int i = 0; i < currPopulation.Length; i += 2)      //генерация нового поколения
                {
                    Chromosome one;
                    Chromosome two;
                    while (true)
                    {
                        one = Select(currPopulation, fitnesses, sum);
                        two = Select(currPopulation, fitnesses, sum);
                        if (one != two) break;
                    }

                    double rand = rnd.NextDouble();
                    if (rand <= CROSSOVER_PROPOBILITY)
                    {
                        Crossover(ref one, ref two);
                    }

                    Mutate(ref one, MUTATE_PROPOBILITY);
                    Mutate(ref two, MUTATE_PROPOBILITY);

                    nextPopulation[i] = new Chromosome();
                    nextPopulation[i + 1] = new Chromosome();
                    nextPopulation[i] = one;
                    nextPopulation[i + 1] = two;
                }

                for (int i = 0; i < currPopulation.Length; i++)         //замена старого поколения на новое
                {
                    currPopulation[i] = nextPopulation[i];
                }
            }
            return GetBestAnswer();
        }

        private void Mutate(ref Chromosome chromosome, double probability)      //мутация
        {
            double rand;
            for (int i = 0; i < CATEGORIES_COUNT; i++)
            {
                if (chromosome.Genes[i] >= 0)
                {
                    rand = rnd.NextDouble();
                    if (rand < probability)
                    {
                        chromosome.Genes[i] = rnd.Next(0, Data.categorys[i].Count);
                    }
                }
            }
        }

        private void Crossover(ref Chromosome first, ref Chromosome second)         //кросовер
        {
            int randPos = rnd.Next(0, CATEGORIES_COUNT);
            for (int i = 0; i < randPos; i++)
            {
                    int temp = first.Genes[i];
                    first.Genes[i] = second.Genes[i];
                    second.Genes[i] = temp;
            }
        }

        private Chromosome Select(Chromosome[] population, double[] fitnesses, double sum)      //выбор генов из популяции
        {
            if (sum == 0)
            {
                foreach (double fit in fitnesses)
                {
                    sum += fit;
                }
            }

            double[] tempFit = new double[fitnesses.Length];
            for (int i = 0; i < tempFit.Length; i++)
            {
                tempFit[i] = fitnesses[i]/sum;
            }

            Chromosome[] tempPop = new Chromosome[population.Length];
            for (int i = 0; i < tempPop.Length; i++)
            {
                tempPop[i] = population[i];
            }

            Array.Sort(tempFit, tempPop);

            sum = 0;

            double[] accumFit = new double[tempFit.Length];
            for (int i = 0; i < accumFit.Length; i++)
            {
                sum += tempFit[i];
                accumFit[i] = sum;
            }

            while (true)
            {
                double val = rnd.NextDouble();
                for (int i = 0; i < accumFit.Length; i++)
                {
                    if (accumFit[i] > val)
                    {
                        Chromosome c = new Chromosome();
                        for (int j = 0; j < CATEGORIES_COUNT; j++)
                        {
                            c.AddGene(tempPop[i].GetGene(j));
                        }
                        return c;
                    }
                }
            }
        }

        private double Fitness(Chromosome chromosome)
        {
            double sum = 0;
            double capacitySum = 0;
            int index;
            List<Item> category;
            for (int i=0; i< chromosome.Genes.Count;i++)
            {
                category = Data.categorys[i];
                index = chromosome.Genes[i];
                if (index >= 0)
                {
                    sum += category[index].ItemWorth * (1 - category[index].ItemCost / MAX_ITEM_COST);
                    capacitySum += category[index].ItemCost;
                }
            }

            double k;
            if (capacitySum > capacity)
            {
                k = capacity / capacitySum ;
            }
            else
            {
                k = 1;
            }

            return chromosome.Fitness = sum * k * k * k/CATEGORIES_COUNT;
        }

        private bool GenerateStartPopulation()
        {
            try
            {
                for (int i = 0; i < currPopulation.Length; i++)
                {
                    currPopulation[i] = new Chromosome();

                    for(int j = 0; j < markers.Length; j++)              // генерируем CATEGORIES_COUNT генов
                    {
                        if(markers[j] == true) {
                            int itemsInCategory = Data.categorys[j].Count;
                            currPopulation[i].AddGene(rnd.Next(0, itemsInCategory));
                        }
                        else
                        {
                            currPopulation[i].AddGene(-1);
                        }
                    }

                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<string> GetBestAnswer()
        {
            List<string> answer = new List<string>();
            int i = 0;
            foreach (int index in BestChromosome.Genes)
            {
                if (index >= 0)
                {
                    answer.Add(Data.categorys[i][index].Id);
                }
                else
                {
                    answer.Add("-1");
                }
                i++;
            }
            return answer;
        }
    }
}
