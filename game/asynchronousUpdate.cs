using System;
using System.Collections.Generic;

namespace Game
{
    class AsynchronousUpdate
    {
        // fun = fitness_function
        // population_size; populationlation size
        // number_dimension; number of dimension
        // cluster_number: number of clusters;  
        // percentage_elitist ==  cluster_number
        // range_left; left boundary of the dynamic range
        // range_right; right boundary of the dynamic range


        public static void Process(Function type)
        {
            int globalIndex = 0;
            double globalValue = Double.MaxValue;
            double minimum = double.MaxValue;

            double[] globalOptimum = new double[Parameter.dimension];

            double[,] population = new double[Parameter.population, Parameter.dimension];
            double[] fitnessTrajectory = new double[Parameter.iteration];

            double[] solution = new double[Parameter.dimension];
            double[] fitness = new double[Parameter.population];
            double stepSize = 1.0; // effecting the step size of generating new individuals by adding random values
            // double[] solution = new double[Parameter.Dimension];

            int[] indexOriginal = new int[Parameter.population];
            //int[] fitness_population_sorted = new int[Parameter.Population];

            List<double> individualList = new List<double>();

            List<double> fitnessRun = new List<double>();
            List<string> log = new List<string>(); // write log
            string logSentence = String.Empty;
            string logName = String.Empty; // log file Name 

            #region time variables
            double diffTime = 0.0;
            TimeSpan diff = TimeSpan.Zero;
            DateTime start = DateTime.Now;
            Console.WriteLine("Hour: {0}, Minute: {1}, Second:{2}", start.Hour, start.Minute, start.Second);
            DateTime end = DateTime.Now;
            #endregion

            #region variable definition
            Random rand = new Random();
            //byte[] buffer = Guid.NewGuid().ToByteArray();
            //int iSeed = BitConverter.ToInt32(buffer, 0);
            //rand = new Random(iSeed);

            #endregion

            // probability Disrupt
            double probabilityDisrupt = 1.0;
            // probability for select elitist, not normals, to generate new individual; 
            double probabilityElitist = 0.2;
            double probabilityHybrid = 0.8;

            // probability for select one individual, not two, to generate new individual; 
            double probabilityOne = 0.5;

            for (int run = 0; run < Parameter.run; ++run)
            {
                #region initialization
                globalValue = Double.MaxValue;
                minimum = double.MaxValue;
                for (int num = 0; num < Parameter.population; ++num)
                {
                    for (int dim = 0; dim < Parameter.dimension; ++dim)
                    {
                        // initialize the populationlation of individuals
                        population[num, dim] = Parameter.lower + (Parameter.upper - Parameter.lower) * rand.NextDouble();
                    }
                }

                // initialize current iterantion  number
                //    int current_iteration_number = 1;

                // number of elitists
                // Parameter.perce == percentage_elitist 
                int numberElitist = Convert.ToInt32(Math.Round(Parameter.population * Parameter.perce));
                // number of normals
                int numberNormal = Parameter.population - numberElitist;

                // initialize corresponding original indexs in the population of sorted fitness values 
                //for (int atom = 0; atom < Parameter.Population; ++atom)
                //{
                //    indexOriginal[atom] = 101;
                //}
                // store best fitness value for each iteration
                //% best_fitness = ones(max_iteration,1); 

                // store fitness value for each individual in each population
                // fitness_population = ones(population_size,1);  
                // store sorted fitness values
                //for (int atom = 0; atom < Parameter.Population; ++atom)
                //{
                //    fitness_population_sorted[atom] = 1;
                //}
                // store a temporary individual
                //individual_temporary = zeros(1,number_dimension);  
                #endregion // initialization

                #region first calculation 
                // calculate fitness for each individual in the initialized populationlation
                for (int part = 0; part < Parameter.population; ++part)
                {
                    for (int dim = 0; dim < Parameter.dimension; ++dim)
                    {
                        solution[dim] = population[part, dim];
                    }

                    #region choose benchmark function
                    switch (type)
                    {
                        case Function.f1single:
                            fitness[part] = Objective.F1single(ref solution);
                            break;
                        case Function.f2group:
                            fitness[part] = Objective.F2group(ref solution);
                            break;

                        default:
                            Console.WriteLine("Not a valid function type");
                            Console.ReadKey();
                            break;
                    }
                    #endregion
                    // fitness[part] = function(type, ref solution);

                    if (fitness[part] < globalValue)
                    {
                        globalValue = fitness[part];
                        globalIndex = part;
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            globalOptimum[dim] = population[part, dim];
                        }
                    }
                }
                #endregion // first calculation

                // start the main loop of the BSO algorithm       
                // compensate the fitness evaulation due to disruption
                // int maxIteration = Convert.ToInt32(Parameter.Iteration - (Parameter.Iteration * probabilityDisrupt) / Parameter.Population);
                // maxIteration = max_iteration - (max_iteration/population_size); 
                // compensate the fitness evaulation due to disruption

                // store best fitness value for each iteration
                //best_fitness = ones(maxIteration,1); 

                // use the compesented maximum iteration
                #region Main Iteration
                for (int ite = 0; ite < Parameter.iteration; ++ite) //while current_iteration_number <= maxIteration 
                {
                    #region sort individuals
                    for (int idx = 0; idx < Parameter.population; ++idx)
                    {
                        individualList.Add(fitness[idx]);

                        // find global information
                        if (fitness[idx] < globalValue)
                        {
                            globalValue = fitness[idx];
                            globalIndex = idx;
                            for (int dim = 0; dim < Parameter.dimension; ++dim)
                            {
                                globalOptimum[dim] = population[idx, dim];
                            }
                        }
                    }

                    // sort individuals in a population based on their fitness values
                    individualList.Sort();
                    for (int i = 0; i < Parameter.population; ++i)
                    {
                        for (int pop = 0; pop < Parameter.population; ++pop)
                        {
                            if (fitness[pop] == individualList[i])
                            {
                                indexOriginal[i] = pop;
                            }
                        }
                    }

                    //[fitness_population_sorted,index_original] = sort(fitness_population,'ascend');

                    #endregion // end sort

                    // record the best fitness in each iteration
                    fitnessTrajectory[ite] = globalValue; // == individualList[0];

                    individualList.Clear();

                    #region generate new individual
                    // generate population_size new individuals by adding Gaussian random values            
                    for (int idx = 0; idx < Parameter.population; ++idx)
                    {
                        // form the seed individual 
                        // generate a randon value
                        double r_1 = rand.NextDouble();

                        if (r_1 < probabilityElitist) // select elitists to generate a new individual
                        {
                            // double r = rand.NextDouble(); // generate a random number
                            int indexOne = rand.Next(numberElitist);// 
                            int indexTwo = rand.Next(numberElitist); // Convert.ToInt32(Math.Floor(number_elitist * rand.NextDouble()));
                            if (indexOne == indexTwo) // avoid same index
                            {
                                indexTwo = numberElitist - indexOne - 1;
                            }
                            if (rand.NextDouble() < probabilityOne)
                            {
                                // use one elitist to generate a new individual
                                for (int dim = 0; dim < Parameter.dimension; ++dim)
                                {
                                    solution[dim] = population[indexOriginal[indexOne], dim];
                                }
                            }
                            else // use two elitists to generate a new individual
                            {
                                double tem = rand.NextDouble(); // combine from two individuals
                                for (int dim = 0; dim < Parameter.dimension; ++dim)
                                {
                                    solution[dim] = tem * population[indexOriginal[indexOne], dim]
                                        + (1.0 - tem) * population[indexOriginal[indexTwo], dim];
                                }
                            }
                        }
                        else if (r_1 > probabilityHybrid) // hybrid mode
                        {
                            int indexOne = rand.Next(numberElitist);
                            int indexTwo = numberElitist + rand.Next(numberNormal);
                            double tem = rand.NextDouble(); // combine from two individuals
                            for (int dim = 0; dim < Parameter.dimension; ++dim)
                            {
                                solution[dim] = tem * population[indexOriginal[indexOne], dim]
                                    + (1.0 - tem) * population[indexOriginal[indexTwo], dim];
                            }
                        }
                        else
                        {
                            // select normals to generate a new individual
                            double r = rand.NextDouble(); // generate a random number
                            int indexOne = numberElitist + rand.Next(numberNormal); // Convert.ToInt32(Math.Floor(number_normal * rand.NextDouble()));
                            int indexTwo = numberElitist + rand.Next(numberNormal); // Convert.ToInt32(Math.Floor(number_normal * rand.NextDouble()));
                            if (indexOne == indexTwo) // avoid same index
                            {
                                // inx_selected_two = number_elitist + number_normal - (inx_selected_one - number_elitist) - 1;
                                indexTwo = numberElitist + Parameter.population - indexOne - 1;
                            }

                            if (r < probabilityOne) // use one normal to generate a new individual
                            {
                                for (int dim = 0; dim < Parameter.dimension; ++dim)
                                {
                                    solution[dim] = population[indexOriginal[indexOne], dim];
                                }
                            }
                            else // use two elitists to generate a new individual
                            {
                                double tem = rand.NextDouble();
                                for (int dim = 0; dim < Parameter.dimension; ++dim)
                                {
                                    solution[dim] = tem * population[indexOriginal[indexOne], dim]
                                        + (1 - tem) * population[indexOriginal[indexTwo], dim];
                                }
                            }
                        }


                        // add Gaussian random value to seed individual to generat a new individual
                        stepSize = 0.1 * Math.Pow(10.0, Math.Exp((-1.0 * ite) / (Parameter.iteration - ite)));

                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            solution[dim] = solution[dim] + stepSize * rand.NextDouble() * Probability.Gaussian(0.0, 1.0);
                            // + stepSize * Probability.cauchy(1.0);

                            if (solution[dim] > Parameter.upper)
                            {
                                solution[dim] = Parameter.upper;
                            }
                            else if (solution[dim] < Parameter.lower)
                            {
                                solution[dim] = Parameter.lower;
                            }
                        }

                        #region selection between new individual and the previous
                        // selection between new one and the old one with the same index

                        #region choose benchmark function
                        switch (type)
                        {
                            case Function.f1single:
                                minimum = Objective.F1single(ref solution);
                                break;
                            case Function.f2group:
                                minimum = Objective.F2group(ref solution);
                                break;
                            default:
                                Console.WriteLine("Not a valid function type");
                                Console.ReadKey();
                                break;
                        }
                        #endregion

                        // double fitnessValue = function(type, ref solution); // calculate
                        if (minimum < fitness[idx])  // better than the previous one, replace
                        {
                            fitness[idx] = minimum;
                            for (int dim = 0; dim < Parameter.dimension; ++dim)
                            {
                                population[idx, dim] = solution[dim];
                            }
                        }
                    }
                    #endregion // selection                                            
                    #endregion // generate new individual

                    #region disrupt individual // local search
                    // don't do disruption for the first generation
                    //if (ite > 0) // not the first iteration
                    //{
                    // disrupt every genration but for one dimension of one individual
                    // double r_1 = rand.NextDouble();  // generate a randon value
                    // decide whether to select one individual to be disrupted
                    if (rand.NextDouble() < probabilityDisrupt)
                    {
                        // index of the selected individual
                        int idx = Convert.ToInt32(Math.Floor(Parameter.population * rand.NextDouble()));

                        if (idx == globalIndex)
                        {
                            if (idx > Parameter.population / 2.0)
                            {
                                idx--;
                            }
                            else
                            { idx++; }
                        }

                        // temporary individual = selected individual
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            solution[dim] = population[idx, dim];
                        }
                        // one dimention of selected individual to be replaceed by a random number
                        solution[rand.Next(Parameter.dimension)] = Parameter.lower + (Parameter.upper - Parameter.lower) * rand.NextDouble();

                        // evaluate the disrupted individual
                        //double fv = function(type, ref solution);

                        #region choose benchmark function
                        switch (type)
                        {
                            case Function.f1single:
                                fitness[idx] = Objective.F1single(ref solution);
                                break;
                            case Function.f2group:
                                fitness[idx] = Objective.F2group(ref solution);
                                break;

                            default:
                                Console.WriteLine("Not a valid function type");
                                Console.ReadKey();
                                break;
                        }
                        #endregion

                        // if (fv < fitness_population[idx] { // if better
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            // replace the selected individual with the disrupted one
                            population[idx, dim] = solution[dim];
                        }

