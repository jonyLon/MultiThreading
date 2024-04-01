using System.Threading;

namespace MultiThreading
{

    class AlgorithmConfigurator
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public TimeSpan Delay { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var fibonacciConfig = new AlgorithmConfigurator() { Name = "Fibonacci", Count = 10, Delay = TimeSpan.FromSeconds(0.05)};
            var primeConfig = new AlgorithmConfigurator() { Name = "Prime", Count = 36, Delay = TimeSpan.FromSeconds(0.05) };
            var factorialConfig = new AlgorithmConfigurator() { Name = "Factorial", Count = 10, Delay = TimeSpan.FromSeconds(0.05) };
            var fibonacciThread = new Thread(() => GenerateFibonacciNums(fibonacciConfig));
            var primeThread = new Thread(() => FindMaxFactorial(factorialConfig));
            var factorialThread = new Thread(() => GeneratePrimeNumbers(primeConfig));

            fibonacciThread.Start();
            primeThread.Start();
            factorialThread.Start();

            Console.WriteLine("Press 'x' to exit.");
            while(Console.ReadKey().Key != ConsoleKey.X)
            {
                Thread.Sleep(100);
            }

            fibonacciThread.Abort();
            factorialThread.Abort();
            primeThread.Abort();
        }
        static void GenerateFibonacciNums(AlgorithmConfigurator config) {
        
            int a = 0;
            int b = 1;
            for (int i = 0; i < config.Count; i++)
            {
                Console.WriteLine($"{config.Name}: {a}");
                int temp = a + b;
                a = b;
                b = temp;
                Thread.Sleep(config.Delay);
            }


        }
        static void FindMaxFactorial(AlgorithmConfigurator config)
        {
            for (int i = 0; i < config.Count; i++)
            {
                Console.WriteLine($"{config.Name} of {i}: {Factorial(i)}");
                Thread.Sleep(config.Delay);
            }
        }
        static int Factorial(int number)
        {
            int res = 1;
            for (int i = 2; i <= number; i++)
            {
                res *= i;
            }
            return res;
        }

        static void GeneratePrimeNumbers(AlgorithmConfigurator config)
        {
            int count = 0;
            int number = 2;
            while (count < config.Count)
            {
                if (IsPrime(number))
                {
                    Console.WriteLine($"{config.Name}: {number}");
                    count++;
                    Thread.Sleep(config.Delay);
                }


                number++;
            }
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            var limit = Math.Sqrt(number);

            for (int i = 2; i <= limit; ++i)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}