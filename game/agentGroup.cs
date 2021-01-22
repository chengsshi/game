using System;

namespace Game
{
    public class Agent
    {

        public static double[] agentGamma = new double[Parameter.agentNumber];
        public static double[] agentC = new double[Parameter.agentNumber];
        public static double[] agentMu = new double[Parameter.agentNumber];
        public static double[] agentDelta = new double[Parameter.agentNumber];
        public static double[] agentK = new double[Parameter.agentNumber];
        public static double[] agentI = new double[Parameter.agentNumber];
        public static double[] agentE = new double[Parameter.agentNumber];

        public static void AgentInitialization()
        {
            Random rand = new Random();
            for (int idx = 0; idx < Parameter.agentNumber; ++idx)
            {
                agentGamma[idx] = rand.NextDouble();
                agentC[idx] = rand.NextDouble();
                agentMu[idx] = rand.NextDouble();
                agentDelta[idx] = rand.NextDouble();
                agentK[idx] = rand.NextDouble();
                agentI[idx] = rand.Next(100, 150) * 1.0;
                agentE[idx] = rand.Next(10, 20)* 1.0;
            }
        }
    }
}