                        // assign the fitness value to the disrupted individual
                        // fitness[idx] = fv;
                        //  }
                    }
                    //}
                    #endregion // disrupt individual
                }
                #endregion // main loop
                end = DateTime.Now;
                Console.WriteLine("Hour: {0}, Minute: {1}, Second:{2}", end.Hour, end.Minute, end.Second);

                diff = end - start;

                //Console.WriteLine($"{diff.TotalSeconds} seconds have passed since the start.");

                diffTime = diff.TotalSeconds;

                Console.WriteLine("{0} seconds have passed since the start.", diffTime);

                // bso-os stopped after last iteration
                Console.WriteLine("function {0}, run {1}, global value {2}, global index {3}", type, run, globalValue, globalIndex);
                fitnessRun.Add(fitnessTrajectory[Parameter.iteration - 1]);
                string trajectory = type.ToString() + ".asy." + Parameter.dimension.ToString() + "." + run.ToString() + ".txt";
                Write.writeTrajectory(trajectory, ref fitnessTrajectory);
            }

            logName = "_" + type.ToString() + ".asy.bsoos." + Parameter.dimension.ToString() + ".txt";

            logSentence = type.ToString() + " asy bsoos " + diffTime + " seconds for " + Parameter.run.ToString() + " runs";
            log.Add(logSentence);

            #region statistical indicator
            // best of local ring best fitness
            fitnessRun.Sort();
            logSentence = type.ToString() + " asy bsoos best fitness "
                + fitnessRun[0].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // middle of local ring best fitness
            logSentence = type.ToString() + " asy bsoos middle fitness "
                + fitnessRun[Parameter.run / 2].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // worst of local ring best fitness
            logSentence = type.ToString() + " asy bsoos worst fitness "
                + fitnessRun[Parameter.run - 1].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // calculate mean of the best particle fitness in each round
            double mean = 0.0;
            foreach (double number in fitnessRun)
            {
                mean += number;
            }
            mean /= Parameter.run;
            logSentence = type.ToString() + " " + Parameter.run.ToString() + " runs, asy bsoos best mean "
                + mean.ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // variance
            double variance = 0.0;
            foreach (double number in fitnessRun)
            {
                variance += (number - mean) * (number - mean);
            }
            variance /= Parameter.run;
            logSentence = type.ToString() + " " + Parameter.run.ToString()
                + " runs, variance " + variance.ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // standard deviation
            double deviation = 0.0;
            deviation = Math.Sqrt(variance);
            logSentence = type.ToString() + " " + Parameter.run.ToString()
                + " runs, standard deviation " + deviation.ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);
            fitnessRun.Clear();

            Write.writeLog(logName, ref log);
            #endregion
            log.Clear();

        }

        //public static double function(Function type, ref double[] solution)
        //{
        //    double minimum = double.MaxValue;

        //    switch (type)
        //    {
        //        case Function.f1single:
        //            minimum = Objective.f1single(ref solution);
        //            break;
        //        case Function.f2group:
        //            minimum = Objective.f2group(ref solution);
        //            break;

        //        default:
        //            Console.WriteLine("Not a valid function type");
        //            Console.ReadKey();
        //            break;
        //    }
        //    return minimum;
        //}
    }
}

