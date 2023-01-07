using BenchmarkDotNet.Running;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Benchmark - Entity Framework Core
            Console.WriteLine("Hello World");

            BenchmarkRunner.Run<BenchmarkService>();
        }        
    }   
}