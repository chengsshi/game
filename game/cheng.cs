using System;

namespace Game
{
    public class Cheng
    {
        public static void Main()
        {
            Console.WriteLine("start");
            string beginTime = DateTime.Now.ToString();
            Console.WriteLine("begin time: {0}", beginTime);
            
            Objective.SelfInitialization();
            Agent.AgentInitialization();
            RunFunction();

            string endTime = DateTime.Now.ToString();
            Console.WriteLine("end time: {0}", endTime);
            Console.WriteLine("done");
            Console.ReadKey();
        }

        public static void RunFunction()
        {
            // unimode
            FunctionEvaluation(Function.f1single);
            FunctionEvaluation(Function.f2group);
        }

        public static void FunctionEvaluation(Function func)
        {
            SynchronousUpdate.Process(func);
            AsynchronousUpdate.Process(func);
            StarPSO.Process(func);
            RingPSO.Process(func);
        }
    }
}
