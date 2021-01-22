using System;
using System.Collections.Generic;

namespace ResultStatistic
{
    public class Indicator
    {
        public static void indicator(string logName, ref List<double> data)
        {
            string logSentence = String.Empty;
            List<string> log = new List<string>();

            if (data.Count > 0)
            {
                // the smallest value
                data.Sort();
                logSentence = "the smallest value is " + data[0].ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // the middle value
                logSentence = "the middle value is " + data[data.Count / 2].ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // the largest value
                logSentence = "the largest value is " + data[data.Count - 1].ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // mean
                double mean = 0.0;
                foreach (double number in data)
                {
                    mean += number;
                }
                mean /= data.Count;
                logSentence = "the mean value is " + mean.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // average deviation
                // d_{i} = \frac{\sum |x - \bar{x} |}{n}
                double average = 0.0;
                foreach (double number in data)
                {
                    average += Math.Abs(number - mean);
                }
                average /= data.Count;
                logSentence = "the average deviation is " + average.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // variance
                double variance = 0.0;
                foreach (double number in data)
                {
                    variance += (number - mean) * (number - mean);
                }
                variance /= data.Count;
                logSentence = "the variance is " + variance.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // standard deviation
                double deviation = 0.0;
                deviation = Math.Sqrt(variance);
                logSentence = "the standard deviation is " + deviation.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);

                // measure of skewness  
                double skewness = 0.0;
                foreach (double number in data)
                {
                    skewness += (number - mean) * (number - mean) * (number - mean);
                }
                skewness /= data.Count;
                skewness /= (deviation * deviation * deviation);
                logSentence = "the skewness is " + skewness.ToString();
                Console.WriteLine(logSentence);
                log.Add(logSentence);
            }
            else
            {
                Console.WriteLine("data is empty.");
            }

            WriteFile.Write(logName, ref log);
        }
    }
}
