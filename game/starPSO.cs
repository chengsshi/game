using System;
using System.Collections.Generic;

namespace Game
{
    public class StarPSO
    {
        public static void Process(Function type)
        {
            #region time variables
            double diffTime = 0.0;
            TimeSpan diff = TimeSpan.Zero;
            DateTime start = DateTime.Now;
            Console.WriteLine("Hour: {0}, Minute: {1}, Second:{2}", start.Hour, start.Minute, start.Second);
            DateTime end = DateTime.Now;
            #endregion

            #region variable definition
            Random rand = new Random();
            double minimum = double.MaxValue;
            int globalIndex = 0;
            double globalValue = Double.MaxValue;
            double[] globalOptimum = new double[Parameter.dimension];

            double[] fitness = new double[Parameter.population];
            double[,] velocity = new double[Parameter.population, Parameter.dimension];
            double[,] position = new double[Parameter.population, Parameter.dimension];
            double[] solution = new double[Parameter.dimension];

            // personalBest: contains each particle's best position.  
            double[,] pbest = new double[Parameter.population, Parameter.dimension];

            List<double> fitnessRun = new List<double>();

            List<string> log = new List<string>(); // write log
            string logSentence = String.Empty;
            string logName = String.Empty; // log file Name 

            string trajectoryName = String.Empty; // trajectory file name            

            // particle move trajectory
            double[] fitnessTrajectory = new double[Parameter.iteration];

            #endregion
            //loop for runs
            for (int run = 0; run < Parameter.run; ++run)
            {
                globalIndex = 0;    //initialy assume the first particle as the gbest
                globalValue = Double.MaxValue;
                minimum = Double.MaxValue;
                #region initializes the individuals
                for (int part = 0; part < Parameter.population; ++part)
                {
                    for (int dim = 0; dim < Parameter.dimension; ++dim)
                    {
                        position[part, dim] = Parameter.lower +
                            (Parameter.upper - Parameter.lower) * rand.NextDouble();

                        solution[dim] = position[part, dim];
                        pbest[part, dim] = position[part, dim];
                        velocity[part, dim] = Parameter.lower +
                            (Parameter.upper - Parameter.lower) * rand.NextDouble();
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
                    // update global best
                    if (fitness[part] < globalValue)
                    {
                        globalIndex = part;
                        globalValue = fitness[part];
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            globalOptimum[dim] = position[part, dim];
                        }
                    }
                }
                #endregion

                #region main iterations
                // main work Loop for each run here
                for (int ite = 0; ite < Parameter.iteration; ++ite)
                {
                    // update inertia weight
                    // time variant weight, linear from weight to 0.4
                    // weightUp = (Parameter.weight - 0.4) * (Parameter.maxIteration - iterator) / Parameter.maxIteration + 0.4;

                    for (int part = 0; part < Parameter.population; ++part)
                    {
                        #region choose benchmark function
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            solution[dim] = position[part, dim];
                        }

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

                        #region update personal best and global best
                        if (minimum < fitness[part])
                        {
                            fitness[part] = minimum;
                            for (int dim = 0; dim < Parameter.dimension; ++dim)
                                pbest[part, dim] = position[part, dim];
                            // star
                            // update global best
                            if (fitness[part] < globalValue)
                            {
                                globalIndex = part;
                                globalValue = fitness[part];
                                for (int dim = 0; dim < Parameter.dimension; ++dim)
                                {
                                    globalOptimum[dim] = position[part, dim];
                                }
                            }
                        }
                        #endregion

                        #region update velocity and position
                        for (int dim = 0; dim < Parameter.dimension; ++dim)
                        {
                            // velocity
                            velocity[part, dim] = Parameter.weight * velocity[part, dim]
                                + Parameter.c1 * rand.NextDouble() * (pbest[part, dim] - position[part, dim])
                                + Parameter.c2 * rand.NextDouble() * (pbest[globalIndex, dim] - position[part, dim]);

                            if (velocity[part, dim] > Parameter.upper)
                                velocity[part, dim] = Parameter.upper - rand.NextDouble() * 0.01;
                            else if (velocity[part, dim] < Parameter.lower)
                                velocity[part, dim] = Parameter.lower + rand.NextDouble() * 0.01;

                            // position
                            position[part, dim] += velocity[part, dim];
                            if (position[part, dim] > Parameter.upper)
                                position[part, dim] = Parameter.upper - rand.NextDouble() * 0.01;

                            else if (position[part, dim] < Parameter.lower)
                                position[part, dim] = Parameter.lower + rand.NextDouble() * 0.01;
                        }
                        #endregion                        
                    }

                    fitnessTrajectory[ite] = globalValue;
                }
                #endregion // main iterations
                end = DateTime.Now;
                Console.WriteLine("Hour: {0}, Minute: {1}, Second:{2}", end.Hour, end.Minute, end.Second);

                diff = end - start;

                //Console.WriteLine($"{diff.TotalSeconds} seconds have passed since the start.");

                diffTime = diff.TotalSeconds;

                Console.WriteLine("{0} seconds have passed since the start.", diffTime);

                #region pso stopped after last iteration                            

                // write particles postion move trajectory
                trajectoryName = type.ToString() + ".star." + Parameter.dimension.ToString()
                    + "." + run.ToString() + ".txt";
                Write.writeTrajectory(trajectoryName, ref fitnessTrajectory);

                logSentence = type.ToString() + " fitness[star] " + globalValue.ToString() + ", run " + run.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                fitnessRun.Add(globalValue);
                #endregion
            }

            logName = "_" + type.ToString() + ".star." + Parameter.dimension.ToString() + ".txt";

            logSentence = type.ToString() + " star " + diffTime + " seconds for " + Parameter.run.ToString() + " runs";
            log.Add(logSentence);

            #region sort result
            fitnessRun.Sort();
            // best of global star best fitness
            logSentence = type.ToString() + " star best fitness "
                + fitnessRun[0].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // middle of global star best fitness
            logSentence = type.ToString() + " star middle fitness "
                + fitnessRun[Parameter.run / 2].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);

            // worst of global star best fitness
            logSentence = type.ToString() + " star worst fitness "
                + fitnessRun[Parameter.run - 1].ToString();
            Console.WriteLine(logSentence);
            log.Add(logSentence);
            #endregion

            #region mean of result
            // calculate mean of the best particle fitness in each round
            double mean = 0.0;
            foreach (double number in fitnessRun)
            {
                mean += number;
            }
            mean /= Parameter.run;
            logSentence = type.ToString() + " " + Parameter.run.ToString()
                + " runs, global star best mean " + mean.ToString();
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
        //            minimum = Objective.F1single(ref solution);
        //            break;
        //        case Function.f2group:
        //            minimum = Objective.F2group(ref solution);
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

