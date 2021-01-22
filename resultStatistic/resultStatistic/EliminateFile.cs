using System;
using System.Collections.Generic;

namespace ResultStatistic
{
    class EliminateFile
    {
        // find file name from address
        public static string Eliminate(string name, int startPos)
        {
            int index = -1;
            do
            {
                index = name.IndexOf('\\', startPos);
                if (index > -1)
                {
                    startPos = index + 1;
                }
            } while (index > -1 && startPos < name.Length);
            name = name.Substring(startPos, name.Length - startPos);
            return name;
        }

        public static int Eliminate(string name)
        {
            int startPos = 0;
            int index = -1;
            do
            {
                index = name.IndexOf('\\', startPos);
                if (index > -1)
                {
                    startPos = index + 1;
                }
            } while (index > -1 && startPos < name.Length);
            return startPos;
        }
    }
}
