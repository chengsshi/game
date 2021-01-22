using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ResultStatistic
{
    class WriteFile
    {
        public static void Write(string fileName, ref string data)
        {
            string path = fileName.Remove(EliminateFile.Eliminate(fileName));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(data);
                }
            }
        }

        public static void Write(string fileName, ref double[] data)
        {
            string path = fileName.Remove(EliminateFile.Eliminate(fileName));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string content = String.Empty;
            for (int item = 0; item < data.Length; ++item)
            {
                content += data[item].ToString() + "\n";
            }
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

        public static void Write(string fileName, ref List<string> data)
        {
            string path = fileName.Remove(EliminateFile.Eliminate(fileName));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string content = String.Empty;
            foreach (string word in data)
            {
                content += word + "\n";
            }
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

        public static void Write(string fileName, ref Dictionary<int, double> data)
        {
            string path = fileName.Remove(EliminateFile.Eliminate(fileName));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string content = String.Empty;
            foreach (int item in data.Keys)
            {
                content += item.ToString() + "\t" + data[item].ToString() + "\n";
            }
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

        public static void Write(string fileName, ref Dictionary<int, int> data)
        {
            string path = fileName.Remove(EliminateFile.Eliminate(fileName));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string content = String.Empty;
            foreach (int item in data.Keys)
            {
                content += item.ToString() + "\t" + data[item].ToString() + "\n";
            }
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
