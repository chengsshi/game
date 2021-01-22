using System;

namespace Game
{
    class Probability
    {
        public static double Cauchy(double variate)
        {
            return 1.0 / (Math.PI * (1 + variate * variate));
        }

        // \mu -> mean \mu = 0.0
        // \sigma -> standard deviation \sigma = 1.0
        public static double Gaussian(double variate)
        {
            // Console.WriteLine(Math.Exp(-0.5 * variate * variate));
            double result = Math.Exp(-0.5 * variate * variate) / Math.Sqrt(2.0 * Math.PI);
            return result;
        }

        public static double Gaussian(double dExpect, double dVariance)
        {
            double v1 = 0;
            double v2 = 0;
            double S = 0.0;
            double result = 0.0;
            int phase = 0;
            double x = 0.0;
            Random rand = new Random();
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            rand = new Random(iSeed);
            // Console.WriteLine(Guid.NewGuid().ToString());

            if (phase == 0)
            {
                do
                {
                    double U1 = rand.NextDouble();
                    // Console.WriteLine("rand value U1 {0}", U1);
                    double U2 = rand.NextDouble();
                    //Console.WriteLine("U2 rand value {0}", U2);

                    v1 = 2 * U1 - 1;
                    v2 = 2 * U2 - 1;
                    S = v1 * v1 + v2 * v2;
                } while (S >= 1.0 || S == 0.0);

                x = v1 * Math.Sqrt(-2 * Math.Log(S) / S);
            }
            else
            {
                x = v2 * Math.Sqrt(-2 * Math.Log(S) / S);
            }

            phase = 1 - phase;
            result = x * dVariance + dExpect;

            // Console.WriteLine("Gaussian value {0}", result);
            // Console.ReadKey();
            return result;
        }


        // Logistic function: logsig(x) = 1.0 / (1.0 + \exp(-x))
        public static double Logsig(double variate)
        {
            return 1.0 / (1.0 + Math.Exp(-1.0 * variate));
        }
    }
}