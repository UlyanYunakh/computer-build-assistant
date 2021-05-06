using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class Algorithm
    {
        public InitialData Data { get; set; }
        public int MaxPop { get; set; } = 50;
        public int MaxItemCost { get; set; } = 100;
        public int CategoriesCount { get; set; } = 7;
        public int Capacity
        {
            set
            {
                if (value > 0)
                    _capacity = value;
                else
                    _capacity = 1;
            }
            get => _capacity;
        }

        private int _capacity = 0;
        private int _maxItemWorth = 100;

        private int _iterationCount = 500;
        private double _crossProb = 0.3;
        private double _mutateProb = 0.01;

        private Chromosome _bestChromosome;
        private Chromosome[] _currPopulation;
        private Chromosome[] _nextPopulation;

        public bool[] Markers { get; set; }

        static Random rnd;


        public Algorithm()
        {
            rnd = new Random();
        }

        public List<string> Start()
        {
            if (Markers == null)
                return null;

            if (_capacity == 0)
                return null;

            if (Data == null)
                return null;

            double agefit = 0;
            _bestChromosome = new Chromosome(); //особь т.е. подборка предметов
            _currPopulation = new Chromosome[MaxPop];
            _nextPopulation = new Chromosome[MaxPop];

            GenerateStartPopulation();                 // начальная популяция

            for (int j = 0; j < MaxPop; j++)
            {
                _bestChromosome = _currPopulation[j];
                Fitness(_bestChromosome);
            }

            double[] fitnesses = new double[_currPopulation.Length];
            double sum = 0;

            for (int iter = 0; iter < _iterationCount; iter++)
            {
                sum = 0;
                fitnesses = new double[_currPopulation.Length];

                for (int i = 0; i < fitnesses.Length; i++)      //высчитываем приспособленость
                {
                    fitnesses[i] = Fitness(_currPopulation[i]);

                    agefit += fitnesses[i];

                    if (_currPopulation[i].Fitness > _bestChromosome.Fitness)
                    {
                        for (int j = 0; j < CategoriesCount; j++)
                            _bestChromosome.SetGene(j, _currPopulation[i].Genes[j]);
                        _bestChromosome.Fitness = _currPopulation[i].Fitness;
                    }
                    sum += fitnesses[i];
                }
                agefit = 0;

                if (iter == _iterationCount - 1)
                    break;

                _nextPopulation = new Chromosome[MaxPop];

                for (int i = 0; i < _currPopulation.Length; i += 2)      //генерация нового поколения
                {
                    Chromosome one;
                    Chromosome two;
                    while (true)
                    {
                        one = Select(_currPopulation, fitnesses, sum);
                        two = Select(_currPopulation, fitnesses, sum);
                        if (one != two) break;
                    }

                    double rand = rnd.NextDouble();
                    if (rand <= _crossProb)
                        Crossover(ref one, ref two);

                    Mutate(ref one, _mutateProb);
                    Mutate(ref two, _mutateProb);

                    _nextPopulation[i] = new Chromosome();
                    _nextPopulation[i + 1] = new Chromosome();
                    _nextPopulation[i] = one;
                    _nextPopulation[i + 1] = two;
                }

                for (int i = 0; i < _currPopulation.Length; i++)         //замена старого поколения на новое
                    _currPopulation[i] = _nextPopulation[i];
            }
            return GetBestAnswer();
        }

        private void Mutate(ref Chromosome chromosome, double probability)      //мутация
        {
            double rand;
            for (int i = 0; i < CategoriesCount; i++)
                if (chromosome.Genes[i] >= 0)
                {
                    rand = rnd.NextDouble();
                    if (rand < probability)
                        chromosome.Genes[i] = rnd.Next(0, Data.Category[i].Count);
                }
        }

        private void Crossover(ref Chromosome first, ref Chromosome second)         //кросовер
        {
            int randPos = rnd.Next(0, CategoriesCount);
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
                foreach (double fit in fitnesses)
                    sum += fit;

            double[] tempFit = new double[fitnesses.Length];
            for (int i = 0; i < tempFit.Length; i++)
                tempFit[i] = fitnesses[i] / sum;

            Chromosome[] tempPop = new Chromosome[population.Length];
            for (int i = 0; i < tempPop.Length; i++)
                tempPop[i] = population[i];

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
                        for (int j = 0; j < CategoriesCount; j++)
                            c.AddGene(tempPop[i].GetGene(j));
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
            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                category = Data.Category[i];
                index = chromosome.Genes[i];
                if (index >= 0)
                {
                    sum += category[index].Worth * (1 - category[index].Cost / MaxItemCost);
                    capacitySum += category[index].Cost;
                }
            }

            double k;
            if (capacitySum > _capacity)
                k = _capacity / capacitySum;
            else
                k = 1;

            return chromosome.Fitness = sum * k * k * k / CategoriesCount;
        }

        private bool GenerateStartPopulation()
        {
            try
            {
                for (int i = 0; i < _currPopulation.Length; i++)
                {
                    _currPopulation[i] = new Chromosome();

                    for (int j = 0; j < Markers.Length; j++)              // генерируем CATEGORIES_COUNT генов
                    {
                        if (Markers[j] == true)
                        {
                            int itemsInCategory = Data.Category[j].Count;
                            _currPopulation[i].AddGene(rnd.Next(0, itemsInCategory));
                        }
                        else
                            _currPopulation[i].AddGene(-1);
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
            foreach (int index in _bestChromosome.Genes)
            {
                if (index >= 0)
                    answer.Add(Data.Category[i][index].Id);
                else
                    answer.Add("-1");
                i++;
            }
            return answer;
        }
    }
}
