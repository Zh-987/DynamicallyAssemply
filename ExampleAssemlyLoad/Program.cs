using System;

namespace ExampleAssemlyLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = 5;
            var result = Square(number);
            Console.WriteLine($"Square:{result}");
        }
        static int Square(int number) => number * number;
    }
}
