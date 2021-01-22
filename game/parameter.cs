using System;

namespace Game
{
    public class Parameter
    {
        public static int population = 100;
        public static int agentNumber = 200;
        public static int dimension = 200;

        public static double lower = 0.0;
        public static double upper = 1.0;
        public static int run = 40;
        // reciprocalRunNumber = 1.0 / Run

        public static int iteration = 4000;
        public static double perce = 0.2;
        //public static double pe = 0.2;
        //public static double pone = 0.8;

        // PSO parameters
        public static double weight = 0.72984; // inertia weight
        public static double c1 = 1.496172;
        public static double c2 = 1.496172;

        public static void Display()
        {
            Console.WriteLine("population {0}", population);
            Console.WriteLine("iteration {0}", iteration);
            Console.WriteLine("dimension {0}", dimension);
            Console.WriteLine("run {0}", run);
            Console.WriteLine("upper bound {0}", upper);
            Console.WriteLine("lower bound {0}", lower);
        }

        public static void UpdateBoundary(double boundary)
        {
            upper = boundary;
            lower = -boundary;
            Display();
        }

        public static void UpdateParameter(double low, double up, int dim)
        {
            lower = low;
            upper = up;
            dimension = dim;

            Display();
        }

        public static void UpdateParameter(double low, double up, int dim, int ite)
        {
            lower = low;
            upper = up;
            dimension = dim;
            iteration = ite;

            Display();
        }

        public static void UpdateParameter(double low, double up, int dim, int ite, int popu)
        {
            lower = low;
            upper = up;
            dimension = dim;
            iteration = ite;
            population = popu;

            Display();
        }
    }
}
