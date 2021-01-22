using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Game
{
    public class Write
    {
        public static void writeTrajectory(string fileName, ref List<double> output)
        {
            string content = String.Empty;
            foreach (double number in output)
            {
                content += number.ToString() + "\n";
            }
            fileName = @"..\" + fileName;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(content);
                }
            }
        }

        public static void writeTrajectory2(string fileName, int[] output)
        {
            string content = String.Empty;
            foreach (int number in output)
            {
                content += number.ToString() + "\n";
            }
            fileName = @"..\" + fileName;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(content);
                }
            }
        }

        public static void writeTrajectory(string fileName, ref double[] output)
        {
            string content = String.Empty;
            for (int iter = 0; iter < Parameter.iteration; ++iter)
            {
                content += output[iter].ToString() + "\n";
            }
            fileName = @"..\" + fileName;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(content);
                }
            }
        }

        public static void writeLog(string fileName, ref List<string> log)
        {
            string content = String.Empty;
            foreach (string item in log)
            {
                content += item + "\n";
            }
            fileName = @"..\" + fileName;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(content);
                }
            }
        }
    }
}