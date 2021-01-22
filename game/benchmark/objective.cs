using System;

namespace Game
{
    public class Objective
    {
        public static double selfGamma = 0.0;
  
        public static double selfK = 0.0;
        public static double[] selfC = new double[Parameter.agentNumber];
        public static double[] selfDelta = new double[Parameter.agentNumber];
        public static double[] selfMu = new double[Parameter.agentNumber];

        public static double selfI = 0.0;
        public static double selfE = 0.0;

        public static void SelfInitialization()
        {
            Random rand = new Random();

            selfGamma = rand.NextDouble();
            selfK = rand.NextDouble();
            selfI = rand.Next(100, 150) * 1.0;
            selfE = rand.Next(10, 20) * 1.0;
            for (int idx = 0; idx < Parameter.agentNumber; ++idx)
            {
                selfMu[idx] = rand.NextDouble();
                selfC[idx] = rand.NextDouble();
                selfDelta[idx] = rand.NextDouble();
            }
        }

        #region f1 single agent
        // f({\bf x}) = \sum_{i = 1}^{n}x_{i}^{2} 
        public static double F1single(ref double[] solution)
        {
            double result = 0.0;

            Random rand = new Random();
            int dim = 0;
            int step = Parameter.agentNumber / 5;

            // notice that dim = agent number
            // always cooperation
            for (dim = 0; dim < step; dim++)
            {
                // E_{1} + \gamma_{1}I_{1} + k_{1}I_{2} - c_{1}I_{1} - (1-\mu_{12})\delta_{1}\gamma_{1}I_{1}

                result += selfE + selfGamma * selfI * solution[dim]
                    + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
                    - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
            }

            // always noncooperation
            for (dim = step; dim < step * 2; dim++)
            {
                // E_{ 1} -c_{ 1}I_{ 1}
                result += selfE - selfC[dim] * selfI * solution[dim];
            }

            // ramdom decision 75% cooperation and 25% noncooperation
            for (dim = step * 2; dim < step * 3; dim++)
            {
                if (rand.NextDouble() < 0.75)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }
            }

            // ramdom decision 50% cooperation and 50% noncooperation
            for (dim = step * 3; dim < step * 4; dim++)
            {
                if (rand.NextDouble() < 0.5)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }

            }

            // ramdom decision 25% cooperation and 75% noncooperation
            for (dim = step * 4; dim < step * 5; dim++)
            {
                if (rand.NextDouble() < 0.25)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }
            }

            return -result;
        }
        #endregion

        #region f2 group agent
        // f({\bf x}) = \sum_{i = 1}^{n}x_{i}^{2} 
        public static double F2group(ref double[] solution)
        {
            double result = 0.0;

            Random rand = new Random();
            int dim = 0;
            int step = Parameter.agentNumber / 5;

            // notice that dim = population
            // always cooperation
            for (dim = 0; dim < step; ++dim)
            {
                // E_{1} + \gamma_{1}I_{1} + k_{1}I_{2} - c_{1}I_{1} - (1-\mu_{12})\delta_{1}\gamma_{1}I_{1}

                result += selfE + selfGamma * selfI * solution[dim]
                    + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
                    - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
            }

            // self part
            // always noncooperation
            for (dim = step; dim < step * 2; dim++)
            {
                // E_{ 1} -c_{ 1}I_{ 1}
                result += selfE - selfC[dim] * selfI * solution[dim];
            }

            // ramdom decision 75% cooperation and 25% noncooperation
            for (dim = step * 2; dim < step * 3; dim++)
            {
                if (rand.NextDouble() < 0.75)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }
            }

            // ramdom decision 50% cooperation and 50% noncooperation
            for (dim = step * 3; dim < step * 4; dim++)
            {
                if (rand.NextDouble() < 0.5)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }
            }

            // ramdom decision 25% cooperation and 75% noncooperation
            for (dim = step * 4; dim < step * 5; dim++)
            {
                if (rand.NextDouble() < 0.25)
                {
                    result += selfE + selfGamma * selfI * solution[dim]
             + selfK * Agent.agentI[dim] - selfC[dim] * selfI * solution[dim]
             - (1 - selfMu[dim]) * selfDelta[dim] * selfGamma * selfI * solution[dim];
                }
                else
                {
                    result += selfE - selfC[dim] * selfI * solution[dim];
                }
            }

            // other part
            for (int idx = 0; idx < Parameter.agentNumber; ++idx)
            {
                result += Agent.agentE[idx] + Agent.agentGamma[idx] * Agent.agentI[idx]
        + Agent.agentK[idx] * selfI * solution[idx] - Agent.agentC[idx] * Agent.agentI[idx]
        - (1 - Agent.agentMu[idx]) * Agent.agentDelta[idx] * Agent.agentGamma[idx] * Agent.agentI[idx];
            }

            return -result;
        }
        #endregion
    }
}