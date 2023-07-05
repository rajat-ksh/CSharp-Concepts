/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            Task<int[]> GeneratingRandomArray = Task.Factory.StartNew(CreateRandomArray);
            Task<int[]> MultiplingRandomArray = GeneratingRandomArray.ContinueWith(MultipleRandomNumber);
            Task<int[]> SortingRandomArray = MultiplingRandomArray.ContinueWith(SortRandomArray);
            Task<double> AverageRandomArray = SortingRandomArray.ContinueWith(CalculateAverage);
            Task.WaitAll(GeneratingRandomArray, MultiplingRandomArray, SortingRandomArray, AverageRandomArray);
            Console.WriteLine("\nAverage: " + AverageRandomArray.Result);
            Console.ReadLine();
        }

        private static double CalculateAverage(Task<int[]> task)
        {
            int[] numbers = task.Result;
            return numbers.Average();
        }

        private static int[] SortRandomArray(Task<int[]> task)
        {
            int[] numbers = task.Result;
            Array.Sort(numbers);
            Console.WriteLine("\nArray after sorting:- ");
            PrintArray(numbers);
            return numbers;
        }

        private static int[] MultipleRandomNumber(Task<int[]> task)
        {
            int[] numbers = task.Result;
            int multipler = random.Next(1, 5);
            Console.WriteLine("\nMultiplier:- {0}", multipler);
            for (int i = 0; i < 10; i++)
            {
                numbers[i] *= multipler;
            }
            PrintArray(numbers);
            return numbers;
        }

        private static int[] CreateRandomArray()
        {
            int[] randomNumbers = new int[10];
            for (int i = 0; i < 10; i++)
            {
                randomNumbers[i] = random.Next(1, 25);
            }
            Console.WriteLine("Array Created:- ");
            PrintArray(randomNumbers);
            return randomNumbers;
        }

        private static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
        }
    }
}
