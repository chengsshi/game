using System;
using System.Collections.Generic;

namespace ResultStatistic
{
    public class Cheng
    {
        public static void Main()
        {
            Console.WriteLine("start");
            string beginTime = DateTime.Now.ToString();
            Console.WriteLine("begin time: {0}", beginTime);

            // f1single, f2group .star
            string problem = "f1single.asy"; // asy sy star ring

            string program = "game";

            // brain storm
            string input = @"..\..\..\..\" + program + @"\bin" + @"\" + problem + ".200.";
            string output = @"..\" + problem + ".200.txt";
            Preprocess.LoadFile(input, output);


            //string file = @"..\log.txt";
            //List<double> data = new List<double>();  
            //Indicator.indicator(file, ref data);

            //data.Clear();
            string endTime = DateTime.Now.ToString();
            Console.WriteLine("end time: {0}", endTime);
            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
