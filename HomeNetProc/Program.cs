using System;
using homeNet_Processor;

namespace HomeNetProc
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new homeNet_Processor.procConfig();
            config.readConfig();
            var obj = new homeNet_Processor.processor(config);
            Console.ReadLine();
        }
    }
}
