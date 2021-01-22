using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ResultStatistic
{
    class Read
    {
        public static string read(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw (new FileNotFoundException(
                    "file cannot be read since it does not exist.", fileName));
            }

            string content = String.Empty;

            using (FileStream fileStream = new FileStream(fileName,
            FileMode.Open, FileAccess.Read, FileShare.None))
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                using (StreamReader streamReader = new StreamReader(fileStream, encode))
                {
                    content = streamReader.ReadToEnd();
                }
            }
            return content;
        }

        public static void readFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw (new FileNotFoundException(
                    "file cannot be read since it does not exist.", fileName));
            }

            string content = String.Empty;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                using (StreamReader streamReader = new StreamReader(fileStream, encode))
                {
                    while (streamReader.Peek() != -1)
                    {
                        content = streamReader.ReadLine().Trim();
                        if (content.Length > 0)
                        {
                            // Transfer.dictionaryTransfer(content);
                        }
                    }
                }
            }
        }

        public static void readFile(string fileName, ref double[] result)
        {
            if (!File.Exists(fileName))
            {
                throw (new FileNotFoundException(
                    "file cannot be read since it does not exist.", fileName));
            }

            string content = String.Empty;
            int item = 0;
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                using (StreamReader streamReader = new StreamReader(fileStream, encode))
                {
                    while (streamReader.Peek() != -1)
                    {
                        content = streamReader.ReadLine().Trim();
                        if (content.Length > 0)
                        {
                            result[item] += Double.Parse(content);
                            ++item;
                        }
                        if (item > Parameter.iteration)
                        {
                            Console.WriteLine("Error: item should not larger than iteration");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
    }
}
