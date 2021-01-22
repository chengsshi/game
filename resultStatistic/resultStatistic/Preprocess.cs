using System;
using System.Text;

namespace ResultStatistic
{
    class Preprocess
    {
        public static void LoadFile(string input, string output)
        {
            double[] mean = new double[Parameter.iteration]; 
            string filename = String.Empty; 
            for (int item = 0; item < Parameter.run; ++item)
            {
                filename = input + item.ToString() + ".txt";
                Read.readFile(filename, ref mean);
            }
            for (int item = 0; item < Parameter.iteration; ++item)
            {
                mean[item] /= -1.0*Parameter.run;
            }

            WriteFile.Write(output, ref mean);
        }
    }
}