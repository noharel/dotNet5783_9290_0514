using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9290();
            Welcome0514();
            Console.ReadKey();
            Console.WriteLine("worked for Talel");

        }
        static partial void Welcome0514();
        private static void Welcome9290()
        {
            Console.WriteLine("Enter your name: ");
            string username = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", username);
        }
    }
}



